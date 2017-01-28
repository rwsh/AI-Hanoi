using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Hanoi
{
    // класс для работы со штырями
    class TStalk
    {
        int N; // количество дисков

        public int[] Disks; // массив для хранения дисков

        public TStalk(int N)
        {
            this.N = N; // сохранить количество дисков

            Disks = new int[N]; // создать массив для дисков

            for (int i = 0; i < N; i++)
            {
                Disks[i] = 0; // штырь - пустой
            }
        }

        // создать копию
        public TStalk Copy()
        {
            TStalk Res = new TStalk(N);

            for (int i = 0; i < N; i++)
            {
                Res.Disks[i] = Disks[i];
            }

            return Res;
        }

        // проверить на равенство
        public bool IsEqual(TStalk Stalk)
        {
            for (int i = 0; i < N; i++)
            {
                if (Stalk.Disks[i] != Disks[i])
                {
                    return false;
                }
            }

            return true;
        }

        // вернуть ширину верхнего диска, если штырь пустой, то -1
        public int GetTop()
        {
            for (int i = N - 1; i >= 0; i--)
            {
                if(Disks[i] != 0)
                {
                    return Disks[i];
                }
            }

            return -1;
        }

        // вернуть нужный диск
        public int ForNeed()
        {
            int res = GetTop();

            if (res == -1)
            {
                return -1;
            }

            if (Disks[res - 1] == res - 1)
            {
                return res - 1;
            }
            else
            {
                return -1;
            }

            
        }

        // удалить верхний диск и вернуть ширину диска
        public int RemoveTop()
        {
            for (int i = N - 1; i >= 0; i--)
            {
                if (Disks[i] != 0)
                {
                    int res = Disks[i];
                    Disks[i] = 0;
                    return res;
                }
            }

            return -1;
        }

        // добавить диск на верх
        public void AddTop(int k)
        {
            for (int i = 0; i < N; i++)
            {
                if (Disks[i] == 0)
                {
                    Disks[i] = k;
                    return;
                }
            }
        }

        // заполнить штырь в начальный момент
        public void Fill()
        {
            for (int i = 0; i < N; i++)
            {
                Disks[i] = N - i; // каждый диск имеет свою ширину от 1 до N
            }
        }

        // нарисовать штырь на холсте g, с номером штыря n
        public void Draw(Canvas g, int n)
        {
            double W = g.Width;
            double H = g.Height;

            double X = (W / 3) * n + W / 6; // вычислить положение штыря

            // рисуем штырь
            Rectangle R = new Rectangle();
            R.Height = H * 0.9;
            R.Width = 10;
            R.Margin = new Thickness(X - 5, H - R.Height, 0, 0);
            R.Fill = Brushes.Black;
            g.Children.Add(R);

            // рисуем диски
            for (int k = 0; k < N; k++)
            {
                if (Disks[k] == 0)
                {
                    break;
                }

                R = new Rectangle();
                R.Height = H * 0.1;
                R.Width = (W / 3) * 0.8 * Disks[k];
                R.Width /= N;
                R.Margin = new Thickness(X - R.Width / 2, H - R.Height * (k + 1), 0, 0);

                R.Fill = Brushes.Gray;

                if (Disks[k] == 3)
                {
                    R.Fill = Brushes.Red;
                }
                if (Disks[k] == 2)
                {
                    R.Fill = Brushes.Blue;
                }
                if (Disks[k] == 1)
                {
                    R.Fill = Brushes.Green;
                }

                g.Children.Add(R);

            }
        }
    }
}
