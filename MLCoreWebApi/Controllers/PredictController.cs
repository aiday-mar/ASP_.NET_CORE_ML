using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using MLCoreWebApi.DataModels;

// prediction engine : Class for making single predictions on a previously trained model 
// (and preceding transform pipeline).

public class PredictController : ControllerBase
{
    private readonly PredictEnginePool<SentimentData, SentimentPrediction> _predictionEnginePool;

    public PredictController(PredictionEnginePool<SentimentData,SentimentPrediction> predictionEnginePool)
    {
        _predictionEnginePool = predictionEnginePool;
    }

    [HttpPost]
    public ActionResult<string> Post([FromBody] SentimentData input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        SentimentPrediction prediction = _predictionEnginePool.Predict(modelName: "SentimentAnalysisMdoel", example: input);

        // supposing the conversion is true we output positive, otherwise we output negative
        string sentiment = Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative";

        return Ok(sentiment);
    }
}