using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class Program
    {
        const int MAXAVR = 5;
        List<HocVien> list;
        public Program()
        {
            list = new List<HocVien>();
        }
        static void Main(string[] args)
        {
            Program p = new Program();

            while (true)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("MENU CHUONG TRINH");
                Console.WriteLine("1. Nhap danh sach Hoc vien");
                Console.WriteLine("2. Xuat danh sach Hoc vien voi Hoc Luc");
                Console.WriteLine("3. Tim kiem theo khoang diem Trung binh");
                Console.WriteLine("4. Tim kiem theo hoc luc");
                Console.WriteLine("5. Tim kiem theo ma so va cap nhat");
                Console.WriteLine("6. Sap xep theo diem");
                Console.WriteLine("7. Xuat 5 hoc vien cao diem nhat");
                Console.WriteLine("8. Tinh diem trung binh cua lop");
                Console.WriteLine("9. Xuat danh sach hoc vien co diem tren trung binh");
                Console.WriteLine("10. Tong hop so hoc vien theo hoc luc");
                Console.WriteLine("----------------------------------------");
                int n;
                Console.WriteLine("Chon gia tri tuong ung: ");
                n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        p.InputList();
                        break;
                    case 2:
                        p.DisplayAll();
                        break;
                    case 3:
                        double From, To;
                        Console.Write("Nhap DTB bat dau: ");
                        From = double.Parse(Console.ReadLine());
                        Console.Write("Nhap DTB ket thuc: ");
                        To = double.Parse(Console.ReadLine());
                        p.DisplayDurationAVR(From, To);
                        break;
                    case 4:
                        string str;
                        Console.Write("Nhap hoc luc: ");
                        str = Console.ReadLine();
                        p.DisplayGPA(str);
                        break;
                    case 5:
                        int Id;
                        Console.Write("Nhap Id can tim kiem: ");
                        Id = Convert.ToInt32(Console.ReadLine());
                        p.UpdateHocVien(Id);
                        break;
                    case 6:
                        p.SortByDESCAVR();
                        p.DisplayTop5();
                        break;
                    case 7:
                        p.DisplayTop5();
                        break;
                    case 8:
                        Console.WriteLine("Diem trung binh của ca lop: {0}",p.SumAVR());
                        break;
                    case 9: p.DisplayMaxAverage(); 
                        break;
                    case 10:
                        p.DisplayClass();
                        break;
                    case 11:
                        p.SortByDESCAVR();
                        p.DisplayAll();
                        break;
                    default:
                        Console.WriteLine("Chon lai?");
                        break;
                }
            }
        }

        List<string> list_HocLuc = new List<string>{ "Excellent", "Very Good", "Good", "Average","Poor", "Fail" };
        void DisplayClass()
        {
            foreach(var i in list_HocLuc)
            {
                Console.WriteLine("Hoc luc: {0}",i);
                foreach(var j in list)
                {
                    if (j.GPA.ToLower() == i.ToLower())
                    {
                        Console.WriteLine($"{j.Id,3}-{j.Name,15}-{j.AVR,10}");
                    }
                }
                Console.WriteLine("/-----/");
            }
        }
        void DisplayMaxAverage()
        {
            foreach (var i in list)
            {
                if (i.AVR >= MAXAVR)
                {
                    Console.WriteLine($"{i.Id,3}-{i.Name,15}-{i.AVR,5}");
                }
            }
        }

        double SumAVR()
        {
            double Sum = 0, count = 0;
            foreach (var i in list)
            {
                Sum += i.AVR;
                count++;
            }
            return Sum / count;
        }

        //Sắp xếp điểm trung bình tăng dần
        void SortByAVR()
        {
            list.Sort(delegate (HocVien hv1, HocVien hv2)
            {
                return hv1.AVR.CompareTo(hv2.AVR);
            });
        }

        //Sắp xếp điểm trung bình giảm dần
        void SortByDESCAVR()
        {
            list.Sort(delegate (HocVien hv1, HocVien hv2)
            {
                return hv2.AVR.CompareTo(hv1.AVR);
            });
        }

        void UpdateHocVien(int Id)
        {
            //Tìm kiếm Học viên
            HocVien hv = FindId(Id);
            if (hv != null)
            {
                Console.WriteLine("Ho va ten: ");
                string Name = Console.ReadLine();
                if (Name != null & Name.Length > 0)
                    hv.Name = Name;
                Console.WriteLine("Diem A: ");
                string a, b, c;
                a = Console.ReadLine();
                if (a != null & a.Length > 0)
                    hv.ScoreA = double.Parse(a);
                Console.WriteLine("Diem B: ");
                b = Console.ReadLine();
                if (b != null & b.Length > 0)
                    hv.ScoreB = double.Parse(b);
                Console.WriteLine("Diem C: ");
                c = Console.ReadLine();
                if (c != null & c.Length > 0)
                    hv.ScoreC = double.Parse(c);
                //Cập nhật lại điểm trung bình
                Average(hv);
                //Cập nhật học lực
                GPA(hv);
            }
            else
            {
                Console.WriteLine("Khong tim thay Hoc vien");
            }
        }

        HocVien FindId(int Id)
        {
            HocVien SearchHV = null;
            if (list != null && list.Count > 0)
            {
                foreach (var i in list)
                {
                    if (Id == i.Id)
                    {
                        SearchHV = i;
                    }
                }
            }
            return SearchHV;
        }

        void DisplayGPA(string str)
        {
            int count = 0;
            foreach (var i in list)
            {
                if (str.ToLower() == i.GPA.ToLower())
                {
                    Console.WriteLine($"{i.Id,3}-{i.Name}");
                    count++;
                }
            }
            if (count > 0)
            {
                Console.WriteLine("Co {0} hoc vien", count);
            }
        }

        void DisplayDurationAVR(double From, double To)
        {
            int count = 0;
            foreach (var i in list)
            {
                double AVR = i.AVR;
                if (AVR >= From && AVR <= To)
                {
                    Console.WriteLine($"{i.Id,3}-{i.Name}");
                    count++;
                }
            }
            if (count > 0)
            {
                Console.WriteLine("Co {0} hoc vien", count);
            }
        }

        void DisplayAllTop5()
        {

            foreach (var i in list)
            {
                Console.WriteLine($"{i.Id,5}-{i.Name,15}-{i.ScoreA,3}-{i.ScoreB,3}-{i.ScoreC,3}-{i.AVR,5}-{i.GPA,8}");
            }
        }

        void DisplayAll()
        {
            foreach (var i in list)
            {
                Console.WriteLine($"{i.Id,5}-{i.Name,15}-{i.ScoreA,3}-{i.ScoreB,3}-{i.ScoreC,3}-{i.AVR,5}-{i.GPA,8}");
            }
        }

        void DisplayTop5()
        {
            int count = 0;
            foreach (var i in list)
            {
                if (count < 5)
                {
                    Console.WriteLine($"{i.Id,5}-{i.Name,15}-{i.ScoreA,3}-{i.ScoreB,3}-{i.ScoreC,3}-{i.AVR,5}-{i.GPA,8}");
                    count++;
                }
            }
        }

        void GPA(HocVien hv)
        {
            double AVR = hv.AVR;
            if (AVR >= 9)
            {
                hv.GPA = "Excellent";
            }
            else if (AVR >= 7.5)
            {
                hv.GPA = "Very Good";
            }
            else if (AVR > 6.5)
            {
                hv.GPA = "Good";
            }
            else if (AVR >= 5)
            {
                hv.GPA = "Average";
            }
            else if (AVR >= 3)
            {
                hv.GPA = "Poor";
            }
            else hv.GPA = "Fail";
        }

        void Average(HocVien hv)
        {
            double AVR = (hv.ScoreA + hv.ScoreB + hv.ScoreC) / 3;
            hv.AVR = AVR;
        }

        void InputList()
        {
            while (true)
            {
                HocVien hv = new HocVien();
                hv.Id = GenerateID();
                Console.WriteLine("Ho va ten: ");
                hv.Name = Console.ReadLine();
                Console.WriteLine("Diem A: ");
                hv.ScoreA = double.Parse(Console.ReadLine());
                Console.WriteLine("Diem B: ");
                hv.ScoreB = double.Parse(Console.ReadLine());
                Console.WriteLine("Diem C: ");
                hv.ScoreC = double.Parse(Console.ReadLine());
                list.Add(hv);
                //Cập nhật điểm trung bình
                Average(hv);
                //Cập nhật học lực
                GPA(hv);
                Console.WriteLine("Ban co nhap tiep khong? (1/0)");
                int Value;
                Value = Convert.ToInt32(Console.ReadLine());
                if (Value == 1)
                {
                    continue;
                }
                else break;
            }
        }

        int GenerateID()
        {
            int max = 1;
            if (list != null & list.Count > 0)
            {
                max = list[0].Id;
                foreach (var i in list)
                {
                    if (max < i.Id) max = i.Id;
                }
                max++;
            }
            return max;
        }
    }
}
