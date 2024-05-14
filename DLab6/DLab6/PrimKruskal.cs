using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLab6
{
    public class Edge
    {
        public int Source { get; set; }
        public int Destination { get; set; }
        public int Weight { get; set; }
    }
    public class PrimKruskal
    {
        #region Prim
        private static int size = 10;
        static int MinimumKey(int[] key, bool[] mstSet)
        {
            int min = int.MaxValue, minIndex = 0;

            for (int v = 0; v < size; v++)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        static void PrintMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("\nМетод Прима");
            Console.WriteLine("Ребро \tВес");
            int sum = 0;
            for (int i = 1; i < size; i++)
            {
                Console.WriteLine((parent[i] + 1) + " - " + (i + 1) + "\t " + graph[i, parent[i]]);
                sum += graph[i, parent[i]];
            }
            Console.WriteLine("Суммарный вес: " + sum);
        }

        public void PrimMST(int[,] graph)
        {
            int[] parent = new int[size];
            int[] key = new int[size];
            bool[] mstSet = new bool[size];

            for (int i = 0; i < size; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < size - 1; count++)
            {
                int u = MinimumKey(key, mstSet);
                mstSet[u] = true;

                for (int v = 0; v < size; v++)
                {
                    if (graph[u, v] != 0 && mstSet[v] == false && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }
            PrintMST(parent, graph);
        }
        #endregion

        public List<Edge> FindMinimumSpanningTree(int[,] graph)
        {
            List<Edge> result = new List<Edge>();
            List<Edge> edges = new List<Edge>();
            // Заполняем список рёбер
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = i; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                    {
                        edges.Add(new Edge { Source = i, Destination = j, Weight = graph[i, j] });
                    }
                }
            }
            // Сортируем рёбра по весу
            edges.Sort((e1, e2) => e1.Weight - e2.Weight);
            int[] parent = new int[graph.GetLength(0)];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                parent[i] = i;
            }
            foreach (Edge edge in edges)
            {
                int root1 = Find(parent, edge.Source);
                int root2 = Find(parent, edge.Destination);

                if (root1 != root2)
                {
                    result.Add(edge);
                    parent[root1] = root2;
                }
            }
            return result;
        }

        // Метод для поиска корня вершины
        private int Find(int[] parent, int vertex)
        {
            while (vertex != parent[vertex])
            {
                vertex = parent[vertex];
            }
            return vertex;
        }
        // Метод вывода ребер и веса
        public void PrintMST(int[,] arr)
        {
            List<Edge> result = FindMinimumSpanningTree(arr);
            Console.WriteLine("\nМетод Краскала");
            Console.WriteLine("Ребро \tВес");
            int sum = 0;
            foreach (Edge edge in result)
            {
                Console.WriteLine($"{edge.Source} - {edge.Destination} \t {edge.Weight}");
                sum += edge.Weight;
            }
            Console.WriteLine("Суммарный вес: " + sum);
        }
    }
}
