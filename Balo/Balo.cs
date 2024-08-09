using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balo
{
    internal class Balo
    {
        static int[] weights, values;
        static int[,] dp;
        static int n, W;

        static void Main()
        {
            // Giá trị cho sẵn
            n = 4;
            W = 7;
            values = new int[] { 1, 4, 5, 7 }; // Giá trị của các vật phẩm
            weights = new int[] { 1, 3, 4, 5 }; // Trọng lượng của các vật phẩm


            dp = new int[n + 1, W + 1];
            Console.WriteLine($"Trong luong gioi han cua balo: {W}");
            Console.WriteLine("\n\tThong tin cac vat pham");
            PrintTable();

            int maxValue = balo(W, weights, values, n);

            Console.WriteLine("\n\t     Bang phuong an ");
            PrintDPTable();

            Console.WriteLine("\nCac vat pham duoc chon:");
            int totalWeight = Truyvet(); // Tính tổng trọng lượng của các vật phẩm được chọn

            Console.WriteLine("\nGia tri lon nhat co the bo vao balo: " + maxValue);
            Console.WriteLine("Tong trong luong cua cac vat pham duoc chon: " + totalWeight);

            Console.ReadLine();
        }

        static int Max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        static int balo(int W, int[] weights, int[] values, int n)
        {
            for (int i = 0; i <= n; i++)
            {
                for (int w = 0; w <= W; w++)
                {
                    if (i == 0 || w == 0)
                    {
                        dp[i, w] = 0;
                    }
                    else if (weights[i - 1] <= w)
                    {
                        dp[i, w] = Max(values[i - 1] + dp[i - 1, w - weights[i - 1]], dp[i - 1, w]);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            return dp[n, W];
        }

        static int Truyvet()
        {
            int i = n;
            int w = W;
            int totalWeight = 0;

            while (i > 0 && w > 0)
            {
                if (dp[i, w] != dp[i - 1, w])
                {
                    Console.WriteLine($"Vat pham {i} co gia tri = {values[i - 1]} va trong luong = {weights[i - 1]}");
                    totalWeight += weights[i - 1]; // Cộng dồn trọng lượng của các vật phẩm được chọn
                    w -= weights[i - 1];
                }
                i--;
            }

            return totalWeight; // Trả về tổng trọng lượng của các vật phẩm được chọn
        }

        // Hàm hiển thị bảng thông tin đồ vật
        static void PrintTable()
        {
            Console.WriteLine("  | Do vat | Trong luong | Gia tri |");
            Console.WriteLine("  +--------+-------------+---------+");

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"  |  {i + 1,2}    | {weights[i],6}      |{values[i],6}   |");
            }
        }

        // Hàm hiển thị bảng phương án dp
        static void PrintDPTable()
        {
            Console.Write("   |  ");
            for (int j = 0; j <= W; j++)
            {
                Console.Write($"{j,3} ");
            }
            Console.WriteLine();

            for (int i = 0; i <= n; i++)
            {
                Console.Write($"{i,2} |  ");
                for (int j = 0; j <= W; j++)
                {
                    Console.Write($"{dp[i, j],3} ");
                }
                Console.WriteLine();
            }
        }
    }
}