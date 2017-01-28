using System;
using System.Collections.Generic;
using System.Collections;
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
    // Основной класс для работы с Ханойскими башнями
    class THanoi
    {
        public int N; // количество дисков
        Canvas g; // холст для рисования

        public TStalk[] Stalk; // массив штырей

        public THanoi(int N, Canvas g)
        {
            this.N = N; // сохранить количество
            this.g = g; // созранить холст

            Stalk = new TStalk[3]; // создать три штыря

            Stalk[0] = new TStalk(N);
            Stalk[1] = new TStalk(N);
            Stalk[2] = new TStalk(N);

            Stalk[0].Fill(); // нулевой штырь заполнить дисками

            //Draw();
        }

        // создать копию
        public THanoi Copy()
        {
            THanoi Res = new THanoi(N, g);

            Res.Stalk[0] = Stalk[0].Copy();
            Res.Stalk[1] = Stalk[1].Copy();
            Res.Stalk[2] = Stalk[2].Copy();

            return Res;
        }

        // проверить на равенство
        public bool IsEqual(THanoi Hanoi)
        {
            for (int i = 0; i < 3; i++)
            {
                if(!Hanoi.Stalk[i].IsEqual(Stalk[i]))
                {
                    return false;
                }
            }
            
            return true;
        }

        // проверить завершенность игры
        public bool IsWin()
        {
            if ((Stalk[2].Disks[0] == 3) && (Stalk[2].Disks[1] == 2) && (Stalk[2].Disks[2] == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // нарисовать штыри с дисками
        public void Draw()
        {
            g.Children.Clear();

            Stalk[0].Draw(g, 0);
            Stalk[1].Draw(g, 1);
            Stalk[2].Draw(g, 2);
        }

        // переместить диск со штыря i на штырь j
        // если ход недоступен, то возврящаем false
        public bool Move(int i, int j)
        {
            if (!Can(i, j))
            {
                return false;
            }

            Stalk[j].AddTop(Stalk[i].RemoveTop());

            //Draw();

            return true;
        }

        // проверяем возможность хода со штыря i на штырь j
        // если не возможно, то false
        public bool Can(int i, int j)
        {
            int Di = Stalk[i].GetTop();

            if (Di < 0)
            {
                return false;
            }

            int Dj = Stalk[j].GetTop();

            if (Dj < 0)
            {
                return true;
            }

            if (Di < Dj)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // найти допустимые ходы
        public TRuns GetMove()
        {
            TRuns Runs = new TRuns();

            int[] res = new int[2];

            ArrayList arr = new ArrayList();
            Random rnd = new Random();

            res = new int[2]; res[0] = 0; res[1] = 1; if (Can(res[0], res[1])) { arr.Add(res); }
            res = new int[2]; res[0] = 0; res[1] = 2; if (Can(res[0], res[1])) { arr.Add(res); }
            res = new int[2]; res[0] = 1; res[1] = 0; if (Can(res[0], res[1])) { arr.Add(res); }
            res = new int[2]; res[0] = 1; res[1] = 2; if (Can(res[0], res[1])) { arr.Add(res); }
            res = new int[2]; res[0] = 2; res[1] = 0; if (Can(res[0], res[1])) { arr.Add(res); }
            res = new int[2]; res[0] = 2; res[1] = 1; if (Can(res[0], res[1])) { arr.Add(res); }

            while(arr.Count > 0)
            {
                int k = rnd.Next(arr.Count);
                Runs.Add((int[])arr[k]);

                arr.RemoveAt(k);
            }



            return Runs;
        }
    }

    // класс для хранения ходов
    class TRuns
    {
        public ArrayList arr;

        public TRuns()
        {
            arr = new ArrayList();
        }

        public void Add(int[] run)
        {
            arr.Add(run);
        }

        public int Count
        {
            get { return arr.Count; }
        }

        public int[] this[int ind]
        {
            get
            {
                return (int[])(arr[ind]);
            }
        }

      
    }
}
