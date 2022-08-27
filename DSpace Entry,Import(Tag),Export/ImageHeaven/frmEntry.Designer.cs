namespace ImageHeaven
{
    partial class frmEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntry));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.labelBatch = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.labelProject = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelIssuedDate = new System.Windows.Forms.Label();
            this.cmbDocType = new System.Windows.Forms.ComboBox();
            this.labelDocType = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.cmbSubCat = new System.Windows.Forms.ComboBox();
            this.labelSubCat = new System.Windows.Forms.Label();
            this.txtIssuedTo = new System.Windows.Forms.TextBox();
            this.labelIssuedTo = new System.Windows.Forms.Label();
            this.txtIssuedFrom = new System.Windows.Forms.TextBox();
            this.labelIssuedFrom = new System.Windows.Forms.Label();
            this.txtLetterNo = new System.Windows.Forms.TextBox();
            this.labelLetter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBatch);
            this.groupBox1.Controls.Add(this.labelBatch);
            this.groupBox1.Controls.Add(this.txtProject);
            this.groupBox1.Controls.Add(this.labelProject);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project-Batch Details :";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtBatch
            // 
            this.txtBatch.Enabled = false;
            this.txtBatch.Location = new System.Drawing.Point(161, 61);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(338, 21);
            this.txtBatch.TabIndex = 3;
            this.txtBatch.TextChanged += new System.EventHandler(this.txtBatch_TextChanged);
            // 
            // labelBatch
            // 
            this.labelBatch.AutoSize = true;
            this.labelBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBatch.Location = new System.Drawing.Point(62, 64);
            this.labelBatch.Name = "labelBatch";
            this.labelBatch.Size = new System.Drawing.Size(88, 13);
            this.labelBatch.TabIndex = 2;
            this.labelBatch.Text = "Batch Name : ";
            this.labelBatch.Click += new System.EventHandler(this.labelBatch_Click);
            // 
            // txtProject
            // 
            this.txtProject.Enabled = false;
            this.txtProject.Location = new System.Drawing.Point(161, 27);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(338, 21);
            this.txtProject.TabIndex = 1;
            this.txtProject.TextChanged += new System.EventHandler(this.txtProject_TextChanged);
            // 
            // labelProject
            // 
            this.labelProject.AutoSize = true;
            this.labelProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProject.Location = new System.Drawing.Point(55, 30);
            this.labelProject.Name = "labelProject";
            this.labelProject.Size = new System.Drawing.Size(95, 13);
            this.labelProject.TabIndex = 0;
            this.labelProject.Text = "Project Name : ";
            this.labelProject.Click += new System.EventHandler(this.labelProject_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.cmbMonth);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDate);
            this.groupBox2.Controls.Add(this.txtYear);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labelIssuedDate);
            this.groupBox2.Controls.Add(this.cmbDocType);
            this.groupBox2.Controls.Add(this.labelDocType);
            this.groupBox2.Controls.Add(this.txtSubject);
            this.groupBox2.Controls.Add(this.labelSubject);
            this.groupBox2.Controls.Add(this.cmbSubCat);
            this.groupBox2.Controls.Add(this.labelSubCat);
            this.groupBox2.Controls.Add(this.txtIssuedTo);
            this.groupBox2.Controls.Add(this.labelIssuedTo);
            this.groupBox2.Controls.Add(this.txtIssuedFrom);
            this.groupBox2.Controls.Add(this.labelIssuedFrom);
            this.groupBox2.Controls.Add(this.txtLetterNo);
            this.groupBox2.Controls.Add(this.labelLetter);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(4, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 355);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Metadata Fields :";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // cmbMonth
            // 
            this.cmbMonth.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cmbMonth.Location = new System.Drawing.Point(238, 286);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(58, 23);
            this.cmbMonth.TabIndex = 23;
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);
            this.cmbMonth.CursorChanged += new System.EventHandler(this.cmbMonth_CursorChanged);
            this.cmbMonth.TabIndexChanged += new System.EventHandler(this.cmbMonth_TabIndexChanged);
            this.cmbMonth.TextChanged += new System.EventHandler(this.cmbMonth_TextChanged);
            this.cmbMonth.Click += new System.EventHandler(this.cmbMonth_Click);
            this.cmbMonth.Enter += new System.EventHandler(this.cmbMonth_Enter);
            this.cmbMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMonth_KeyDown);
            this.cmbMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMonth_KeyPress);
            this.cmbMonth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbMonth_KeyUp);
            this.cmbMonth.Leave += new System.EventHandler(this.cmbMonth_Leave);
            this.cmbMonth.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbMonth_MouseClick);
            this.cmbMonth.MouseLeave += new System.EventHandler(this.cmbMonth_MouseLeave);
            this.cmbMonth.MouseHover += new System.EventHandler(this.cmbMonth_MouseHover);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(310, 329);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 12);
            this.label15.TabIndex = 32;
            this.label15.Text = "[Format : dd]";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(232, 329);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 12);
            this.label14.TabIndex = 31;
            this.label14.Text = "[Format : mm]";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(136, 328);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 12);
            this.label13.TabIndex = 30;
            this.label13.Text = "[Format : yyyy]";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(320, 311);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "(Date)";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(245, 313);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "(Month)";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(156, 311);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "(Year)";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(298, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 18);
            this.label9.TabIndex = 26;
            this.label9.Text = "-";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(221, 288);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 18);
            this.label8.TabIndex = 25;
            this.label8.Text = "-";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(312, 287);
            this.txtDate.MaxLength = 2;
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(53, 21);
            this.txtDate.TabIndex = 24;
            this.txtDate.Click += new System.EventHandler(this.txtDate_Click);
            this.txtDate.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtDate_MouseClick);
            this.txtDate.TabIndexChanged += new System.EventHandler(this.txtDate_TabIndexChanged);
            this.txtDate.TextChanged += new System.EventHandler(this.txtDate_TextChanged);
            this.txtDate.Enter += new System.EventHandler(this.txtDate_Enter);
            this.txtDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDate_KeyDown);
            this.txtDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDate_KeyPress);
            this.txtDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDate_KeyUp);
            this.txtDate.Leave += new System.EventHandler(this.txtDate_Leave);
            this.txtDate.MouseLeave += new System.EventHandler(this.txtDate_MouseLeave);
            this.txtDate.MouseHover += new System.EventHandler(this.txtDate_MouseHover);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(133, 287);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(82, 21);
            this.txtYear.TabIndex = 22;
            this.txtYear.Tag = "(Year)";
            this.txtYear.Click += new System.EventHandler(this.txtYear_Click);
            this.txtYear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtYear_MouseClick);
            this.txtYear.TabIndexChanged += new System.EventHandler(this.txtYear_TabIndexChanged);
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            this.txtYear.Enter += new System.EventHandler(this.txtYear_Enter);
            this.txtYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYear_KeyPress);
            this.txtYear.Leave += new System.EventHandler(this.txtYear_Leave);
            this.txtYear.MouseLeave += new System.EventHandler(this.txtYear_MouseLeave);
            this.txtYear.MouseHover += new System.EventHandler(this.txtYear_MouseHover);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(369, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 16);
            this.label7.TabIndex = 21;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(373, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 16);
            this.label6.TabIndex = 20;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(373, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 19;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(376, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 18;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(372, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 17;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(374, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 16;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(369, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 15;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelIssuedDate
            // 
            this.labelIssuedDate.AutoSize = true;
            this.labelIssuedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIssuedDate.Location = new System.Drawing.Point(36, 293);
            this.labelIssuedDate.Name = "labelIssuedDate";
            this.labelIssuedDate.Size = new System.Drawing.Size(83, 13);
            this.labelIssuedDate.TabIndex = 13;
            this.labelIssuedDate.Text = "Issued Date :";
            this.labelIssuedDate.Click += new System.EventHandler(this.labelIssuedDate_Click);
            // 
            // cmbDocType
            // 
            this.cmbDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocType.FormattingEnabled = true;
            this.cmbDocType.Location = new System.Drawing.Point(129, 247);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(234, 23);
            this.cmbDocType.TabIndex = 12;
            this.cmbDocType.SelectedIndexChanged += new System.EventHandler(this.cmbDocType_SelectedIndexChanged);
            this.cmbDocType.CursorChanged += new System.EventHandler(this.cmbDocType_CursorChanged);
            this.cmbDocType.TabIndexChanged += new System.EventHandler(this.cmbDocType_TabIndexChanged);
            this.cmbDocType.TextChanged += new System.EventHandler(this.cmbDocType_TextChanged);
            this.cmbDocType.Click += new System.EventHandler(this.cmbDocType_Click);
            this.cmbDocType.Enter += new System.EventHandler(this.cmbDocType_Enter);
            this.cmbDocType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDocType_KeyDown);
            this.cmbDocType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDocType_KeyPress);
            this.cmbDocType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbDocType_KeyUp);
            this.cmbDocType.Leave += new System.EventHandler(this.cmbDocType_Leave);
            this.cmbDocType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbDocType_MouseClick);
            this.cmbDocType.MouseLeave += new System.EventHandler(this.cmbDocType_MouseLeave);
            this.cmbDocType.MouseHover += new System.EventHandler(this.cmbDocType_MouseHover);
            // 
            // labelDocType
            // 
            this.labelDocType.AutoSize = true;
            this.labelDocType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDocType.Location = new System.Drawing.Point(15, 249);
            this.labelDocType.Name = "labelDocType";
            this.labelDocType.Size = new System.Drawing.Size(104, 13);
            this.labelDocType.TabIndex = 11;
            this.labelDocType.Text = "Document Type :";
            this.labelDocType.Click += new System.EventHandler(this.labelDocType_Click);
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(129, 192);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubject.Size = new System.Drawing.Size(234, 43);
            this.txtSubject.TabIndex = 10;
            this.txtSubject.Click += new System.EventHandler(this.txtSubject_Click);
            this.txtSubject.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSubject_MouseClick);
            this.txtSubject.TabIndexChanged += new System.EventHandler(this.txtSubject_TabIndexChanged);
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            this.txtSubject.Enter += new System.EventHandler(this.txtSubject_Enter);
            this.txtSubject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSubject_KeyPress);
            this.txtSubject.Leave += new System.EventHandler(this.txtSubject_Leave);
            this.txtSubject.MouseLeave += new System.EventHandler(this.txtSubject_MouseLeave);
            this.txtSubject.MouseHover += new System.EventHandler(this.txtSubject_MouseHover);
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubject.Location = new System.Drawing.Point(61, 198);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(58, 13);
            this.labelSubject.TabIndex = 9;
            this.labelSubject.Text = "Subject :";
            this.labelSubject.Click += new System.EventHandler(this.labelSubject_Click);
            // 
            // cmbSubCat
            // 
            this.cmbSubCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCat.FormattingEnabled = true;
            this.cmbSubCat.Location = new System.Drawing.Point(128, 154);
            this.cmbSubCat.Name = "cmbSubCat";
            this.cmbSubCat.Size = new System.Drawing.Size(236, 23);
            this.cmbSubCat.TabIndex = 8;
            this.cmbSubCat.SelectedIndexChanged += new System.EventHandler(this.cmbSubCat_SelectedIndexChanged);
            this.cmbSubCat.CursorChanged += new System.EventHandler(this.cmbSubCat_CursorChanged);
            this.cmbSubCat.TabIndexChanged += new System.EventHandler(this.cmbSubCat_TabIndexChanged);
            this.cmbSubCat.TextChanged += new System.EventHandler(this.cmbSubCat_TextChanged);
            this.cmbSubCat.Click += new System.EventHandler(this.cmbSubCat_Click);
            this.cmbSubCat.Enter += new System.EventHandler(this.cmbSubCat_Enter);
            this.cmbSubCat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSubCat_KeyDown);
            this.cmbSubCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbSubCat_KeyPress);
            this.cmbSubCat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbSubCat_KeyUp);
            this.cmbSubCat.Leave += new System.EventHandler(this.cmbSubCat_Leave);
            this.cmbSubCat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbSubCat_MouseClick);
            this.cmbSubCat.MouseLeave += new System.EventHandler(this.cmbSubCat_MouseLeave);
            this.cmbSubCat.MouseHover += new System.EventHandler(this.cmbSubCat_MouseHover);
            // 
            // labelSubCat
            // 
            this.labelSubCat.AutoSize = true;
            this.labelSubCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubCat.Location = new System.Drawing.Point(8, 161);
            this.labelSubCat.Name = "labelSubCat";
            this.labelSubCat.Size = new System.Drawing.Size(112, 13);
            this.labelSubCat.TabIndex = 7;
            this.labelSubCat.Text = "Subject Category :";
            this.labelSubCat.Click += new System.EventHandler(this.labelSubCat_Click);
            // 
            // txtIssuedTo
            // 
            this.txtIssuedTo.Location = new System.Drawing.Point(129, 114);
            this.txtIssuedTo.Name = "txtIssuedTo";
            this.txtIssuedTo.Size = new System.Drawing.Size(236, 21);
            this.txtIssuedTo.TabIndex = 6;
            this.txtIssuedTo.Click += new System.EventHandler(this.txtIssuedTo_Click);
            this.txtIssuedTo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIssuedTo_MouseClick);
            this.txtIssuedTo.TabIndexChanged += new System.EventHandler(this.txtIssuedTo_TabIndexChanged);
            this.txtIssuedTo.TextChanged += new System.EventHandler(this.txtIssuedTo_TextChanged);
            this.txtIssuedTo.Enter += new System.EventHandler(this.txtIssuedTo_Enter);
            this.txtIssuedTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIssuedTo_KeyPress);
            this.txtIssuedTo.Leave += new System.EventHandler(this.txtIssuedTo_Leave);
            this.txtIssuedTo.MouseLeave += new System.EventHandler(this.txtIssuedTo_MouseLeave);
            this.txtIssuedTo.MouseHover += new System.EventHandler(this.txtIssuedTo_MouseHover);
            // 
            // labelIssuedTo
            // 
            this.labelIssuedTo.AutoSize = true;
            this.labelIssuedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIssuedTo.Location = new System.Drawing.Point(48, 117);
            this.labelIssuedTo.Name = "labelIssuedTo";
            this.labelIssuedTo.Size = new System.Drawing.Size(71, 13);
            this.labelIssuedTo.TabIndex = 5;
            this.labelIssuedTo.Text = "Issued To :";
            this.labelIssuedTo.Click += new System.EventHandler(this.labelIssuedTo_Click);
            // 
            // txtIssuedFrom
            // 
            this.txtIssuedFrom.Location = new System.Drawing.Point(130, 73);
            this.txtIssuedFrom.Name = "txtIssuedFrom";
            this.txtIssuedFrom.Size = new System.Drawing.Size(236, 21);
            this.txtIssuedFrom.TabIndex = 4;
            this.txtIssuedFrom.Click += new System.EventHandler(this.txtIssuedFrom_Click);
            this.txtIssuedFrom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIssuedFrom_MouseClick);
            this.txtIssuedFrom.TabIndexChanged += new System.EventHandler(this.txtIssuedFrom_TabIndexChanged);
            this.txtIssuedFrom.TextChanged += new System.EventHandler(this.txtIssuedFrom_TextChanged);
            this.txtIssuedFrom.Enter += new System.EventHandler(this.txtIssuedFrom_Enter);
            this.txtIssuedFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIssuedFrom_KeyPress);
            this.txtIssuedFrom.Leave += new System.EventHandler(this.txtIssuedFrom_Leave);
            this.txtIssuedFrom.MouseLeave += new System.EventHandler(this.txtIssuedFrom_MouseLeave);
            this.txtIssuedFrom.MouseHover += new System.EventHandler(this.txtIssuedFrom_MouseHover);
            // 
            // labelIssuedFrom
            // 
            this.labelIssuedFrom.AutoSize = true;
            this.labelIssuedFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIssuedFrom.Location = new System.Drawing.Point(37, 76);
            this.labelIssuedFrom.Name = "labelIssuedFrom";
            this.labelIssuedFrom.Size = new System.Drawing.Size(83, 13);
            this.labelIssuedFrom.TabIndex = 3;
            this.labelIssuedFrom.Text = "Issued From :";
            this.labelIssuedFrom.Click += new System.EventHandler(this.labelIssuedFrom_Click);
            // 
            // txtLetterNo
            // 
            this.txtLetterNo.Location = new System.Drawing.Point(129, 34);
            this.txtLetterNo.Name = "txtLetterNo";
            this.txtLetterNo.Size = new System.Drawing.Size(237, 21);
            this.txtLetterNo.TabIndex = 2;
            this.txtLetterNo.Click += new System.EventHandler(this.txtLetterNo_Click);
            this.txtLetterNo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtLetterNo_MouseClick);
            this.txtLetterNo.TabIndexChanged += new System.EventHandler(this.txtLetterNo_TabIndexChanged);
            this.txtLetterNo.TextChanged += new System.EventHandler(this.txtLetterNo_TextChanged);
            this.txtLetterNo.Enter += new System.EventHandler(this.txtLetterNo_Enter);
            this.txtLetterNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLetterNo_KeyPress);
            this.txtLetterNo.Leave += new System.EventHandler(this.txtLetterNo_Leave);
            this.txtLetterNo.MouseLeave += new System.EventHandler(this.txtLetterNo_MouseLeave);
            this.txtLetterNo.MouseHover += new System.EventHandler(this.txtLetterNo_MouseHover);
            // 
            // labelLetter
            // 
            this.labelLetter.AutoSize = true;
            this.labelLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLetter.Location = new System.Drawing.Point(20, 36);
            this.labelLetter.Name = "labelLetter";
            this.labelLetter.Size = new System.Drawing.Size(98, 13);
            this.labelLetter.TabIndex = 0;
            this.labelLetter.Text = "Reference No. :";
            this.labelLetter.Click += new System.EventHandler(this.labelLetter_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Location = new System.Drawing.Point(300, 479);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 47);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(154, 6);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(105, 32);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "&Exit";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSave.Location = new System.Drawing.Point(21, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(106, 32);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // frmEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 528);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEntry";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Entry";
            this.Load += new System.EventHandler(this.frmEntry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelProject;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label labelBatch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelLetter;
        private System.Windows.Forms.TextBox txtLetterNo;
        private System.Windows.Forms.TextBox txtIssuedFrom;
        private System.Windows.Forms.Label labelIssuedFrom;
        private System.Windows.Forms.TextBox txtIssuedTo;
        private System.Windows.Forms.Label labelIssuedTo;
        private System.Windows.Forms.Label labelSubCat;
        private System.Windows.Forms.ComboBox cmbSubCat;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label labelDocType;
        private System.Windows.Forms.ComboBox cmbDocType;
        private System.Windows.Forms.Label labelIssuedDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbMonth;
    }
}