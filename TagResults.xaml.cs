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

namespace ReceiveAutonomousReadingDemodotNet
{
    /// <summary>
    /// TagResults.xaml 的交互逻辑
    /// </summary>
    public partial class TagResults : UserControl
    {
        private List<TagReadRecord> _tagList = new List<TagReadRecord>();

        public TagResults()
        {
            InitializeComponent();
            GenerateColmnsForDataGrid();
            this.DataContext = TagList;
        }

        public List<TagReadRecord> TagList
        {
            get { return _tagList; }
            set { _tagList = value; }
        }

        /// <summary>
        /// Generate columns for datagrid
        /// </summary>
        public void GenerateColmnsForDataGrid()
        {
            dgTagResults.AutoGenerateColumns = false;
            serialNoColumn.Binding = new Binding("SerialNumber");
            serialNoColumn.Header = "#";
            serialNoColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Auto);

            epcColumn.Binding = new Binding("EPC");
            epcColumn.Header = "EPC";
            epcColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            timeStampColumn.Binding = new Binding("TimeStamp");
            timeStampColumn.Binding.StringFormat = "{0:hh:mm:ss.fff tt}";
            timeStampColumn.Header = "TimeStamp(msec)";
            timeStampColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            rssiColumn.Binding = new Binding("RSSI");
            rssiColumn.Header = "RSSI(dBm)";
            rssiColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            readCountColumn.Binding = new Binding("ReadCount");
            readCountColumn.Header = "ReadCount";
            readCountColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            antennaColumn.Binding = new Binding("Antenna");
            antennaColumn.Header = "Antenna";
            antennaColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            protocolColumn.Binding = new Binding("Protocol");
            protocolColumn.Header = "Protocol";
            protocolColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            frequencyColumn.Binding = new Binding("Frequency");
            frequencyColumn.Header = "Frequency(kHz)";
            frequencyColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            phaseColumn.Binding = new Binding("Phase");
            phaseColumn.Header = "Phase";
            phaseColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            dataColumn.Binding = new Binding("Data");
            dataColumn.Header = "Data";
            dataColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            dgTagResults.ItemsSource = TagList;
        }
    }
}
