namespace STK_AGENTS
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
            StartButton = new Button();
            StopButton = new Button();
            PauseButton = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dataGridCustomers = new DataGridView();
            tabPage2 = new TabPage();
            dataGridParking = new DataGridView();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dataGridPayment = new DataGridView();
            dataGridCheckIn = new DataGridView();
            dataGridClerks = new DataGridView();
            dataGridMechanicsGr1 = new DataGridView();
            dataGridMechanicsGr2 = new DataGridView();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label7 = new Label();
            textBox1 = new TextBox();
            checkBox1 = new CheckBox();
            checkBoxTurbo = new CheckBox();
            trackBarDuration = new TrackBar();
            trackBarInterval = new TrackBar();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            label16 = new Label();
            checkBox3 = new CheckBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label17 = new Label();
            label18 = new Label();
            button1 = new Button();
            button2 = new Button();
            label19 = new Label();
            label20 = new Label();
            button3 = new Button();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            label24 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridCustomers).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridParking).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridPayment).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridCheckIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridClerks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMechanicsGr1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMechanicsGr2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarInterval).BeginInit();
            SuspendLayout();
            // 
            // StartButton
            // 
            StartButton.Location = new Point(12, 12);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(130, 30);
            StartButton.TabIndex = 0;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartSim;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(12, 48);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(130, 30);
            StopButton.TabIndex = 1;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += Stop;
            // 
            // PauseButton
            // 
            PauseButton.Location = new Point(12, 84);
            PauseButton.Name = "PauseButton";
            PauseButton.Size = new Size(130, 30);
            PauseButton.TabIndex = 2;
            PauseButton.Text = "Pause";
            PauseButton.UseVisualStyleBackColor = true;
            PauseButton.Click += Pause;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 287);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(642, 522);
            tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridCustomers);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(634, 494);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Customers in system";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridCustomers
            // 
            dataGridCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridCustomers.Location = new Point(6, 26);
            dataGridCustomers.Name = "dataGridCustomers";
            dataGridCustomers.RowTemplate.Height = 25;
            dataGridCustomers.Size = new Size(622, 462);
            dataGridCustomers.TabIndex = 4;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridParking);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(dataGridPayment);
            tabPage2.Controls.Add(dataGridCheckIn);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(634, 494);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Customers in queues";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridParking
            // 
            dataGridParking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridParking.Location = new Point(215, 26);
            dataGridParking.Name = "dataGridParking";
            dataGridParking.RowTemplate.Height = 25;
            dataGridParking.Size = new Size(204, 462);
            dataGridParking.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(423, 8);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 8;
            label3.Text = "Payment queue";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(217, 8);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 7;
            label2.Text = "Parking lot";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 8);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 3;
            label1.Text = "Check in queue";
            // 
            // dataGridPayment
            // 
            dataGridPayment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPayment.Location = new Point(425, 26);
            dataGridPayment.Name = "dataGridPayment";
            dataGridPayment.RowTemplate.Height = 25;
            dataGridPayment.Size = new Size(203, 462);
            dataGridPayment.TabIndex = 2;
            // 
            // dataGridCheckIn
            // 
            dataGridCheckIn.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridCheckIn.Location = new Point(6, 26);
            dataGridCheckIn.Name = "dataGridCheckIn";
            dataGridCheckIn.RowTemplate.Height = 25;
            dataGridCheckIn.Size = new Size(203, 462);
            dataGridCheckIn.TabIndex = 0;
            // 
            // dataGridClerks
            // 
            dataGridClerks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridClerks.Location = new Point(660, 334);
            dataGridClerks.Name = "dataGridClerks";
            dataGridClerks.RowTemplate.Height = 25;
            dataGridClerks.Size = new Size(300, 465);
            dataGridClerks.TabIndex = 4;
            // 
            // dataGridMechanicsGr1
            // 
            dataGridMechanicsGr1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMechanicsGr1.Location = new Point(966, 334);
            dataGridMechanicsGr1.Name = "dataGridMechanicsGr1";
            dataGridMechanicsGr1.RowTemplate.Height = 25;
            dataGridMechanicsGr1.Size = new Size(300, 465);
            dataGridMechanicsGr1.TabIndex = 5;
            // 
            // dataGridMechanicsGr2
            // 
            dataGridMechanicsGr2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMechanicsGr2.Location = new Point(1272, 334);
            dataGridMechanicsGr2.Name = "dataGridMechanicsGr2";
            dataGridMechanicsGr2.RowTemplate.Height = 25;
            dataGridMechanicsGr2.Size = new Size(300, 465);
            dataGridMechanicsGr2.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(660, 311);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 7;
            label4.Text = "Clerks";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(966, 311);
            label5.Name = "label5";
            label5.Size = new Size(108, 15);
            label5.TabIndex = 8;
            label5.Text = "Mechanics group 1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1272, 311);
            label6.Name = "label6";
            label6.Size = new Size(108, 15);
            label6.TabIndex = 9;
            label6.Text = "Mechanics group 2";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(178, 20);
            label7.Name = "label7";
            label7.Size = new Size(132, 15);
            label7.TabIndex = 10;
            label7.Text = "Number of replications:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(316, 17);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(77, 23);
            textBox1.TabIndex = 11;
            textBox1.Text = "10000";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.CheckAlign = ContentAlignment.MiddleRight;
            checkBox1.Location = new Point(252, 70);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(78, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "Validation";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBoxTurbo
            // 
            checkBoxTurbo.AutoSize = true;
            checkBoxTurbo.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxTurbo.Location = new Point(239, 46);
            checkBoxTurbo.Name = "checkBoxTurbo";
            checkBoxTurbo.Size = new Size(91, 19);
            checkBoxTurbo.TabIndex = 13;
            checkBoxTurbo.Text = "Turbo mode";
            checkBoxTurbo.UseVisualStyleBackColor = true;
            // 
            // trackBarDuration
            // 
            trackBarDuration.Location = new Point(252, 128);
            trackBarDuration.Maximum = 30;
            trackBarDuration.Minimum = 10;
            trackBarDuration.Name = "trackBarDuration";
            trackBarDuration.Size = new Size(141, 45);
            trackBarDuration.TabIndex = 14;
            trackBarDuration.TickFrequency = 2;
            trackBarDuration.Value = 10;
            trackBarDuration.Scroll += sleepTime_Scroll;
            // 
            // trackBarInterval
            // 
            trackBarInterval.Location = new Point(252, 179);
            trackBarInterval.Minimum = 1;
            trackBarInterval.Name = "trackBarInterval";
            trackBarInterval.Size = new Size(141, 45);
            trackBarInterval.TabIndex = 15;
            trackBarInterval.Value = 5;
            trackBarInterval.Scroll += interval_Scroll;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(259, 159);
            label8.Name = "label8";
            label8.Size = new Size(13, 15);
            label8.TabIndex = 16;
            label8.Text = "1";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(376, 161);
            label9.Name = "label9";
            label9.Size = new Size(13, 15);
            label9.TabIndex = 17;
            label9.Text = "3";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(259, 212);
            label10.Name = "label10";
            label10.Size = new Size(13, 15);
            label10.TabIndex = 18;
            label10.Text = "1";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(370, 212);
            label11.Name = "label11";
            label11.Size = new Size(19, 15);
            label11.TabIndex = 19;
            label11.Text = "10";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(168, 128);
            label12.Name = "label12";
            label12.Size = new Size(78, 15);
            label12.TabIndex = 20;
            label12.Text = "Sleep time (s)";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(156, 179);
            label13.Name = "label13";
            label13.Size = new Size(90, 15);
            label13.TabIndex = 21;
            label13.Text = "Interval of sleep";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(510, 20);
            label14.Name = "label14";
            label14.Size = new Size(98, 15);
            label14.TabIndex = 22;
            label14.Text = "Number of clerks";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(439, 50);
            label15.Name = "label15";
            label15.Size = new Size(169, 15);
            label15.TabIndex = 23;
            label15.Text = "Number of mechanics group 1";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(439, 78);
            label16.Name = "label16";
            label16.Size = new Size(169, 15);
            label16.TabIndex = 24;
            label16.Text = "Number of mechanics group 2";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.BackColor = SystemColors.Control;
            checkBox3.CheckAlign = ContentAlignment.MiddleRight;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(178, 95);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(152, 19);
            checkBox3.TabIndex = 25;
            checkBox3.Text = "Standard customer flow";
            checkBox3.UseVisualStyleBackColor = false;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(614, 17);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(40, 23);
            textBox2.TabIndex = 26;
            textBox2.Text = "10";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(614, 46);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(40, 23);
            textBox3.TabIndex = 27;
            textBox3.Text = "10";
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(614, 75);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(40, 23);
            textBox4.TabIndex = 28;
            textBox4.Text = "10";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(966, 32);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(300, 276);
            textBox5.TabIndex = 29;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(1272, 32);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(300, 276);
            textBox6.TabIndex = 30;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(492, 179);
            label17.Name = "label17";
            label17.Size = new Size(91, 15);
            label17.TabIndex = 31;
            label17.Text = "Simulation time";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(513, 194);
            label18.Name = "label18";
            label18.Size = new Size(49, 15);
            label18.TabIndex = 32;
            label18.Text = "00:00:00";
            // 
            // button1
            // 
            button1.Location = new Point(687, 12);
            button1.Name = "button1";
            button1.Size = new Size(166, 30);
            button1.TabIndex = 33;
            button1.Text = "Graph Clerks/Customers";
            button1.UseVisualStyleBackColor = true;
            button1.Click += GraphA;
            // 
            // button2
            // 
            button2.Location = new Point(687, 48);
            button2.Name = "button2";
            button2.Size = new Size(166, 30);
            button2.TabIndex = 34;
            button2.Text = "Graph Mechanics/Time";
            button2.UseVisualStyleBackColor = true;
            button2.Click += GraphB;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(661, 194);
            label19.Name = "label19";
            label19.Size = new Size(13, 15);
            label19.TabIndex = 36;
            label19.Text = "0";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(614, 179);
            label20.Name = "label20";
            label20.Size = new Size(106, 15);
            label20.TabIndex = 35;
            label20.Text = "Current replication";
            // 
            // button3
            // 
            button3.Location = new Point(687, 84);
            button3.Name = "button3";
            button3.Size = new Size(166, 30);
            button3.TabIndex = 37;
            button3.Text = "Show statistics";
            button3.UseVisualStyleBackColor = true;
            button3.Click += showStat;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(966, 12);
            label21.Name = "label21";
            label21.Size = new Size(83, 15);
            label21.TabIndex = 38;
            label21.Text = "Local statistics";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(1272, 12);
            label22.Name = "label22";
            label22.Size = new Size(89, 15);
            label22.TabIndex = 39;
            label22.Text = "Global statistics";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(758, 179);
            label23.Name = "label23";
            label23.Size = new Size(74, 15);
            label23.TabIndex = 40;
            label23.Text = "Labour costs";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(776, 194);
            label24.Name = "label24";
            label24.Size = new Size(19, 15);
            label24.TabIndex = 41;
            label24.Text = "0€";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1584, 821);
            Controls.Add(label24);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(label21);
            Controls.Add(button3);
            Controls.Add(label19);
            Controls.Add(label20);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(checkBox3);
            Controls.Add(label16);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(trackBarInterval);
            Controls.Add(trackBarDuration);
            Controls.Add(checkBoxTurbo);
            Controls.Add(checkBox1);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dataGridMechanicsGr2);
            Controls.Add(dataGridMechanicsGr1);
            Controls.Add(dataGridClerks);
            Controls.Add(tabControl1);
            Controls.Add(PauseButton);
            Controls.Add(StopButton);
            Controls.Add(StartButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridCustomers).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridParking).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridPayment).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridCheckIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridClerks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMechanicsGr1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridMechanicsGr2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartButton;
        private Button StopButton;
        private Button PauseButton;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private DataGridView dataGridCustomers;
        private TabPage tabPage2;
        private DataGridView dataGridPayment;
        private DataGridView dataGridCheckIn;
        private DataGridView dataGridClerks;
        private DataGridView dataGridMechanicsGr1;
        private DataGridView dataGridMechanicsGr2;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        private Label label5;
        private Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label7;
        private TextBox textBox1;
        private CheckBox checkBox1;
        private CheckBox checkBoxTurbo;
        private TrackBar trackBarDuration;
        private TrackBar trackBarInterval;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private CheckBox checkBox3;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label17;
        private Label label18;
        private Button button1;
        private Button button2;
        private Label label19;
        private Label label20;
        private DataGridView dataGridParking;
        private Button button3;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
    }
}