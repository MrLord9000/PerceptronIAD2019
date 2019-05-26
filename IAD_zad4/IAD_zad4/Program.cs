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

            /*
            Neuron test = new Neuron(5);
            Console.WriteLine(test.ToString());
            test.W[0] = 5;
            test.W[1] = 4;
            test.W[2] = 3;
            test.W[3] = 2;
            test.W[4] = 1;
            Console.WriteLine(test.ToString());
            Console.ReadKey();
            */

            /*
            NeuronLayer test = new NeuronLayer(4, 3);
            test.W[0][1] = 3;
            test.W[1][2] = 3;
            test.W[2][0] = 1;
            Console.WriteLine(test.ToString());
            Console.ReadKey();
            */

            NeuralNetwork test = new NeuralNetwork(new int[] { 4, 6, 8, 3 });
            test.RandomizeWeights(new Random());
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
