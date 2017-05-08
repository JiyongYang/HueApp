namespace DropBox
{
    partial class Scenario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scenario));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_left = new System.Windows.Forms.Panel();
            this.fp_route = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_done = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.panel_for_picturebox = new System.Windows.Forms.Panel();
            this.pictureBox_selectedimage = new System.Windows.Forms.PictureBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.comboBox_images = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_top = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.panel_left.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel_right.SuspendLayout();
            this.panel_for_picturebox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_selectedimage)).BeginInit();
            this.panel_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(186, 22);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(393, 37);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(186, 75);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(393, 37);
            this.textBox2.TabIndex = 6;
            this.textBox2.TextChanged += new System.EventHandler(this.textBoxPurpose_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(186, 131);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(393, 37);
            this.textBox3.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(39, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 33);
            this.label1.TabIndex = 8;
            this.label1.Text = "제      목 :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(37, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 33);
            this.label2.TabIndex = 9;
            this.label2.Text = "목      적 :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(37, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "예상시간 :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_left
            // 
            this.panel_left.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_left.BackColor = System.Drawing.Color.White;
            this.panel_left.Controls.Add(this.fp_route);
            this.panel_left.Controls.Add(this.label1);
            this.panel_left.Controls.Add(this.label2);
            this.panel_left.Controls.Add(this.label3);
            this.panel_left.Controls.Add(this.textBox1);
            this.panel_left.Controls.Add(this.textBox3);
            this.panel_left.Controls.Add(this.textBox2);
            this.panel_left.Location = new System.Drawing.Point(2, 50);
            this.panel_left.Margin = new System.Windows.Forms.Padding(2);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(660, 448);
            this.panel_left.TabIndex = 11;
            // 
            // fp_route
            // 
            this.fp_route.AutoScroll = true;
            this.fp_route.BackColor = System.Drawing.Color.Gainsboro;
            this.fp_route.Location = new System.Drawing.Point(0, 167);
            this.fp_route.Name = "fp_route";
            this.fp_route.Size = new System.Drawing.Size(598, 225);
            this.fp_route.TabIndex = 14;
            this.fp_route.WrapContents = false;
            // 
            // panel_bottom
            // 
            this.panel_bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_bottom.Controls.Add(this.button_cancel);
            this.panel_bottom.Controls.Add(this.button_done);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.ForeColor = System.Drawing.Color.Maroon;
            this.panel_bottom.Location = new System.Drawing.Point(0, 573);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(1053, 104);
            this.panel_bottom.TabIndex = 17;
            // 
            // button_cancel
            // 
            this.button_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_cancel.BackgroundImage")));
            this.button_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.ForeColor = System.Drawing.Color.DimGray;
            this.button_cancel.Location = new System.Drawing.Point(251, 25);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(140, 47);
            this.button_cancel.TabIndex = 16;
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_done
            // 
            this.button_done.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_done.BackgroundImage")));
            this.button_done.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_done.Location = new System.Drawing.Point(623, 25);
            this.button_done.Margin = new System.Windows.Forms.Padding(2);
            this.button_done.Name = "button_done";
            this.button_done.Size = new System.Drawing.Size(140, 47);
            this.button_done.TabIndex = 4;
            this.button_done.UseVisualStyleBackColor = true;
            this.button_done.Click += new System.EventHandler(this.button_done_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel_right);
            this.panel4.Controls.Add(this.panel_left);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.ForeColor = System.Drawing.Color.Maroon;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1053, 677);
            this.panel4.TabIndex = 18;
            // 
            // panel_right
            // 
            this.panel_right.BackColor = System.Drawing.Color.White;
            this.panel_right.Controls.Add(this.panel_for_picturebox);
            this.panel_right.Controls.Add(this.button_reset);
            this.panel_right.Controls.Add(this.button_add);
            this.panel_right.Controls.Add(this.comboBox_images);
            this.panel_right.Controls.Add(this.label5);
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_right.Location = new System.Drawing.Point(745, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(306, 675);
            this.panel_right.TabIndex = 12;
            // 
            // panel_for_picturebox
            // 
            this.panel_for_picturebox.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_for_picturebox.Controls.Add(this.pictureBox_selectedimage);
            this.panel_for_picturebox.Location = new System.Drawing.Point(16, 113);
            this.panel_for_picturebox.Name = "panel_for_picturebox";
            this.panel_for_picturebox.Size = new System.Drawing.Size(215, 293);
            this.panel_for_picturebox.TabIndex = 5;
            // 
            // pictureBox_selectedimage
            // 
            this.pictureBox_selectedimage.Location = new System.Drawing.Point(34, 0);
            this.pictureBox_selectedimage.Name = "pictureBox_selectedimage";
            this.pictureBox_selectedimage.Size = new System.Drawing.Size(151, 240);
            this.pictureBox_selectedimage.TabIndex = 2;
            this.pictureBox_selectedimage.TabStop = false;
            // 
            // button_reset
            // 
            this.button_reset.BackColor = System.Drawing.Color.White;
            this.button_reset.BackgroundImage = global::DropBox.Properties.Resources._5_Reset;
            this.button_reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_reset.Location = new System.Drawing.Point(127, 412);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(104, 37);
            this.button_reset.TabIndex = 4;
            this.button_reset.UseVisualStyleBackColor = false;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // button_add
            // 
            this.button_add.BackColor = System.Drawing.Color.White;
            this.button_add.BackgroundImage = global::DropBox.Properties.Resources._5_add;
            this.button_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_add.ForeColor = System.Drawing.Color.DimGray;
            this.button_add.Location = new System.Drawing.Point(16, 412);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(104, 37);
            this.button_add.TabIndex = 3;
            this.button_add.UseVisualStyleBackColor = false;
            this.button_add.Click += new System.EventHandler(this.button_add_click);
            // 
            // comboBox_images
            // 
            this.comboBox_images.DropDownHeight = 120;
            this.comboBox_images.Font = new System.Drawing.Font("굴림", 15F);
            this.comboBox_images.FormattingEnabled = true;
            this.comboBox_images.IntegralHeight = false;
            this.comboBox_images.ItemHeight = 20;
            this.comboBox_images.Location = new System.Drawing.Point(16, 79);
            this.comboBox_images.Name = "comboBox_images";
            this.comboBox_images.Size = new System.Drawing.Size(151, 28);
            this.comboBox_images.TabIndex = 1;
            this.comboBox_images.SelectedIndexChanged += new System.EventHandler(this.comboBox_images_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(14, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "경로 만들기";
            // 
            // panel_top
            // 
            this.panel_top.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_top.BackgroundImage")));
            this.panel_top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_top.Controls.Add(this.label_title);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(1053, 50);
            this.panel_top.TabIndex = 16;
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("굴림", 14F);
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(351, 12);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(122, 19);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "New Scenario";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Scenario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1053, 677);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_top);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Scenario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scenario";
            this.panel_left.ResumeLayout(false);
            this.panel_left.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel_right.ResumeLayout(false);
            this.panel_right.PerformLayout();
            this.panel_for_picturebox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_selectedimage)).EndInit();
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_done;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.FlowLayoutPanel fp_route;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.PictureBox pictureBox_selectedimage;
        private System.Windows.Forms.ComboBox comboBox_images;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Panel panel_for_picturebox;
    }
}