﻿using System;


namespace NeuralNetworks
{
    class NeuralNetwork
    {
        private int L;
        private NeuronLayer[] layers;
        private Delegate activationFunction;
        private Delegate activationDerivative;



        public NeuralNetwork(int[] neuronsCount)
        {
            L = neuronsCount.Length;
            layers = new NeuronLayer[L];
            for(int i = 0; i < (L - 1); i++)
            {
                layers[i] = new NeuronLayer(neuronsCount[i], neuronsCount[i + 1]);
            }
            layers[L - 1] = new NeuronLayer(neuronsCount[L - 1], 0);
        }

        public void RandomizeWeights(Random rand)
        {
            for(int i = 0; i < L; i++)
            {
                layers[i].RandomizeWeights(rand);
            }
        }

        public override string ToString()
        {
            string str = "";
            for(int i = 0; i < L; i++)
            {
                str += "L = " + i + "\n" + layers[i];
            }
            return str;
        }
    }
}
