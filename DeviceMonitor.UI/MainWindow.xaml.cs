using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeviceMonitor.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly TelemetryClient _client = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(_client);
            _client.Start();
            Closed += (_, _) => _client.Dispose();
        }
    }
}