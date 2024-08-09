using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    internal class TSP
    {
        static int soDiem = 5;

        static double[,] maTranKhoangCach;

        // Bảng memoization để lưu trữ chi phí tối thiểu
        static double[,] memo;

        static void Main()
        {
            double[,] toaDo = new double[,]
            {
            { 0, 0 },   // Điểm 1
            { 1, 2 },   // Điểm 2
            { 2, 4 },   // Điểm 3
            { 3, 1 },   // Điểm 4
            { 4, 3 }    // Điểm 5
            };

            Console.WriteLine("\nDanh sach cac diem va toa do");
            for (int i = 0; i < soDiem; i++)
            {
                Console.WriteLine($"Diem {i + 1}: ({toaDo[i, 0]}, {toaDo[i, 1]})");
            }

            // Tạo ma trận khoảng cách giữa các điểm
            maTranKhoangCach = new double[soDiem, soDiem];
            for (int i = 0; i < soDiem; i++)
            {
                for (int j = 0; j < soDiem; j++)
                {
                    // Tính khoảng cách giữa điểm i và điểm j
                    maTranKhoangCach[i, j] = tinhKhoangCach(toaDo[i, 0], toaDo[i, 1], toaDo[j, 0], toaDo[j, 1]);
                }
            }

            Console.WriteLine("\n\t Ma tran khoang cach");
            for (int i = 0; i < soDiem; i++)
            {
                for (int j = 0; j < soDiem; j++)
                {
                    Console.Write($"{maTranKhoangCach[i, j]:F2}\t");
                }
                Console.WriteLine();
            }

            // Khởi tạo bảng memoization với giá trị -1
            memo = new double[soDiem, 1 << soDiem];
            for (int i = 0; i < soDiem; i++)
            {
                for (int j = 0; j < (1 << soDiem); j++)
                {
                    memo[i, j] = -1;
                }
            }

            double chiPhiToiThieu = tsp(0, 1);
            Console.WriteLine($"\nChi phi toi thieu la: {chiPhiToiThieu:F2}");

            Console.ReadLine();
        }

        // Hàm tính khoảng cách giữa hai điểm (x1, y1) và (x2, y2)
        static double tinhKhoangCach(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        // Hàm tính toán chi phí tối thiểu bằng kỹ thuật memoization
        static double tsp(int viTri, int daThamQuan)
        {
            // Nếu tất cả các điểm đã được tham quan
            if (daThamQuan == (1 << soDiem) - 1)
            {
                return maTranKhoangCach[viTri, 0]; // Quay lại điểm xuất phát
            }

            // Nếu giá trị đã được tính toán trước đó
            if (memo[viTri, daThamQuan] != -1)
            {
                return memo[viTri, daThamQuan];
            }

            double chiPhiThapNhat = double.MaxValue;

            // Thử tất cả các điểm chưa được tham quan
            for (int i = 0; i < soDiem; i++)
            {
                if ((daThamQuan & (1 << i)) == 0)
                {
                    double chiPhiMoi = maTranKhoangCach[viTri, i] + tsp(i, daThamQuan | (1 << i));
                    chiPhiThapNhat = Math.Min(chiPhiThapNhat, chiPhiMoi);
                }
            }

            // Lưu kết quả vào bảng memoization
            return memo[viTri, daThamQuan] = chiPhiThapNhat;
        }
    }
}
