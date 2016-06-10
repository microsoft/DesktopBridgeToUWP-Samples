using System;
using System.Windows.Forms;

namespace MyDesktopApp
{
    public partial class Form1 : Form
    {
        bool initialized = false; 
        public Form1()
        {
            InitializeComponent();        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in comboBox1.Items)
            {
                if (item.ToString() == Program.CurrentStatus)
                {
                    comboBox1.SelectedItem = item;
                    initialized = true;
                    break;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initialized) return;
            Program.RegKey.SetValue("Status", comboBox1.SelectedItem);
        }
    }
}
