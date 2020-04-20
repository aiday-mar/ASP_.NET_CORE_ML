using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.Data;

public class IrisData
{
    // definition for each feature of the data set
    [LoadColumn(0)]
    public float SepalLength;

    [LoadColumn(1)]
    public float SepalWidth;

    [LoadColumn(2)]
    public float PetalLength;

    [LoadColumn(3)]
    public float PetalWidth;
}

// output of clustering model
public class ClusterPrediction
{
    // bound to specific column names as written in the double quotes
    // contains id of predicted cluster
    [ColumnName("PredictedLabel")]
    public uint PredictedClusterId;

    [ColumnName("Score")]
    // an array of floating variables
    // Score column contains an array with squared Euclidean distances to the cluster centroids. The array length is equal to the number of clusters.

    public float[] Distances;
}

