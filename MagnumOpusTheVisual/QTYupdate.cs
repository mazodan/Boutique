using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MagnumOpusTheVisual
{
    public partial class QTYupdate : Form
    {
        public QTYupdate()
        {
            InitializeComponent();
        }

        public string QTY { get; set; } //GET QUANTTY

        private void QTYupdate_Load(object sender, EventArgs e)
        {
            txtQTY.Text = QTY;
        } 
    }
}
