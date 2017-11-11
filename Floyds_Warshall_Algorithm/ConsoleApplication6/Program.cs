

    using System;  
    using System.Collections.Generic;  
    using System.Linq;  
    using System.Text;  
      
    namespace Floyds_Warshall_Algorithm {  
        class Program {  
      
            public  
            const int INF = 10000;  
      
            private static void disp(int[, ] distance, int verticesCount) {  
                Console.WriteLine("Distance Matrix for Shortest Distance between the nodes");  
                Console.Write("\n");  
      
                for (int i = 0; i < verticesCount; ++i) {  
                    for (int j = 0; j < verticesCount; ++j) {  
                        // IF THE DISTANCE TO THE NODE IS NOT DIRECTED THAN THE COST IN iNIFINITY  
      
                        if (distance[i, j] == INF)  
                            Console.Write("INF".PadLeft(7));  
                        else  
                            Console.Write(distance[i, j].ToString().PadLeft(7));  
                    }  
      
                    Console.WriteLine();  
                }  
            }  
      
            public static void FloydWarshall(int[, ] graph, int verticesCount) {  
                int[, ] distance = new int[verticesCount, verticesCount];  
      
                for (int i = 0; i < verticesCount; ++i)  
                    for (int j = 0; j < verticesCount; ++j)  
                        distance[i, j] = graph[i, j];  
      
                for (int k = 0; k < verticesCount; k++) {  
                    for (int i = 0; i < verticesCount; i++) {  
                        for (int j = 0; j < verticesCount; j++) {  
                            if (distance[i, k] + distance[k, j] < distance[i, j])  
                                distance[i, j] = distance[i, k] + distance[k, j];  
                        }  
                    }  
                }  
      
                disp(distance, verticesCount);  
                Console.ReadKey();  
      
            }  
      
            static void Main(string[] args) {  
                //inital Graph Given = D^0  
      
                int[, ] graph = {  
      
                    {  
                        0,  
                        8,  
                        5  
                    },  
                    {  
                        2,  
                        0,  
                        INF  
                    },  
                    {  
                        INF,  
                        1,  
                        0  
                    },  
      
                };  
      
                FloydWarshall(graph, 3);  
      
            }  
      
        }  
      
    }

