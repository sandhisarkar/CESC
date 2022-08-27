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
    public partial class frmFileSum : Form
    {
        string name = frmMain.name;
        OdbcConnection sqlCon = null;
        public static string proj_name;
        public static string batch_name;
        public static string projK;
        public static string batchK;
        public static string filename;
        Credentials crd = new Credentials();
        public frmFileSum()
        {
            InitializeComponent();
        }

        public frmFileSum(OdbcConnection pCon, Credentials prmCrd)
        {
            InitializeComponent();

            sqlCon = pCon;

            crd = prmCrd;
        }

        public void fileList(string projK, string batchK)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "SELECT c.reference_no as 'Reference No',c.issue_from as 'Issued From',c.issue_to as 'Issued To',c.issue_date as 'Issued Date',c.`sub_cat` AS 'Subject Category',c.`doc_type` AS 'Document Type' FROM project_master a,batch_master b,metadata_entry c WHERE a.proj_key = b.proj_code AND b.proj_code = c.proj_key AND b.batch_key = c.batch_key AND c.proj_key = a.proj_key AND a.Proj_Key = '" + projK+"' AND b.batch_key = '"+batchK+"' ORDER BY c.item_no";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {

                dgvDash.DataSource = dt;

            }
            else
            {
                dgvDash.DataSource = null;
            }
        }

        private void frmFileSum_Load(object sender, EventArgs e)
        {
            txtProject.Text = frmFile.proj;
            txtBatch.Text = frmFile.batch;
            fileList(frmFile.projKey, frmFile.batchKey);
        }

        private void frmFileSum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.F5)
            {
                fileList(frmFile.projKey, frmFile.batchKey);
            }
        }

        private void dgvDash_DoubleClick(object sender, EventArgs e)
        {
            //this.Hide();
            proj_name = txtProject.Text;
            batch_name = txtBatch.Text;
            projK = frmFile.projKey;
            batchK = frmFile.batchKey;
            filename = dgvDash.SelectedRows[0].Cells[0].Value.ToString();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "SELECT c.status,b.status FROM project_master a,batch_master b,metadata_entry c WHERE a.proj_key = b.proj_code AND b.proj_code = c.proj_key AND b.batch_key = c.batch_key AND c.proj_key = a.proj_key AND a.Proj_key = '" + projK + "' AND b.batch_key = '" + batchK + "' and c.filename = '"+filename+"'";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);

            if (dt.Rows[0][0].ToString() == "N" && dt.Rows[0][1].ToString() == "0")
            {
                
                frmEntry frm = new frmEntry(sqlCon, DataLayerDefs.Mode._Edit);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(this, "Batch isn't for Ready for Metadata Entry", "You cannot open this Batch",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            
        }

        private void dgvDash_MouseClick(object sender, MouseEventArgs e)
        {
            if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
            {
                string file = dgvDash.SelectedRows[0].Cells[0].Value.ToString();
                if (_GetFileCaseDetailsIndividualStatus(frmFile.projKey, frmFile.batchKey, file).Rows[0][0].ToString() == "N")
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (dgvDash.SelectedRows[0].Cells[0].Value.ToString() != "")
                        {
                            cmsDeeds.Show(Cursor.Position);
                        }
                    }
                }
            }
        }

        public DataTable _GetFileCaseDetailsIndividualStatus(string proj, string bundle, string fileName)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status from metadata_entry where proj_key = '" + proj + "' and batch_key = '" + bundle + "' and filename = '" + fileName + "'  ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void deleteDeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
            {
                string file = dgvDash.SelectedRows[0].Cells[0].Value.ToString();
                if (_GetFileCaseDetailsIndividualStatus(frmFile.projKey, frmFile.batchKey, file).Rows[0][0].ToString() == "N")
                {
                    DialogResult dr = MessageBox.Show(this, "Do you want to delete this file ? ", "CESC - Record Management ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        bool deletemeta = deleteMeta(frmFile.projKey, frmFile.batchKey, file);

                        if (deletemeta == true )
                        {

                            dgvDash.DataSource = null;

                            fileList(frmFile.projKey, frmFile.batchKey);


                            MessageBox.Show(this, "File deleted successfully ...", "CESC - Record Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }

                }
                else
                {
                    MessageBox.Show(this, "This file is proceed for further process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        public bool deleteMeta(string proj, string bundle, string fileName)
        {
            bool ret = false;
            if (ret == false)
            {
                _deleteMeta(fileName);

                ret = true;
            }
            return ret;
        }

        public bool _deleteMeta(string fileName)
        {
            string sqlStr = null;

            OdbcCommand sqlCmd = new OdbcCommand();

            bool retVal = false;
            string sql = string.Empty;

            sqlStr = "DELETE from metadata_entry WHERE proj_key = '" + frmFile.projKey + "' AND batch_key = '" + frmFile.batchKey + "' and filename = '" + fileName + "' ";
            //sqlCmd.Connection = sqlCon;
            //sqlCmd.Transaction = txn;
            //sqlCmd.CommandText = sqlStr;
            //int j = sqlCmd.ExecuteNonQuery();
            //if (j > 0)
            //{
            //    retVal = true;
            //}
            //else
            //{
            //    retVal = false;
            //}
            System.Diagnostics.Debug.Print(sqlStr);
            OdbcCommand cmd = new OdbcCommand(sqlStr, sqlCon);
            //cmd.Connection = sqlCon;
            //cmd.CommandText = sqlStr;
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
                //txn.Commit();
            }
            

            return retVal;
        }
    }
}
