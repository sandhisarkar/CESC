using System;
using System.Drawing;
using System.Windows.Forms;
using NovaNet.Utils;
using NovaNet.wfe;
using LItems;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Runtime.InteropServices;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.OCRProcessor;
using Syncfusion.OCRProcessor.Interop;

namespace ImageHeaven
{
    public partial class frmExport : Form
    {
        public static string temp;
        MemoryStream stateLog;
        byte[] tmpWrite;
        NovaNet.Utils.dbCon dbcon;
        OdbcConnection sqlCon = null;
        eSTATES[] state;
        wfeProject tmpProj = null;
        SqlConnection sqls = null;
        DataSet ds = null;
        DataSet dsexport = new DataSet();
        string batchCount;
        
        wfeBox box = null;
        string sqlFileName = null;
        CtrlPolicy ctrPol = null;
        //		CtrlPolicy ctrlPolicy = null;
        wfePolicy policy = null;
        wfePolicy wPolicy = null;
        CtrlPolicy pPolicy = null;
        CtrlImage pImage = null;
        wfeImage wImage = null;
        wfeBatch wBatch = null;
        private udtPolicy policyData = null;
        StreamWriter sw;
        StreamWriter expLog;
        FileorFolder exportFile;
        string error = null;
        string sqlIp = null;
        string exportPath = null;
        string globalPath = string.Empty;
        string[] imageName;
        Credentials crd = new Credentials();
        private long expImageCount = 0;
        private long expPolicyCount = 0;
        private CtrlBox pBox = null;
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);
        public Imagery img;
        private ImageConfig config = null;

        public frmExport()
        {
            InitializeComponent();
        }
        public frmExport(OdbcConnection prmCon, Credentials prmCrd)
        {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            this.Text = "Export: ";
            dbcon = new NovaNet.Utils.dbCon();
            sqlCon = prmCon;
            crd = prmCrd;
            exMailLog.SetNextLogger(exTxtLog);
            img = IgrFactory.GetImagery(Constants.IGR_CLEARIMAGE);
            ReadINI();
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void ReadINI()
        {
            string iniPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Configuration.ini";
            if (File.Exists(iniPath))
            {
                NovaNet.Utils.INIFile ini = new NovaNet.Utils.INIFile();
                sqlIp = ini.ReadINI("SQLSERVERIP", "IP", "", iniPath);
                sqlIp = sqlIp.Replace("\0", "").Trim();
                exportPath = ini.ReadINI("EXPORTPATH", "SQLDBEXPORTPATH", "", iniPath);
                exportPath = exportPath.Replace("\0", "").Trim();
            }
        }
        
        private void frmExport_Load(object sender, EventArgs e)
        {
            populateProject();
            populateBatch();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
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
                cmbProject.Select();
            }
            else
            {
                cmbProject.Text = "";
                MessageBox.Show("Add one project first...");

            }

        }

