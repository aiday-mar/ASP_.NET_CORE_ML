using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

// extends the sentiment data file which specifies how to load the csv file
public class SentimentPrediction : SentimentData
{
    [ColumnName("PredictedLabel")]

    // the prediction of the label 
    public bool Prediction { get; set; }
    
    // the probability of this being true?
    public float Probability { get; set; }

    public float Score { get; set; }
}

