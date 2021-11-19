
namespace Generator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.dgName = new System.Windows.Forms.DataGridView();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbSel = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblLoad = new System.Windows.Forms.Label();
            this.btnDelViol = new System.Windows.Forms.Button();
            this.btnGetViol = new System.Windows.Forms.Button();
            this.btnDelCar = new System.Windows.Forms.Button();
            this.btnGetCar = new System.Windows.Forms.Button();
            this.btnDelDriver = new System.Windows.Forms.Button();
            this.btnGetDriver = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkbDrTrans = new System.Windows.Forms.CheckBox();
            this.lblDrInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbDrGenerator = new System.Windows.Forms.ProgressBar();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbGender = new System.Windows.Forms.TrackBar();
            this.label22 = new System.Windows.Forms.Label();
            this.upCount = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblDrInfo2 = new System.Windows.Forms.Label();
            this.chkbCarTrans = new System.Windows.Forms.CheckBox();
            this.pbDrGenerator2 = new System.Windows.Forms.ProgressBar();
            this.btnGenerate2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.upCount2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGender2 = new System.Windows.Forms.TrackBar();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkbViolTrans = new System.Windows.Forms.CheckBox();
            this.lblViolGen = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.upCountViol = new System.Windows.Forms.NumericUpDown();
            this.lblViol = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblQuantityGr = new System.Windows.Forms.Label();
            this.chkHasKill = new System.Windows.Forms.CheckBox();
            this.chkHasAff = new System.Windows.Forms.CheckBox();
            this.pbViol = new System.Windows.Forms.ProgressBar();
            this.btGenerateViol = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbViolCup = new System.Windows.Forms.TrackBar();
            this.tbQuantity = new System.Windows.Forms.TrackBar();
            this.tbQuantityGr = new System.Windows.Forms.TrackBar();
            this.label10 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblStatistics = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgName)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upCount)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upCount2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGender2)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upCountViol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbViolCup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantityGr)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(30, 24);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(771, 23);
            this.txtConnectionString.TabIndex = 0;
            this.txtConnectionString.Text = "Data Source=RUGAMELINK\\SQLEXPRESS;Initial Catalog=fines;Persist Security Info=Tru" +
    "e;User ID=sa;Password=123";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(6, 6);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(829, 225);
            this.txtQuery.TabIndex = 1;
            // 
            // dgName
            // 
            this.dgName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgName.Location = new System.Drawing.Point(6, 289);
            this.dgName.Name = "dgName";
            this.dgName.RowTemplate.Height = 25;
            this.dgName.Size = new System.Drawing.Size(829, 138);
            this.dgName.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(699, 246);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(136, 26);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Выдать запрос";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbSel
            // 
            this.cmbSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSel.FormattingEnabled = true;
            this.cmbSel.Items.AddRange(new object[] {
            "Локальное подключение",
            "Подключение к серверу"});
            this.cmbSel.Location = new System.Drawing.Point(518, 249);
            this.cmbSel.Name = "cmbSel";
            this.cmbSel.Size = new System.Drawing.Size(175, 23);
            this.cmbSel.TabIndex = 4;
            this.cmbSel.SelectedIndexChanged += new System.EventHandler(this.cmbSel_SelectedIndexChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(807, 24);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(68, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(30, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(849, 461);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblLoad);
            this.tabPage1.Controls.Add(this.btnDelViol);
            this.tabPage1.Controls.Add(this.btnGetViol);
            this.tabPage1.Controls.Add(this.btnDelCar);
            this.tabPage1.Controls.Add(this.btnGetCar);
            this.tabPage1.Controls.Add(this.btnDelDriver);
            this.tabPage1.Controls.Add(this.btnGetDriver);
            this.tabPage1.Controls.Add(this.txtQuery);
            this.tabPage1.Controls.Add(this.cmbSel);
            this.tabPage1.Controls.Add(this.dgName);
            this.tabPage1.Controls.Add(this.btnQuery);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(841, 433);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Запросы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.ForeColor = System.Drawing.Color.Red;
            this.lblLoad.Location = new System.Drawing.Point(519, 237);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(64, 15);
            this.lblLoad.TabIndex = 11;
            this.lblLoad.Text = "Загрузка...";
            this.lblLoad.Visible = false;
            // 
            // btnDelViol
            // 
            this.btnDelViol.Location = new System.Drawing.Point(422, 241);
            this.btnDelViol.Name = "btnDelViol";
            this.btnDelViol.Size = new System.Drawing.Size(90, 41);
            this.btnDelViol.TabIndex = 10;
            this.btnDelViol.Text = "Удалить Нарушения";
            this.btnDelViol.UseVisualStyleBackColor = true;
            this.btnDelViol.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // btnGetViol
            // 
            this.btnGetViol.Location = new System.Drawing.Point(329, 241);
            this.btnGetViol.Name = "btnGetViol";
            this.btnGetViol.Size = new System.Drawing.Size(87, 41);
            this.btnGetViol.TabIndex = 9;
            this.btnGetViol.Text = "Выдать Нарушения";
            this.btnGetViol.UseVisualStyleBackColor = true;
            this.btnGetViol.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // btnDelCar
            // 
            this.btnDelCar.Location = new System.Drawing.Point(249, 241);
            this.btnDelCar.Name = "btnDelCar";
            this.btnDelCar.Size = new System.Drawing.Size(74, 42);
            this.btnDelCar.TabIndex = 8;
            this.btnDelCar.Text = "Удалить автомобили";
            this.btnDelCar.UseVisualStyleBackColor = true;
            this.btnDelCar.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // btnGetCar
            // 
            this.btnGetCar.Location = new System.Drawing.Point(168, 240);
            this.btnGetCar.Name = "btnGetCar";
            this.btnGetCar.Size = new System.Drawing.Size(75, 43);
            this.btnGetCar.TabIndex = 7;
            this.btnGetCar.Text = "Выдать автомобили";
            this.btnGetCar.UseVisualStyleBackColor = true;
            this.btnGetCar.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // btnDelDriver
            // 
            this.btnDelDriver.Location = new System.Drawing.Point(87, 238);
            this.btnDelDriver.Name = "btnDelDriver";
            this.btnDelDriver.Size = new System.Drawing.Size(75, 44);
            this.btnDelDriver.TabIndex = 6;
            this.btnDelDriver.Text = "Удалить Водителей";
            this.btnDelDriver.UseVisualStyleBackColor = true;
            this.btnDelDriver.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // btnGetDriver
            // 
            this.btnGetDriver.Location = new System.Drawing.Point(6, 237);
            this.btnGetDriver.Name = "btnGetDriver";
            this.btnGetDriver.Size = new System.Drawing.Size(75, 46);
            this.btnGetDriver.TabIndex = 5;
            this.btnGetDriver.Text = "Выдать Водителей";
            this.btnGetDriver.UseVisualStyleBackColor = true;
            this.btnGetDriver.Click += new System.EventHandler(this.btnActionSql_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkbDrTrans);
            this.tabPage2.Controls.Add(this.lblDrInfo);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.pbDrGenerator);
            this.tabPage2.Controls.Add(this.btnGenerate);
            this.tabPage2.Controls.Add(this.tbGender);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.upCount);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(841, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Генератор водителей";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // chkbDrTrans
            // 
            this.chkbDrTrans.AutoSize = true;
            this.chkbDrTrans.Location = new System.Drawing.Point(143, 356);
            this.chkbDrTrans.Name = "chkbDrTrans";
            this.chkbDrTrans.Size = new System.Drawing.Size(99, 19);
            this.chkbDrTrans.TabIndex = 16;
            this.chkbDrTrans.Text = "В транзакции";
            this.chkbDrTrans.UseVisualStyleBackColor = true;
            // 
            // lblDrInfo
            // 
            this.lblDrInfo.AutoSize = true;
            this.lblDrInfo.Location = new System.Drawing.Point(298, 396);
            this.lblDrInfo.Name = "lblDrInfo";
            this.lblDrInfo.Size = new System.Drawing.Size(0, 15);
            this.lblDrInfo.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(775, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Женщины";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 13;
            this.label1.Text = "Мужчины";
            // 
            // pbDrGenerator
            // 
            this.pbDrGenerator.Location = new System.Drawing.Point(12, 392);
            this.pbDrGenerator.Name = "pbDrGenerator";
            this.pbDrGenerator.Size = new System.Drawing.Size(698, 23);
            this.pbDrGenerator.TabIndex = 9;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(716, 392);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(113, 23);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Сгенерировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_ClickAsync);
            // 
            // tbGender
            // 
            this.tbGender.Location = new System.Drawing.Point(12, 61);
            this.tbGender.Name = "tbGender";
            this.tbGender.Size = new System.Drawing.Size(813, 45);
            this.tbGender.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 333);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(72, 15);
            this.label22.TabIndex = 7;
            this.label22.Text = "Количество";
            // 
            // upCount
            // 
            this.upCount.Location = new System.Drawing.Point(12, 351);
            this.upCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.upCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upCount.Name = "upCount";
            this.upCount.Size = new System.Drawing.Size(120, 23);
            this.upCount.TabIndex = 4;
            this.upCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblDrInfo2);
            this.tabPage3.Controls.Add(this.chkbCarTrans);
            this.tabPage3.Controls.Add(this.pbDrGenerator2);
            this.tabPage3.Controls.Add(this.btnGenerate2);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.upCount2);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.tbGender2);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(841, 433);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Генератор автомобилей";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblDrInfo2
            // 
            this.lblDrInfo2.AutoSize = true;
            this.lblDrInfo2.Location = new System.Drawing.Point(271, 397);
            this.lblDrInfo2.Name = "lblDrInfo2";
            this.lblDrInfo2.Size = new System.Drawing.Size(0, 15);
            this.lblDrInfo2.TabIndex = 8;
            // 
            // chkbCarTrans
            // 
            this.chkbCarTrans.AutoSize = true;
            this.chkbCarTrans.Location = new System.Drawing.Point(137, 354);
            this.chkbCarTrans.Name = "chkbCarTrans";
            this.chkbCarTrans.Size = new System.Drawing.Size(99, 19);
            this.chkbCarTrans.TabIndex = 7;
            this.chkbCarTrans.Text = "В транзакции";
            this.chkbCarTrans.UseVisualStyleBackColor = true;
            // 
            // pbDrGenerator2
            // 
            this.pbDrGenerator2.Location = new System.Drawing.Point(16, 394);
            this.pbDrGenerator2.Name = "pbDrGenerator2";
            this.pbDrGenerator2.Size = new System.Drawing.Size(698, 23);
            this.pbDrGenerator2.TabIndex = 6;
            // 
            // btnGenerate2
            // 
            this.btnGenerate2.Location = new System.Drawing.Point(720, 394);
            this.btnGenerate2.Name = "btnGenerate2";
            this.btnGenerate2.Size = new System.Drawing.Size(105, 23);
            this.btnGenerate2.TabIndex = 5;
            this.btnGenerate2.Text = "Сгенерировать";
            this.btnGenerate2.UseVisualStyleBackColor = true;
            this.btnGenerate2.Click += new System.EventHandler(this.btnGenerate2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Количество";
            // 
            // upCount2
            // 
            this.upCount2.Location = new System.Drawing.Point(16, 348);
            this.upCount2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.upCount2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upCount2.Name = "upCount2";
            this.upCount2.Size = new System.Drawing.Size(110, 23);
            this.upCount2.TabIndex = 3;
            this.upCount2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(773, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Женщины";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Мужчины";
            // 
            // tbGender2
            // 
            this.tbGender2.Location = new System.Drawing.Point(16, 64);
            this.tbGender2.Name = "tbGender2";
            this.tbGender2.Size = new System.Drawing.Size(809, 45);
            this.tbGender2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkbViolTrans);
            this.tabPage4.Controls.Add(this.lblViolGen);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.upCountViol);
            this.tabPage4.Controls.Add(this.lblViol);
            this.tabPage4.Controls.Add(this.lblQuantity);
            this.tabPage4.Controls.Add(this.lblQuantityGr);
            this.tabPage4.Controls.Add(this.chkHasKill);
            this.tabPage4.Controls.Add(this.chkHasAff);
            this.tabPage4.Controls.Add(this.pbViol);
            this.tabPage4.Controls.Add(this.btGenerateViol);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.tbViolCup);
            this.tabPage4.Controls.Add(this.tbQuantity);
            this.tabPage4.Controls.Add(this.tbQuantityGr);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(841, 433);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Генератор нарушений";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkbViolTrans
            // 
            this.chkbViolTrans.AutoSize = true;
            this.chkbViolTrans.Location = new System.Drawing.Point(111, 365);
            this.chkbViolTrans.Name = "chkbViolTrans";
            this.chkbViolTrans.Size = new System.Drawing.Size(83, 19);
            this.chkbViolTrans.TabIndex = 16;
            this.chkbViolTrans.Text = "checkBox1";
            this.chkbViolTrans.UseVisualStyleBackColor = true;
            // 
            // lblViolGen
            // 
            this.lblViolGen.AutoSize = true;
            this.lblViolGen.Location = new System.Drawing.Point(306, 395);
            this.lblViolGen.Name = "lblViolGen";
            this.lblViolGen.Size = new System.Drawing.Size(0, 15);
            this.lblViolGen.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 341);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "Количество";
            // 
            // upCountViol
            // 
            this.upCountViol.Location = new System.Drawing.Point(14, 362);
            this.upCountViol.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.upCountViol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upCountViol.Name = "upCountViol";
            this.upCountViol.Size = new System.Drawing.Size(94, 23);
            this.upCountViol.TabIndex = 13;
            this.upCountViol.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblViol
            // 
            this.lblViol.AutoSize = true;
            this.lblViol.Location = new System.Drawing.Point(24, 43);
            this.lblViol.Name = "lblViol";
            this.lblViol.Size = new System.Drawing.Size(0, 15);
            this.lblViol.TabIndex = 12;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(268, 194);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(0, 15);
            this.lblQuantity.TabIndex = 11;
            // 
            // lblQuantityGr
            // 
            this.lblQuantityGr.AutoSize = true;
            this.lblQuantityGr.Location = new System.Drawing.Point(252, 43);
            this.lblQuantityGr.Name = "lblQuantityGr";
            this.lblQuantityGr.Size = new System.Drawing.Size(0, 15);
            this.lblQuantityGr.TabIndex = 10;
            // 
            // chkHasKill
            // 
            this.chkHasKill.AutoSize = true;
            this.chkHasKill.Location = new System.Drawing.Point(290, 289);
            this.chkHasKill.Name = "chkHasKill";
            this.chkHasKill.Size = new System.Drawing.Size(67, 19);
            this.chkHasKill.TabIndex = 9;
            this.chkHasKill.Text = "Убитые";
            this.chkHasKill.UseVisualStyleBackColor = true;
            // 
            // chkHasAff
            // 
            this.chkHasAff.AutoSize = true;
            this.chkHasAff.Location = new System.Drawing.Point(150, 289);
            this.chkHasAff.Name = "chkHasAff";
            this.chkHasAff.Size = new System.Drawing.Size(108, 19);
            this.chkHasAff.TabIndex = 8;
            this.chkHasAff.Text = "Пострадавшие";
            this.chkHasAff.UseVisualStyleBackColor = true;
            // 
            // pbViol
            // 
            this.pbViol.Location = new System.Drawing.Point(14, 391);
            this.pbViol.Name = "pbViol";
            this.pbViol.Size = new System.Drawing.Size(679, 23);
            this.pbViol.TabIndex = 7;
            // 
            // btGenerateViol
            // 
            this.btGenerateViol.Location = new System.Drawing.Point(699, 391);
            this.btGenerateViol.Name = "btGenerateViol";
            this.btGenerateViol.Size = new System.Drawing.Size(108, 23);
            this.btGenerateViol.TabIndex = 6;
            this.btGenerateViol.Text = "Сгенерировать";
            this.btGenerateViol.UseVisualStyleBackColor = true;
            this.btGenerateViol.Click += new System.EventHandler(this.btGenerateViol_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "Сопротивление";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(138, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Нарушений в группе";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Привлекался ранее";
            // 
            // tbViolCup
            // 
            this.tbViolCup.Location = new System.Drawing.Point(24, 64);
            this.tbViolCup.Maximum = 3;
            this.tbViolCup.Minimum = 1;
            this.tbViolCup.Name = "tbViolCup";
            this.tbViolCup.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbViolCup.Size = new System.Drawing.Size(45, 196);
            this.tbViolCup.TabIndex = 2;
            this.tbViolCup.Value = 1;
            // 
            // tbQuantity
            // 
            this.tbQuantity.Location = new System.Drawing.Point(138, 215);
            this.tbQuantity.Maximum = 5;
            this.tbQuantity.Minimum = 1;
            this.tbQuantity.Name = "tbQuantity";
            this.tbQuantity.Size = new System.Drawing.Size(669, 45);
            this.tbQuantity.TabIndex = 1;
            this.tbQuantity.Value = 1;
            // 
            // tbQuantityGr
            // 
            this.tbQuantityGr.Location = new System.Drawing.Point(138, 64);
            this.tbQuantityGr.Maximum = 5;
            this.tbQuantityGr.Minimum = 1;
            this.tbQuantityGr.Name = "tbQuantityGr";
            this.tbQuantityGr.Size = new System.Drawing.Size(669, 45);
            this.tbQuantityGr.TabIndex = 0;
            this.tbQuantityGr.Value = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(891, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 32);
            this.label10.TabIndex = 8;
            this.label10.Text = "Статистика";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblStatistics
            // 
            this.lblStatistics.Location = new System.Drawing.Point(891, 77);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.ReadOnly = true;
            this.lblStatistics.Size = new System.Drawing.Size(260, 433);
            this.lblStatistics.TabIndex = 9;
            this.lblStatistics.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 526);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtConnectionString);
            this.Name = "Form1";
            this.Text = "Window";
            ((System.ComponentModel.ISupportInitialize)(this.dgName)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upCount)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upCount2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbGender2)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upCountViol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbViolCup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbQuantityGr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.DataGridView dgName;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cmbSel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TrackBar tbGender;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown upCount;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ProgressBar pbDrGenerator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbGender2;
        private System.Windows.Forms.Button btnGenerate2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown upCount2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar pbDrGenerator2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tbViolCup;
        private System.Windows.Forms.TrackBar tbQuantity;
        private System.Windows.Forms.TrackBar tbQuantityGr;
        private System.Windows.Forms.ProgressBar pbViol;
        private System.Windows.Forms.Button btGenerateViol;
        private System.Windows.Forms.CheckBox chkHasKill;
        private System.Windows.Forms.CheckBox chkHasAff;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblQuantityGr;
        private System.Windows.Forms.Label lblViol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown upCountViol;
        private System.Windows.Forms.Button btnDelViol;
        private System.Windows.Forms.Button btnGetViol;
        private System.Windows.Forms.Button btnDelCar;
        private System.Windows.Forms.Button btnGetCar;
        private System.Windows.Forms.Button btnDelDriver;
        private System.Windows.Forms.Button btnGetDriver;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox lblStatistics;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.Label lblDrInfo;
        private System.Windows.Forms.CheckBox chkbDrTrans;
        private System.Windows.Forms.CheckBox chkbCarTrans;
        private System.Windows.Forms.Label lblDrInfo2;
        private System.Windows.Forms.Label lblViolGen;
        private System.Windows.Forms.CheckBox chkbViolTrans;
    }
}

