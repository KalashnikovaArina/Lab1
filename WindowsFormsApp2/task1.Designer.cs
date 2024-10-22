namespace WindowsFormsApp2
{
    partial class task1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonFindBoundary = new System.Windows.Forms.Button();
            this.buttonDrawBoundary = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);

            // buttonFindBoundary
            this.buttonFindBoundary.Location = new System.Drawing.Point(520, 20);
            this.buttonFindBoundary.Name = "buttonFindBoundary";
            this.buttonFindBoundary.Size = new System.Drawing.Size(120, 30);
            this.buttonFindBoundary.TabIndex = 1;
            this.buttonFindBoundary.Text = "Найти границу";
            this.buttonFindBoundary.UseVisualStyleBackColor = true;
            this.buttonFindBoundary.Click += new System.EventHandler(this.buttonFindBoundary_Click);

            // buttonDrawBoundary
            this.buttonDrawBoundary.Location = new System.Drawing.Point(520, 60);
            this.buttonDrawBoundary.Name = "buttonDrawBoundary";
            this.buttonDrawBoundary.Size = new System.Drawing.Size(120, 30);
            this.buttonDrawBoundary.TabIndex = 2;
            this.buttonDrawBoundary.Text = "Рисовать границу";
            this.buttonDrawBoundary.UseVisualStyleBackColor = true;
            this.buttonDrawBoundary.Click += new System.EventHandler(this.buttonDrawBoundary_Click);

            // buttonClear
            this.buttonClear.Location = new System.Drawing.Point(520, 100);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 30);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);

            // task1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 530);
            this.Controls.Add(this.buttonFindBoundary);
            this.Controls.Add(this.buttonDrawBoundary);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.pictureBox1);
            this.Name = "task1";
            this.Text = "Boundary Detection";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFindBoundary;
        private System.Windows.Forms.Button buttonDrawBoundary;
        private System.Windows.Forms.Button buttonClear;

        #endregion
    }
}
