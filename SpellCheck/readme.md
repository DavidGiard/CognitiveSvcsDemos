# Cognitive Services Demo

## Sentiment Analysis

### Overview

This demo calls the Cognitive Services Sentiment API, passing in text and returning a score between 0 and 1, representing the sentiment of that text. A lower score represents a negative sentiment; a higher score represents a positive sentiment.

### Setup

The following steps are required to run this sample code:

1. Sign up for an Azure account
2. Create a Text Analytics API key
3. Create the getkey.js file

Details:

#### Sign up for an Azure account

You will need to install node to run this sample. You can download and install node from [here](https://nodejs.org/).

#### Create a Text Analytics API key

You will need to create an API key for the v Cognitive Service. To do this, sign up for an Azure account at [azure.com](http://azure.com). You can sign up for a Free Account.

#### Create the getkey.js file

To protect the API key, I did not check in the getkey.js file. You will need to create this file in the root folder. Add the following code to getkey.js

```javascript
var getKey = function () {
    return 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx';
}

where xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx is your API key.

### Run the sample.

To run the sample, open index.html in a web browser.
