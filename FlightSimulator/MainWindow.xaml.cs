using FlightSimulator.Model;
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
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //when the window closed disconnect the server and client channels.
            Closed += MainWindowClosed;
            InitializeComponent();
        }

        private void MainWindowClosed(object sender, EventArgs e)
        {
            if (Server.Instance.isConnected()) { Server.Instance.DisConnect(); }
            if (CommandConnect.Instance.IsConnected) { CommandConnect.Instance.DisConnect(); }
        }
    }
}
