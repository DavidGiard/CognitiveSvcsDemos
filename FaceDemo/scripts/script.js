$(function () {
    var showImage = function () {
        var imageUrl = $("#imageUrlTextbox").val();
        if (imageUrl) {
            $("#ImageToAnalyze").attr("src", imageUrl);
        }
    };

    const missingKeyErrorMsg = `<div>No key found.<br>
        This demo will not work without a key.<br>
        Create a getkey.js file with the following code:.</div>
        <div style="color:red; padding-left: 20px;">
        var getKey = function(){<br>
            &nbsp; &nbsp; return "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";<br>
        }
        </div>
        <div>where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your Azure Face API key</div>`
    
    var getFaceInfo = function () {

        var outputDiv = $("#OutputDiv");
        outputDiv.text("Thinking...");

        try {
            var subscriptionKey = getKey();
        }
        catch(err) {
            outputDiv.html(missingKeyErrorMsg);
            return;
        }

        var imageUrl = $("#imageUrlTextbox").val();

        var webSvcUrl = "https://eastus2.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=true&returnFaceAttributes=age,gender,smile,facialHair,headPose,glasses";
		


        $.ajax({
            type: "POST",
            url: webSvcUrl,
            headers: { "Ocp-Apim-Subscription-Key": subscriptionKey },
            contentType: "application/json",
            data: '{ "Url": "' + imageUrl + '" }'
        }).done(function (data) {

            var returnText = JSON.stringify(data);
            $("#RawJsonDiv").text(returnText);

            if (data.length > 0) {
                var firstFace = data[0];
                var faceId = firstFace.faceId;
                var faceRectange = firstFace.faceRectangle;
                var faceWidth = faceRectange.width;
                var faceHeight = faceRectange.height;
                var faceLeft = faceRectange.left;
                var faceTop = faceRectange.top;
                
                $("#Rectangle").css("top", faceTop);
                $("#Rectangle").css("left", faceLeft);
                $("#Rectangle").css("height", faceHeight);
                $("#Rectangle").css("width", faceWidth);
                $("#Rectangle").css("display", "block");

                var faceLandmarks = firstFace.faceLandmarks;
                var faceAttributes = firstFace.faceAttributes;

                var leftPupil = faceLandmarks.pupilLeft;
                var rightPupil = faceLandmarks.pupilRight;
                var mouth = faceLandmarks.mouthLeft;
                var nose = faceLandmarks.noseLeftAlarOutTip;
                var noseHorizontalCenter = (faceLandmarks.noseLeftAlarOutTip.x + faceLandmarks.noseRightAlarOutTip.x)/2; 
                var mouthTop = faceLandmarks.upperLipTop;
                var mouthBottom = faceLandmarks.underLipBottom;
                var mouthVerticalCenter = (mouthBottom.y + mouthTop.y)/2;
                

                $("#LeftEyeDiv").css("top", leftPupil.y);
                $("#LeftEyeDiv").css("left", leftPupil.x);
                $("#LeftEyeDiv").css("display", "block");
                $("#RightEyeDiv").css("top", rightPupil.y);
                $("#RightEyeDiv").css("left", rightPupil.x);
                $("#RightEyeDiv").css("display", "block");
                $("#NoseDiv").css("top", nose.y);
                $("#NoseDiv").css("left", noseHorizontalCenter);
                $("#NoseDiv").css("display", "block");
                $("#MouthDiv").css("top", mouthVerticalCenter);
                $("#MouthDiv").css("left", mouthTop.x);
                $("#MouthDiv").css("display", "block");
                
                
                

                leftEyeWidth = faceLandmarks.eyebrowLeftInner.x - faceLandmarks.eyebrowLeftOuter.x;
                rightEyeWidth = faceLandmarks.eyebrowRightOuter.x - faceLandmarks.eyebrowRightInner.x;
                mouthWidth = faceLandmarks.mouthRight.x - faceLandmarks.mouthLeft.x;

                var mouthLeft = faceLandmarks.mouthLeft;
                var mouthRight = faceLandmarks.mouthRight;
                var mouthTop = faceLandmarks.upperLipTop;
                var mouthBottom = faceLandmarks.underLipBottom;

                var outputText = "";
                outputText += "Face ID: " + faceId + "<br>";
                outputText += "Top: " + faceTop + "<br>";
                outputText += "Left: " + faceLeft + "<br>";
                outputText += "Width: " + faceWidth + "<br>";
                outputText += "Height: " + faceHeight + "<br>";
                outputText += "Right Pupil: " + rightPupil.x + ", " + rightPupil.y + "<br>";
                outputText += "Left Pupil: " + leftPupil.x + ", " + leftPupil.y + "<br>";
                outputText += "Mouth: <br>";
                outputText += " -Left: " + mouthLeft.x + ", " + mouthLeft.y + "<br>";
                outputText += " -Right: " + mouthRight.x + ", " + mouthRight.y + "<br>";
                outputText += " -Top: " + mouthTop.x + ", " + mouthTop.y + "<br>";
                outputText += " -Bottom: " + mouthBottom.x + ", " + mouthBottom.y + "<br>";
                outputText += "Attributes:" + "<br>";
                outputText += "age: " + faceAttributes.age + "<br>";
                outputText += "gender: " + faceAttributes.gender + "<br>";
                outputText += "smile: " + (faceAttributes.smile || "n/a") + "<br>";
                outputText += "glasses: " + faceAttributes.glasses + "<br>";
                
                outputDiv.html(outputText);

            }
            else {
                outputDiv.text("No faces detected.");

            }

        }).fail(function (err) {
            $("#OutputDiv").text("ERROR!" + err.responseText);
        });
    };


    var hideMarkers = function () {
        $(".FaceLabel").css("display", "none");
        $("#Rectangle").css("display", "none");
    };


    $("#imageUrlTextbox").change(function () {
        hideMarkers();
        showImage();
        getFaceInfo();
    })

    // $("#imageUrlTextbox").val("https://tena2013.eventpoint.com/resources/documents/p/tena2013/photos/913a496b-7099-e211-ae09-001ec953730b.jpg");
    // $("#imageUrlTextbox").val("http://content.latest-hairstyles.com/wp-content/uploads/2013/07/haircuts-for-round-faces-175x125.jpg");
    //$("#imageUrlTextbox").val("http://cache3.asset-cache.net/gc/481501023-close-up-portrait-of-old-black-man-with-gettyimages.jpg?v=1&c=IWSAsset&k=2&d=esg%2bVK6qs4oIjoYYcweKEaG2pptAYfOPpuLOWcyxTSE%3d");
    $("#imageUrlTextbox").val("https://usercontent2.hubstatic.com/3988615_f260.jpg");
	
    showImage();
    getFaceInfo();


});

