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

            layers[0] = new NeuronLayer(neuronsCount[0], 0);

            for (int i = 1; i < L; i++)
            {
                layers[i] = new NeuronLayer(neuronsCount[i], neuronsCount[i - 1]);
            }
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

                for (int i = 0; i < layers[L - 1].N; i++)
                {
                    deltaW[L - 1][j][i] += (-learningCriterion) * b_current[j] * this[L - 1, i];            
                    W[L - 1][j][i] += deltaW[L - 1][j][i];
                }
            }

            float[] b_last;

            for(int l = L - 2; l > 0; l--) // L-2 => L-3
            {
                b_last = b_current;
                b_current = new float[layers[l].N];

                for (int j = 0; j < layers[l].N; j++)
                {
                    for(int k = 0; k < layers[l + 1].N; k++)
                    {
                        b_current[j] += b_last[k] * W[l + 1][k][j]; 
                    }

                    b_current[j] *= activationDerivative(A(l, j));

                    for (int i = 0; i < layers[l + 1].N; i++)
                    {
                        deltaW[l][j][i] *= momentumCriterion;                                           // deltaW[l + 1] = momentum * deltaW[l] + (-learningCriterion) * b_current[j] * this[l, i]
                        deltaW[l][j][i] += (-learningCriterion) * b_current[j] * this[l, i]; //i na j            // we are using iteration to avoid keeping all of those weights deltas
                        W[l][j][i] += deltaW[l][j][i];
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
                a += this[l - 1, i] * W[l][j][i];       // well... that could be quite heavy... who gives a damn? It's 21st century!!!
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

        public string PrintOutput()
        {
            string str = "[";

            for(int i = 0; i < layers[L - 1].N; i++)
            {
                str += this[L - 1, i] + ", ";
            }
            str = str.Substring(0, str.Length - 2) + "]";

            return str;
        }
    }
}
