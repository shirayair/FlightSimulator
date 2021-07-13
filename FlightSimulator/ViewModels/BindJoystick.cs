using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class BindJoystick
    {
        /*the function responsible about the change of the joystick and update the flight simulator,
         the value of throttle*/
        public float ThrottleCommand
        {
            
            set
            {
                string throttleLine = "set controls/engines/current-engine/throttle " + value + "\r\n";
                CommandConnect.Instance.Send(throttleLine);
            }
        }
        /*the function responsible about the change of the joystick and update the flight simulator,
         the value of rudder*/
        public float RudderCommand
        {
            set
            {
                string rudderLine = "set controls/flight/rudder " + value + "\r\n";
                CommandConnect.Instance.Send(rudderLine);
            }
        }
        /*the function responsible about the change of the joystick and update the flight simulator,
         the value of Elevator*/
        public float ElevatorCommand
        {
            set
            {
                string ElevatorLine = "set /controls/flight/elevator " + value + "\r\n";
                CommandConnect.Instance.Send(ElevatorLine);
            }
        }
        /*the function responsible about the change of the joystick and update the flight simulator,
          the value of ailorn */
        public float AileronCommand
        {
            set
            {
                string AileronLine = "set /controls/flight/aileron " + value + "\r\n";
                CommandConnect.Instance.Send(AileronLine);
            }
        }


    }
}
