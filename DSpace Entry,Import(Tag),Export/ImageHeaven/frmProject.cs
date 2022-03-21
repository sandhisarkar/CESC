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

namespace ImageHeaven
{
    public partial class frmProject : Form
    {
        wItem objProject;
        OdbcConnection sqlCon = null;


        string name = frmMain.name;
        

        public frmProject(wItem prmCmd, OdbcConnection prmCon)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            sqlCon = prmCon;
			objProject = (wfeProject) prmCmd;
			if (objProject.GetMode()==Constants._ADDING)
				this.Text = "Record Management - Add Project";
			else
                this.Text = "Record Management - Edit Project";
					
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

        public frmProject()
        {
            InitializeComponent();
        }

        private void frmProject_Load(object sender, EventArgs e)
        {
            
            CmdSave.Enabled = false;

            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                CmdBrowseScannLoc.Focus();
                groupBox1.Enabled = true;
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdBrowseScannLoc_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            int pos = 0;
            FolderBrowserDialog browseForComputer = new FolderBrowserDialog();
            string origIp = string.Empty;
            System.IO.FileStream fs;

            if (browseForComputer.ShowDialog() == DialogResult.OK)
            {
                txtScannedLoc.Text = browseForComputer.SelectedPath;
                path = txtScannedLoc.Text;
                if (path != string.Empty)
                {
                    CmdSave.Enabled = true;
                    pos = path.IndexOf("\\\\");
                    if (pos == -1)
                    {
                        //int posSnd = path.IndexOf("\\", 2);
                        //string compName = path.Substring(pos + 2, posSnd - 2);
                        string compName = path;
                        //string restPath = path.Substring(posSnd);
                        if (compName != string.Empty)
                        {
                            try
                            {
                                //IPHostEntry ip = Dns.GetHostEntry(compName);
                                //IPAddress[] IpA = ip.AddressList;
                                //for (int i = 0; i < IpA.Length; i++)
                                //{
                                //    origIp = IpA[i].ToString();
                                //}
                                //path = "\\\\" + origIp + restPath;
                                fs = new System.IO.FileStream(path + "\\temp.txt", System.IO.FileMode.Append);
                                fs.Close();
                                System.IO.File.Delete(path + "\\temp.txt");
                                CmdSave.Enabled = true;
                                txtScannedLoc.Text = path;
                            }
                            catch (Exception ex)
                            {
                                CmdSave.Enabled = false;
                                MessageBox.Show(ex.Message.ToString());
                            }
                        }
                    }
                    else
                    {
                        CmdSave.Enabled = false;
                        MessageBox.Show("Selected drive is invalid. Please select folder from network drive..");
                    }
                }
            }
            else
            {
                CmdSave.Enabled = false;
            }
        }

        private void txtProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 92) || (e.KeyChar == 39) || (e.KeyChar == 47))
            {
                e.Handled = true;
            }
        }

        private void ClearAllField()
        {
            txtProjectName.Text = string.Empty;
            txtScannedLoc.Text = string.Empty;
        }

        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            CmdBrowseScannLoc.Focus();
            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void txtProjectName_MouseLeave(object sender, EventArgs e)
        {
            CmdBrowseScannLoc.Focus();
            if (txtProjectName.Text == null || txtProjectName.Text == "")
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void CmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                NovaNet.Utils.dbCon dbcon = new NovaNet.Utils.dbCon();


                wfeProject crtProject = new wfeProject(sqlCon);

                udtProject udtProj = new udtProject();
                udtProj.Code = txtProjectName.Text.ToUpper();
                udtProj.Project_Path = txtScannedLoc.Text;
                udtProj.Created_By = name;
                udtProj.Created_DTTM = dbcon.GetCurrenctDTTM(1, sqlCon);
                if (crtProject.TransferValues(udtProj) == true)
                {
                    statusStrip1.Items.Clear();
                    statusStrip1.Items.Add("Status: Data SucessFully Saved");
                    statusStrip1.ForeColor = System.Drawing.Color.Black;
                    ClearAllField();
                }
                else
                {
                    statusStrip1.Items.Clear();
                    statusStrip1.Items.Add("Status: Data Can not be Saved");
                    statusStrip1.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (KeyCheckException ex)
            {
                MessageBox.Show(ex.Message, "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DbCommitException dbex)
            {
                MessageBox.Show(dbex.Message, "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CreateFolderException folex)
            {
                MessageBox.Show(folex.Message, "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DBConnectionException conex)
            {
                MessageBox.Show(conex.Message, "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (INIFileException iniex)
            {
                MessageBox.Show(iniex.Message, "Record Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
