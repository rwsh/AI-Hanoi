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
using System.Windows.Threading;

namespace Hanoi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TRun Run;

        public MainWindow()
        {
            InitializeComponent();

            Run = new TRun(3, g); // Создать основной класс с тремя дисками
        }

        private void cmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        int Step; // количество сделанных ходов
        DispatcherTimer Timer; // таймер


        private void cmRun(object sender, RoutedEventArgs e)
        {
            Run = new TRun(3, g); // Создать основной класс с тремя дисками

            // создаем таймер
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            Step = 0; // обнулить счетчик

            Timer.Start(); // включить таймер
        }

        // обработка таймера
        void onTick(object sender, EventArgs e)
        {
            Step++; // увеличить счетчик

            bool Optimize = false; 
            bool Select = false;

            // проверить флаг оптимизации
            if (checkBox1.IsChecked == true)
            {
                Optimize = true;
            }

            // проверить флаг отбора ходов
            if (checkBox2.IsChecked == true)
            {
                Select = true;
            }

            // делаем ход
            if (Run.Run(Optimize, Select))
            {
                // в случае выигрыша останавливаем таймер и выводим сообщение
                Timer.Stop();
                MessageBox.Show("Ok! " + Step.ToString());
            }
        }

    }
}
