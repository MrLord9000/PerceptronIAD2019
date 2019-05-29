using System;


namespace NeuralNetworks
{
    class NeuralNetwork
    {
        private NeuronLayer[] layers;

        // PROPERTIES

        private int L
        {
            get;
            set;
        }

        public float learningCriterion
        {
            get;
            set;
        }

        public float momentumCriterion
        {
            get;
            set;
        }

        public Func<float, float> activationFunction
        {
            get;
            set;
        }

        public Func<float, float> activationDerivative
        {
            get;
            set;
        }

        public float this[int l, int i]
        {
            get => layers[l][i];
            set => layers[l][i] = value;
        }

        public float[] Output                                   // current output of the network
        {
            get
            {
                float[] Output = new float[layers[L - 1].N];
                for (int i = 0; i < layers[L - 1].N; i++)
                {
                    Output[i] = this[L - 1, i];
                }
                return Output;
            }
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

        public float[][][] deltaW
        {
            get
            {
                float[][][] deltaW = new float[L][][];
                for (int i = 0; i < L; i++)
                {
                    deltaW[i] = layers[i].deltaW;
                }
                return deltaW;
            }
        }

        // CONSTRUCTORS

        public NeuralNetwork()
        {

        }

        public NeuralNetwork(int[] neuronsCount)
        {
            ChangeNeuronsCount(neuronsCount);
        }

        public NeuralNetwork(int[] neuronsCount, Func<float, float> activationFunction, Func<float, float> activationDerivative)
            : this(neuronsCount)
        {
            this.activationFunction = activationFunction;
            this.activationDerivative = activationDerivative;
        }

        public NeuralNetwork(int[] neuronsCount, Func<float, float> activationFunction, Func<float, float> activationDerivative, float learningCriterion, float momentumCriterion)
            : this(neuronsCount, activationFunction, activationDerivative)
        {
            this.learningCriterion = learningCriterion;
            this.momentumCriterion = momentumCriterion;
        }

        // METHODS

        public void ChangeNeuronsCount(int[] neuronsCount)
        {
            L = neuronsCount.Length;
            layers = new NeuronLayer[L];
            for (int i = 0; i < (L - 1); i++)
            {
                layers[i] = new NeuronLayer(neuronsCount[i], neuronsCount[i + 1]);
            }
            layers[L - 1] = new NeuronLayer(neuronsCount[L - 1], 0);
        }

        public void Run(float[] input)
        {
            if(input.Length != layers[0].N)
            {
                throw new ArgumentException("Input data array length must be equal to first layer neurons count (N).");
            }

            for(int l = 1; l < L; l++)
            {
                for(int j = 0; j < layers[l].N; j++)
                {
                    this[l, j] = activationFunction(A(l, j));
                }
            }
        }

        public void BackPropagation(float[] output)                                                        // We could use more of if statements to make it more DRY styled
        {
            if(output.Length != layers[L - 1].N)
            {
                throw new ArgumentException("Output data array length must be equal to last layer neurons count (N).");
            }

            float[] b_current = new float[layers[L - 1].N];                                                // keeps b-values for layer we update

            for (int j = 0; j < layers[L - 1].N; j++)
            {
                b_current[j] = (this[L - 1, j] - output[j]) * activationDerivative(A(L - 1, j));

                for (int i = 0; i < layers[L - 2].N; i++)
                {
                    deltaW[L - 2][i][j] += (-learningCriterion) * b_current[j] * this[L - 1, j]; //ZAMIENIŁEM I NA J W THIS[L - 1, i]
                    W[L - 2][i][j] += deltaW[L - 2][i][j];
                }
            }

            float[] b_last;

            for(int l = L - 3; l > 0; l++) // L-2 => L-3
            {
                b_last = b_current;
                b_current = new float[layers[l - 1].N];

                for (int j = 0; j < layers[l - 1].N; j++)
                {
                    for(int k = 0; k < layers[l].N; k++)
                    {
                        b_current[j] += b_last[k] * W[l][j][k]; 
                    }

                    b_current[j] *= activationDerivative(A(l - 1, j));

                    for (int i = 0; i < layers[l].N; i++)
                    {
                        deltaW[l - 1][i][j] *= momentumCriterion;                                           // deltaW[l + 1] = momentum * deltaW[l] + (-learningCriterion) * b_current[j] * this[l, i]
                        deltaW[l - 1][i][j] += (-learningCriterion) * b_current[j] * this[l, j]; //i na j            // we are using iteration to avoid keeping all of those weights deltas
                        W[l - 1][i][j] += deltaW[l - 1][i][j];
                    }
                }
            }
        }

        public float A(int l, int j)
        {
            if(l < 1)
            {
                throw new ArgumentException("Cannot calculate activation values for first layer.");
            }

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
