using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

static class TestIrisData
{
    // we introduce an instance of the IrisData class called Setosa, and it will have specific details
    internal static readonly IrisData Setosa = new IrisData
    {
        SepalLength = 5.1f,
        SepalWidth = 3.5f,
        PetalLength = 1.4f,
        PetalWidth = 0.2f
    };
}
