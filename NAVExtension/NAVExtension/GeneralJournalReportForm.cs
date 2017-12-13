using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAVExtension.Models;
using Microsoft.Reporting.WinForms;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace NAVExtension
{
    public partial class frmGeneralJournalReport : Form
    {
        public frmGeneralJournalReport()
        {
            InitializeComponent();
        }

        private void frmGeneralJournalReport_Load(object sender, EventArgs e)
        {
            generaljournal.GeneralJournalService_Service service = new generaljournal.GeneralJournalService_Service();
            service.UseDefaultCredentials = true;
            
            var gjList = service.ReadMultiple("DEFAULT", null, string.Empty, 100);
            IEnumerable<GeneralJournal> list = gjList.Select(j=>new GeneralJournal {
             PostingDate=j.Posting_Date,
             DocumentType=j.Document_Type.ToString(),
             DocumentNo=j.Document_No,
             AccountType=j.Account_Type.ToString(),
             AccountNo=j.Account_No,
             Description=j.Description,
             GeneralPostingType=j.Gen_Posting_Type.ToString(),
             Amount=j.Amount,
             BalAccountNo=j.Bal_Account_No,
             Balance=j.Balance
            });
            //reportViewer1.Reset();  
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;            
            Assembly asm = Assembly.Load("NAVExtension");
            ResourceManager rm = new ResourceManager("NAVExtension.Language.Resource", asm);
            
            GeneralJournalBindingSource.DataSource = list.ToList();
            ReportParameter[] reportParameter = new ReportParameter[] {
                 new ReportParameter("par1",rm.GetString("genjournal",ci)),
                 new ReportParameter("language",Thread.CurrentThread.CurrentCulture.ToString()),
                 new ReportParameter("postingdate",rm.GetString("postingdate",ci)),
                 new ReportParameter("documenttype",rm.GetString("documenttype",ci)),
                 new ReportParameter("documentno",rm.GetString("documentno",ci)),
                 new ReportParameter("accounttype",rm.GetString("accounttype",ci)),
                 new ReportParameter("accountno",rm.GetString("accountno",ci)),
                 new ReportParameter("description",rm.GetString("description",ci)),
                 new ReportParameter("genposttype",rm.GetString("genposttype",ci)),
                 new ReportParameter("amount",rm.GetString("amount",ci)),
                 new ReportParameter("balaccountno",rm.GetString("balaccountno",ci)),
                 new ReportParameter("balance",rm.GetString("balance",ci)),
                 new ReportParameter("journaltemname",rm.GetString("journaltemname",ci)),
                 new ReportParameter("journalbatch",rm.GetString("journalbatch",ci)),
             };
           
            this.reportViewer1.LocalReport.SetParameters(reportParameter);
            this.reportViewer1.RefreshReport();
        }
    }
}
