using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5OOKPForms
{
    public partial class WarningForm : Form
    {
        public WarningForm(string WarningText)
        {
            InitializeComponent();
            label1.Text = WarningText;
        }

        private void WarningForm_Load(object sender, EventArgs e)
        {

        }

        private void Ok_Click(object sender, EventArgs e)
        {
            WarningForm.ActiveForm.Close();
        }
    }
}
