# Cognitive Services Demo

## Optical Character Recognition

### Overview

This demo calls the OCR API, passing in an image containing text and returning the text in the image.

### Setup

The following steps are required to run this sample code:

1. Sign up for an Azure account
2. Create a Computer Vision API key
3. Create the getkey.js file

Details:

#### Sign up for an Azure account

You will need to install node to run this sample. You can download and install node from [here](https://nodejs.org/).

#### Create a Computer Vision API key

You will need to create an API key for the Cognitive Service. To do this, sign up for an Azure account at [azure.com](http://azure.com). You can sign up for a Free Account.

#### Create the getkey.js file

To protect the API key, I did not check in the getkey.js file. You will need to create this file in the */Scripts* folder. Add the following code to getkey.js

```javascript
var getKey = function () {
    return 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx';
}

where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your API key.

### Run the sample.

To run the sample, open index.html in a web browser.
