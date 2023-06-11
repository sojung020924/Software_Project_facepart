using System;
using System.Collections.Generic;
using System.IO;

namespace csv_merge
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath1 = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\늙은 사람 이미지 벡터\\all_data.csv";
            string csvFilePath2 = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\젊은 사람 이미지 벡터\\all_data.csv"; 
            string mergedFilePath = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\people.csv";

            List<string> file1Lines = ReadCSVFile(csvFilePath1);

            
            List<string> file2Lines = ReadCSVFile(csvFilePath2);

           
            List<string> mergedLines = new List<string>(file1Lines);
            mergedLines.AddRange(file2Lines);

            
            WriteCSVFile(mergedFilePath, mergedLines);
        }

        // CSV 파일 읽기
        static List<string> ReadCSVFile(string filePath)
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        // CSV 파일 쓰기
        static void WriteCSVFile(string filePath, List<string> lines)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
