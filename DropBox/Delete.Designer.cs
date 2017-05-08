namespace DropBox
{
    partial class Delete
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_delete_normData = new System.Windows.Forms.Button();
            this.btn_delete_survey = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(21, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 618);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_delete_normData);
            this.flowLayoutPanel1.Controls.Add(this.btn_delete_survey);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(758, 59);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn_delete_normData
            // 
            this.btn_delete_normData.Location = new System.Drawing.Point(3, 3);
            this.btn_delete_normData.Name = "btn_delete_normData";
            this.btn_delete_normData.Size = new System.Drawing.Size(113, 56);
            this.btn_delete_normData.TabIndex = 0;
            this.btn_delete_normData.Text = "일반데이터";
            this.btn_delete_normData.UseVisualStyleBackColor = true;
            this.btn_delete_normData.Click += new System.EventHandler(this.btn_delete_normData_Click);
            // 
            // btn_delete_survey
            // 
            this.btn_delete_survey.Location = new System.Drawing.Point(122, 3);
            this.btn_delete_survey.Name = "btn_delete_survey";
            this.btn_delete_survey.Size = new System.Drawing.Size(113, 56);
            this.btn_delete_survey.TabIndex = 1;
            this.btn_delete_survey.Text = "설문조사";
            this.btn_delete_survey.UseVisualStyleBackColor = true;
            this.btn_delete_survey.Click += new System.EventHandler(this.btn_delete_survey_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(0, 59);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(758, 559);
            this.listBox1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(820, 613);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 50);
            this.button3.TabIndex = 1;
            this.button3.Text = "삭제";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 763);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Name = "Delete";
            this.Text = "Delete";
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_delete_normData;
        private System.Windows.Forms.Button btn_delete_survey;
        private System.Windows.Forms.Button button3;
    }
}