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
    public partial class frmEntry : Form
    {
        string name = frmMain.name;
        OdbcConnection sqlCon = null;
        public static bool _modeBool;
        //DataLayerDefs.Mode _mode = _modeBool;

        public static DataLayerDefs.Mode _mode =  DataLayerDefs.Mode._Edit;

        public static string projKey;
        public static string batchKey;
        public static string filename;


        public frmEntry()
        {
            InitializeComponent();
        }

        public frmEntry(OdbcConnection pCon,DataLayerDefs.Mode mode)
        {
            InitializeComponent();

            if(mode == DataLayerDefs.Mode._Add)
            {
                txtProject.Text = frmBatchSelect.projName;
                txtBatch.Text = frmBatchSelect.batchName;
                projKey = frmBatchSelect.projKey;
                batchKey = frmBatchSelect.batchKey;

                

                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;

                this.Text = "Data Entry : Add ";
                _mode = mode;
                cmbMonth.SelectedIndex = 0;
            }
            if(mode == DataLayerDefs.Mode._Edit)
            {
                txtProject.Text = frmFileSum.proj_name;
                txtBatch.Text = frmFileSum.batch_name;
                //projKey = frmBatchSelect.projKey;
                //batchKey = frmBatchSelect.batchKey;

                

                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;

                this.Text = "Data Entry : Edit ";
                _mode = mode;
            }
            sqlCon = pCon;
        }
        

        private void frmEntry_Load(object sender, EventArgs e)
        {
            populateSubCat();
            populateDocType();
            cmbMonth.SelectedIndex = 0;

            this.txtSubject.AutoCompleteCustomSource = GetSuggestions("metadata_entry", "sub_name");
            this.txtSubject.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.txtSubject.AutoCompleteSource = AutoCompleteSource.CustomSource;
            if (_mode == DataLayerDefs.Mode._Add)
            {
                txtLetterNo.Select();
            }

            if(_mode == DataLayerDefs.Mode._Edit)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select c.proj_key,c.batch_key from project_master a,batch_master b,metadata_entry c where a.proj_key = c.proj_key and a.proj_key = b.proj_code and b.batch_key = c.batch_key and a.proj_code = '" + frmFileSum.proj_name + "' and b.batch_code = '"+frmFileSum.batch_name+"'";

                OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
                odap.Fill(dt);

                projKey = dt.Rows[0][0].ToString();
                batchKey = dt.Rows[0][1].ToString();

                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();
                string sql1 = "select letter_no,issue_from,issue_to,sub_cat,sub_name,doc_type,issue_date,filename from metadata_entry where proj_key = '"+projKey+"' and batch_key = '"+batchKey+"' and filename = '"+frmFileSum.filename+"'";
                OdbcDataAdapter odap1 = new OdbcDataAdapter(sql1, sqlCon);
                odap1.Fill(dt1);

                txtLetterNo.Text = dt1.Rows[0][0].ToString();
                txtIssuedFrom.Text = dt1.Rows[0][1].ToString();
                txtIssuedTo.Text = dt1.Rows[0][2].ToString();
                cmbSubCat.Text = dt1.Rows[0][3].ToString();
                txtSubject.Text = dt1.Rows[0][4].ToString();
                cmbDocType.Text = dt1.Rows[0][5].ToString();
                string datefrmdt = dt1.Rows[0][6].ToString();
                txtYear.Text = datefrmdt.Substring(0, 4);
                cmbMonth.Text = datefrmdt.Substring(5,2);
                txtDate.Text = datefrmdt.Substring(8,2);
                filename = dt1.Rows[0][7].ToString();


                groupBox2.Select();
            }

        }

        public AutoCompleteStringCollection GetSuggestions(string tblName, string fldName)
        {
            AutoCompleteStringCollection x = new AutoCompleteStringCollection();
            string sql = "Select distinct " + fldName + " from " + tblName;
            DataSet ds = new DataSet();
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    x.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
                }
            }
            x.Add("Others");
            //x.Add("NA");
            return x;
        }
        private void populateSubCat()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select sub_cat_id, sub_cat_name from sub_cat_master ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbSubCat.DataSource = dt;
                cmbSubCat.DisplayMember = "sub_cat_name";
                cmbSubCat.ValueMember = "sub_cat_id";

            }
            else
            {
                cmbSubCat.Text = string.Empty;
                cmbSubCat.DataSource = null;
                cmbSubCat.DisplayMember = "";
                cmbSubCat.ValueMember = "";

            }
        }

        private void populateDocType()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select doc_id, doc_type from document_master ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbDocType.DataSource = dt;
                cmbDocType.DisplayMember = "doc_type";
                cmbDocType.ValueMember = "doc_id";

            }
            else
            {
                cmbDocType.Text = string.Empty;
                cmbDocType.DataSource = null;
                cmbDocType.DisplayMember = "";
                cmbDocType.ValueMember = "";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           // dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";


            string currDate = DateTime.Now.ToString("yyyy-MM-dd");
            //if (DateTime.Parse(dateTimePicker1.Text) <= DateTime.Parse(currDate))
            //{
            //    label7.ForeColor = Color.Black;
            //    label7.Visible = true;
            //    label7.Text = "When was the document issued";
            //}
            //else
            //{
            //    label7.ForeColor = Color.Red;
            //    label7.Visible = true;
            //    label7.Text = "You cannot select any date\n beyond Current Date";
            //    return;
            //}
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure?? Do you Want to Exit???", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //frmEntry_Load(sender, e);
                //txtLetterNo.Text = string.Empty;
                //txtIssuedFrom.Text = string.Empty;
                //txtIssuedTo.Text = string.Empty;
                //txtSubject.Text = string.Empty;
                
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validate() == true)
            {
                if(_mode == DataLayerDefs.Mode._Add)
                {
                    bool insertmeta = insertFn();
                    if (insertmeta == true)
                    {
                        MessageBox.Show(this, "Successfully Inserted...", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmEntry_Load(sender, e);
                        txtLetterNo.Text = string.Empty;
                        txtIssuedFrom.Text = string.Empty;
                        txtIssuedTo.Text = string.Empty;
                        txtSubject.Text = string.Empty;
                        //txtYear.Text = string.Empty;
                        //txtDate.Text = string.Empty;
                        txtLetterNo.Select();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", " ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (_mode == DataLayerDefs.Mode._Edit)
                {
                    bool updatemeta = updateFn();
                    if (updatemeta == true)
                    {
                        MessageBox.Show(this, "Successfully Inserted...", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Ooops!!! There is an Error - Record not Saved...", " ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("Please select all the parameter...", " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    //txtLetterNo.Focus();
            //    //txtLetterNo.Select();
            //}
        }

        public bool insertFn()
        {
            bool ret = false;
            if (ret == false)
            {
                _InsertMeta();

                ret = true;
            }
            return ret;
        }

        public bool updateFn()
        {
            bool ret = false;
            if (ret == false)
            {
                _UpdateMeta();

                ret = true;
            }
            return ret;
        }

        public bool _InsertMeta()
        {
            bool retVal = false;
            string sql = string.Empty;
            string sl_no;
            int item_no;
            string filenameAdd;
            string sqlcou = "select MAX(sl_no) from metadata_entry where proj_key = '" + frmBatchSelect.projKey + "' and batch_key = '" + frmBatchSelect.batchKey + "' and issue_from = '" + txtIssuedFrom.Text.ToUpper() + "' and issue_to = '" + txtIssuedTo.Text.ToUpper() + "' and issue_date = '" + txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcDataAdapter odap = new OdbcDataAdapter(sqlcou, sqlCon);
            odap.Fill(dt);
            string xyz = dt.Rows[0][0].ToString();
            if ((xyz == null) || (xyz == ""))
            {

                sl_no = "0";
                filenameAdd = txtIssuedFrom.Text + "_" + txtIssuedTo.Text + "_" + txtYear.Text +  cmbMonth.Text +  txtDate.Text;
            }
            else
            {
                int tmp_sl = Convert.ToInt32(dt.Rows[0][0].ToString());
                int tmp_sl_1 = tmp_sl + 1;
                sl_no = Convert.ToString(tmp_sl_1);
                filenameAdd = txtIssuedFrom.Text + "_" + txtIssuedTo.Text + "_" + txtYear.Text + cmbMonth.Text +  txtDate.Text + "_" + sl_no;
            }

            string sqlcouItem = "select Count(item_no) from metadata_entry where proj_key = '" + frmBatchSelect.projKey + "' and batch_key = '" + frmBatchSelect.batchKey + "' ";
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            OdbcDataAdapter odap1 = new OdbcDataAdapter(sqlcouItem, sqlCon);
            odap1.Fill(dt1);
            int max_item = Convert.ToInt32(dt1.Rows[0][0].ToString());

            if (max_item == 0)
            {
                item_no = 1;
            }
            else
            {
                int itemConv = max_item;
                int itemCounter = itemConv + 1;
                item_no = itemCounter;
            }


            sql = "insert into metadata_entry(proj_key,batch_key,sl_no,item_no,letter_no,issue_from,issue_to,sub_cat,sub_name,doc_type,issue_date,filename,created_by,created_dttm,status)" +
                    "values('" + frmBatchSelect.projKey + "', " +
                        "'" + frmBatchSelect.batchKey + "', " +
                        "'" + sl_no + "', " +
                        "'" + item_no + "', " +
                        "'" + txtLetterNo.Text.ToUpper() + "', " +
                        "'" + txtIssuedFrom.Text.ToUpper() + "', " +
                        "'" + txtIssuedTo.Text.ToUpper() + "','" + cmbSubCat.Text + "','" + txtSubject.Text + "','" + cmbDocType.Text + "','" + txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text + "','" + filenameAdd.ToUpper() + "','" + frmMain.name + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','N')";


            System.Diagnostics.Debug.Print(sql);
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }
            return retVal;
        }

        public bool _UpdateMeta()
        {
            bool retVal = false;
            string sql = string.Empty;
            string sl_no;
            string filenameEdit;

            string sqlcou = "select COUNT(*) from metadata_entry where proj_key = '" + projKey + "' and batch_key = '" + batchKey + "' and issue_from = '" + txtIssuedFrom.Text.ToUpper() + "' and issue_to = '" + txtIssuedTo.Text.ToUpper() + "' and issue_date = '" + txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text + "'";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcDataAdapter odap = new OdbcDataAdapter(sqlcou, sqlCon);
            odap.Fill(dt);

            int xyz = Convert.ToInt32(dt.Rows[0][0].ToString());
            
            if (xyz == 0)
            {
                    sl_no = "0";
                    filenameEdit = txtIssuedFrom.Text + "_" + txtIssuedTo.Text + "_" + txtYear.Text + cmbMonth.Text + txtDate.Text;
            }
            else
            {
                string sqlcou1 = "select sl_no from metadata_entry where proj_key = '" + projKey + "' and batch_key = '" + batchKey + "' AND filename = '"+filename+"'";
                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();
                OdbcDataAdapter odap1 = new OdbcDataAdapter(sqlcou1, sqlCon);
                odap1.Fill(dt1);
                string zyx = dt1.Rows[0][0].ToString();
                string fname = txtIssuedFrom.Text.ToUpper() + "_" + txtIssuedTo.Text.ToUpper() + "_" + txtYear.Text + cmbMonth.Text + txtDate.Text;
                if ((fname == filename))
                {
                    sl_no = "0";
                    filenameEdit = txtIssuedFrom.Text + "_" + txtIssuedTo.Text + "_" + txtYear.Text + cmbMonth.Text + txtDate.Text;
                }
                else
                {
                    int tmp_sl = Convert.ToInt32(dt1.Rows[0][0].ToString());
                    int tmp_sl_1 = tmp_sl + 1;
                    sl_no = Convert.ToString(tmp_sl_1);
                    filenameEdit = txtIssuedFrom.Text + "_" + txtIssuedTo.Text + "_" + txtYear.Text + cmbMonth.Text + txtDate.Text + "_" + sl_no;
                }
                
            }

            sql = "UPDATE metadata_entry SET sl_no = '" + sl_no + "',letter_no = '" + txtLetterNo.Text.ToUpper() + "',issue_from = '" + txtIssuedFrom.Text.ToUpper() + "',issue_to = '" + txtIssuedTo.Text.ToUpper() + "',sub_cat = '" + cmbSubCat.Text + "',sub_name= '" + txtSubject.Text + "',doc_type= '" + cmbDocType.Text + "',issue_date= '" + txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text + "',filename = '"+filenameEdit.ToUpper()+"' WHERE proj_key = '"+projKey+"' AND batch_key = '"+batchKey+"' AND filename ='"+filename+"'";
            System.Diagnostics.Debug.Print(sql);
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            if (cmd.ExecuteNonQuery() > 0)
            {
                retVal = true;
            }


            return retVal;
        }

        public bool validate()
        {
            bool retval = false;

            //if (txtLetterNo.Text == null || txtLetterNo.Text == "")
            //{
            //    //retval = true;
            //    retval = false;
            //    MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtLetterNo.Focus();
            //    txtLetterNo.Select();
            //    return retval;
            //}
            //else
            //{
            //    retval = true;
            //    //MessageBox.Show("", "You cannot leave this field blank..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    //txtLetterNo.Focus();
            //    //txtLetterNo.Select();
            //    //return retval;
            //}
            if (txtIssuedFrom.Text == null || txtIssuedFrom.Text == "")
            {

                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIssuedFrom.Focus();
                txtIssuedFrom.Select();
                return retval;
            }
            else
            {
                retval = true;
            }
            if (txtIssuedTo.Text == null || txtIssuedTo.Text == "")
            {

                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIssuedTo.Focus();
                txtIssuedTo.Select();
                return retval;
            }
            else
            {
                retval = true;
            }
            if (txtSubject.Text == null || txtSubject.Text == "")
            {

                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSubject.Focus();
                txtSubject.Select();
                return retval;
            }
            else
            {
                retval = true;
            }
            //if (dateTimePicker1.Text == null || dateTimePicker1.Text == " ")
            //{

            //    retval = false;
            //    MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dateTimePicker1.Focus();
            //    dateTimePicker1.Select();
            //    return retval;
            //}
            //else
            //{
            //    retval = true;
            //}
            if (txtYear.Text == null || txtYear.Text == "")
            {
                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYear.Focus();
                txtYear.Select();
                return retval;
            }
            else if (txtYear.Text.Length != 4)
            {
                retval = false;
                label7.ForeColor = Color.Red;
                txtYear.Focus();

                label7.Text = "You Cannot Type any invalid\n Year !";
                txtYear.Select();
                return retval;
            }
            else if (txtYear.Text.Substring(0, 1) == "0")
            {
                retval = false;
                label7.ForeColor = Color.Red;
                txtYear.Focus();

                label7.Text = "You Cannot start with 0\n ";
                txtYear.SelectAll();
                return retval;
            }
            else
            {
                retval = true;
            }
            if (cmbMonth.Text == null || cmbMonth.Text == "")
            {
                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMonth.Focus();
                cmbMonth.Select();
                return retval;
            }
            else
            {
                retval = true;
            }
            if (txtDate.Text == null || txtDate.Text == "")
            {
                retval = false;
                MessageBox.Show("You cannot leave this field blank..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                txtDate.Select();
                return retval;
            }
            else if (txtDate.Text.Length != 2)
            {
                retval = false;
                label7.ForeColor = Color.Red;
                txtDate.Focus();

                label7.ForeColor = Color.Red;
                label7.Text = "You Cannot Type any invalid\n Date !";
                txtDate.Select();
                return retval;
            }
            else
            {
                retval = true;
            }
            string currDate = DateTime.Now.ToString("yyyy-MM-dd");
            //if (DateTime.Parse(dateTimePicker1.Text) <= DateTime.Parse(currDate))
            //{
            //    retval = true;
            //}
            //else
            //{
            //    retval = false;
            //    MessageBox.Show("You Cannot Select any date beyond Current Date...", "Please select a valid date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dateTimePicker1.Focus();
            //    dateTimePicker1.Select();
            //    return retval;
            //}
            string isDate = txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text;
            if (DateTime.Parse(txtYear.Text+"-"+cmbMonth.Text+"-"+txtDate.Text) <= DateTime.Parse(currDate))
            {
                retval = true;
            }
            else
            {
                retval = false;
                MessageBox.Show("You Cannot Select any date beyond Current Date...", "Please select a valid date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //dateTimePicker1.Focus();
                //dateTimePicker1.Select();
                return retval;
            }
            
            //string sqlcou = "select * from metadata_entry where proj_key = '" + frmBatchSelect.projKey + "' and batch_key = '" + frmBatchSelect.batchKey + "' and letter_no = '" + txtLetterNo.Text.ToUpper() + "' and issue_date = '" + txtYear.Text + "-" + cmbMonth.Text + "-" + txtDate.Text + "'";
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //OdbcDataAdapter odap = new OdbcDataAdapter(sqlcou, sqlCon);
            //odap.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    retval = false;
            //    MessageBox.Show("Letter No and Issue Date is same...", "Please try again !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtLetterNo.Focus();
            //    txtLetterNo.Select();
            //    return retval;
            //}
            //else
            //{
            //    retval = true;
            //}

            return retval;
        }

        private void txtLetterNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtLetterNo_TextChanged(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";

            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtLetterNo.Text, "[\'\"]"))
                {

                    txtLetterNo.Text = txtLetterNo.Text.Remove(txtLetterNo.Text.Length - 1);
                    label1.ForeColor = Color.Red;
                    txtLetterNo.Focus();
                    txtLetterNo.SelectAll();

                    char x = '"';
                    label1.Text = "You Cannot Type Some keys, \nLike: ' and " + x.ToString() + " in this field";
                    return;

                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtLetterNo_TabIndexChanged(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtLetterNo_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtLetterNo_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void txtLetterNo_Leave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void txtLetterNo_Click(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtLetterNo_Enter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtIssuedFrom_TextChanged(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";

            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtIssuedFrom.Text, "[\'\"]"))
                {

                    txtIssuedFrom.Text = txtIssuedFrom.Text.Remove(txtIssuedFrom.Text.Length - 1);
                    label2.ForeColor = Color.Red;
                    txtIssuedFrom.Focus();
                    txtIssuedFrom.SelectAll();

                    char x = '"';
                    label2.Text = "You Cannot Type Some keys, \nLike: ' and " + x.ToString() + " in this field";
                    return;

                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtIssuedFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtIssuedFrom_Enter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtIssuedFrom_TabIndexChanged(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtIssuedFrom_MouseClick(object sender, MouseEventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtIssuedFrom_Click(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtLetterNo_MouseClick(object sender, MouseEventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.Visible = true;
            label1.Text = "Reference No. of the document,\n Eg- Letter No./ Order No.";
        }

        private void txtIssuedFrom_Leave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void txtIssuedFrom_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void txtIssuedFrom_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Visible = true;
            label2.Text = "Who issued the document";
        }

        private void txtIssuedTo_TextChanged(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";

            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtIssuedTo.Text, "[\'\"]"))
                {

                    txtIssuedTo.Text = txtIssuedTo.Text.Remove(txtIssuedTo.Text.Length - 1);
                    label3.ForeColor = Color.Red;
                    txtIssuedTo.Focus();
                    txtIssuedTo.SelectAll();

                    char x = '"';
                    label3.Text = "You Cannot Type Some keys, \nLike: ' and " + x.ToString() + " in this field";
                    return;

                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtIssuedTo_TabIndexChanged(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void txtIssuedTo_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void txtIssuedTo_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void txtIssuedTo_MouseClick(object sender, MouseEventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void txtIssuedTo_Leave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void txtIssuedTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void txtIssuedTo_Enter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void txtIssuedTo_Click(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
            label3.Visible = true;
            label3.Text = "To whom the document issued";
        }

        private void cmbSubCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_TabIndexChanged(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_TextChanged(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_KeyDown(object sender, KeyEventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_KeyUp(object sender, KeyEventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_MouseHover(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_Leave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void cmbSubCat_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void cmbSubCat_Enter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_MouseClick(object sender, MouseEventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_Click(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void cmbSubCat_CursorChanged(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Select proper Subject Category \nfrom the dropdown";
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";

            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtSubject.Text, "[\'\"]"))
                {

                    txtSubject.Text = txtSubject.Text.Remove(txtSubject.Text.Length - 1);
                    label5.ForeColor = Color.Red;
                    txtSubject.Focus();
                    txtSubject.SelectAll();

                    char x = '"';
                    label5.Text = "You Cannot Type Some keys, \nLike: ' and " + x.ToString() + " in this field";
                    return;

                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtSubject_TabIndexChanged(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void txtSubject_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void txtSubject_MouseHover(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void txtSubject_MouseClick(object sender, MouseEventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void txtSubject_KeyPress(object sender, KeyPressEventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void txtSubject_Enter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void txtSubject_Click(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label5.Visible = true;
            label5.Text = "Subject of the document";
        }

        private void cmbDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_TextChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_TabIndexChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void cmbDocType_MouseHover(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_MouseClick(object sender, MouseEventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_Leave(object sender, EventArgs e)
        {
            label6.Visible = false;

            //dateTimePicker1.Value = DateTime.Today;


        }

        private void cmbDocType_KeyUp(object sender, KeyEventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_KeyPress(object sender, KeyPressEventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_Enter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_KeyDown(object sender, KeyEventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void cmbDocType_CursorChanged(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            label6.Visible = true;
            label6.Text = "Select proper document type\n from the dropdown";
        }

        private void dateTimePicker1_TabIndexChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

        }

        private void dateTimePicker1_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void dateTimePicker1_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void dateTimePicker1_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = false;
        }

        private void dateTimePicker1_KeyUp(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
            //dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

           // dateTimePicker1.Value = DateTime.Today;

        }

        private void dateTimePicker1_CursorChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";

            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtYear.Text, "[^0-9]"))
                {

                    txtYear.Text = txtYear.Text.Remove(txtYear.Text.Length - 1);
                    label7.ForeColor = Color.Red;
                    txtYear.Focus();
                    txtYear.SelectAll();

                    char x = '"';
                    label7.Text = "You Cannot Type any alphabets\n or special charecters";
                    return;

                }
                else if (txtYear.Text.Length != 4)
                {
                    label7.ForeColor = Color.Red;
                    txtYear.Focus();

                    label7.Text = "You Cannot Type any invalid\n Year !";
                    //txtYear.Select();
                    return;
                }
                else if (txtYear.Text.Substring(0,1) == "0")
                {
                    label7.ForeColor = Color.Red;
                    txtYear.Focus();

                    label7.Text = "You Cannot start with 0\n ";
                    //txtYear.SelectAll();
                    return;
                }
                else
                {
                    label7.ForeColor = Color.Black;
                    label7.Visible = true;
                    label7.Text = "When was the document issued";
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {

            if (txtYear.Text.Length != 4)
            {
                label7.ForeColor = Color.Red;
                txtYear.Focus();

                label7.Text = "You Cannot Type any invalid\n Year !";
                //txtYear.Select();
                return;
            }
            else if (txtYear.Text.Substring(0, 1) == "0")
            {
                label7.ForeColor = Color.Red;
                txtYear.Focus();

                label7.Text = "You Cannot start with 0\n ";
                //txtYear.SelectAll();
                return;
            }
            else
            {
                label7.ForeColor = Color.Black;
                label7.Visible = true;
                label7.Text = "When was the document issued";
            }

        }

        private void txtYear_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void txtYear_Click(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_Enter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_MouseClick(object sender, MouseEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtYear_TabIndexChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }


        private void cmbMonth_TabIndexChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_TextChanged(object sender, EventArgs e)
        {
            if (cmbMonth.Text == "" || cmbMonth.Text == null)
            {
                label7.ForeColor = Color.Red;
                label7.Visible = true;
                label7.Text = "You cannot leave this\n field blank ";
                cmbMonth.Select();
                return;
            }
            else
            {
                label7.ForeColor = Color.Black;
                label7.Visible = true;
                label7.Text = "When was the document issued";
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.Text == "" || cmbMonth.Text == null)
            {
                label7.ForeColor = Color.Red;
                label7.Visible = true;
                label7.Text = "You cannot leave this\n field blank ";
                cmbMonth.Select();
                return;
            }
            else
            {
                label7.ForeColor = Color.Black;
                label7.Visible = true;
                label7.Text = "When was the document issued";
            }
        }

        private void cmbMonth_Click(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_CursorChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_Enter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_KeyDown(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_KeyPress(object sender, KeyPressEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_KeyUp(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_Leave(object sender, EventArgs e)
        {
            if (cmbMonth.Text == "" || cmbMonth.Text == null)
            {
                label7.ForeColor = Color.Red;
                label7.Visible = true;
                label7.Text = "You cannot leave this\n field blank ";
                cmbMonth.Select();
                return;
            }
            else
            {
                label7.ForeColor = Color.Black;
                label7.Visible = true;
                label7.Text = "When was the document issued";
            }
        }

        private void cmbMonth_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void cmbMonth_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void cmbMonth_MouseClick(object sender, MouseEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_Enter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_Click(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_KeyUp(object sender, KeyEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_Leave(object sender, EventArgs e)
        {
            if (txtDate.Text.Length != 2)
            {
                label7.ForeColor = Color.Red;
                txtDate.Focus();

                label7.Text = "You Cannot Type any invalid\n Date !";
                //txtYear.Select();
                return;
            }
            else
            {
                label7.ForeColor = Color.Black;
                label7.Visible = true;
                label7.Text = "When was the document issued";
            }
        }

        private void txtDate_MouseClick(object sender, MouseEventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;
        }

        private void txtDate_TabIndexChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label7.Visible = true;
            label7.Text = "When was the document issued";
            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(txtDate.Text, "[^0-9]"))
                {

                    txtDate.Text = txtDate.Text.Remove(txtDate.Text.Length - 1);
                    label7.ForeColor = Color.Red;
                    txtDate.Focus();
                    txtDate.SelectAll();

                    char x = '"';
                    label7.Text = "You Cannot Type any alphabets\n or special charecters";
                    return;

                }
                else if (txtDate.Text.Length != 2)
                {
                    label7.ForeColor = Color.Red;
                    txtDate.Focus();

                    label7.ForeColor = Color.Red;
                    label7.Text = "You Cannot Type any invalid\n Date !";
                    //txtYear.Select();
                    return;
                }
                else
                {
                    label7.ForeColor = Color.Black;
                    label7.Visible = true;
                    label7.Text = "When was the document issued";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtBatch_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelBatch_Click(object sender, EventArgs e)
        {

        }

        private void txtProject_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelProject_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelIssuedDate_Click(object sender, EventArgs e)
        {

        }

        private void labelDocType_Click(object sender, EventArgs e)
        {

        }

        private void labelSubject_Click(object sender, EventArgs e)
        {

        }

        private void labelSubCat_Click(object sender, EventArgs e)
        {

        }

        private void labelIssuedTo_Click(object sender, EventArgs e)
        {

        }

        private void labelIssuedFrom_Click(object sender, EventArgs e)
        {

        }

        private void labelLetter_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
