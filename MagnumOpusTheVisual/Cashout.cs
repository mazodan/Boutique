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
    public partial class Cashout : Form
    {
        public decimal Amount { get; set; }

        public Cashout()
        {
            InitializeComponent();
        }
    }
}
