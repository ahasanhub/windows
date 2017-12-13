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
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Resources;
using Microsoft.Reporting.WinForms;

namespace NAVExtension
{
    public partial class CustomerReportForm : Form
    {
        public CustomerReportForm()
        {
            InitializeComponent();
        }

        private void CustomerReportForm_Load(object sender, EventArgs e)
        {

            CustomerService.CustomerService_Service service = new CustomerService.CustomerService_Service();
            service.UseDefaultCredentials = true;
            var customers = service.ReadMultiple(null, string.Empty, 100);
            var list = customers.Select(c => new Customer { No = c.No, Name = c.Name, LocationCode = c.Location_Code, Contact = c.Contact, Balance = c.Balance_LCY, BalanceDue = c.Balance_Due_LCY, Sales = c.Sales_LCY });

           
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Assembly asm = Assembly.Load("NAVExtension");
            ResourceManager rm = new ResourceManager("NAVExtension.Language.Resource", asm);
            
            CustomerBindingSource.DataSource = list.ToList();
            ReportParameter[] reportParameter = new ReportParameter[] {
                 new ReportParameter("par1",rm.GetString("clist",ci)),
                 new ReportParameter("language",Thread.CurrentThread.CurrentCulture.ToString()),
                 new ReportParameter("cno",rm.GetString("cno",ci)),
                 new ReportParameter("cname",rm.GetString("cname",ci)),
                 new ReportParameter("clocationcode",rm.GetString("clocationcode",ci)),
                 new ReportParameter("ccontact",rm.GetString("ccontact",ci)),
                 new ReportParameter("cbalance",rm.GetString("cbalance",ci)),
                 new ReportParameter("cbalancedue",rm.GetString("cbalancedue",ci)),
                 new ReportParameter("csales",rm.GetString("csales",ci)),
                
             };

            this.reportViewer1.LocalReport.SetParameters(reportParameter);
            this.reportViewer1.RefreshReport();
        }
    }
}
