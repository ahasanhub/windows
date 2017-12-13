using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.IO;

namespace NAVExtension
{
    public partial class LanguageForm : Form
    {
        public LanguageForm()
        {
            InitializeComponent();
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            Dictionary<string, string> lan = new Dictionary<string, string>();
            lan.Add("en-US", "Engliah");
            lan.Add("bn-BD", "Bangla");            
            comboBox1.DataSource = new BindingSource(lan, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;            
            Assembly asm = Assembly.Load("NAVExtension");
            ResourceManager rm = new ResourceManager("NAVExtension.Language.Resource",asm);
            lbllanguage.Text=rm.GetString("language", ci);
            lblHeading.Text = rm.GetString("linfo", ci);           
            btnSave.Text = rm.GetString("save",ci);            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var lancode = comboBox1.SelectedValue;
            using (StreamWriter sw = new StreamWriter("lang.txt"))
            {
                sw.Write(lancode.ToString());
            }
            CultureInfo ci = new CultureInfo(lancode.ToString());            
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            //MessageBox.Show("Update successfully.");
            LanguageForm NewForm = new LanguageForm();
            NewForm.Show();
            this.Dispose(false);
        }
    }
}
