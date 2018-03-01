using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsProducts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is ListViewItem && (e.Item as ListViewItem).SubItems.Count > 2)
            {
                var productId = (e.Item as ListViewItem).SubItems[2].Text;
                (sender as ListView).DoDragDrop(productId, DragDropEffects.Copy);
            }
        }
    }
}
