using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private Server modelServer;
        /*the function add to flight simulator the all notification
         in order to update the simulator about changes*/
        public FlightBoardViewModel(Server server)
        {
            this.modelServer = server;
            modelServer.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
        /*the function return the lon and the lat from simulator for the map.*/
        public double Lon
        {
            get { return modelServer.Lon; }
        }

        public double Lat
        {
            get { return modelServer.Lat; }
        }
        /*this function responsible about the buttom setting in autoPilot*/
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return _settingsCommand ?? (_settingsCommand = new CommandHandler(() => SettingsClick()));
            }
        }
        /*the function open the window's setting and show him.*/
        private void SettingsClick()
        {
            var settingWin = new Settings();
            settingWin.Show();
        }
        /*this function responsible about the buttom connect in autoPilot*/
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return _connectCommand ?? (_connectCommand = new CommandHandler(() => ConnectClick()));
            }
        }
        /*the function connect to simulator and the simulator connect to us.*/
        private void ConnectClick()
        {
            Server.Instance.connectServer();
            CommandConnect.Instance.ConnetAsClient();

        }
    }
}