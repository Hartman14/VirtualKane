using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public partial class Diagnostics : Form
    {

        ValueClass social = new ValueClass();

        private SerialPort myport;
        string read;
        static string sensRead1 = "";
        static string sensRead2 = "";

        int SensorDistance1 = -1;
        int SensorDistance2 = -1;

        bool sensorComplete1 = false;
        bool sensorComplete2 = false;
        bool Errors = false;

        bool pageLoadComplete = false;
        bool sensActive1 = true;
        bool sensActive2 = true;
        bool vibActive1 = true;
        bool vibActive2 = true;
        DateTime time = DateTime.Now;

        public readonly Timer clock = new Timer();

        public Diagnostics()
        {
            InitializeComponent();
            loadUpdate();
            checkBools();
            Init();
            kingParser();
            myport.Close();
            pageLoadComplete = true;
            clock.Interval = 250;
            clock.Tick += RefreshValues;
        }

        //---------------------------------------------------------------------------------
        //updates values for load
        private void loadUpdate()
        {
            Read1.Text = "0";
            Read2.Text = "0";
            time = DateTime.Now;

            MidClose1.Text = CR1.Text;
            MidClose1.Refresh();
            MidClose2.Text = CR2.Text;
            MidClose2.Refresh();

            FarClose1.Text = MR1.Text;
            FarClose1.Refresh();
            FarClose2.Text = MR2.Text;
            FarClose2.Refresh();

        }

        //----------------------------------------------------------------------------------
        //keeps values up to date in Timer
        public void RefreshValues(object sender, EventArgs e){
            RenewValues();
        }

        //---------------------------------------------------------------------------------
        //simple refresh function for quick refresh
        public void RenewValues()
        {
            if (myport.IsOpen)
            {
                myport.Close();
            }
            Init();
            if (!Errors)
            {
                kingParser();
            }
            else
            {

            }
        }

        //---------------------------------------------------------------------------------
        //BACK button
        private void Back_Click(object sender, EventArgs e)
        {
            if (myport.IsOpen)
            {
                myport.Close();
            }
            Close();
            Form1 home = new Form1();
            social.setIsShown(true);
            social.showForm(home);
        }

        //---------------------------------------------------------------------------------
        //Returns whether or not vibration motors are vibrating, still or disabled
        private void isVibing(int input, bool vib, Label output, ComboBox close, ComboBox mid, ComboBox far)
        {
            if (vib)
            {
                if (pageLoadComplete)
                {
                    try
                    {
                        myport.WriteLine(input.ToString() + " " + close.Text + " " + mid.Text + " " + far.Text + "\n");
                    }
                    catch
                    {
                        MessageBox.Show("Error in vibration command to hardware");
                    }
                }
                try
                {
                    if (input <= 1)
                    {
                        try
                        {
                            output.Text = "Touching";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text 0 \n \n" + exe);
                            Errors = true;
                        }
                    }
                    else if ((1 < input) && (input < Convert.ToInt32(close.Text.ToString())))
                    {
                        try
                        {
                            output.Text = "Vibrating1";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text 1 \n \n" + exe);
                            Errors = true;
                        }
                    }
                    else if ((Convert.ToInt32(close.Text.ToString()) <= input) && (input < Convert.ToInt32(mid.Text.ToString())))
                    {
                        try
                        {
                            output.Text = "Vibrating2";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text 2 \n \n" + exe);
                            Errors = true;
                        }
                    }
                    else if ((Convert.ToInt32(mid.Text.ToString()) <= input) && (input < Convert.ToInt32(far.Text.ToString())))
                    {
                        try
                        {
                            output.Text = "Vibrating3";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text 3 \n \n" + exe);
                            Errors = true;
                        }
                    }
                    else if ((Convert.ToInt32(far.Text.ToString()) <= input))
                    {
                        try
                        {
                            output.Text = "Safe";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text safe \n \n" + exe);
                            Errors = true;
                        }
                    }
                    else
                    {
                        try
                        {
                            output.Text = "Error";
                            output.Refresh();
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Failed to update Motor to text Err \n \n" + exe);
                            Errors = true;
                        }
                    }
                }
                catch(Exception exe)
                {
                    MessageBox.Show("Failed to read motor ranges CR: " + close + " MR: " + mid + " LR: " + far + " \n \n" + exe);
                    Errors = true;
                }
            }
            else
            {
                try
                {
                    output.Text = "Disabled";
                    output.Refresh();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Failed to update Motor to text Dis \n \n" + exe);
                    Errors = true;
                }
            }
        }
        
        //^---------------^--------------------^-----------------------------^------------------------^---
        //Short version of method above for plug-in purposes
        private void setVib(int place, int vibratingMoter)
        {
            if (vibratingMoter == 1)
            {
                try
                {
                    isVibing(place, EnableVib1.Checked, VibRead1, CR1, MR1, LR1);
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Failed to call motor #1 \n \n" + exe);
                    Errors = true;
                }
            }
            else if (vibratingMoter == 2)
            {
                try
                {
                    isVibing(place, EnableVib2.Checked, VibRead2, CR2, MR2, LR2);
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Failed to call motor #2 \n \n" + exe);
                    Errors = true;
                }
            }
            else
            {
                MessageBox.Show("Incorrect viration motor selected with value of " + vibratingMoter);
                Errors = true;
            }
        }

        
        //---------------------------------------------------------------------------------
        //Returns calculated Distance from sensors
        private void EnableSens1_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSens1.Checked)
            {
                sensActive1 = true;
                checkStoreValue(1);
                if (EnableVib1.Checked == false)
                {
                    EnableVib1.Checked = true;
                }
                if (pageLoadComplete)
                {
                    RenewValues();
                }
                Read1.Text = SensorDistance1.ToString();
            }
            else
            {
                sensActive1 = false;
                if (EnableVib1.Checked == true)
                {
                    EnableVib1.Checked = false;
                }
                Read1.Text = "Disabled";
            }
            social.setSensorActive(sensActive1, 1);
        }

        private void EnableSens2_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSens2.Checked)
            {
                sensActive2 = true;
                checkStoreValue(2);
                if (EnableVib2.Checked == false)
                {
                    EnableVib2.Checked = true;
                }
                if (pageLoadComplete)
                {
                    RenewValues();
                }
                Read2.Text = SensorDistance2.ToString();
            }
            else
            {
                sensActive2 = false;
                if (EnableVib2.Checked == true)
                {
                    EnableVib2.Checked = false;
                }
                Read2.Text = "Disabled";
            }
            social.setSensorActive(sensActive2, 2);
        }

        //---------------------------------------------------------------------------------
        //Returns calculated Distance from sensors
        private void EnableVib2_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableVib2.Checked)
            {
                vibActive2 = true;
                isVibing(SensorDistance2, true, VibRead2, CR2, MR2, LR2);
            }
            else
            {
                vibActive2 = false;
                VibRead2.Text = "Disabled";
            }
            social.setVibActive(vibActive2, 2);
        }

        private void EnableVib1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (EnableVib1.Checked)
            {
                vibActive1 = true;
                isVibing(SensorDistance1, true, VibRead1, CR1, MR1, LR1);
            }
            else
            {
                vibActive1 = false;
                VibRead1.Text = "Disabled";
            }
            social.setVibActive(vibActive1, 1);
        }

        //---------------------------------------------------------------------------------
        //Sets Distance ranges for sensors
        //close range
        private void CR1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MidClose1.Text = CR1.Text;
            MidClose1.Refresh();
        }

        private void CR2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MidClose2.Text = CR2.Text;
            MidClose2.Refresh();
        }

        //Mid range
        private void MR1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FarClose1.Text = MR1.Text;
            FarClose1.Refresh();
        }

        private void MR2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FarClose2.Text = MR2.Text;
            FarClose2.Refresh();
        }

        //long range
        private void LR1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LR2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //---------------------------------------------------------------------------------
        //Transition between Quick Settings
        private void QS_Click(object sender, EventArgs e)
        {
            if (myport.IsOpen)
            {
                myport.Close();
            }
            Settings instance = new Settings();
            instance.Show();
            Close();
        }

        //--------------------------------------------------------------------------------
        //Creates hardware access 
        private void Init()
        {
            try
            {
                myport = new SerialPort();
                myport.BaudRate = 9600;
                myport.PortName = "COM3";
                myport.Open();
            }
            catch(Exception exc)
            {
                MessageBox.Show("Unable to reach hardware \n \n" + exc.ToString());
                Errors = true;
            }
        }

        //----------------------------------------------------------------------------------------
        //Call the data grabbing function and creates a looping parser *trys to that is*
        private void kingParser()
        {
            try
            {
                retrieveValues();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Unable to retrieve input string \n \n" + exe.ToString());
                Errors = true;
            }
        }

        //----------------------------------------------------------------------------------------
        //grabs input values from hardware and sends to parser
        private void retrieveValues()
        {
            read = myport.ReadLine();
            ParseValues(read);
        }

        //-----------------------------------------------------------------------------------------
        //grabs bool values for checkboxes and updates upon page entry
        private void checkBools()
        {
            vibActive1 = social.getVibActive(1);
            vibActive2 = social.getVibActive(2);
            sensActive1 = social.getSensorActive(1);
            sensActive2 = social.getSensorActive(2);
            updateChecks();
        }

        //----------------------------------------------------------------------------------------
        //updates checkboxes
        private void updateChecks()
        {
            if (vibActive1)
            {
                EnableVib1.Checked = true;
            }
            if (vibActive2)
            {
                EnableVib2.Checked = true;
            }
            if (sensActive1)
            {
                EnableSens1.Checked = true;
            }
            if (sensActive2)
            {
                EnableSens2.Checked = true;
            }
        }

        //---------------------------------------------------------------------------------------
        //boolean checker for parser cuz fuck booleans
        private bool checkBools(bool a, bool b){
            return a == b;
        }

        //---------------------------------------------------------------------------------------
        //confirms char is a singal digit int for the parser
        private bool canBeInt(char input)
        {
            if (input == '0')
            {
                return true;
            }
            else if (input == '1')
            {
                return true;
            }
            else if (input == '2')
            {
                return true;
            }
            else if (input == '3')
            {
                return true;
            }
            else if (input == '4')
            {
                return true;
            }
            else if (input == '5')
            {
                return true;
            }
            else if (input == '6')
            {
                return true;
            }
            else if (input == '7')
            {
                return true;
            }
            else if (input == '8')
            {
                return true;
            }
            else if (input == '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //-------------------------------------------------------------------------------------
        //Parses through the incoming sensor values and displays them in graph
        private void ParseValues(string input)
        {
            char[] stack = input.ToCharArray();
            char part;
            SensorDistance1 = -1;
            SensorDistance2 = -1;
            sensorComplete1 = false;
            sensorComplete2 = false;

            for (int i = 0; i < (input.Length); i++)
            {
                part = stack[i];
                if((part == 'n') || (part == '/')){

                }
                else if (validInput(part, sensorComplete1) && EnableSens1.Checked)
                {
                    try
                    {
                        sensRead1 += part;
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Unable to add input char value");
                        Errors = true;
                    }
                }
                else if ((!canBeInt(part)) && (sensorComplete1 == false) && EnableSens1.Checked)
                {
                    try
                    {
                        SensorDistance1 = Convert.ToInt32(sensRead1);
                        checkStoreValue(1);
                    }
                    catch
                    {
                        checkStoreValue(1);
                        Errors = true;
                    }
                    if (EnableVib1.Checked)
                    {
                        try
                        {
                            setVib(SensorDistance1, 1);
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Vibration Update Failure \n \n" + exe);
                            Errors = true;
                        }
                    }
                    Read1.Text = "";
                    Read1.Text = SensorDistance1.ToString();
                    Read1.Refresh();
                    sensRead1 = "";
                    sensorComplete1 = true;
                }
                else if (validInput(part, sensorComplete2) && EnableSens2.Checked)
                {
                    try
                    {
                        sensRead2 += part;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to add input char value");
                        Errors = true;
                    }
                }
                else if ((!canBeInt(part)) && (sensorComplete2 == false) && EnableSens2.Checked)
                {
                    try
                    {
                        SensorDistance2 = Convert.ToInt32(sensRead2);
                        checkStoreValue(2);
                    }
                    catch
                    {
                        checkStoreValue(2);
                        Errors = true;
                    }
                    if(EnableVib2.Checked){
                        try
                        {
                            setVib(SensorDistance2, 2);
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show("Vibration Update Failure \n \n" + exe);
                            Errors = true;
                        }
                    }
                    Read2.Text = "";
                    Read2.Text = SensorDistance2.ToString();
                    Read2.Refresh();
                    sensRead2 = "";
                    sensorComplete2 = true;
                }
                else if ((sensorComplete1 == true) && (sensorComplete2 == true))
                {
                    i = input.Length;
                }
                else
                {
                    if ((SensorDistance1 > -1) && (SensorDistance2 > -1))
                    {
                        MessageBox.Show("Error occured in sensor data retrieval for all Sensors");
                        i = input.Length;
                        Errors = true;
                    }
                    else if (!(SensorDistance1 > -1) && EnableSens1.Checked)
                    {
                        MessageBox.Show("Error occured in sensor data retrieval for Sensor1");
                    }
                    else if (!(SensorDistance2 > -1) && EnableSens2.Checked)
                    {
                        MessageBox.Show("Error occured in sensor data retrieval Sensor2");
                    }
                    else
                    {
                    }
                }
            }
            
        }

        //-------------------------------------------------------------------------------------
        //checks for valid input
        private bool validInput(Char input, bool sensor)
        {
            if((canBeInt(input)) && (sensor == false)){
                return true;
            }
            else{
                return false;
            }
        }

        //-------------------------------------------------------------------------------------
        //starts the kingParser on its infinite loop
        private void RunReader_Click(object sender, EventArgs e)
        {
            clock.Enabled = true;
        }
        
        //-------------------------------------------------------------------------------------
        //Checks for invalid and extreme returns
        private void checkStoreValue(int SensorNum)
        {
            if (SensorNum == 1)
            {
                if (SensorDistance1 > 240)
                {
                    SensorDistance1 = 99;
                }
                else if (SensorDistance1 < 0)
                {
                    SensorDistance1 = 1;
                }
                else
                {

                }
            }
            else if (SensorNum == 2)
            {
                if (SensorDistance2 > 240)
                {
                    SensorDistance2 = 99;
                }
                else if (SensorDistance2 < 0)
                {
                    SensorDistance2 = 1;
                }
                else
                {

                }
            }
        }

    }
}
