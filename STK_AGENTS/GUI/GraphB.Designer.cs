namespace STK_AGENTS.GUI
{
    partial class GraphB
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
            button1 = new Button();
            formsPlot1 = new ScottPlot.FormsPlot();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label2 = new Label();
            label1 = new Label();
            button2 = new Button();
            checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(460, 12);
            button1.Name = "button1";
            button1.Size = new Size(89, 43);
            button1.TabIndex = 11;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new Point(13, 67);
            formsPlot1.Margin = new Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(774, 374);
            formsPlot1.TabIndex = 10;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(303, 38);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(42, 23);
            textBox2.TabIndex = 9;
            textBox2.Text = "10";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(303, 9);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(42, 23);
            textBox1.TabIndex = 8;
            textBox1.Text = "1000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(199, 41);
            label2.Name = "label2";
            label2.Size = new Size(98, 15);
            label2.TabIndex = 7;
            label2.Text = "Number of clerks";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(168, 12);
            label1.Name = "label1";
            label1.Size = new Size(129, 15);
            label1.TabIndex = 6;
            label1.Text = "Number of replications";
            // 
            // button2
            // 
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 12;
            button2.Text = "Simulation";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.BackColor = SystemColors.Control;
            checkBox3.CheckAlign = ContentAlignment.MiddleRight;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(41, 42);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(152, 19);
            checkBox3.TabIndex = 27;
            checkBox3.Text = "Standard customer flow";
            checkBox3.UseVisualStyleBackColor = false;
            // 
            // GraphB
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(formsPlot1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "GraphB";
            Text = "GraphB";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ScottPlot.FormsPlot formsPlot1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label2;
        private Label label1;
        private Button button2;
        private CheckBox checkBox3;
    }
}