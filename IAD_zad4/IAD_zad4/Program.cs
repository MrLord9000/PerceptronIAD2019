using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using NeuralNetworks;

namespace IAD_zad4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] neuronsCount = new int[] { 4, 8, 12, 16 };
            float[] learningCriteria = new float[] { 0.05f, 0.1f, 0.5f };
            float[] momentumCriteria = new float[] { 0.0f, 0.1f, 0.5f, 0.9f };

            CSV_import csvFile = new CSV_import("data_class_train.csv");

            // Initializing arrays with adequate sizes
            float[][] trainInputs = new float[csvFile.CsvData.Count][];
            float[][] trainResults = new float[csvFile.CsvData.Count][];

            // Filling train inputs array
            for (int i = 0; i < trainInputs.GetLength(0); i++)
            { 
                trainInputs[i] = csvFile.CsvData[i].X;
            }

            // Filling train results array
            for (int i = 0; i < trainResults.GetLength(0); i++)
            {
                if(csvFile.CsvData[i].D == Species.Setosa)
                    trainResults[i] = new float[3] {1f, 0f, 0f};
                else if (csvFile.CsvData[i].D == Species.Versicolor)
                    trainResults[i] = new float[3] { 0f, 1f, 0f };
                else if (csvFile.CsvData[i].D == Species.Virginica)
                    trainResults[i] = new float[3] { 0f, 0f, 1f };
            }


            csvFile = new CSV_import("data_class_test.csv");

            // Initializing arrays with adequate sizes
            float[][] testInputs = new float[csvFile.CsvData.Count][];
            float[][] testResults = new float[csvFile.CsvData.Count][];

            // Filling train inputs array
            for (int i = 0; i < testInputs.GetLength(0); i++)
            {
                testInputs[i] = csvFile.CsvData[i].X;
            }

            // Filling train results array
            for (int i = 0; i < testResults.GetLength(0); i++)
            {
                if (csvFile.CsvData[i].D == Species.Setosa)
                    testResults[i] = new float[3] { 1f, 0f, 0f };
                else if (csvFile.CsvData[i].D == Species.Versicolor)
                    testResults[i] = new float[3] { 0f, 1f, 0f };
                else if (csvFile.CsvData[i].D == Species.Virginica)
                    testResults[i] = new float[3] { 0f, 0f, 1f };
            }

            float[][][][] trainOutputs = null;
            float[][][][] testOutputs = null;


            NeuralNetwork perceptron = new NeuralNetwork();
            perceptron.activationFunction = (x) => (float)(1 / (1 + Math.Pow(Math.E, -x)));
            perceptron.activationDerivative = (x) => (float)(Math.Pow(Math.E, x) / Math.Pow(1 + Math.Pow(Math.E, x), 2));
            perceptron.learningCriterion = 0.1f;
            perceptron.momentumCriterion = 0.0f;

            for (int N = 0; N < neuronsCount.Length; N++)
            {
                perceptron.ChangeNeuronsCount(new int[] { 4, neuronsCount[N], 3 });

                for (int it = 0; it < 12; it++)
                {
                    perceptron.RandomizeWeights(new Random());

                    for (int e = 0; e < trainInputs.Length; e++)
                    {
                        perceptron.Run(trainInputs[e]);
                        trainOutputs[N][it][e] = perceptron.Output;
                        perceptron.BackPropagation(trainResults[e]);
                    }

                    for (int e = 0; e < testInputs.Length; e++)
                    {
                        perceptron.Run(testInputs[e]);
                        testOutputs[N][it][e] = perceptron.Output;
                    }
                }
            }


        }
    }
}
