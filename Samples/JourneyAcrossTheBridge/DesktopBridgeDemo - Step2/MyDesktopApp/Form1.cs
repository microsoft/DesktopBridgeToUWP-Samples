using System;
using System.Windows.Forms;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

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

        /// <summary>
        /// Updates the app live tile
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initialized) return;
            Program.RegKey.SetValue("Status", comboBox1.SelectedItem);

            // Update the apps live tile
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);

            XmlNodeList textNodes = tileXml.GetElementsByTagName("text");
            textNodes[0].InnerText = "MyDesktopApp";
            textNodes[1].InnerText = "Status: ";
            textNodes[2].InnerText = comboBox1.SelectedItem.ToString();
            textNodes[3].InnerText = DateTime.Now.ToString("hh:mm:ss");

            TileNotification tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}
