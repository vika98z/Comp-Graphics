namespace raster_algorithms
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioPen = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioSelectBorder = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chooseBorderColorBtn = new System.Windows.Forms.Button();
            this.borderColorPan = new System.Windows.Forms.Panel();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(622, 414);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // radioPen
            // 
            this.radioPen.AutoSize = true;
            this.radioPen.Location = new System.Drawing.Point(6, 19);
            this.radioPen.Name = "radioPen";
            this.radioPen.Size = new System.Drawing.Size(76, 17);
            this.radioPen.TabIndex = 8;
            this.radioPen.Text = "Карандаш";
            this.radioPen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioSelectBorder);
            this.groupBox1.Controls.Add(this.radioPen);
            this.groupBox1.Location = new System.Drawing.Point(640, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 69);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // radioSelectBorder
            // 
            this.radioSelectBorder.AutoSize = true;
            this.radioSelectBorder.Location = new System.Drawing.Point(5, 42);
            this.radioSelectBorder.Name = "radioSelectBorder";
            this.radioSelectBorder.Size = new System.Drawing.Size(128, 17);
            this.radioSelectBorder.TabIndex = 11;
            this.radioSelectBorder.TabStop = true;
            this.radioSelectBorder.Text = "Выделение границы";
            this.radioSelectBorder.UseVisualStyleBackColor = true;
            this.radioSelectBorder.CheckedChanged += new System.EventHandler(this.radioSelectBorder_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chooseBorderColorBtn);
            this.groupBox2.Controls.Add(this.borderColorPan);
            this.groupBox2.Location = new System.Drawing.Point(640, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 63);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // chooseBorderColorBtn
            // 
            this.chooseBorderColorBtn.Location = new System.Drawing.Point(5, 18);
            this.chooseBorderColorBtn.Margin = new System.Windows.Forms.Padding(2);
            this.chooseBorderColorBtn.Name = "chooseBorderColorBtn";
            this.chooseBorderColorBtn.Size = new System.Drawing.Size(109, 26);
            this.chooseBorderColorBtn.TabIndex = 12;
            this.chooseBorderColorBtn.Text = "Цвет границы";
            this.chooseBorderColorBtn.UseVisualStyleBackColor = true;
            this.chooseBorderColorBtn.Click += new System.EventHandler(this.chooseBorderColorBtn_Click);
            // 
            // borderColorPan
            // 
            this.borderColorPan.BackColor = System.Drawing.Color.Black;
            this.borderColorPan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.borderColorPan.Location = new System.Drawing.Point(119, 18);
            this.borderColorPan.Name = "borderColorPan";
            this.borderColorPan.Size = new System.Drawing.Size(26, 26);
            this.borderColorPan.TabIndex = 13;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(708, 398);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(81, 23);
            this.clearButton.TabIndex = 16;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 433);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioPen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button chooseBorderColorBtn;
        private System.Windows.Forms.Panel borderColorPan;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RadioButton radioSelectBorder;
    }
}

