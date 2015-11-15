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
    public partial class DBmodifier : Form
    {
        public DBmodifier()
        {
            InitializeComponent();
        }

        public string Command { get; set; }  //THIS IS CALLED A PROPERTY, ALLOWS SAFE MODIFICATION TO PRIVATE CONTROLS
                                //THANKS TO THE OOP FEATURE CALLED ENCAPSULATION, TO AVOID DETECTION BY REVERSE ENGINEERS

        private void DBmodifier_Load(object sender, EventArgs e)
        {
            lblHelp.Text = Command;     //PROPERTY SET TO THE LABEL, DON'T HAVE TO MAKE LABEL PUBLIC, JUST PASS THE VALUE
        }

    }
}
