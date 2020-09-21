using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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
using WcfFIARService;

namespace FIARHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ServiceHost host;
        private List<ServerEvent> events;
        private void Window_Initialized(object sender, EventArgs e)
        {
            events = new List<ServerEvent>();
            FIARService sv = new FIARService(messagesHandler);
            host = new ServiceHost(sv);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            try
            {
                host.Open();
                dgData.ItemsSource = new List<ServerEvent>(events);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void messagesHandler(string msg, DateTime datetime)
        {
            events.Add(new ServerEvent { Date = datetime, Message = msg });
            dgData.ItemsSource = new List<ServerEvent>(events); ;
        }
    }
}
