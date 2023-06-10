using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace image_normalization
{
    class Program
    {
        static void Main(string[] args)
        {
            //폴더 경로 (늙은 사람)
            string folderPath = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\늙은 사람 이미지";

            // 폴더 내의 모든 이미지 파일
            string[] imageFiles = Directory.GetFiles(folderPath, "*.jpg");

            foreach (string imageFile in imageFiles)
            {

                //이미지를 비트맵으로(RGB)
                Bitmap image = ResizeImage(imageFile, 256, 256);

                // 정규화
                Bitmap normalizedImage = NormalizeImage(image);

                // 정규화된 이미지 저장
                string fileName = Path.GetFileNameWithoutExtension(imageFile);
                string outputFilePath = Path.Combine(folderPath, $"{fileName}_normalized.jpg"); //구분이 되도록 뒤에 normalized 붙이기
                normalizedImage.Save(outputFilePath);
            }

            //폴더 경로 (젊은 사람)
            folderPath = "C:\\Users\\sojun\\source\\repos\\Software_Project_facepart\\Software_Project_facepart\\bin\\Debug\\net6.0\\젊은 사람 이미지";

            // 폴더 내의 모든 이미지 파일
            imageFiles = Directory.GetFiles(folderPath, "*.jpg");

            foreach (string imageFile in imageFiles)
            {
                //이미지를 비트맵으로(RGB)
                Bitmap image = ResizeImage(imageFile,256,256);

                // 정규화
                Bitmap normalizedImage = NormalizeImage(image);

                // 정규화된 이미지 저장
                string fileName = Path.GetFileNameWithoutExtension(imageFile);
                string outputFilePath = Path.Combine(folderPath, $"{fileName}_normalized.jpg"); //구분이 되도록 뒤에 normalized 붙이기
                normalizedImage.Save(outputFilePath);
            }

            Console.WriteLine("처리 완료");
        }
        static Bitmap ResizeImage(string imagePath, int width, int height)
        {
            using (var image = new Bitmap(imagePath))
            {
                var resizedImage = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(image, 0, 0, width, height);
                }
                return resizedImage;
            }
        }
        static Bitmap NormalizeImage(Bitmap image)
        {
            //초기화 

            int minR = 255, maxR = 0; //Red
            int minG = 255, maxG = 0;  //Green
            int minB = 255, maxB = 0; //Blue

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    //반복문을 돌면서 최소값이랑 최대값을 찾을 수 있게됨
                    Color pixel = image.GetPixel(x, y);

                    minR = Math.Min(minR, pixel.R);
                    maxR = Math.Max(maxR, pixel.R);

                    minG = Math.Min(minG, pixel.G);
                    maxG = Math.Max(maxG, pixel.G);

                    minB = Math.Min(minB, pixel.B);
                    maxB = Math.Max(maxB, pixel.B);
                }
            }

            //새로운 이미지 생성 (정규화한것이 들어감)
            Bitmap normalizedImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++) //한 픽셀씩
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y); //이미지에서 픽셀 얻어옴
                    
                    //히스토그램 정규화 공식 적용
                    byte newR = (byte)(((pixel.R - minR) / (double)(maxR - minR)) * 255);
                    byte newG = (byte)(((pixel.G - minG) / (double)(maxG - minG)) * 255);
                    byte newB = (byte)(((pixel.B - minB) / (double)(maxB - minB)) * 255);


                    normalizedImage.SetPixel(x, y, Color.FromArgb(newR, newG, newB));
                }
            }

            return normalizedImage;
        }
    }
}