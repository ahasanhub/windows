using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Resources;
using NAVExtension.Models;

namespace NAVExtension
{
    public partial class GeneralJournalForm : Form
    {
        public GeneralJournalForm()
        {
            InitializeComponent();
            LoadGeneralJournal();
        }
        private void LoadGeneralJournal()
        {
            generaljournal.GeneralJournalService_Service service = new generaljournal.GeneralJournalService_Service();
            service.UseDefaultCredentials = true;

            var gjList = service.ReadMultiple("DEFAULT", null, string.Empty, 100);
            IEnumerable<GeneralJournal> list = gjList.Select(j => new GeneralJournal
            {
                PostingDate = j.Posting_Date,
                DocumentType = j.Document_Type.ToString(),
                DocumentNo=j.Document_No,
                AccountType = j.Account_Type.ToString(),
                AccountNo = j.Account_No,
                Description = j.Description,
                GeneralPostingType = j.Gen_Posting_Type.ToString(),
                Amount = j.Amount,
                BalAccountNo = j.Bal_Account_No,
                Balance = j.Balance
            });
            //dataGridView1.DataSource = list.ToList();
            InitGridview(list);


        }

        private void InitGridview(IEnumerable<GeneralJournal> list)
        {
            // Create an unbound DataGridView by declaring a column count.
            dataGridView1.ColumnCount = 10;
            dataGridView1.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            Assembly asm = Assembly.Load("NAVExtension");
            ResourceManager rm = new ResourceManager("NAVExtension.Language.Resource", asm);
            lblPostingDate.Text = rm.GetString("postingdate", ci);
            lblDocumentNo.Text = rm.GetString("documentno", ci);
            lblAccountNo.Text = rm.GetString("accountno", ci);
            lblAmount.Text = rm.GetString("amount", ci);
            btnInsert.Text = rm.GetString("insert", ci);
            btnUpdate.Text = rm.GetString("update", ci);
            btnDelete.Text = rm.GetString("delete", ci);
            // Set the column header names.
            dataGridView1.Columns[0].Name = rm.GetString("postingdate", ci);
            dataGridView1.Columns[1].Name = rm.GetString("documenttype", ci);
            dataGridView1.Columns[2].Name = rm.GetString("documentno", ci);
            dataGridView1.Columns[3].Name = rm.GetString("accounttype", ci);
            dataGridView1.Columns[4].Name = rm.GetString("accountno", ci);
            dataGridView1.Columns[5].Name = rm.GetString("description", ci);
            dataGridView1.Columns[6].Name = rm.GetString("genposttype", ci);
            dataGridView1.Columns[7].Name = rm.GetString("amount", ci);
            dataGridView1.Columns[8].Name = rm.GetString("balaccountno", ci);            
            dataGridView1.Columns[9].Name = rm.GetString("balance", ci);

            

            object[] rows = list.Select(g => new string[] { g.PostingDate.ToShortDateString(), g.DocumentType, g.DocumentNo, g.AccountType, g.AccountNo, g.Description, g.GeneralPostingType,g.Amount.ToString(),g.BalAccountNo,g.Balance.ToString() }).ToArray<object>();

            foreach (string[] rowArray in rows)
            {
                dataGridView1.Rows.Add(rowArray);
            }

        }

        private void GeneralJournalForm_Load(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            
            if (ci.Name=="en-US")
                 dtpPostingDate.Value = Convert.ToDateTime("1/26/2018");
            else
                dtpPostingDate.Value = Convert.ToDateTime("26/1/2018");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            generaljournal.GeneralJournalService_Service service = new generaljournal.GeneralJournalService_Service();
            service.UseDefaultCredentials = true;
            generaljournal.GeneralJournalService gj = new generaljournal.GeneralJournalService { Posting_Date=dtpPostingDate.Value,Document_No=txtDocumentNo.Text,Account_No=txtAccountNo.Text,Debit_Amount=Convert.ToDecimal(txtAmount.Text)};

            service.Create("DEFAULT",ref gj);
            if (ci.Name == "en-US")
                dtpPostingDate.Value = Convert.ToDateTime("1/26/2018");
            else
                dtpPostingDate.Value = Convert.ToDateTime("26/1/2018");
            txtDocumentNo.Text = "";
            txtAccountNo.Text = "";
            txtAmount.Text = "";
            LoadGeneralJournal();
        }
    }
}
