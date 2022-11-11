using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.IO;
using NovaNet.Utils;
using NovaNet.wfe;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using LItems;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft;
using Microsoft.Office;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using DataTable = System.Data.DataTable;

namespace ImageHeaven
{
    public partial class frmBatchReport : Form
    {
        OdbcConnection sqlCon = null;

        public frmBatchReport()
        {
            InitializeComponent();
        }

        public frmBatchReport(OdbcConnection prmCon)
        {
            InitializeComponent();
            sqlCon = prmCon;
        }

        private void frmBatchReport_Load(object sender, EventArgs e)
        {
            populateProject();
            deButton20.Enabled = false;
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

                populateBundle();
            }
            else
            {
                cmbProject.DataSource = null;
                // cmbProject.Text = "";
                MessageBox.Show("Add one project first...");
                this.Close();
            }


        }

        private void populateBundle()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select a.batch_key, a.batch_name from batch_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and a.status >= 0";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbBundle.DataSource = dt;
                cmbBundle.DisplayMember = "batch_name";
                cmbBundle.ValueMember = "batch_key";
            }
            else
            {

                cmbBundle.Text = string.Empty;
                cmbBundle.DataSource = null;
                cmbBundle.DisplayMember = "";
                cmbBundle.ValueMember = "";
                cmbProject.Select();

            }
        }

        private DataSet ReadDatabase()
        {
            DataSet ds = new DataSet();
            DataSet dsdeed = new DataSet();
            int imgCount = 0;
            try
            {

                string sql = "select * from metadata_entry where proj_key = '" + cmbProject.SelectedValue.ToString() + "' and batch_key = '" + cmbBundle.SelectedValue.ToString() + "' ";


                OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
                odap.Fill(ds);
                //odap.Fill(dsdeed);
                ds.Tables[0].Columns.Add("Image Count");
                for(int i=0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["Image Count"] = _GetImageCount(cmbProject.SelectedValue.ToString(), cmbBundle.SelectedValue.ToString(), ds.Tables[0].Rows[i]["filename"].ToString()).Rows[0][0].ToString();
                }
                ds.Tables[0].Columns.Remove("proj_key");
                ds.Tables[0].Columns.Remove("batch_key");
                ds.Tables[0].Columns.Remove("sl_no");
                ds.Tables[0].Columns.Remove("item_no");
                ds.Tables[0].Columns.Remove("status");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                ds.Clear();
            }
            return ds;
        }

        public System.Data.DataTable _GetImageCount(string pk, string bk, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select Count(*) from image_import " +
                         "where proj_key = '" + pk + "' and batch_key = '" + bk + "' and filename ='"+file+"'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void cmdsearch_Click(object sender, EventArgs e)
        {
            deButton20.Enabled = false;

            if (cmbProject.Text != "" && cmbBundle.Text != "")
            {
                grdCsv.DataSource = null;
                grdCsv.DataSource = ReadDatabase().Tables[0];
                if (grdCsv.Rows.Count > 0)
                {
                    //FormatDataGridView();
                    deButton20.Enabled = true;
                    //for (int i = 0; i < grdCsv.Rows.Count; i++)
                    //{
                    //    //lstImage.Items.Add(ReadDatabase().Tables[0].Rows[i][1]);

                    //}
                }
                else
                {
                    deButton20.Enabled = false;
                }
            }
        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {
            populateBundle();
        }

        private void deButton20_Click(object sender, EventArgs e)
        {
            if (grdCsv.Rows.Count > 0)
            {
                deButton20.Enabled = false;

                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = false;

                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                worksheet.Name = "Batch Wise Report";

                worksheet.Cells[1, 6] = "Batch Wise Report";
                Range range44 = worksheet.get_Range("F1");
                range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();


                worksheet.Cells[3, 1] = "Batch Name : " + cmbBundle.Text;
                Range range33 = worksheet.get_Range("A3");
                range33.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                Range range = worksheet.get_Range("A3", "A3");
                range.Borders.Color = ColorTranslator.ToOle(Color.Black);

                Range range1 = worksheet.get_Range("A5", "K5");
                range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                for (int i = 1; i < grdCsv.Columns.Count + 1; i++)
                {


                    Range range2 = worksheet.get_Range("A5", "K5");
                    range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range2.EntireRow.AutoFit();
                    range2.EntireColumn.AutoFit();
                    worksheet.Cells[5, i] = grdCsv.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < grdCsv.Rows.Count; i++)
                {
                    for (int j = 0; j < grdCsv.Columns.Count; j++)
                    {
                        Range range3 = worksheet.Cells;
                        //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range3.EntireRow.AutoFit();
                        range3.EntireColumn.AutoFit();
                        worksheet.Cells[i + 6, j + 1] = grdCsv.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 6, j + 1].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    }

                }

                string namexls = "Batch_Wise_Report_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xls";
                string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                sfdUAT.Filter = "Xls files (*.xls)|*.xls";
                sfdUAT.FilterIndex = 2;
                sfdUAT.RestoreDirectory = true;
                sfdUAT.FileName = namexls;
                sfdUAT.ShowDialog();

                workbook.SaveAs(sfdUAT.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                app.Quit();

                deButton20.Enabled = false;
            }
        }

        private void deButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
