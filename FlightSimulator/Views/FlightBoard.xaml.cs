using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        private FlightBoardViewModel flightBoardVM;
        private bool isFirst = true;
        public FlightBoard()
        {
            InitializeComponent();
            flightBoardVM = new FlightBoardViewModel(Server.Instance);
            DataContext = flightBoardVM;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
            // add to the VM event when property changes, do Vm_PropertyChanged
            flightBoardVM.PropertyChanged += Vm_PropertyChanged;

        }

        /*the function that happens when lon and lat are recieved. add the recieved
        data to the points list to plot on the chart.*/
        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // the first data is not used.
            if (isFirst)
            {
                isFirst = false;
            }
            else if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                //create new point according to the lat and lon data.
                Point p1 = new Point(flightBoardVM.Lat, flightBoardVM.Lon);
                planeLocations.AppendAsync(Dispatcher, p1);
                isFirst = true;
            }
        }

    }
}

