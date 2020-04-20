using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MLCoreWebApi
{
    public class Program
    {
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "MLModels", "iris.data");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "MLModels", "IrisClusteringModel.zip");

        public static void Main(string[] args)
        {
            var mlContext = new MLContext(seed: 0);
            // infers the data set schema from the provided IrisData type and returns IDataView which can be used as input for transformers.
            IDataView dataView = mlContext.Data.LoadFromTextFile<IrisData>(_dataPath, hasHeader: false, separatorChar: ',');

            // this is the name of the column
            string featuresColumnName = "Features";

            // you concatenate the columns into one feature column
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));
            // above you use a trainer using the given feature column and 3 clusters 

            var model = pipeline.Fit(dataView);
            // data loading and model training

            // you save the result into the output path, you create it here below
            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                // save the model above, using the schema of the dataview into the output filestream
                mlContext.Model.Save(model, dataView.Schema, fileStream);
            }

            // produce instance of output type using the IrisData as an input
            // here the model is an argment in the prediction engine below
            var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(model);

            // to test the above code, you apply the predictor on the Setosa type flower of the class TestIrisData.
            var prediction = predictor.Predict(TestIrisData.Setosa);
            // we output the cluster id on which is it is according to the prediction
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            // the prediction distances is followed with a little space
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
