using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Settings : Form
    {
        bool sensActive1 = true;
        bool sensActive2 = true;
        bool vibActive1 = true;
        bool vibActive2 = true;

        ValueClass social = new ValueClass();

        public Settings()
        {
            InitializeComponent();
            checkBools();
        }

        //-------------------------------------------------------------------------------------
        //Navigates back to the Homepage(Start-up)
        private void Back_Click(object sender, EventArgs e)
        {
            Close();
            Form1 home = new Form1();
            home.Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        //---------------------------------------------------------------------------------------------
        //Naviagtes to Diagnostics page
        private void Diag_Click(object sender, EventArgs e)
        {
            Diagnostics instance = new Diagnostics();
            instance.Show();
            Close();
        }

        //------------------------------------------------------------------------------------------
        //grabs bool values for checkboxes and updates upon page entry
        private void checkBools()
        {
            vibActive1 = social.getVibActive(1);
            vibActive2 = social.getVibActive(2);
            sensActive1 = social.getSensorActive(1);
            sensActive2 = social.getSensorActive(2);
            updateChecks();
        }

        //---------------------------------------------------------------------------------------
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

        private void EnableSens1_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSens1.Checked)
            {
                sensActive1 = true;
                if (EnableVib1.Checked == false)
                {
                    EnableVib1.Checked = true;
                }
            }
            else
            {
                if (EnableVib1.Checked == true)
                {
                    EnableVib1.Checked = false;
                }
                sensActive1 = false;
            }
            social.setSensorActive(sensActive1, 1);
        }

        private void EnableSens2_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableSens2.Checked)
            {
                sensActive2 = true;
                if (EnableVib2.Checked == false)
                {
                    EnableVib2.Checked = true;
                }
            }
            else
            {
                sensActive2 = false;
                if (EnableVib2.Checked == true)
                {
                    EnableVib2.Checked = false;
                }
            }
            social.setSensorActive(sensActive2, 2);
        }

        private void EnableVib1_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableVib1.Checked)
            {
                vibActive1 = true;
            }
            else
            {
                vibActive1 = false;
            }
            social.setVibActive(vibActive1, 1);
        }

        private void EnableVib2_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableVib2.Checked)
            {
                vibActive2 = true;
            }
            else
            {
                vibActive2 = false;
            }
            social.setVibActive(vibActive2, 2);
        }
        
    }
}
