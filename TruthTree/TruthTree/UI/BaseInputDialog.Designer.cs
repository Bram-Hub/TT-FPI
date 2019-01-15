namespace TruthTree.UI
{
    partial class BaseInputDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbConclusions = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPremises = new System.Windows.Forms.TextBox();
            this.bAccept = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 18;
            this.label2.Text = "Conclusion :";
            // 
            // tbConclusions
            // 
            this.tbConclusions.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConclusions.Location = new System.Drawing.Point(5, 241);
            this.tbConclusions.Name = "tbConclusions";
            this.tbConclusions.Size = new System.Drawing.Size(514, 21);
            this.tbConclusions.TabIndex = 16;
            this.tbConclusions.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "Premises (one per line) :";
            // 
            // tbPremises
            // 
            this.tbPremises.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPremises.Location = new System.Drawing.Point(5, 14);
            this.tbPremises.Multiline = true;
            this.tbPremises.Name = "tbPremises";
            this.tbPremises.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbPremises.Size = new System.Drawing.Size(514, 208);
            this.tbPremises.TabIndex = 15;
            this.tbPremises.WordWrap = false;
            // 
            // bAccept
            // 
            this.bAccept.Location = new System.Drawing.Point(437, 268);
            this.bAccept.Name = "bAccept";
            this.bAccept.Size = new System.Drawing.Size(82, 24);
            this.bAccept.TabIndex = 19;
            this.bAccept.Text = "Accept";
            this.bAccept.UseVisualStyleBackColor = true;
            this.bAccept.Click += new System.EventHandler(this.bAccept_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(349, 268);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(82, 24);
            this.bCancel.TabIndex = 20;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // BaseInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 295);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bAccept);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbConclusions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPremises);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BaseInputDialog";
            this.Text = "Initial Tree Input";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbConclusions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPremises;
        private System.Windows.Forms.Button bAccept;
        private System.Windows.Forms.Button bCancel;
    }
}