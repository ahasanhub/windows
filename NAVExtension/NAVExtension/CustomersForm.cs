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

namespace NAVExtension
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            
            InitializeComponent();            
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            CustomerService.CustomerService_Service service = new CustomerService.CustomerService_Service();
            service.UseDefaultCredentials = true;
            var customers = service.ReadMultiple(null,string.Empty,100);
            var list = customers.Select(c => new Customer { No = c.No, Name = c.Name, LocationCode = c.Location_Code, Contact = c.Contact, Balance = c.Balance_LCY, BalanceDue = c.Balance_Due_LCY, Sales = c.Sales_LCY });

            //dataGridView1.DataSource = list.ToList();
            InitGridview(list);
            

        }

        private void InitGridview(IEnumerable<Customer> list)
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView1.ColumnCount = 7;
            dataGridView1.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Assembly asm = Assembly.Load("NAVExtension");
            ResourceManager rm = new ResourceManager("NAVExtension.Language.Resource", asm);
            lblCode.Text= rm.GetString("cno", ci);
            lblName.Text= rm.GetString("cname", ci);
            btnInsert.Text = rm.GetString("insert", ci);
            btnUpdate.Text = rm.GetString("update", ci);
            btnDelete.Text = rm.GetString("delete", ci);
            // Set the column header names.
            dataGridView1.Columns[0].Name = rm.GetString("cno", ci);
            dataGridView1.Columns[1].Name = rm.GetString("cname", ci);
            dataGridView1.Columns[2].Name = rm.GetString("clocationcode", ci);
            dataGridView1.Columns[3].Name = rm.GetString("ccontact", ci);
            dataGridView1.Columns[4].Name = rm.GetString("cbalance", ci);
            dataGridView1.Columns[5].Name = rm.GetString("cbalancedue", ci);
            dataGridView1.Columns[6].Name = rm.GetString("csales", ci);
            


            object[] rows = list.Select(c=> new string[] { c.No,c.Name,c.LocationCode,c.Contact,c.Balance.ToString(),c.BalanceDue.ToString(),c.Sales.ToString() }).ToArray<object>();

            foreach (string[] rowArray in rows)
            {
                dataGridView1.Rows.Add(rowArray);
            }

        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {            
            txtCode.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            castomercard.CastomerCard_Service service = new castomercard.CastomerCard_Service();
            service.UseDefaultCredentials = true;
            castomercard.CastomerCard card = new castomercard.CastomerCard { No=txtCode.Text,Name=txtName.Text};
            service.Create(ref card);
            txtCode.Text = "";
            txtName.Text = "";
            LoadCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            castomercard.CastomerCard_Service service = new castomercard.CastomerCard_Service();
            service.UseDefaultCredentials = true;
            castomercard.CastomerCard card = new castomercard.CastomerCard { No = txtCode.Text, Name = txtName.Text };
            service.Update(ref card);
            txtCode.Text = "";
            txtName.Text = "";
            LoadCustomers();
        }

       
    }
}
