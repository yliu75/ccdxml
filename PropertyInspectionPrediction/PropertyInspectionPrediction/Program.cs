using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Accord.Collections;
using Accord.IO;
using System.IO;
namespace PropertyInspectionPrediction {
    class Program {
        public static string filePath = @"train.csv";
        public static void parseCSV() {
            /*
            using(TextFieldParser parser = new TextFieldParser(@"train.csv")) {
                parser.TextFieldType=FieldType.Delimited;
                parser.SetDelimiters(",");
                List<string> row=new List<string>(0);
                //int rowIndex=2;
                while(!parser.EndOfData) {
                    //Processing row
                    //string[] fields = parser.ReadFields();
                    //
                    //foreach(string unit in fields) {
                    //  Console.WriteLine(unit);
                    //}
                    row.Add(parser.ReadLine());

                }
            }*/
            StreamReader reader = File.OpenText(filePath);
            Console.WriteLine();
            Console.ReadLine();
        }

        static void Main(string[] args) {
            parseCSV();
        }
    }
}
