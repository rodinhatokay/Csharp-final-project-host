using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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
        private ObservableCollection<ServerEvent> events;


        private void Window_Initialized(object sender, EventArgs e)
        {
            events = new ObservableCollection<ServerEvent>();

            FIARService sv = new FIARService(messagesHandler);

            host = new ServiceHost(sv);
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            try
            {

                dgData.ItemsSource = events;
                host.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void messagesHandler(string msg, DateTime datetime)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    events.Add(new ServerEvent { Date = datetime, Message = msg });
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
