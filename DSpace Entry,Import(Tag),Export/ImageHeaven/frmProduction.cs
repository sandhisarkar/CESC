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

namespace ImageHeaven
{
    public partial class frmProduction : Form
    {
        OdbcConnection sqlCon = null;

        public string stDate;
        public string endDate;
        public string stage = string.Empty;
        public string userId = string.Empty;

        public Credentials crd = new Credentials();

        public frmProduction()
        {
            InitializeComponent();
        }

        public frmProduction(OdbcConnection prmCon)
        {
            InitializeComponent();
            sqlCon = prmCon;
        }

        private void frmProduction_Load(object sender, EventArgs e)
        {
            stDate = DateTime.Now.ToString("yyyy-MM-dd");
            endDate = DateTime.Now.ToString("yyyy-MM-dd");

            dateTimePicker1.Text = stDate;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Value = Convert.ToDateTime(stDate.ToString());

            dateTimePicker2.Text = endDate;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Value = Convert.ToDateTime(endDate.ToString());

            grdStatus.DataSource = null;

        }

        private void FormatDataGridView()
        {
            //grdStatus.Columns[0].Visible = false;
            //grdStatus.Columns[1].Visible = false;

            //Set Autosize on for all the columns
            for (int i = 0; i < grdStatus.Columns.Count; i++)
            {
                grdStatus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void init()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntries(userId);

            grdStatus.DataSource = Dt;
            Dt.Columns.Add("File Count");
            Dt.Columns.Add("Image Count");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string pk = Dt.Rows[i][0].ToString();
                string bk = Dt.Rows[i][1].ToString();

                Dt.Rows[i]["File Count"] = _GetFileCount(pk,bk).Rows[0][0].ToString();
                Dt.Rows[i]["Image Count"] = _GetImageCount(pk, bk).Rows[0][0].ToString();
            }
            Dt.Columns.Remove("pk");
            Dt.Columns.Remove("bk");

            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntries(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct proj_code as pk, batch_key as bk, batch_code as 'Batch Code' from batch_master a " +
                         "where date_format(a.created_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.created_dttm,'%Y-%m-%d') <= '" + endDate + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetFileCount(string pk, string bk)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select Count(*) from metadata_entry " +
                         "where proj_key = '"+pk+"' and batch_key = '"+bk+"' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetImageCount(string pk, string bk)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select Count(*) from image_import " +
                         "where proj_key = '" + pk + "' and batch_key = '" + bk + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void deButton1_Click(object sender, EventArgs e)
        {
            grdStatus.DataSource = null;

            stDate = dateTimePicker1.Text;
            endDate = dateTimePicker2.Text;

            init();

            if (grdStatus.Rows.Count > 0)
            {
                deButton20.Enabled = true;
            }
        }

        private void deButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deButton20_Click(object sender, EventArgs e)
        {
            if (grdStatus.Rows.Count > 0)
            {
                deButton20.Enabled = false;

                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = false;

                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                worksheet.Name = "Production Report";

                worksheet.Cells[1, 2] = "Production Report";
                Range range44 = worksheet.get_Range("B1");
                range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();


                worksheet.Cells[3, 1] = "Time : " + dateTimePicker1.Text + " - " + dateTimePicker2.Text;
                Range range33 = worksheet.get_Range("A3");
                range33.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                Range range = worksheet.get_Range("A3", "A3");
                range.Borders.Color = ColorTranslator.ToOle(Color.Black);

                Range range1 = worksheet.get_Range("A5", "C5");
                range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                for (int i = 1; i < grdStatus.Columns.Count + 1; i++)
                {


                    Range range2 = worksheet.get_Range("A5", "C5");
                    range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    range2.EntireRow.AutoFit();
                    range2.EntireColumn.AutoFit();
                    worksheet.Cells[5, i] = grdStatus.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < grdStatus.Rows.Count; i++)
                {
                    for (int j = 0; j < grdStatus.Columns.Count; j++)
                    {
                        Range range3 = worksheet.Cells;
                        //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range3.EntireRow.AutoFit();
                        range3.EntireColumn.AutoFit();
                        worksheet.Cells[i + 6, j + 1] = grdStatus.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 6, j + 1].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    }

                }

                string namexls = "Production_Report_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xls";
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
    }
}
