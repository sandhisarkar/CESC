using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Reflection;
using System.Data.OleDb;
using System.Globalization;
using LItems;
using NovaNet;
using NovaNet.wfe;

namespace ImageHeaven
{
    public partial class frmMain : Form
    {

        static wItem wi;
        //NovaNet.Utils.dbCon dbcon;
        frmMain mainForm;
        OdbcConnection sqlCon = null;
        public Credentials crd;
        static int colorMode;
        dbCon dbcon;
        //
        NovaNet.Utils.GetProfile pData;
        NovaNet.Utils.ChangePassword pCPwd;
        NovaNet.Utils.Profile p;
        public static NovaNet.Utils.IntrRBAC rbc;
        private short logincounter;
        //


        public static string projectName = null;
        public static string batchName = null;
        public static string boxNumber = null;
        public static string projectVal = null;
        public static string batchVal = null;

        public static string name;

        public frmMain(OdbcConnection pCon)
        {
            InitializeComponent();

            sqlCon = pCon;

            logincounter = 0;

            ImageHeaven.Program.Logout = false;
        }

        public frmMain()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            this.Text = "Record Management" + "           Version: " + assemName.ToString();
            InitializeComponent();

            logincounter = 0;
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            int k;
            dbcon = new NovaNet.Utils.dbCon();
            try
            {
                string dllPaths = string.Empty;

                menuStrip1.Visible = false;


                if (sqlCon.State == ConnectionState.Open)
                {
                    pData = getData;
                    pCPwd = getCPwd;
                    rbc = new NovaNet.Utils.RBAC(sqlCon, dbcon, pData, pCPwd);
                    //string test = sqlCon.Database;
                    GetChallenge gc = new GetChallenge(getData);




                    gc.ShowDialog(this);

                    crd = rbc.getCredentials(p);
                    AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                    this.Text = "Record Management" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;

                    name = crd.userName;
                    if (crd.role == ihConstants._ADMINISTRATOR_ROLE)
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = true;
                        toolsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = true;
                        newToolStripMenuItem.Visible = true;
                        toolsToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = true;
                        onlineUsersToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = true;
                        batchUploadToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;

                    }
                }
            }
            catch (DBConnectionException dbex)
            {
                //MessageBox.Show(dbex.Message, "Image Heaven", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string err = dbex.Message;
                this.Close();
            }
        }
        void getData(ref NovaNet.Utils.Profile prmp)
        {
            int i;
            p = prmp;
            for (i = 1; i <= 2; i++)
            {
                if (rbc.authenticate(p.UserId, p.Password) == false)
                {
                    if (logincounter == 2)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        logincounter++;
                        GetChallenge ogc = new GetChallenge(getData);
                        ogc.ShowDialog(this);
                    }
                }
                else
                {
                    if (rbc.CheckUserIsLogged(p.UserId))
                    {

                        p = rbc.getProfile();
                        crd = rbc.getCredentials(p);
                        if (crd.role != ihConstants._ADMINISTRATOR_ROLE)
                        {
                            rbc.LockedUser(p.UserId, crd.created_dttm);
                        }
                        break;
                    }
                    else
                    {
                        p.UserId = null;
                        p.UserName = null;
                        GetChallenge ogc = new GetChallenge(getData);
                        AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                        this.Text = "Record Management" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;
                        ogc.ShowDialog(this);
                    }
                }
            }
        }
        void getCPwd(ref NovaNet.Utils.Profile prmpwd)
        {
            p = prmpwd;
            rbc.changePassword(p.UserId, p.UserName, p.Password);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProject dispProject;
                wi = new wfeProject(sqlCon);
                dispProject = new frmProject(wi, sqlCon);
                dispProject.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEntry frm = new frmEntry(sqlCon);
            //frm.ShowDialog();
        }

        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBatch dispProject;
                wi = new wfeBatch(sqlCon);
                dispProject = new frmBatch(wi, sqlCon);
                dispProject.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PwdChange pwdCh = new PwdChange(ref p, getCPwd);
            pwdCh.ShowDialog(this);
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
            nwUsr.ShowDialog(this);
        }
        void getnwusrData(ref NovaNet.Utils.Profile prmp)
        {
            p = prmp;
            if (rbc.addUser(p.UserId, p.UserName, p.Role_des, p.Password) == false)
            {
                AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
                nwUsr.ShowDialog(this);
            }
        }

        private void onlineUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoggedUser loged = new frmLoggedUser(rbc, crd);
            loged.ShowDialog(this);
        }

        private void dataEntryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //frmBatchSelect frm = new frmBatchSelect(sqlCon);
            //frm.ShowDialog(this);

            frmFile fm = new frmFile(sqlCon, crd);
            fm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            this.Text = "Record Management" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString();
            sqlCon.Close();


            sqlCon.Open();

            menuStrip1.Visible = false;

            frmMain_Load(sender, e);

        }

        private void imageImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataImport data = new frmDataImport(sqlCon, crd);
            data.ShowDialog(this);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExport export = new frmExport(sqlCon, crd);
            export.ShowDialog(this);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nevaehtech.com/");
        }

        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBundleUpload frm = new frmBundleUpload(sqlCon, crd);
            frm.ShowDialog();
        }

        private void productionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduction frm = new frmProduction(sqlCon);
            frm.ShowDialog();
        }
    }
}
