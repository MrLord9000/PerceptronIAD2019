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
            //new CSV_import("data_class_train.csv");

            int[] neuronsCount = new int[] { 4, 8, 12, 16 };
            float[] learningCriterions = new float[] { 0.05f, 0.1f, 0.5f };
            float[] momentumCriterions = new float[] { 0.0f, 0.1f, 0.5f, 0.9f };

            float[][] trainInputs = null;
            float[][] trainResults = null;
            float[][] testInputs = null;
            float[][] testResults = null;
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

                for(int it = 0; it < 12; it++)
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
