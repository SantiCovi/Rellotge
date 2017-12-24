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
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Rellotge
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string path = "C:/Users/SantiCovi/source/repos/Rellotge/SavedAlarm.bin";
        public Alarma alarm;

        public static string Path { get => path; set => path = value; }

        public MainWindow()
        {
            InitializeComponent();
            lbTime.Content = DateTime.Now.ToLongTimeString();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds( 1 );
            timer.Tick += timer_Tick;
            timer.Start();

            string[] horas = new string[24];

            for (int i = 0; i < 24; i++)
            {
                if ( i < 10)
                {
                    horas[i] = "0" + i.ToString();
                }
                else
                {
                    horas[i] = i.ToString();               
                }
            }
            cbHoras.ItemsSource = horas;

            string[] minutos = new string[60];
            for ( int i = 0; i < 60; i++)
            {
                if ( i < 10 )
                {
                    minutos[i] = "0" + i.ToString();
                }
                else
                {
                    minutos[i] = i.ToString();
                }
            }
            cbMinutos.ItemsSource = minutos;

            alarm = new Alarma();



        }

        private void timer_Tick(object sender, EventArgs e)
        {           
            string date = DateTime.Now.ToLongTimeString();
            string fechaFinal = string.Format("{0:HH:mm:ss}", date);

            if ( !string.IsNullOrEmpty( fechaFinal ) )
            {
                lbTime.Content = fechaFinal;
            }


            if ( alarm.alarmaActiva )
            {
                if ( string.Compare( fechaFinal, alarm.hora + ":" + alarm.minuto + ":00" ) == 0 )
                {
                    winAlarma();
                }
            }
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sobre_programa_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Santiago Covi";
            string caption = "Autor";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage img = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, img);
        }

        private void rbActivar_Checked(object sender, RoutedEventArgs e)
        {
            alarm.alarmaActiva = true;
            alarm.hora = cbHoras.SelectedItem.ToString();
            alarm.minuto = cbMinutos.SelectedItem.ToString();
        }

        private void rbDesactivar_Checked(object sender, RoutedEventArgs e)
        {
            alarm.alarmaActiva = false;
        }

        public void winAlarma()
        {
            string messageBoxText = "HO HO HO FELIZ NAVIDAD";
            string caption = "ALARMA";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage img = MessageBoxImage.Information;
            MessageBox.Show(messageBoxText, caption, button, img);
            SystemSounds.Beep.Play();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Stream TestFileStream = File.Create( path );
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(TestFileStream, alarm);
            TestFileStream.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ( File.Exists( path ) )
            {
                Stream TestFileStream = File.OpenRead( path );
                BinaryFormatter deserializer = new BinaryFormatter();
                alarm = (Alarma)deserializer.Deserialize( TestFileStream );
                TestFileStream.Close();

                Console.WriteLine(alarm);
                cbHoras.SelectedIndex = int.Parse ( alarm.hora );
                Console.WriteLine(alarm.minuto);
                cbMinutos.SelectedIndex = alarm.minuto == null ? 0 : int.Parse( alarm.minuto );
                if ( alarm.alarmaActiva )
                {
                    rbActivar.IsChecked = true;
                }else
                {
                    rbDesactivar.IsChecked = true;
                }

            }else
            {
                cbHoras.SelectedIndex = 0;
                cbMinutos.SelectedIndex = 0;
            }

        }

        private void cbHores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( !string.IsNullOrEmpty( cbHoras.Text ) )
            {
                alarm.hora = cbHoras.SelectedItem.ToString();
            }
        }

        private void cbMinuts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( !string.IsNullOrEmpty( cbMinutos.Text ) )
            {
                alarm.minuto = cbMinutos.SelectedItem.ToString();
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            CrearPais nuevoPais = new CrearPais();

         /*   if (nuevoPais.ShowDialog() == true) {
                cbHoras.Add(Agregar.Answer);
                ComboBoxItem nuevo = new ComboBoxItem();
                nuevo.Content = Agregar.Answer.getNombre();
                cbHoras.Items.Add(nuevo);
            } */
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
