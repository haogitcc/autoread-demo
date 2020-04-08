using ReceiveAutonomousReading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ThingMagic;

namespace ReceiveAutonomousReadingDemodotNet
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ReceiveTagData rt = null;
        private List<string> portNames = null;
        TagDatabase tagdb = null;

        public MainWindow()
        {
            InitializeComponent();

            RefreshSerial();

            tagdb = new TagDatabase();
        }

        private void RefreshSerial()
        {
            rt = new ReceiveTagData();
            portNames = rt.GetComPortNames();

            serial_combobox.ItemsSource = portNames;
            if(portNames.Count>0)
                serial_combobox.SelectedIndex = 0;
        }

        private void connect_button_Click(object sender, RoutedEventArgs e)
        {
            if(connect_button.Content.Equals("Connect"))
            {
                //Connect to Serial port
                string portName = serial_combobox.SelectedItem.ToString();
                
                if(true == rt.Connect(portName))
                {
                    connect_button.Content = "Disconnect";
                    tagdb.Clear();
                    TagResults.dgTagResults.ItemsSource = null;
                    TagResults.dgTagResults.ItemsSource = tagdb.TagList;
                }
            }
            else if (connect_button.Content.Equals("Disconnect"))
            {
                connect_button.Content = "Connect";
                rt.Disconnect();
            }
        }

        private void receive_button_Click(object sender, RoutedEventArgs e)
        {
            if(receive_button.Content.Equals("StartReceive"))
            {
                if (rt == null)
                    return;
                receive_button.Content = "Receiving";
                
                rt.TagRead += Rt_TagRead;
                rt.StatsListener += Rt_StatsListener;
                rt.ReceiveData();

            }
            else if (receive_button.Content.Equals("Receiving"))
            {
                receive_button.Content = "StartReceive";
                if (rt == null)
                    return;
                rt.StopReceiveData();
                rt.TagRead -= Rt_TagRead;
                rt.StatsListener -= Rt_StatsListener;
            }
        }

        private void Rt_TagRead(object sender, TagReadDataEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate ()
            {
                tagdb.Add(e.TagReadData);
                //Console.WriteLine("--> {0}", e.TagReadData);
                //TagResults.dgTagResults.ItemsSource = tagdb.TagList;
            }));

            Dispatcher.BeginInvoke(new ThreadStart(delegate ()
            {
                uniqueReadCount_label.Content = tagdb.UniqueTagCount;
                totalReadCount_label.Content = tagdb.TotalTagCount;
            }));
            
        }

        private void Rt_StatsListener(object sender, StatsReportEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate ()
            {
                temperature_label.Content = e.StatsReport.STATS.TEMPERATURE;
                //Console.WriteLine(e.StatsReport.ToString());
            }));
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            tagdb.Clear();
            uniqueReadCount_label.Content = "0";
            totalReadCount_label.Content = "0";
        }

        private void refreshSerial_button_Click(object sender, RoutedEventArgs e)
        {
            RefreshSerial();
        }
    }
}
