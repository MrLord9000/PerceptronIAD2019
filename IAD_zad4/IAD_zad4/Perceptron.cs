using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_zad4
{
    enum Species
    {
        Setosa, Versicolor, Virginica
    }

    struct TrainData
    {
        // Structure fields
        public float[] X { get; }
        public Species D { get; }

        public float this[int i]
        {
            get
            {
                if (i > 4 || i < 0) throw new IndexOutOfRangeException("Tried to reach data element number " + i + " not from range [0, 4]");
                else return X[i];
            }
        }

        public TrainData(float[] x, Species d)
        {
            if (x.Length == 4)
            {
                X = x;
                D = d;
            }
            else throw new ArgumentException("Passed too many x-values. Should be 4, passed " + x.Length);
        }

        public override string ToString()
        {
            return X[0] + ", " + X[1] + ", " + X[2] + ", " + X[3] + ", " + D.ToString();
        }
    }

    class Perceptron
    {
        public Perceptron()
        {
            TrainData trainData = new TrainData(new float[4] { 5.1f, 3.5f, 1.4f, 0.2f }, Species.Setosa);

            Console.WriteLine("Train Data test.\nSpecies: " + trainData.D);
            Console.WriteLine("Data: " + trainData[0] + trainData[1] + trainData[2] + trainData[3]);
        }
    }
}
