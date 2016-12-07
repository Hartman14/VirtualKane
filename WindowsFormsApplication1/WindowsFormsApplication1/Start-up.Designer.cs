namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.Quick_Setting = new System.Windows.Forms.Button();
            this.Diagnostic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Quick_Setting
            // 
            this.Quick_Setting.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quick_Setting.Location = new System.Drawing.Point(193, 89);
            this.Quick_Setting.Name = "Quick_Setting";
            this.Quick_Setting.Size = new System.Drawing.Size(306, 104);
            this.Quick_Setting.TabIndex = 0;
            this.Quick_Setting.Text = "Quick Settings";
            this.Quick_Setting.UseVisualStyleBackColor = true;
            this.Quick_Setting.Click += new System.EventHandler(this.Quick_Setting_Click);
            // 
            // Diagnostic
            // 
            this.Diagnostic.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Diagnostic.Location = new System.Drawing.Point(193, 220);
            this.Diagnostic.Name = "Diagnostic";
            this.Diagnostic.Size = new System.Drawing.Size(306, 104);
            this.Diagnostic.TabIndex = 1;
            this.Diagnostic.Text = "Diagnostics";
            this.Diagnostic.UseVisualStyleBackColor = true;
            this.Diagnostic.Click += new System.EventHandler(this.Diagnostic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 408);
            this.Controls.Add(this.Diagnostic);
            this.Controls.Add(this.Quick_Setting);
            this.Name = "Form1";
            this.Text = "Start-up";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Quick_Setting;
        private System.Windows.Forms.Button Diagnostic;
    }
}

