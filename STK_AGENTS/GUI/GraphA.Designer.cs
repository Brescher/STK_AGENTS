namespace STK_AGENTS.GUI
{
    partial class GraphA
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            formsPlot1 = new ScottPlot.FormsPlot();
            button1 = new Button();
            button2 = new Button();
            textBox3 = new TextBox();
            label3 = new Label();
            checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(168, 9);
            label1.Name = "label1";
            label1.Size = new Size(129, 15);
            label1.TabIndex = 0;
            label1.Text = "Number of replications";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(373, 9);
            label2.Name = "label2";
            label2.Size = new Size(146, 15);
            label2.TabIndex = 1;
            label2.Text = "Number of mechanics Gr1";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(303, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(49, 23);
            textBox1.TabIndex = 2;
            textBox1.Text = "1000";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(525, 6);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(49, 23);
            textBox2.TabIndex = 3;
            textBox2.Text = "12";
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new Point(13, 64);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(774, 374);
            formsPlot1.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(638, 15);
            button1.Name = "button1";
            button1.Size = new Size(89, 43);
            button1.TabIndex = 5;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "Simulation";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(525, 35);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(49, 23);
            textBox3.TabIndex = 8;
            textBox3.Text = "7";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(373, 38);
            label3.Name = "label3";
            label3.Size = new Size(146, 15);
            label3.TabIndex = 7;
            label3.Text = "Number of mechanics Gr2";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.BackColor = SystemColors.Control;
            checkBox3.CheckAlign = ContentAlignment.MiddleRight;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(200, 39);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(152, 19);
            checkBox3.TabIndex = 26;
            checkBox3.Text = "Standard customer flow";
            checkBox3.UseVisualStyleBackColor = false;
            // 
            // GraphA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox3);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(formsPlot1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "GraphA";
            Text = "GraphA";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private ScottPlot.FormsPlot formsPlot1;
        private Button button1;
        private Button button2;
        private TextBox textBox3;
        private Label label3;
        private CheckBox checkBox3;
    }
}