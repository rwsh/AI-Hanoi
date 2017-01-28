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
    class TRun
    {
        public THanoi Hanoi; // текущее состояние

        public THanois Prev; // класс для хранения предыдущих состояний

        Random rnd; // генератор случайных чисел

        // конструктор класса
        public TRun(int N, Canvas g)
        {
            Hanoi = new THanoi(N, g); // создать класс для игры
            Hanoi.Draw(); // нарисовать начальную ситуацию

            Prev = new THanois(); // создать массив для пре

            rnd = new Random(); // инициализировать генератор слуайных чисел
        }

        // сделать ход, если этот приводит к решению, то вернуть true
        public bool Run(bool Optimize, bool Target)
        {
            TRuns Runs = Hanoi.GetMove(); // вернуть допустимые ходы

            // если задан режим отбора целевых ходов
            if (Target)
            {
                Select(Runs); // выбрать целевые ходы
            }

            // если задан режим оптимизации ходов
            if(Optimize)
            {
                THanoi Test; // создаем класс для пробного состояния

                for (int i = 0; i < Runs.Count; i++)
                {
                    Test = Hanoi.Copy(); // создать копию

                    Test.Move((Runs[i])[0], (Runs[i])[1]); // делаем пробный ход

                    // если получаемое состояние не было ранее
                    if (!IsPrev(Test))
                    {
                        Hanoi = Test.Copy(); // сохраняем тестовое состояние
                        Hanoi.Draw(); // рисуем новые состояние

                        Prev.Add(Hanoi.Copy()); // сохраняем получаемое состояние

                        // проверяем нет ли выигрыша
                        if (Hanoi.IsWin())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            // если не было хода, то делаем случайный ход

            int k = rnd.Next(Runs.Count); // выбираем случайный ход

            Hanoi.Move((Runs[k])[0], (Runs[k])[1]); // сделать ход
            Hanoi.Draw(); // рисуем новую ситуацию

            // проверить нет ли выигрыша
            if (Hanoi.IsWin())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // выбрать целевые ходы
        void Select(TRuns Runs)
        {
            int NeedDisk = Hanoi.Stalk[2].ForNeed(); // проверить нет ли необходимого диска

            // если есть необходимый диск, то ищем его
            if (NeedDisk > -1)
            {
                int[] forrun = new int[2]; // создем новй ход
                forrun[0] = -1; // показатель, что хода нет
                forrun[1] = 2;

                // проверяем нет ли необходимого диска на 0 штыре
                if (Hanoi.Stalk[0].GetTop() == NeedDisk)
                {
                    forrun[0] = 0;
                }

                // проверяем нет ли необходимого диска на 1 штыре
                if (Hanoi.Stalk[1].GetTop() == NeedDisk)
                {
                    forrun[0] = 1;
                }

                // если был найден нужный диск, то делаем ход
                if (forrun[0] != -1)
                {
                    Runs.arr.Clear(); // очищяем  возможные ходы
                    Runs.Add(forrun); // добавляем только один необходимый ход

                    return;
                }

            }

            // проверяем, нет ли среди возможных ходов, которые будут шагом назад
            for (int i = 0; i < Runs.Count; i++)
            {
                int[] run = (Runs[i]);
                if (run[0] == 2)
                {
                    if ((Hanoi.Stalk[2].Disks[0] == 3) && (Hanoi.Stalk[2].Disks[1] != 1))
                    {
                        Runs.arr.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        // проверить, есть ли данная ситуация в сохраненных
        bool IsPrev(THanoi R)
        {
            for (int i = 0; i < Prev.Count; i++)
            {
                if (Prev[i].IsEqual(R))
                {
                    return true;
                }
            }

            return false;
        }
    }

    class THanois
    {
        ArrayList arr; // коллекция для хранения предыдущих ходов

        public THanois()
        {
            arr = new ArrayList(); // инициализировать коллекцию
        }

        // вернуть количество элементов
        public int Count
        {
            get
            {
                return arr.Count;
            }
        }

        // добавить новый элемент
        public void Add(THanoi Hanoi)
        {
            arr.Add(Hanoi);
        }

        // доступ к элементу по индексу
        public THanoi this[int ind]
        {
            get
            {
                return (THanoi)arr[ind];
            }
        }
    }
}
