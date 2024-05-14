using System.Drawing;
using System.IO.Enumeration;

class Program
{

    static int[,] ReadFile(string filename)
    {
        int[,] matrix = new int[10, 10];
        string[] lines = File.ReadAllLines(filename);

        for (int i = 0; i < 10; i++)
        {
            string[] values = lines[i].Split(' ');
            for (int j = 0; j < 10; j++)
            {
                matrix[i, j] = int.Parse(values[j]);
            }
        }

        return matrix;
    }
    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "  ");
            }
            Console.WriteLine();
        }
    }
    static void Main(string[] args)
    {
        int n = 10;
        int[,] adjacencyMatrix = new int[n, n];
        string begingFile = "g3";
        for (int i = 1; i <= 3; i++)
        {
            var filename = begingFile + i + ".txt"; 
            Console.WriteLine(filename);
            adjacencyMatrix = ReadFile(filename);
            PrintMatrix(adjacencyMatrix);
            //метод для раскраски графа
            List<int> coloring = GreedyColoring(adjacencyMatrix);
            Console.WriteLine("Раскраска графа:");
            int j = 1;
            foreach(var color in coloring)
            {
                Console.WriteLine($"Вершина {j++}: {color + 1}");
            }
            Console.WriteLine();
        }
    }

    //код для раскраски графа
    static List<int> GreedyColoring(int[,] adjacencyMatrix)
    {
        List<int> colors = new List<int>();
        int n = adjacencyMatrix.GetLength(0);

        // Присваиваем всем вершинам начальный цвет -1
        for (int i = 0; i < n; i++)
        {
            colors.Add(-1);
        }

        // Жадно раскрашиваем каждую вершину
        for (int v = 0; v < n; v++)
        {
            // Массив для хранения цветов, используемых смежными вершинами
            bool[] available = new bool[n];
            Array.Fill(available, true);

            // Проверяем цвета уже раскрашенных смежных вершин и помечаем их как недоступные
            for (int i = 0; i < n; i++)
            {
                if (adjacencyMatrix[v, i] == 1 && colors[i] != -1)
                {
                    available[colors[i]] = false;
                }
            }

            // Находим доступный цвет для текущей вершины
            int cr;
            for (cr = 0; cr < n; cr++)
            {
                if (available[cr])
                {
                    break;
                }
            }

            // Присваиваем цвет текущей вершине
            colors[v] = cr;
        }

        return colors;
    }
}