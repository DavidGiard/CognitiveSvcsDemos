window.onload = function () {
    const missingKeyErrorMsg = `
        <div>
            No key found.<br>
            This demo will not work without a key.<br>
            Create a script.js file with the following code:.
        </div>
        <div style="color:red; padding-left: 20px;">
        var getKey = function(){<br>
            &nbsp; &nbsp; return "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";<br>
        }
        </div>
        <div>where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your Azure Face API key</div>`

    var outputDiv = document.getElementById("OutputDiv");
    try {
        var subscriptionKey = getKey();
    }
    catch (err) {
        outputDiv.innerHTML = missingKeyErrorMsg;
    }

    var spellCheckButton = document.getElementById("SpellCheckButton");
    spellCheckButton.onclick = function () {
        var subscriptionKey = getKey() || "Copy your Subscription key here";
        var textToCheck = document.getElementById("TextToCheck").textContent;

        var webSvcUrl = "https://api.cognitive.microsoft.com/bing/v7.0/spellcheck/?text=" + textToCheck;
        webSvcUrl = webSvcUrl + "&mode=proof&mkt=en-US";

        outputDiv.innerHTML = "Thinking...";

        var httpReq = new XMLHttpRequest();
        httpReq.open("GET", webSvcUrl, true);
        httpReq.setRequestHeader("Ocp-Apim-Subscription-Key", subscriptionKey)
        httpReq.setRequestHeader("contentType", "application/json")
        httpReq.onload = onSpellCheckSuccess;
        httpReq.onerror = onSpellCheckError;
        httpReq.send(null);
    };

    function onSpellCheckSuccess(evt) {
        var req = evt.srcElement;
        var resp = req.response;
        var data = JSON.parse(resp);

        var flaggedTokens = data.flaggedTokens;
        if (data.flaggedTokens.length > 0) {
            var newText = document.getElementById("TextToCheck").textContent;
            ;
            var outputHtml = "";
            flaggedTokens.forEach(flaggedToken => {
                var token = flaggedToken.token;
                var tokenType = flaggedToken.type;
                var offset = flaggedToken.offset;
                var suggestions = flaggedToken.suggestions;
                outputHtml += "<div>"
                outputHtml += "<h3>Token: " + token + "</h3>";
                outputHtml += "Type: " + tokenType + "<br/>";
                outputHtml += "Offset: " + offset + "<br/>";
                outputHtml += "<div>Suggestions</div>";
                outputHtml += "<ul>";

                if (suggestions.length > 0) {
                    suggestions.forEach(suggestion => {
                        outputHtml += "<li>" + suggestion.suggestion;
                        outputHtml += " (" + (suggestion.score * 100).toFixed(2) + "%)" 
                    });
                    outputHtml += "</ul>";
                    outputHtml += "</div>";

                    newText = replaceTokenWithSuggestion(newText, token, offset, suggestions[0].suggestion)
                }
                else {
                    outputHtml += "<ul><li>No suggestions for this token</ul>";
                }
            });

            newText = "<h2>New Text:</h2>" + newText;
            var newTextDiv = document.getElementById("NewTextDiv");
            newTextDiv.innerHTML = newText;

            outputHtml = "<h2>Details</h2>" + outputHtml;
            outputDiv.innerHTML = outputHtml;

        }
        else {
            outputDiv.innerHTML = "No errors found.";
        }
    };

    function onSpellCheckError(evt) {
        outputDiv.innerHTML = "An error has occurred!!!";
    };


    // NOTE: This only works if the suggestion is the same length as the token
    function replaceTokenWithSuggestion(originalString, oldToken, offset, newWord) {
        var textBeforeToken = originalString.substring(0, offset);

        var textAfterToken = "";
        if (originalString.length > textBeforeToken.length + oldToken.length) {
            textAfterToken = originalString.substring(offset + oldToken.length, originalString.length);
        }

        var newString = textBeforeToken + newWord + textAfterToken;

        return newString;
    }

};

