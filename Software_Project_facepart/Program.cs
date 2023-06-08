using System.IO;
using System.Security.AccessControl;

namespace Software_Project_facepart
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileNames1 = Directory.GetFiles(@"젊은 사람 이미지(가공)");

            if (File.Exists("young.csv")){
                File.Delete("young.csv");
            }
            //해당 폴더에 있는 파일이름을 출력
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"young.csv"))
            {
                foreach (string filePath in fileNames1)
                {
                    file.WriteLine("{0},{1}", Path.GetFileName(filePath), "young");
                }

            }
            string[] fileNames2 = Directory.GetFiles(@"늙은 사람 이미지(가공)");

            if (File.Exists("old.csv"))
            {
                File.Delete("old.csv");
            }
            //해당 폴더에 있는 파일이름을 출력
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"old.csv"))
            {
                foreach (string filePath in fileNames2)
                {
                    file.WriteLine("{0},{1}", Path.GetFileName(filePath), "old");
                }

            }

            if (File.Exists("people.csv"))
            {
                File.Delete("people.csv");
            }
            string filePath1 = "young.csv"; // 첫 번째 CSV 파일 경로
            string filePath2 = "old.csv"; // 두 번째 CSV 파일 경로

            using (StreamReader reader1 = new StreamReader(filePath1))
            using (StreamReader reader2 = new StreamReader(filePath2))
            using (StreamWriter writer = new StreamWriter("people.csv"))
            {
                writer.WriteLine("{0},{1}","file_name","age");
                string content1 = reader1.ReadToEnd(); // 첫 번째 파일 내용 전체를 읽음
                string content2 = reader2.ReadToEnd(); // 두 번째 파일 내용 전체를 읽음

                // 첫 번째 파일 내용 쓰기
                writer.Write(content1);

                // 두 번째 파일 내용 쓰기
                writer.Write(content2); 
            }
        }
    }
}