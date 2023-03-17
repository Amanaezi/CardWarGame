
namespace CardWarGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pPl2 = new System.Windows.Forms.Panel();
            this.pPl1 = new System.Windows.Forms.Panel();
            this.pPl4 = new System.Windows.Forms.Panel();
            this.pPl3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pPl2
            // 
            this.pPl2.Location = new System.Drawing.Point(413, 12);
            this.pPl2.Name = "pPl2";
            this.pPl2.Size = new System.Drawing.Size(404, 146);
            this.pPl2.TabIndex = 3;
            // 
            // pPl1
            // 
            this.pPl1.Location = new System.Drawing.Point(12, 12);
            this.pPl1.Name = "pPl1";
            this.pPl1.Size = new System.Drawing.Size(395, 146);
            this.pPl1.TabIndex = 2;
            // 
            // pPl4
            // 
            this.pPl4.Location = new System.Drawing.Point(413, 759);
            this.pPl4.Name = "pPl4";
            this.pPl4.Size = new System.Drawing.Size(404, 146);
            this.pPl4.TabIndex = 5;
            // 
            // pPl3
            // 
            this.pPl3.Location = new System.Drawing.Point(12, 759);
            this.pPl3.Name = "pPl3";
            this.pPl3.Size = new System.Drawing.Size(395, 146);
            this.pPl3.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(138, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 531);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(829, 917);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pPl4);
            this.Controls.Add(this.pPl3);
            this.Controls.Add(this.pPl2);
            this.Controls.Add(this.pPl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pPl2;
        private System.Windows.Forms.Panel pPl1;
        private System.Windows.Forms.Panel pPl4;
        private System.Windows.Forms.Panel pPl3;
        private System.Windows.Forms.Panel panel1;
    }
}

