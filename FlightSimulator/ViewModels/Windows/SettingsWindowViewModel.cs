using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;
        Window window;

        public SettingsWindowViewModel(ISettingsModel model, Window window)
        {
            this.model = model;
            this.window = window;
        }

        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

     

        public void SaveSettings()
        {
            model.SaveSettings();
        }

        public void ReloadSettings()
        {
            model.ReloadSettings();
        }
        /*this function responsible about the buttom  in autoPilot*/
        #region Commands
        #region ClickCommand
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        /*the function save the  setting and open the main windows.*/
        private void OnClick()
        {
            model.SaveSettings();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.Show();
            window.Close();
        }
        #endregion
        /*this function responsible about the buttom cancel in autoPilot*/
        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        /*the function save the previous setting and open the main windows.*/
        private void OnCancel()
        {
            model.ReloadSettings();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.Show();
            window.Close();
        }
        #endregion
        #endregion
    }
}

