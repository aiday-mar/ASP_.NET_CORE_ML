using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

public class SentimentData
{
    // here you specify the different columns to be loaded from the csv file
    [LoadColumn(0)]
    public string SentimentText;

    [LoadColumn(1)]
    [ColumnName("Label")]
    public bool Sentiment;
}
