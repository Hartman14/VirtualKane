using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class ValueClass
    {

        public static bool isShown = true;
        public static bool SensorActive1 = true;
        public static bool SensorActive2 = true;
        public static bool VibActive1 = true;
        public static bool VibActive2 = true;

        public void setSensorActive(bool input, int sensor)
        {
            if (sensor == 1)
            {
                SensorActive1 = input;
            }
            else if (sensor == 2)
            {
                SensorActive2 = input;
            }
        }

        public void setVibActive(bool input, int vib)
        {
            if (vib == 1)
            {
                VibActive1 = input;
            }
            else if (vib == 2)
            {
                VibActive2 = input;
            }
        }

        public bool getSensorActive(int sensor)
        {
            if (sensor == 1)
            {
                return SensorActive1;
            }
            else if (sensor == 2)
            {
                return SensorActive2;
            }

            else
            {
                return false;
            }
        }

        public bool getVibActive(int sensor)
        {
            if (sensor == 1)
            {
                return VibActive1;
            }
            else if (sensor == 2)
            {
                return VibActive2;
            }

            else
            {
                return false;
            }
        }

        public bool getIsShown()
        {
            return isShown;
        }

        public void setIsShown(bool input)
        {
            isShown = input;
        }

        public void showForm(Form input)
        {
            input.Visible = isShown;
        }
        
    }
}
