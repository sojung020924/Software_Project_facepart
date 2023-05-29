using System.IO;
using System.Security.AccessControl;

namespace Software_Project_facepart
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileNames1 = Directory.GetFiles(@"젊은 사람 이미지");

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
            string[] fileNames2 = Directory.GetFiles(@"늙은 사람 이미지");

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
        }
    }
}