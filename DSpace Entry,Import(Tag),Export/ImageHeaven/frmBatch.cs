using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.wfe;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Net;
using LItems;
using System.IO;

namespace ImageHeaven
{
    public partial class frmBatch : Form
    {
        wfeBatch crtBatch = null;
        OdbcConnection sqlCon = null;
        MemoryStream stateLog;
        byte[] tmpWrite;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);

        string name = frmMain.name;

        public frmBatch()
        {
            InitializeComponent();
        }

        public frmBatch(wItem prmCmd, OdbcConnection prmCon)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            //this.Icon = 
            exMailLog.SetNextLogger(exTxtLog);
            
			crtBatch = (wfeBatch) prmCmd;
            sqlCon = prmCon;
			if (crtBatch.GetMode()==Constants._ADDING)
				this.Text = "B'Zer - Add Batch";
			else
				this.Text = "B'Zer - Edit Batch";
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        

        private void frmBatch_Load(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            panel3.Enabled = false;

            populateProject();
        }

        private void populateProject()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select proj_key, proj_code from project_master  ";
            
            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbProject.DataSource = dt;
                cmbProject.DisplayMember = "proj_code";
                cmbProject.ValueMember = "proj_key";
            }
            else
            {
                MessageBox.Show("Add one project first...");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {
            if (cmbProject.Text == "" || cmbProject.Text == null)
            {
                MessageBox.Show("Please select a project name");
                cmbProject.Focus();
                cmbProject.Select();
            }
            else
            {
                panel2.Enabled = true;
                
                textBox1.Focus();
                textBox1.Select();
            }
        }

        private void cmbProject_MouseLeave(object sender, EventArgs e)
        {
            if (cmbProject.Text == "" || cmbProject.Text == null)
            {
                MessageBox.Show("Please select a project name");
                cmbProject.Focus();
                cmbProject.Select();
            }
            else
            {
                panel2.Enabled = true;
               
                textBox1.Focus();
                textBox1.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox1.Text == null)
            {
                MessageBox.Show("You cannot left this field blank....");
                textBox1.Focus();
                textBox1.Select();
            }
            if (textBox2.Text == "" || textBox2.Text == null)
            {
                MessageBox.Show("You cannot left this field blank....");
                textBox2.Focus();
                textBox2.Select();
            }
            if ((textBox1.Text != "" || textBox1.Text != null) && (textBox2.Text != "" || textBox2.Text != null))
            {
                string batchCode = textBox1.Text.ToUpper() + "_" + textBox2.Text.ToUpper();

                textBox3.Text = batchCode;
                textBox4.Text = batchCode;

                button2.Enabled = true;
            }
        }

        private void ClearAllField()
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == null || textBox3.Text == "")
            {
                MessageBox.Show("Please generate a batch code...");
                textBox1.Focus();
                textBox1.Select();
            }
            else
            {
                NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();
                udtBatch objBatch = new udtBatch();
                try
                {

                    crtBatch = new wfeBatch(sqlCon);

                    objBatch.proj_code = Convert.ToInt32(cmbProject.SelectedValue);
                    objBatch.batch_code = textBox4.Text;
                    objBatch.batch_name = textBox3.Text;
                    objBatch.Created_By = name;
                    objBatch.Created_DTTM = dbcon.GetCurrenctDTTM(1, sqlCon);

                    if (crtBatch.TransferValues(objBatch) == true)
                    {
                        statusStrip1.Items.Add("Status: Data SucessFully Saved");
                        statusStrip1.ForeColor = System.Drawing.Color.Black;
                        ClearAllField();
                    }
                    else
                    {
                        statusStrip1.Items.Add("Status: Data Can not be Saved");
                        statusStrip1.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (KeyCheckException ex)
                {
                    MessageBox.Show(ex.Message, "CESC Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stateLog = new MemoryStream();
                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                    //exMailLog.Log(ex, this);
                }
                catch (DbCommitException dbex)
                {
                    MessageBox.Show(dbex.Message, "CESC Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stateLog = new MemoryStream();
                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Commit" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                   // exMailLog.Log(dbex, this);
                }
                catch (CreateFolderException folex)
                {
                    MessageBox.Show(folex.Message, "CESC Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stateLog = new MemoryStream();
                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Create Folder" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                   // exMailLog.Log(folex, this);
                }
                catch (DBConnectionException conex)
                {
                    MessageBox.Show(conex.Message, "CESC Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stateLog = new MemoryStream();
                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while Connection error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                    //exMailLog.Log(conex, this);
                }
                catch (INIFileException iniex)
                {
                    MessageBox.Show(iniex.Message, "CESC Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stateLog = new MemoryStream();
                    tmpWrite = new System.Text.ASCIIEncoding().GetBytes("Error while INI read error" + "Batch Key-" + objBatch.batch_key + "\n" + "project Key-" + objBatch.proj_code + "\n");
                    stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                    //exMailLog.Log(iniex, this);
                }
            }
        }

    }
}
