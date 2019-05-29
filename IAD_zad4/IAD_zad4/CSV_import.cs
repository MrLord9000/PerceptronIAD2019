using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAD_zad4
{
    class CSV_import
    {
        public List<TrainData> CsvData { get; }

        public CSV_import(string defaultFile = "")
        {
            CsvData = new List<TrainData>();

            Console.WriteLine("File name to import data (enter to use default " + defaultFile + "): ");
            string filePath = Console.ReadLine();

            if (filePath.Length == 0 && defaultFile.Length > 0) filePath = defaultFile;
            else throw new ArgumentException("Default file path not specified!");

            using (TextReader reader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(reader))
            {
                Console.WriteLine("Reading .csv file...");

                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.CultureInfo = new CultureInfo("en-EN");

                while (csvReader.Read())
                {
                    //Console.WriteLine(csvReader.GetField(1));
                    float[] x = new float[4] {  csvReader.GetField<float>(0),
                                                csvReader.GetField<float>(1),
                                                csvReader.GetField<float>(2),
                                                csvReader.GetField<float>(3) };
                    Species species = new Species();

                    if (csvReader.GetField<bool>(4))
                        species = Species.Setosa;
                    else if (csvReader.GetField<bool>(5))
                        species = Species.Versicolor;
                    else if (csvReader.GetField<bool>(6))
                        species = Species.Virginica;

                    CsvData.Add(new TrainData(x, species));
                }
            }
            Console.WriteLine("Csv read succesful!");
        }

        public void PrintCSV()
        {
            foreach(TrainData elem in CsvData)
            {
                Console.WriteLine(elem.ToString());
            }
        }

        public static void Main()
        {
            //CSV_import testCSV = new CSV_import("data_class_test.csv");
            //testCSV.PrintCSV();
        }
    }
}

//public CSV_import(float[][] data, string defaultFile = "")
//{
//    string filePath;
//    if (defaultFile == "")
//    {
//        Console.WriteLine("File name to import data (enter to use default " + defaultFile + "): ");
//        filePath = Console.ReadLine();
//    }
//    else
//    {
//        filePath = defaultFile;
//    }

//    using (TextReader reader = new StreamReader(filePath))
//    using (CsvReader csvReader = new CsvReader(reader))
//    {
//        csvReader.Configuration.Delimiter = ",";
//        csvReader.Configuration.CultureInfo = new CultureInfo("en-EN");

//        IEnumerable<float> temp = csvReader.GetRecords<float>();
//        float[] dataRead = temp.ToArray<float>();

//        data = new float[dataRead.Length / 7][];
//        data = new float[dataRead.Length / 7][];

//        for (int i = 0; i < dataRead.Length / 7; i++)
//        {
//            data[i] = new float[7];
//            for (int j = 0; j < 7; j++)
//            {
//                data[i][j] = dataRead[7 * i + j];
//                Console.Write(data[i][j] + ", ");
//            }
//            Console.WriteLine();
//        }
//    }
//}