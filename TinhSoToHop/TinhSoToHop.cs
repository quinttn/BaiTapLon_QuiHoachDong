using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinhSoToHop
{
    internal class TinhSoToHop
    {
        public static int TinhToHopDPCaiTien(int n, int k)
        {
            int[] V = new int[n + 1];
            int p1, p2;

            // Khởi tạo giá trị cơ sở
            V[0] = 1;  // C(n, 0) = 1 với mọi n
            V[1] = 1;  // C(1, 1) = 1

            // Sử dụng vòng lặp để tính giá trị tổ hợp
            for (int i = 2; i <= n; i++)
            {
                // Lưu trữ giá trị V[0] vào p1
                p1 = V[0];

                for (int j = 1; j < i; j++)
                {
                    // Lưu trữ giá trị V[j] vào p2
                    p2 = V[j];

                    // Tính giá trị mới cho V[j] dựa trên giá trị trước đó
                    V[j] = p1 + p2;

                    // Cập nhật p1 cho vòng lặp tiếp theo
                    p1 = p2;
                }

                // Cài đặt giá trị cuối cùng của hàng i
                V[i] = 1;
            }

            // Trả về giá trị của C(n, k)
            return V[k];
        }

        static void Main()
        {
            Console.Write("Nhap gia tri cua n: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Nhap gia tri cua k: ");
            int k = int.Parse(Console.ReadLine());

            if (k > n || k < 0)
            {
                Console.WriteLine("Gia tri cua k phai nam trong khoang tu 0 den n");
                return;
            }

            int result = TinhToHopDPCaiTien(n, k);
            Console.WriteLine($"C({n}, {k}) = {result}");

            Console.ReadLine();
        }
    }
}