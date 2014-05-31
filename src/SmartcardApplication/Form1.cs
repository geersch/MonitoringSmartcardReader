using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmartcardLibrary;

namespace SmartcardApplication
{
    public partial class Form1 : Form
    {
        private SmartcardManager manager = SmartcardManager.GetManager(); 

        public Form1()
        {
            InitializeComponent();
        }   
    }
}