        private void populateBatch()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = "select a.batch_key, a.batch_name from batch_master a, project_master b where a.proj_code = b.proj_key and a.status = '2' and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "'";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                cmbBatch.DataSource = dt;
                cmbBatch.DisplayMember = "batch_name";
                cmbBatch.ValueMember = "batch_key";
                
            }
            else
            {
                //cmbBatch.Text = "";
                //MessageBox.Show("No Batch found for this project...","Add Batch");
                cmbBatch.Text = string.Empty;
                cmbBatch.DataSource = null;
                cmbBatch.DisplayMember = "";
                cmbBatch.ValueMember = "";
                cmbProject.Select();
                
            }

        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {
            populateBatch();
        }

        
        
        public System.Data.DataSet GetAllBatchRun(string proj_key, string batch_key)
        {
            string sqlStr = null;

            sqlStr = "select filename,proj_key,batch_key from metadata_entry where proj_key = '" + proj_key + "' and batch_key = '" + batch_key + "' and status = 'Imported' order by item_no";

            OdbcDataAdapter odap = new OdbcDataAdapter(sqlStr, sqlCon);
            
            DataSet projDs = new DataSet();
            try
            {
                odap = new OdbcDataAdapter(sqlStr, sqlCon);
                odap.Fill(projDs);
            }
            catch (Exception ex)
            {
                odap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                //exMailLog.Log(ex, this);
            }
            return projDs;
        }

       

       
       
        public class DeedImageDetails
        {
            public string DistrictCode { get; set; }
            public string RoCode { get; set; }
            public string DeedNumber { get; set; }
            public string Book { get; set; }
            public string DeedYear { get; set; }
            public string DeedImage { get; set; }
        }

        public DataSet GetAllExportedImage(string proj_key, string batch_key, string filename)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            sqlStr = "select page_index_name,status,page_name,doc_type from image_import " +
                    " where proj_key ='" + proj_key + "'" +
                " and batch_key ='" + batch_key + "' " +
                " and filename ='" + filename + "' order by serial_no";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

            }
            return dsImage;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            
        }


        public System.Data.DataTable GetExportableDeed()
        {
            string sqlStr = null;
            System.Data.DataTable dsBox = new System.Data.DataTable();
            OdbcDataAdapter sqlAdap = null;


            sqlStr = "select b.filename as File_name,a.proj_key,a.batch_key,a.status,b.status from image_import a,metadata_entry b where a.proj_key = b.proj_key and a.batch_key = b.batch_key and b.filename = '" + temp + "'  ";

            sqlStr = sqlStr + " order by b.filename";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

            }

            return dsBox;
        }

        private void dgvbatch_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvbatch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvbatch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvbatch_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {

        }

        private void cmbBatch_Leave(object sender, EventArgs e)
        {
            PopulateBatch();
        }
        private void PopulateBatch()
        {
            DataSet ds = new DataSet();

            tmpProj = new wfeProject(sqlCon);
            //cmbProject.Items.Add("Select");
            ds = GetAllBatchRun(cmbProject.SelectedValue.ToString(), cmbBatch.SelectedValue.ToString());
            dgvbatch.DataSource = ds.Tables[0];
            dgvbatch.Columns[0].Width = 25;
            dgvbatch.Columns[1].Width = 160;
            dgvbatch.Columns[1].ReadOnly = true;
            dgvbatch.Columns[0].Visible = false;
            dgvbatch.Columns[2].Visible = false;
            dgvbatch.Columns[3].Visible = false;
        }

        private void dgvbatch_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            populateDeeds(dgvbatch.CurrentRow.Cells[2].Value.ToString(), dgvbatch.CurrentRow.Cells[3].Value.ToString(), dgvbatch.CurrentRow.Cells[1].Value.ToString());
        }
        public void cleargrid()
        {
            dgvexport.DataSource = null;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            dgvPlot.DataSource = null;
            dgvKhatian.DataSource = null;
            if (dgvexport.Columns.Contains("Status"))
            {
                dgvexport.Columns.Remove("Status");
            }
            lbl.Text = "";
            progressBar1.Value = 0;
            txtMsg.Text = "";
            tblExp.SelectedTab = tabPage1;
        }
        private void populateDeeds(string proj_key, string batch_key,string file)
        {
            cleargrid();
            string batchKey = null;
            int holdDeed = 0;
            dbcon = new NovaNet.Utils.dbCon();

            //string file = dgvbatch.CurrentRow.Cells[1].Value.ToString();

            batchKey = batch_key;
            dsexport = GetExportableDeed(file);

            if (dsexport.Tables[0].Rows.Count > 0)
            {
                dgvexport.DataSource = dsexport.Tables[0];
                dgvexport.Columns[0].Width = 110;
                dgvexport.Columns[1].Visible = false;
                dgvexport.Columns[2].Visible = false;
                dgvexport.Columns[3].Visible = false;
                dgvexport.Columns[4].Visible = true;
                //dgvexport.Columns[5].Visible = false;
            }
            

            
            lbldeedCount.Text = dgvexport.Rows.Count.ToString();
            
            // btnExport.Enabled = true;
        }
        public DataSet GetExportableDeed(string file)
        {
            string sqlStr = null;
            DataSet dsBox = new DataSet();
            OdbcDataAdapter sqlAdap = null;

            sqlStr = "select Distinct(b.filename) as File_Name,a.proj_key,a.batch_key,a.status,b.status as 'Export Status' from image_import a,metadata_entry b where a.proj_key = b.proj_key and a.batch_key = b.batch_key and a.proj_key=" + cmbProject.SelectedValue.ToString() + " and a.batch_key=" + cmbBatch.SelectedValue.ToString() + " and b.filename = '"+file+"' ";

            sqlStr = sqlStr + " order by b.filename";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsBox);
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();
                stateLog = new MemoryStream();
                tmpWrite = new System.Text.ASCIIEncoding().GetBytes(sqlStr + "\n");
                stateLog.Write(tmpWrite, 0, tmpWrite.Length);
                //exMailLog.Log(ex, this);
            }
            return dsBox;
        }

        private void dgvbatch_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dgvbatch.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvbatch_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                PopulateSelectedBatchCount();
        }
        private void PopulateSelectedBatchCount()
        {
            int StatusStep = 0;
            try
            {
                if (dgvbatch.Rows.Count > 0)
                {
                    for (int x = 0; x < dgvbatch.Rows.Count; x++)
                    {
                        if (Convert.ToBoolean(dgvbatch.Rows[x].Cells[0].Value))
                        {
                            StatusStep = StatusStep + 1;
                        }

                    }
                    lblBatchSelected.Text = StatusStep.ToString();
                }
            }
            catch (Exception ex)
            {
                lblBatchSelected.Text = "0";
            }
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            try
            {
                btnExport.Enabled = false;



                string batchPath = string.Empty;
                string batchName = string.Empty;
                string resultMsg = "Hold Deeds" + "\r\n";
                DataTable deedEx = new DataTable();
                DataTable NameEx = new DataTable();
                DataTable PropEx = new DataTable();
                DataTable CSVPropEx = new DataTable();
                DataTable CSVPropEx1 = new DataTable();
                DataTable PlotEx = new DataTable();
                DataTable KhatianEx = new DataTable();
                string expFolder = "C:\\";
                bool isDeleted = false;
                int MaxExportCount = 0;
                int StatusStep = 0;
                config = new ImageConfig(ihConstants.CONFIG_FILE_PATH);
                //expFolder = config.GetValue(ihConstants.EXPORT_FOLDER_SECTION, ihConstants.EXPORT_FOLDER_KEY).Trim();

                System.Text.StringBuilder Builder1 = new System.Text.StringBuilder();
                Builder1.Append(PropEx.Rows.Count.ToString());
                Builder1.Append(",");

                //int len = expFolder.IndexOf('\0');
                //expFolder = expFolder.Substring(0, len);
                List<DeedImageDetails> dList = new List<DeedImageDetails>();


                lblFinalStatus.Text = "Please wait while Exporting....  ";
                Application.DoEvents();


                if (dgvbatch.Rows.Count > 0)
                {
                    StatusStep = dgvbatch.Rows.Count;
                    progressBar2.Value = 0;
                    progressBar1.Value = 0;
                    int step = 100 / StatusStep;
                    progressBar2.Step = step;

                    for (int z = 0; z < dgvbatch.Rows.Count; z++)
                    {
                        dgvexport.DataSource = null;
                        dataGridView1.DataSource = null;
                        dataGridView2.DataSource = null;
                        dataGridView3.DataSource = null;
                        dgvKhatian.DataSource = null;
                        dgvPlot.DataSource = null;
                        deedEx.Clear();
                        NameEx.Clear();
                        CSVPropEx.Clear();
                        CSVPropEx1.Clear();
                        PlotEx.Clear();
                        KhatianEx.Clear();
                        string file = dgvbatch.Rows[z].Cells[1].Value.ToString();
                        populateDeeds(dgvbatch.Rows[z].Cells[2].Value.ToString(), dgvbatch.Rows[z].Cells[3].Value.ToString(), dgvbatch.Rows[z].Cells[1].Value.ToString());
                       // MaxExportCount = wPolicy.getMaxExportCount(cmbProject.SelectedValue.ToString(), dgvbatch.Rows[z].Cells[2].Value.ToString());

                        if (dgvexport.Rows.Count > 0)
                        {
                            for (int i = 0; i < dgvexport.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dgvexport.Rows[i].Cells["status"].Value.ToString()) == 30 || Convert.ToInt32(dgvexport.Rows[i].Cells["status"].Value.ToString()) == 21)
                                {
                                    dgvexport.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    MessageBox.Show("There is some Problem in one or more deeds, Please Check and Retry..., Export Failed");
                                    btnExport.Enabled = true;
                                    return;
                                }
                            }
                        }

                        if (dgvexport.Rows.Count > 0)
                        {
                            Application.DoEvents();
                            dgvbatch.Rows[z].DefaultCellStyle.BackColor = Color.GreenYellow;
                            int i1 = 100 / dsexport.Tables[0].Rows.Count;
                            progressBar1.Step = i1;
                            progressBar1.Increment(i1);
                            Application.DoEvents();
                            wBatch = new wfeBatch(sqlCon);
                            batchPath = wBatch.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(dgvbatch.Rows[z].Cells[3].Value.ToString()));
                            batchPath = batchPath + "\\1\\Export";


                            if (!Directory.Exists(expFolder + "\\Export\\" + cmbBatch.Text))
                            {
                                Directory.CreateDirectory(expFolder + "\\Export\\" + cmbBatch.Text);
                            }

                            for (int x = 0; x < dsexport.Tables[0].Rows.Count; x++)
                            {
                                //if (dsexport.Tables[0].Rows[x][11].ToString() != "Y")
                                //{
                                DeedImageDetails imgDetails = new DeedImageDetails();
                                wfeImage tmpBox = new wfeImage(sqlCon);
                                DataSet dsimage = new DataSet();
                                Application.DoEvents();
                                lbl.Text = "Exporting :" + dgvbatch.Rows[z].Cells[1].Value.ToString();
                                Application.DoEvents();
                                string aa = wBatch.GetPath(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(dgvbatch.Rows[z].Cells[3].Value.ToString()));
                                sqlFileName = dsexport.Tables[0].Rows[x][0].ToString();
                                int index = sqlFileName.IndexOf('[');
                                sqlFileName = sqlFileName.ToString();
                                batchName = sqlFileName.ToString();
                                batchName = dsexport.Tables[0].Rows[x][0].ToString() ;
                                sqlFileName = sqlFileName + dsexport.Tables[0].Rows[x][0].ToString() + ".mdf";
                                dsimage = GetAllExportedImage(dsexport.Tables[0].Rows[x][1].ToString(), dsexport.Tables[0].Rows[x][2].ToString(), dsexport.Tables[0].Rows[x][0].ToString());
                                imageName = new string[dsimage.Tables[0].Rows.Count];
                                string IMGName = dsexport.Tables[0].Rows[x][0].ToString();
                                //string IMGName1 = IMGName.Split(new char[] { '[', ']' })[1];
                                //IMGName = IMGName.Replace("[", "");
                                //IMGName = IMGName.Replace("]", "");
                                string fileName = dsexport.Tables[0].Rows[x][0].ToString();
                                if (dsimage.Tables[0].Rows.Count > 0)
                                {
                                    for (int a = 0; a < dsimage.Tables[0].Rows.Count; a++)
                                    {
                                      
                                        //imageName[a] = dsexport.Tables[0].Rows[x][4].ToString() + "\\QC" + "\\" + dsimage.Tables[0].Rows[a]["page_name"].ToString();
                                        imageName[a] = aa + "\\"+ fileName + "\\Scan" + "\\" + dsimage.Tables[0].Rows[a]["page_name"].ToString();
                                    }
                                    if (imageName.Length != 0)
                                    {
                                        if (Directory.Exists(expFolder + "\\Export\\" + cmbBatch.Text ) && isDeleted == false)
                                        {
                                            Directory.Delete(expFolder + "\\Export\\" + cmbBatch.Text , true);

                                        }
                                        if (!Directory.Exists(expFolder + "\\Export\\" + cmbBatch.Text ))
                                        {
                                            Directory.CreateDirectory(expFolder + "\\Export\\" + cmbBatch.Text );
                                            isDeleted = true;
                                        }

                                        ////sumit for export problem
                                        //for (int i = 0; i < imageName.Length; i++)
                                        //{
                                        //    if (File.Exists(imageName[i]))
                                        //    {

                                        //    }
                                        //    else
                                        //    {
                                        //        MessageBox.Show("There is a problem in " + imageName[i]);
                                        //        return;
                                        //    }

                                        //}

                                        expFolder = "C:\\";

                                        if (img.TifToPdf(imageName, 80, expFolder + "\\Export\\" + cmbBatch.Text + "\\" + IMGName + ".pdf") == true)
                                        {
                                            string pdf_path = expFolder + "\\Export\\" + cmbBatch.Text + "\\" + fileName + ".pdf";
                                            file = fileName + ".pdf";
                                            string dirname = Path.GetDirectoryName(pdf_path);
                                            //PdfDocument document = new PdfDocument(PdfPage.PAGE.ToPdf,PdfImage.BEST_COMPRESSION);
                                            PdfReader pdfReader = new PdfReader(pdf_path);
                                            int noofpages = pdfReader.NumberOfPages;

                                            List<string> fileNames = new List<string>();

                                            iTextSharp.text.Document document = new iTextSharp.text.Document();

                                            //ocr directory create
                                            string dirEx = dirname + "\\OCR";
                                            if (!Directory.Exists(dirEx))
                                            {
                                                Directory.CreateDirectory(dirEx);
                                            }

                                            //split pdf
                                            for (int i = 0; i < noofpages; i++)
                                            {
                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdf_path);
                                                    Syncfusion.Pdf.PdfDocument documentPage = new Syncfusion.Pdf.PdfDocument();
                                                    documentPage.ImportPage(loadedDocument, i);
                                                    documentPage.Save(dirEx + "\\OCR_" + i + ".pdf");
                                                    string filenameNew = dirEx + "\\OCR_" + i + ".pdf";
                                                    //documentPage.Close();
                                                    documentPage.Close(true);
                                                    //documentPage.Dispose();
                                                    //loadedDocument.Close();
                                                    loadedDocument.Close(true);
                                                    //loadedDocument.Dispose();
                                                    documentPage.EnableMemoryOptimization = true;
                                                    loadedDocument.EnableMemoryOptimization = true;
                                                    fileNames.Add(filenameNew);
                                                    //lstImage.Items.Add(filenameNew);

                                                    ms.Close();

                                                    GC.Collect();
                                                    GC.WaitForPendingFinalizers();
                                                    GC.Collect();

                                                    Application.DoEvents();
                                                }
                                                Application.DoEvents();
                                            }
                                            string expath = Path.GetDirectoryName(Application.ExecutablePath);
                                            //ocr
                                            try
                                            {
                                                System.IO.DirectoryInfo di3 = new DirectoryInfo(dirEx);
                                                foreach (string filename in fileNames)
                                                {
                                                    Application.DoEvents();
                                                    string xyz = filename;
                                                    //PdfLoadedDocument loadedDocument = new PdfLoadedDocument(pdf_path);
                                                    //Syncfusion.Pdf.PdfDocument documentPage = new Syncfusion.Pdf.PdfDocument();
                                                    //documentPage.ImportPage(loadedDocument, i);
                                                    using (OCRProcessor oCR = new OCRProcessor(expath + "\\TesseractBinaries\\3.02\\"))
                                                    {
                                                        try
                                                        {

                                                            PdfLoadedDocument pdfLoadedDocument = new PdfLoadedDocument(xyz);

                                                            oCR.Settings.Language = Syncfusion.OCRProcessor.Languages.English;

                                                            oCR.PerformOCR(pdfLoadedDocument, expath + "\\tessdata\\", true);

                                                            pdfLoadedDocument.EnableMemoryOptimization = true;

                                                            pdfLoadedDocument.Save(xyz);

                                                            pdfLoadedDocument.Close(true);

                                                            oCR.Dispose();

                                                            GC.Collect();
                                                            GC.WaitForPendingFinalizers();
                                                            GC.Collect();
                                                        }
                                                        catch (Exception)
                                                        { continue; }
                                                    }

                                                }
                                                string outFile = expFolder + "\\Export\\" + cmbBatch.Text + "\\" + fileName + ".pdf";
                                                try
                                                {
                                                    //create newFileStream object which will be disposed at the end
                                                    using (FileStream newFileStream = new FileStream(outFile, FileMode.Create))
                                                    {
                                                        Application.DoEvents();
                                                        // step 2: we create a writer that listens to the document
                                                        PdfCopy writer = new PdfCopy(document, newFileStream);
                                                        if (writer == null)
                                                        {
                                                            return;
                                                        }

                                                        // step 3: open the document
                                                        document.Open();

                                                        foreach (string filename in fileNames)
                                                        {
                                                            Application.DoEvents();
                                                            string xyz = filename;
                                                            // create a reader for a certain document
                                                            PdfReader reader = new PdfReader(xyz);
                                                            reader.ConsolidateNamedDestinations();

                                                            // step 4: add content
                                                            for (int i = 1; i <= reader.NumberOfPages; i++)
                                                            {
                                                                PdfImportedPage page = writer.GetImportedPage(reader, i);
                                                                writer.AddPage(page);
                                                            }

                                                            PRAcroForm form = reader.AcroForm;
                                                            if (form != null)
                                                            {
                                                                writer.CopyAcroForm(reader);
                                                            }

                                                            reader.Close();
                                                        }

                                                        // step 5: close the document and writer
                                                        writer.Close();
                                                        document.Close();

                                                        GC.Collect();
                                                        GC.WaitForPendingFinalizers();
                                                        GC.Collect();
                                                    }//disposes the newFileStream object

                                                    Directory.Delete(dirEx, true);

                                                    //MessageBox.Show("OCR Completed Successfully ...");
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.ToString());
                                                }
                                            }
                                            catch (Exception ex1)
                                            { MessageBox.Show(ex1.ToString()); }

                                        }
                                        else
                                        {
                                            MessageBox.Show("There is a problem in one or more pages of File No: " + IMGName + "\n The error is: " + img.GetError());
                                            return;
                                        }

                                        if (dsexport.Tables[0].Rows[x][4].ToString() != "Y")
                                        {
                                            dgvexport.Rows[x].Cells[4].Value = "Exported";
                                            dgvexport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                            dgvexport.CurrentCell = dgvexport.Rows[x].Cells[4];
                                            progressBar1.PerformStep();
                                        }
                                        else
                                        {
                                            dgvexport.Rows[x].Cells[4].Value = "Exported";
                                            dgvexport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                            dgvexport.CurrentCell = dgvexport.Rows[x].Cells[4];
                                            progressBar1.PerformStep();
                                        }
                                    }
                                }

                                

                                Application.DoEvents();
                                progressBar2.PerformStep();
                                Application.DoEvents();
                               // ChangeStatus(dgvbatch.Rows[z].Cells[2].Value.ToString());
                               // InsertExportLog(cmbProject.SelectedValue.ToString(), dgvbatch.Rows[z].Cells[2].Value.ToString(), crd, MaxExportCount);
                               // pBox = new CtrlBox(Convert.ToInt32(cmbProject.SelectedValue.ToString()), Convert.ToInt32(dgvbatch.Rows[z].Cells[2].Value.ToString()), "1");
                               // wfeBox box = new wfeBox(sqlCon, pBox);
                               // box.UpdateStatus(eSTATES.BOX_EXPORTED);
                                sqlFileName = string.Empty;
                                txtMsg.Text = resultMsg;
                                btnExport.Enabled = true;
                            }
                            
                        }
                    }
                    lbl.Text = "Generating CSV....";
                    dgvdeedDetails.DataSource = GetAllDeedEX(cmbProject.SelectedValue.ToString(),cmbBatch.SelectedValue.ToString());

                    tabTextFile(dgvdeedDetails, expFolder + "\\Export\\" + cmbBatch.Text + "\\" + cmbBatch.Text + ".csv");

                    progressBar1.Value = 100;
                    lblFinalStatus.Text = "Finished....";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public System.Data.DataTable GetAllDeedEX(string proj_key, string batch_key)
        {
            string sqlStr = null;
            DataSet dsImage = new DataSet();
            DataSet ds = new DataSet();
            string exception = null;
            OdbcDataAdapter sqlAdap = null;
            string indexPageName = string.Empty;

            //sqlStr = "select a.District_Code,a.RO_Code,a.Book,a.Deed_year,a.Deed_no,a.Serial_No,a.Serial_Year,a.tran_maj_code,a.tran_min_code,a.Volume_No,a.Page_From,a.Page_To,a.Date_of_Completion,a.Date_of_Delivery,replace(replace(replace(a.Deed_Remarks,'\t',''),'\n',''),'\r','') as Deed_Remarks,a.Scan_doc_type,a.hold as Exception from deed_details a,deed_details_exception b where a.district_code = '" + Do_code + "' and a.Ro_code = '" + RO_Code + "' and a.book = '" + year + "' and a.deed_year = '" + deed_year + "'  and a.deed_no = '" + deed_no + "' and a.district_code = b.district_code and a.Ro_code = b.ro_code and a.book = b.book and a.deed_year =b.deed_year and a.deed_no = b.deed_no";
            sqlStr = "select filename, issue_from as 'cesc.issued.from[en_US]',issue_to as 'cesc.issued.to[en_US]', sub_cat as 'cesc.subject.category[]', sub_name as 'cesc.subject[en_US]',date_format(issue_date,'%Y-%m-%d') as 'dc.date.issued[en_US]', reference_no as 'dc.title[en_US]', doc_type as 'dc.type[en_US]' from metadata_entry where proj_key = '" + proj_key + "' and batch_key = '" + batch_key + "'";
            try
            {
                sqlAdap = new OdbcDataAdapter(sqlStr, sqlCon);
                sqlAdap.Fill(dsImage);
                
                for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                {
                    dsImage.Tables[0].Rows[i][0] = dsImage.Tables[0].Rows[i][0].ToString() + ".pdf" ;
                }
                //dsImage.Tables[0].Columns.Add("Exception_Type");
                //for (int i = 0; i < dsImage.Tables[0].Rows.Count; i++)
                //{
                //    dsImage.Tables[0].Rows[i]["Exception_Type"] = exception.TrimEnd(';');
                //}
                dsImage.Tables[0].Columns.Remove("filename");
            }
            catch (Exception ex)
            {
                sqlAdap.Dispose();

            }
            //DataRow dr = dsImage.Tables[0].Rows[0];
            //dsImage.Dispose();

            return dsImage.Tables[0];
        }
        private void SaveDataGridData(string strData, string strFileName)
        {
            FileStream fs;
            TextWriter tw = null;
            try
            {
                if (File.Exists(strFileName))
                {
                    fs = new FileStream(strFileName, FileMode.Open);
                }
                else
                {
                    fs = new FileStream(strFileName, FileMode.Create);
                }
                tw = new StreamWriter(fs);
                tw.Write(strData);
            }
            finally
            {
                if (tw != null)
                {
                    tw.Flush();
                    tw.Close();
                }
            }
        }

        private void tabTextFile(DataGridView dg, string filename)
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dtSource = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRow dr;
            if (dg.DataSource != null)
            {
                if (dg.DataSource.GetType() == typeof(DataSet))
                {
                    DataSet dsSource = (DataSet)dg.DataSource;
                    if (dsSource.Tables.Count > 0)
                    {
                        string strTables = string.Empty;
                        foreach (System.Data.DataTable dt1 in dsSource.Tables)
                        {
                            strTables += TableToString(dt1);
                            strTables += "\r\n\r\n";
                        }
                        if (strTables != string.Empty)
                            SaveDataGridData(strTables, filename);
                    }
                }
                else
                {
                    if (dg.DataSource.GetType() == typeof(System.Data.DataTable))
                        dtSource = (System.Data.DataTable)dg.DataSource;
                    if (dtSource != null)

                        SaveDataGridData(TableToString(dtSource), filename);
                }
            }
        }


        private string TableToString(System.Data.DataTable dt)
        {
            string strData = string.Empty;
            string sep = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.DataType != typeof(System.Guid) &&
                    c.DataType != typeof(System.Byte[]))
                    {
                        strData += sep + c.ColumnName;
                        sep = ",";
                    }
                }
                strData += "\r\n";
                foreach (DataRow r in dt.Rows)
                {
                    sep = string.Empty;
                    foreach (DataColumn c in dt.Columns)
                    {
                        if (c.DataType != typeof(System.Guid) &&
                        c.DataType != typeof(System.Byte[]))
                        {
                            if (!Convert.IsDBNull(r[c.ColumnName]))

                                strData += sep +
                                '"' + r[c.ColumnName].ToString().Replace("\n", " ").Replace(",", "-") + '"';

                            else

                                strData += sep + "";
                            sep = ",";

                        }
                    }
                    strData += "\r\n";

                }
            }
            else
            {
                //strData += "\r\n---> Table was empty!";
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.DataType != typeof(System.Guid) &&
                    c.DataType != typeof(System.Byte[]))
                    {
                        strData += sep + c.ColumnName;
                        sep = ",";
                    }
                }
                strData += "\r\n";
            }
            return strData;
        }


        private void cmdCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
