using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblProductName.Text = string.Format("Product Name :{0}",Application.ProductName);
            lblPoductVersion.Text = string.Format("Version : {0}", Application.ProductVersion);
            lblCopyRight.Text = "Copyright ©  2021 by Sultan Khalid";

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
