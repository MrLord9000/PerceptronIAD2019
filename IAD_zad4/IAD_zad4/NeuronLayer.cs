using System;

namespace NeuralNetworks
{
    public class NeuronLayer
    {
        private Neuron[] neurons;

        public int N
        {
            get;
        }

        public float this[int i]
        {
            get => neurons[i].X;
            set => neurons[i].X = value;
        }

        public float[][] W
        {
            get
            {
                float[][] W = new float[N][];
                for(int i = 0; i < N; i++)
                {
                    W[i] = neurons[i].W;
                }
                return W;
            }
        }

        public NeuronLayer(int N_current, int N_next)
        {
            N = N_current;
            neurons = new Neuron[N];

            for (int i = 0; i < N; i++)
            {
                this.neurons[i] = new Neuron(N_next);
            }
        }

        public void RandomizeWeights(Random rand)
        {
            for(int i = 0; i < N; i++)
            {
                neurons[i].RandomizeWeights(rand);
            }
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < N; i++)
            {
                str += neurons[i] + "\n";
            }
            return str;
        }
    }
}