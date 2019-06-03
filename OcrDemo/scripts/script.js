$(function () {
    $("#ImageUrlDropdown").change(function () {
        var url = this.value;
        showTextImage(url);
    })

    showTextImage = function (url) {
        $("#SourceUrlSpan").text(url);
        $("#TextImageFrame").attr("src", url);
        $("#OutputDiv").text("");

    }

    const missingKeyErrorMsg = `<div>No key found.<br>
        Create a script.js file with the following code:.</div>
        <div style="color:red; padding-left: 20px;">
        var getKey = function(){<br>
            &nbsp; &nbsp; return "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";<br>
        }
        </div>
        <div>where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your Azure Computer Vision API key</div>`
    

    $("#GetTextFromPictureButton").click(function () {
        var outputDiv = $("#OutputDiv");
        outputDiv.text("Thinking...");
        var url = $("#ImageUrlDropdown").val();
        var language = $("#LanguageDropdown").val();

        try {
            var computerVisionKey = getKey();
        }
        catch(err) {
            outputDiv.html(missingKeyErrorMsg);
            return;
        }


        
        var webSvcUrl = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/ocr";
        webSvcUrl = webSvcUrl + "?language=" + language;
        $.ajax({
            type: "POST",
            url: webSvcUrl,
            headers: { "Ocp-Apim-Subscription-Key": computerVisionKey },
            contentType: "application/json",
            data: '{ "Url": "' + url + '" }'
        }).done(function (data) {
            outputDiv.text("");

            var regionsOfText = data.regions;
            for (var r = 0; r < regionsOfText.length; h++) {
                var linesOfText = data.regions[r].lines;
                for (var l = 0; l < linesOfText.length; l++) {
                    var output = "";

                    var thisLine = linesOfText[l];
                    var words = thisLine.words;
                    for (var w = 0; w < words.length; w++) {
                        var thisWord = words[w];
                        output += thisWord.text;
                        output += " ";
                    }
                    var newDiv = "<div>" + output + "</div>";
                    outputDiv.append(newDiv);

                }
                outputDiv.append("<hr>");
            }

        }).fail(function (err) {
            $("#OutputDiv").text("ERROR!" + err.responseText);
        });



    });

    $('#ImageUrlDropdown option:eq(0)').prop('selected', true);
    showTextImage($('#ImageUrlDropdown option:eq(0)').val());


});


