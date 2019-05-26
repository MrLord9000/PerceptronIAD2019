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

        public Neuron(int N_next)
        {
            if(N_next != 0)
            {
                W = new float[N_next];
            }
        }

        public override string ToString()
        {
            string str = "";
            str += "X = " + X + "\n";

            for (int i = 0; i < W.Length; i++)
                str += "W[" + i + "] = " + W[i] + " ";

            return str;
        }
    }
}