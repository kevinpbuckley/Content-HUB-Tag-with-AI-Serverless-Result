using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

/**
    The goal of this script is to translate the output from the AI to expected format by Content Hub

    The new data format from the AI looks like the following:

    {
        "predictions": [
            {
                "probability": 0.7597155570983887,
                "tagName": "video game",
                "source": "default"
            },
            ...
        ]
    }

    The data must be returned via Context.Suggestions, in the following format:

    {
        "TagToAsset": [
            "video game",
            "Ultron"
        ]
    }
**/

MClient.Logger.Info($"Prediction: {Context.AIResult.ToString()}");

const string TagToAssetRelationName = "TagToAsset";

// Get the predictions array from the AI result
var predictionsToken = Context.AIResult["predictions"];
if (predictionsToken == null)
{
    MClient.Logger.Error("No predictions found in AI result.");
    return;
}

// Extract tag names from predictions
var tagNames = predictionsToken
    .Children()
    .Select(p => p["tagName"]?.ToString())
    .Where(tag => !string.IsNullOrWhiteSpace(tag))
    .Distinct()
    .ToList();

// Add tag names to suggestions
var suggestionsByRelationName = new Dictionary<string, IEnumerable<string>>
{
    { TagToAssetRelationName, tagNames }
};

// Return the suggestions in the Context bag
Context.Suggestions = suggestionsByRelationName;
