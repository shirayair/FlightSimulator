using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private String content = "";
        private bool isSend = false;
        /*The function makes a red background when there are no commands before clicking the OK*/
        public String BackgroundColor
        {
            get {
                //if no commands
                if (content != "") {
                    //if the command send.
                    if (isSend)
                    {
                        //the background is white.
                        isSend = false;
                        return "White";
                    }
                    //if no send,and no command.
                    return "Pink";
                }
                else {
                    //if have command .
                    return "White";
                }
            }
        }
        /*the function return the content of the autoPilot */
        public String Content
        {
           
            get { return content; }
            //if the contant change we send notifiction about the change,the backgrond effect from this.
            set
            {
                content = value;
                NotifyPropertyChanged("Content");
                NotifyPropertyChanged("BackgroundColor");
            }
        }
        /*this function responsible about the buttom clear in autoPilot*/
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClearClick()));
            }
        }
        /*the function that change the content to be empty and do notifiction because the change*/
        private void ClearClick()
        {
            content = "";
            NotifyPropertyChanged("Content");
            NotifyPropertyChanged("BackgroundColor");
        }

        /*this function responsible about the buttom OK in autoPilot*/
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                //we send to commandHandler objectFunction about the use of the buttom .
                return okCommand ?? (okCommand = new CommandHandler(() => OKClick()));
            }
        }
        /*the function send the command to flight simulator*/
        private void OKClick()
        {
            CommandConnect.Instance.Send(content);
            //update that the command recived.
            isSend = true;
            //we do notfication about the background,after we recived the commands the background need to be white.
            NotifyPropertyChanged("BackgroundColor");
        }
}
}
