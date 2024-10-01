namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1a = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1b = new System.Windows.Forms.Button();
            this.button1c = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1a.Location = new System.Drawing.Point(43, 28);
            this.button1a.Margin = new System.Windows.Forms.Padding(4);
            this.button1a.Name = "button1a";
            this.button1a.Size = new System.Drawing.Size(268, 108);
            this.button1a.TabIndex = 0;
            this.button1a.Text = "button1a";
            this.button1a.UseVisualStyleBackColor = true;
            this.button1a.Click += new System.EventHandler(this.button1a_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(392, 202);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(268, 108);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(733, 202);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(268, 108);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1b
            // 
            this.button1b.Location = new System.Drawing.Point(43, 202);
            this.button1b.Margin = new System.Windows.Forms.Padding(4);
            this.button1b.Name = "button1b";
            this.button1b.Size = new System.Drawing.Size(268, 108);
            this.button1b.TabIndex = 3;
            this.button1b.Text = "button1b";
            this.button1b.UseVisualStyleBackColor = true;
            this.button1b.Click += new System.EventHandler(this.button1b_Click);
            // 
            // button1c
            // 
            this.button1c.Location = new System.Drawing.Point(43, 377);
            this.button1c.Margin = new System.Windows.Forms.Padding(4);
            this.button1c.Name = "button1c";
            this.button1c.Size = new System.Drawing.Size(268, 108);
            this.button1c.TabIndex = 4;
            this.button1c.Text = "button1c";
            this.button1c.UseVisualStyleBackColor = true;
            this.button1c.Click += new System.EventHandler(this.button1c_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.button1c);
            this.Controls.Add(this.button1b);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1a);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1a;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1b;
        private System.Windows.Forms.Button button1c;
    }
}

