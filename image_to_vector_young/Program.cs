using System;
using System.Drawing;
using System.IO;

namespace image_to_vector
{
    class Program
    {
        static void Main(string[] args)
        {
            // 사진들이 있는 폴더 경로
            string folderPath = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\젊은 사람 이미지(가공)";

            // 모든 사진 데이터를 저장할 리스트
            var allData = new List<double[]>();

            // 폴더 내의 모든 사진 파일에 대해 처리
            string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");

            foreach (string imageFile in imageFiles)
            {
                // 이미지 로드
                using Bitmap bitmap = new Bitmap(imageFile, true);

                // 이미지를 그레이스케일로 변환
                using Bitmap grayscaleBitmap = ToGrayscale(bitmap);

                // 이미지를 double 형식의 벡터로 변환
                double[] vector = ImageToArray(grayscaleBitmap);

                // 벡터 데이터 추가
                allData.Add(vector);
            }

            // 벡터 데이터 저장
            string csvFilePath = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\젊은 사람 이미지 벡터\\all_data.csv";
            SaveDataToCSV(allData, csvFilePath);
        }

        // 이미지를 그레이스케일로 변환하는 함수
        static Bitmap ToGrayscale(Bitmap image)
        {
            Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int grayscale = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    grayscaleImage.SetPixel(x, y, Color.FromArgb(grayscale, grayscale, grayscale));
                }
            }

            return grayscaleImage;
        }

        // 이미지를 double 형식의 벡터로 변환하는 함수
        static double[] ImageToArray(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            double[] array = new double[width * height];

            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    double grayscale = pixel.R; // 0에서 255까지의 값으로 설정
                    array[index] = grayscale;
                    index++;
                }
            }

            return array;
        }

        // 벡터 데이터를 CSV 파일에 저장하는 함수
        static void SaveDataToCSV(List<double[]> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                // 벡터 데이터 작성
                foreach (double[] vector in data)
                {
                    string line = "young," + string.Join(",", vector);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
