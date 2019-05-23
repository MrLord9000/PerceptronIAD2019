using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_zad4
{
    class CSV_import
    {
        private List<TrainData> csvData;
        public List<TrainData> CsvData => csvData;

        public CSV_import(string defaultFile = "")
        {
            csvData = new List<TrainData>();

            Console.WriteLine("File name to import data (enter to use default " + defaultFile + "): ");
            string filePath = Console.ReadLine();

            if (filePath.Length == 0 && defaultFile.Length > 0) filePath = defaultFile;
            else throw new ArgumentException("Default file path not specified!");

            using (TextReader reader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(reader))
            {
                Console.WriteLine("Reading .csv file...");
                while (csvReader.Read())
                {
                    Console.WriteLine(csvReader.GetField(0));
                    float[] x = new float[4] {  float.Parse(csvReader.GetField(0)), float.Parse(csvReader.GetField(1)), float.Parse(csvReader.GetField(2)), float.Parse(csvReader.GetField(3)) };
                    Species species = new Species();

                    if (csvReader.GetField<bool>(4))
                        species = Species.Setosa;
                    else if (csvReader.GetField<bool>(5))
                        species = Species.Versicolor;
                    else if (csvReader.GetField<bool>(6))
                        species = Species.Virginica;

                    csvData.Add(new TrainData(x, species));
                }
            }
            Console.WriteLine("Csv read succesful!");
        }

        public void PrintCSV()
        {
            foreach(TrainData elem in csvData)
            {
                Console.WriteLine(elem.ToString());
            }
        }
    }
}
