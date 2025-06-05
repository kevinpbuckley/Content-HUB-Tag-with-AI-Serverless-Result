# Content Hub Tag with AI Serverless Result

This project demonstrates how to integrate a custom AI service with Sitecore Content Hub using a serverless approach. The script processes the output from your AI service and formats it to match the expected structure for Content Hub's tagging suggestions.

## Overview

When integrating your own AI service with Sitecore Content Hub, your AI should return predictions in the following format:

```json
{
  "predictions": [
    {
      "probability": 0.7597,
      "tagName": "Ultron",
      "source": "custom"
    },
    {
      "probability": 0.7312,
      "tagName": "Avengers",
      "source": "default"
    }
  ]
}
```

The script in Content-Hub-Tag-With-AI-Serverless-Result.cs extracts the tagName values from the predictions and returns them to Content Hub in the required format:

```json
{
  "TagToAsset": ["Ultron", "Avengers"]
}
```

How It Works
The script reads the AI result from Context.AIResult.
It extracts all unique, non-empty tagName values from the predictions array.
These tag names are added to the TagToAsset relation in the Context.Suggestions dictionary.
Content Hub uses these suggestions to tag assets automatically.
References

Follow the Documentation for wiring up your own script. Use the default input script along with this Result Script.

Sitecore Documentation:
https://doc.sitecore.com/ch/en/users/content-hub/ai-assisted-grounded-image-tagging.html

The script will handle the transformation and provide tagging suggestions to Content Hub.

BuckeyBuilds.ai for more information.
