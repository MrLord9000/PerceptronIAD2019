using System;

namespace NeuralNetworks
{
    public class Neuron
    {
        public float X
        {
            get;
            set;
        }

        public float[] W
        {
            get;
            set;
        }

        public float[] deltaW
        {
            get;
            set;
        }

        public Neuron(int N_last)
        {
            if(N_last != 0)
            {
                W = new float[N_last];
                deltaW = new float[N_last];
            }
        }

        public void RandomizeWeights(Random rand)
        {
            if(W != null)
            {
                for (int i = 0; i < W.Length; i++)
                {
                    W[i] = (float) rand.NextDouble() * 2 - 1;
                    deltaW[i] = 0;
                }
            }
        }

        public override string ToString()
        {
            string str = "";
            str += "X = " + X + "\n";

            if(W != null)
            {
                for (int i = 0; i < W.Length; i++)
                {
                    str += "W[" + i + "] = " + W[i] + " ";
                }
            }

            return str;
        }
    }
}