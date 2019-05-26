﻿using System;


namespace NeuralNetworks
{
    class NeuralNetwork
    {
        private NeuronLayer[] layers;
        private Delegate activationFunction;
        private Delegate activationDerivative;

        public int L
        {
            get;
            set;
        }

        public float this[int l, int i]
        {
            get => layers[l][i];
            set => layers[l][i] = value;
        }

        public float[][][] W
        {
            get
            {
                float[][][] W = new float[L][][];
                for (int i = 0; i < L; i++)
                {
                    W[i] = layers[i].W;
                }
                return W;
            }
        }
        
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

        public float A(int l, int j)
        {
            float a = 0.0f;
            for(int i = 0; i < layers[l - 1].N; i++)
            {
                a += this[l - 1, i] * W[l - 1][i][j];       // well... that could be quite heavy... who gives a damn? It's 21st century!!!
            }
            return a;
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
