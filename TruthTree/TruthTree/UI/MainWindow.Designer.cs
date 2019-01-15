namespace TruthTree.UI
{
    partial class MainWindow
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.bGrow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lResult = new System.Windows.Forms.Label();
            this.bCheck = new System.Windows.Forms.Button();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set Root";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(89, 104);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(78, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Set Decomp";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bGrow
            // 
            this.bGrow.Enabled = false;
            this.bGrow.Location = new System.Drawing.Point(160, 12);
            this.bGrow.Name = "bGrow";
            this.bGrow.Size = new System.Drawing.Size(72, 25);
            this.bGrow.TabIndex = 3;
            this.bGrow.Text = "Grow Tree";
            this.bGrow.UseVisualStyleBackColor = true;
            this.bGrow.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(403, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Result:";
            // 
            // lResult
            // 
            this.lResult.AutoSize = true;
            this.lResult.Location = new System.Drawing.Point(449, 17);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(10, 13);
            this.lResult.TabIndex = 5;
            this.lResult.Text = "-";
            // 
            // bCheck
            // 
            this.bCheck.Enabled = false;
            this.bCheck.Location = new System.Drawing.Point(238, 12);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(72, 25);
            this.bCheck.TabIndex = 6;
            this.bCheck.Text = "Check Tree";
            this.bCheck.UseVisualStyleBackColor = true;
            this.bCheck.Visible = false;
            this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Checked = true;
            this.cbAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAuto.Location = new System.Drawing.Point(316, 17);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(81, 17);
            this.cbAuto.TabIndex = 7;
            this.cbAuto.Text = "Auto-check";
            this.cbAuto.UseVisualStyleBackColor = true;
            this.cbAuto.Visible = false;
            this.cbAuto.CheckedChanged += new System.EventHandler(this.cbAuto_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(544, 354);
            this.Controls.Add(this.lResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAuto);
            this.Controls.Add(this.bCheck);
            this.Controls.Add(this.bGrow);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "MainWindow";
            this.Text = "Truth Trees";
            this.Load += new System.EventHandler(this.Thing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bGrow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.Button bCheck;
        private System.Windows.Forms.CheckBox cbAuto;
    }
}