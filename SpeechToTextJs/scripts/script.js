window.onload = function(){

  var listenButton = document.getElementById("listenButton");
  var outputTextDiv = document.getElementById("outputTextDiv");
  var statusDiv = document.getElementById("StatusDiv");
  
  
  const missingKeyErrorMsg = `<div>No key found.<br>
  This demo will not work without a key.<br>
  Create a getkey.js file with the following code:.</div>
  <div style="color:red; padding-left: 20px;">
  var getKey = function(){<br>
      &nbsp; &nbsp; return "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";<br>
  }
  </div>
  <div>where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your Azure Face API key</div>`
  
  try {
    var key = getKey();
  }
  catch(err) {
    outputTextDiv.innerHTML = missingKeyErrorMsg;
    return;
  }
  
  
  
  //var key = "6a9e14a5057f4d4e921f22dd782853c2";
  var region = "eastus";
  speechConfig = SpeechSDK.SpeechConfig.fromSubscription(key, region);
  speechConfig.speechRecognitionLanguage = "en-US";
  
  var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
  recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);
  
  listenButton.addEventListener("click", function () {
    statusDiv.innerHTML = "Listening...";
  
    recognizer.recognizeOnceAsync(
      function (result) {
        listenButton.disabled = false;
        outputTextDiv.innerHTML += result.text;
        window.console.log(result);
  
        recognizer.close();
        recognizer = undefined;
        statusDiv.innerHTML = "Done";
      },
      function (err) {
        listenButton.disabled = false;
        outputTextDiv.innerHTML += err;
        window.console.log(err);
  
        recognizer.close();
        recognizer = undefined;
        statusDiv.innerHTML = "Error";
      });
  });
  
  
}
