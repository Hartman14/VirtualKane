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
    public partial class Form1 : Form
    {
        ValueClass social = new ValueClass();

        public Form1()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------------------------------------
        //Navigates to Diagnostics page
        private void Diagnostic_Click(object sender, EventArgs e)
        {
            Diagnostics instance = new Diagnostics();
            instance.Show();
            social.setIsShown(false);
            setVisible();
        }

        public void setVisible()
        {
            Visible = social.getIsShown();
        }

        //----------------------------------------------------------------------------------------
        //Navigates to Quick Setting Page
        private void Quick_Setting_Click(object sender, EventArgs e)
        {
            Settings instance = new Settings();
            instance.Show();
            social.setIsShown(false);
            setVisible();
        }
    }
}
