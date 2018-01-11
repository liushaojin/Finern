using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatTemplate
{
    public partial class ucEqCheck : UserControl
    {
        public ucEqCheck()
        {
            InitializeComponent();
           
            base.Load += new System.EventHandler(UILoad);
            base.Disposed += new System.EventHandler(this.UnLoad);
        }

        private void UILoad(object sender, EventArgs e)
        {

        }

        private void UnLoad(object sender, EventArgs e)
        {

        }
    }
}
