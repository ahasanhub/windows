using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;

namespace NAVExtension
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            InitCulture();
        }

        private void InitCulture()
        {
            CultureInfo ci;
            string lan = string.Empty;
            if(File.Exists(Application.StartupPath + "/lang.txt"))
            {
                using (StreamReader sw = new StreamReader("lang.txt"))
                {
                    lan = sw.ReadLine();
                }
            }
           
            if(string.IsNullOrEmpty(lan))
             ci= new CultureInfo("en-US");
            else
             ci = new CultureInfo(lan);
            //CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LanguageForm form1 = new LanguageForm();
            form1.Show();
        }

        private void generalJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGeneralJournalReport frm = new frmGeneralJournalReport();
            frm.Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomersForm form = new CustomersForm();
            form.Show();
        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomerReportForm form = new CustomerReportForm();
            form.Show();
        }

        private void generalJournalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GeneralJournalForm form = new GeneralJournalForm();
            form.Show();
        }
    }
}
