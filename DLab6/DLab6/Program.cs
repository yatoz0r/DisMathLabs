

namespace DLab6
{   
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\DLab6\g21.txt";         
            // Открытие файла для чтения
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                string matrix = "";
                int k = 0;
                Console.WriteLine("Исходная матрица:\n");
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    k++;
                    matrix += s + " ";
                }
                int rows = k;
                int columns = rows;
                int count = 0;
                int[,] arr = new int[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        arr[i, j] = (int)Char.GetNumericValue(matrix[count]);
                        count += 2;
                    }
                }
                var prim = new PrimKruskal();
                prim.PrimMST(arr);
                prim.PrintMST(arr);
            }
        }
    }

}