
namespace CarRentalService
{
    partial class demageformview
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
            this.demagedataGridView1 = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.backbutton3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.demagedataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // demagedataGridView1
            // 
            this.demagedataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.demagedataGridView1.Location = new System.Drawing.Point(12, 78);
            this.demagedataGridView1.Name = "demagedataGridView1";
            this.demagedataGridView1.RowHeadersWidth = 51;
            this.demagedataGridView1.RowTemplate.Height = 24;
            this.demagedataGridView1.Size = new System.Drawing.Size(845, 292);
            this.demagedataGridView1.TabIndex = 0;
            this.demagedataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.demagedataGridView1_CellContentClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(346, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 25);
            this.label9.TabIndex = 106;
            this.label9.Text = "Demage List";
            // 
            // backbutton3
            // 
            this.backbutton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backbutton3.Location = new System.Drawing.Point(752, 385);
            this.backbutton3.Name = "backbutton3";
            this.backbutton3.Size = new System.Drawing.Size(91, 33);
            this.backbutton3.TabIndex = 107;
            this.backbutton3.Text = "Back";
            this.backbutton3.UseVisualStyleBackColor = true;
            this.backbutton3.Click += new System.EventHandler(this.backbutton3_Click);
            // 
            // demageformview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 421);
            this.Controls.Add(this.backbutton3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.demagedataGridView1);
            this.Name = "demageformview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "demageformview";
            this.Load += new System.EventHandler(this.demageformview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.demagedataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView demagedataGridView1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button backbutton3;
    }
}