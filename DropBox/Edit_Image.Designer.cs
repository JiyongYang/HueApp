namespace DropBox
{
    partial class Edit_Image
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edit_Image));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_image_link = new System.Windows.Forms.Panel();
            this.panel_for_pic = new System.Windows.Forms.Panel();
            this.pictureBox_main = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.del_btn = new System.Windows.Forms.Button();
            this.fpanel_editImage_link = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_editImage_total = new System.Windows.Forms.Panel();
            this.panel_editImage_right = new System.Windows.Forms.Panel();
            this.panel_detail_info_label = new System.Windows.Forms.Panel();
            this.label_detail_info_title = new System.Windows.Forms.Label();
            this.panel_detail_info = new System.Windows.Forms.Panel();
            this.label_info = new System.Windows.Forms.Label();
            this.panel_editImage_left2 = new System.Windows.Forms.Panel();
            this.btn_swipe_down = new System.Windows.Forms.Button();
            this.btn_swipe_up = new System.Windows.Forms.Button();
            this.btn_swipe_right = new System.Windows.Forms.Button();
            this.btn_swipe_left = new System.Windows.Forms.Button();
            this.label_zoom = new System.Windows.Forms.Label();
            this.label_swipe = new System.Windows.Forms.Label();
            this.panel_editImage_link = new System.Windows.Forms.Panel();
            this.btn_close_linkPanel = new System.Windows.Forms.Button();
            this.link_alloc = new System.Windows.Forms.Button();
            this.panel_for_pic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).BeginInit();
            this.panel_editImage_total.SuspendLayout();
            this.panel_editImage_right.SuspendLayout();
            this.panel_detail_info_label.SuspendLayout();
            this.panel_detail_info.SuspendLayout();
            this.panel_editImage_left2.SuspendLayout();
            this.panel_editImage_link.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_image_link
            // 
            this.panel_image_link.AutoScroll = true;
            this.panel_image_link.AutoSize = true;
            this.panel_image_link.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_image_link.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_image_link.Location = new System.Drawing.Point(0, 739);
            this.panel_image_link.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel_image_link.Name = "panel_image_link";
            this.panel_image_link.Size = new System.Drawing.Size(1197, 0);
            this.panel_image_link.TabIndex = 7;
            this.panel_image_link.Visible = false;
            // 
            // panel_for_pic
            // 
            this.panel_for_pic.BackColor = System.Drawing.Color.Transparent;
            this.panel_for_pic.Controls.Add(this.pictureBox_main);
            this.panel_for_pic.Location = new System.Drawing.Point(370, 40);
            this.panel_for_pic.Margin = new System.Windows.Forms.Padding(2);
            this.panel_for_pic.Name = "panel_for_pic";
            this.panel_for_pic.Size = new System.Drawing.Size(283, 444);
            this.panel_for_pic.TabIndex = 10;
            // 
            // pictureBox_main
            // 
            this.pictureBox_main.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_main.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_main.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox_main.Name = "pictureBox_main";
            this.pictureBox_main.Size = new System.Drawing.Size(283, 444);
            this.pictureBox_main.TabIndex = 9;
            this.pictureBox_main.TabStop = false;
            this.pictureBox_main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.pictureBox_main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBox_main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(832, 202);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(163, 81);
            this.listBox1.TabIndex = 13;
            this.listBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseUp);
            // 
            // del_btn
            // 
            this.del_btn.Location = new System.Drawing.Point(932, 142);
            this.del_btn.Margin = new System.Windows.Forms.Padding(2);
            this.del_btn.Name = "del_btn";
            this.del_btn.Size = new System.Drawing.Size(63, 39);
            this.del_btn.TabIndex = 15;
            this.del_btn.Text = "UNLINK";
            this.del_btn.UseVisualStyleBackColor = true;
            this.del_btn.Click += new System.EventHandler(this.del_btn_Click);
            // 
            // fpanel_editImage_link
            // 
            this.fpanel_editImage_link.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpanel_editImage_link.Location = new System.Drawing.Point(0, 43);
            this.fpanel_editImage_link.Margin = new System.Windows.Forms.Padding(2);
            this.fpanel_editImage_link.Name = "fpanel_editImage_link";
            this.fpanel_editImage_link.Size = new System.Drawing.Size(633, 160);
            this.fpanel_editImage_link.TabIndex = 17;
            // 
            // panel_editImage_total
            // 
            this.panel_editImage_total.BackColor = System.Drawing.Color.White;
            this.panel_editImage_total.Controls.Add(this.panel_editImage_right);
            this.panel_editImage_total.Controls.Add(this.panel_editImage_left2);
            this.panel_editImage_total.Controls.Add(this.panel_for_pic);
            this.panel_editImage_total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_editImage_total.Location = new System.Drawing.Point(0, 0);
            this.panel_editImage_total.Name = "panel_editImage_total";
            this.panel_editImage_total.Size = new System.Drawing.Size(1197, 739);
            this.panel_editImage_total.TabIndex = 20;
            // 
            // panel_editImage_right
            // 
            this.panel_editImage_right.BackColor = System.Drawing.Color.White;
            this.panel_editImage_right.Controls.Add(this.panel_detail_info_label);
            this.panel_editImage_right.Controls.Add(this.panel_detail_info);
            this.panel_editImage_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_editImage_right.Location = new System.Drawing.Point(731, 0);
            this.panel_editImage_right.Margin = new System.Windows.Forms.Padding(2);
            this.panel_editImage_right.Name = "panel_editImage_right";
            this.panel_editImage_right.Size = new System.Drawing.Size(466, 739);
            this.panel_editImage_right.TabIndex = 12;
            // 
            // panel_detail_info_label
            // 
            this.panel_detail_info_label.BackgroundImage = global::DropBox.Properties.Resources._8_4_line;
            this.panel_detail_info_label.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_detail_info_label.Controls.Add(this.label_detail_info_title);
            this.panel_detail_info_label.Location = new System.Drawing.Point(4, 44);
            this.panel_detail_info_label.Name = "panel_detail_info_label";
            this.panel_detail_info_label.Size = new System.Drawing.Size(120, 40);
            this.panel_detail_info_label.TabIndex = 2;
            // 
            // label_detail_info_title
            // 
            this.label_detail_info_title.ForeColor = System.Drawing.Color.Black;
            this.label_detail_info_title.Location = new System.Drawing.Point(11, 4);
            this.label_detail_info_title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_detail_info_title.Name = "label_detail_info_title";
            this.label_detail_info_title.Size = new System.Drawing.Size(107, 23);
            this.label_detail_info_title.TabIndex = 0;
            this.label_detail_info_title.Text = "세부정보";
            this.label_detail_info_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_detail_info
            // 
            this.panel_detail_info.AutoScroll = true;
            this.panel_detail_info.BackgroundImage = global::DropBox.Properties.Resources.test2;
            this.panel_detail_info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_detail_info.Controls.Add(this.label_info);
            this.panel_detail_info.Location = new System.Drawing.Point(4, 100);
            this.panel_detail_info.Name = "panel_detail_info";
            this.panel_detail_info.Size = new System.Drawing.Size(420, 548);
            this.panel_detail_info.TabIndex = 1;
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.BackColor = System.Drawing.Color.Transparent;
            this.label_info.ForeColor = System.Drawing.Color.Black;
            this.label_info.Location = new System.Drawing.Point(30, 20);
            this.label_info.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(0, 12);
            this.label_info.TabIndex = 11;
            // 
            // panel_editImage_left2
            // 
            this.panel_editImage_left2.BackColor = System.Drawing.Color.White;
            this.panel_editImage_left2.Controls.Add(this.btn_swipe_down);
            this.panel_editImage_left2.Controls.Add(this.btn_swipe_up);
            this.panel_editImage_left2.Controls.Add(this.btn_swipe_right);
            this.panel_editImage_left2.Controls.Add(this.btn_swipe_left);
            this.panel_editImage_left2.Controls.Add(this.label_zoom);
            this.panel_editImage_left2.Controls.Add(this.label_swipe);
            this.panel_editImage_left2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_editImage_left2.Location = new System.Drawing.Point(0, 0);
            this.panel_editImage_left2.Name = "panel_editImage_left2";
            this.panel_editImage_left2.Size = new System.Drawing.Size(305, 739);
            this.panel_editImage_left2.TabIndex = 11;
            // 
            // btn_swipe_down
            // 
            this.btn_swipe_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_swipe_down.BackgroundImage")));
            this.btn_swipe_down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_swipe_down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_swipe_down.Location = new System.Drawing.Point(165, 173);
            this.btn_swipe_down.Name = "btn_swipe_down";
            this.btn_swipe_down.Size = new System.Drawing.Size(75, 75);
            this.btn_swipe_down.TabIndex = 18;
            this.btn_swipe_down.UseVisualStyleBackColor = true;
            this.btn_swipe_down.Click += new System.EventHandler(this.btn_swipe_down_Click);
            // 
            // btn_swipe_up
            // 
            this.btn_swipe_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_swipe_up.BackgroundImage")));
            this.btn_swipe_up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_swipe_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_swipe_up.Location = new System.Drawing.Point(80, 173);
            this.btn_swipe_up.Name = "btn_swipe_up";
            this.btn_swipe_up.Size = new System.Drawing.Size(75, 75);
            this.btn_swipe_up.TabIndex = 17;
            this.btn_swipe_up.UseVisualStyleBackColor = true;
            this.btn_swipe_up.Click += new System.EventHandler(this.btn_swipe_up_Click);
            // 
            // btn_swipe_right
            // 
            this.btn_swipe_right.BackColor = System.Drawing.Color.White;
            this.btn_swipe_right.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_swipe_right.BackgroundImage")));
            this.btn_swipe_right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_swipe_right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_swipe_right.Location = new System.Drawing.Point(165, 80);
            this.btn_swipe_right.Name = "btn_swipe_right";
            this.btn_swipe_right.Size = new System.Drawing.Size(75, 75);
            this.btn_swipe_right.TabIndex = 16;
            this.btn_swipe_right.UseVisualStyleBackColor = false;
            this.btn_swipe_right.Click += new System.EventHandler(this.btn_swipe_right_Click);
            // 
            // btn_swipe_left
            // 
            this.btn_swipe_left.BackColor = System.Drawing.Color.White;
            this.btn_swipe_left.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_swipe_left.BackgroundImage")));
            this.btn_swipe_left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_swipe_left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_swipe_left.Location = new System.Drawing.Point(80, 80);
            this.btn_swipe_left.Name = "btn_swipe_left";
            this.btn_swipe_left.Size = new System.Drawing.Size(75, 75);
            this.btn_swipe_left.TabIndex = 15;
            this.btn_swipe_left.UseVisualStyleBackColor = false;
            this.btn_swipe_left.Click += new System.EventHandler(this.btn_swipe_left_Click);
            // 
            // label_zoom
            // 
            this.label_zoom.AutoSize = true;
            this.label_zoom.Font = new System.Drawing.Font("굴림", 18F);
            this.label_zoom.ForeColor = System.Drawing.Color.Black;
            this.label_zoom.Location = new System.Drawing.Point(80, 342);
            this.label_zoom.Name = "label_zoom";
            this.label_zoom.Size = new System.Drawing.Size(70, 24);
            this.label_zoom.TabIndex = 14;
            this.label_zoom.Text = "Zoom";
            this.label_zoom.Visible = false;
            // 
            // label_swipe
            // 
            this.label_swipe.AutoSize = true;
            this.label_swipe.Font = new System.Drawing.Font("굴림", 18F);
            this.label_swipe.ForeColor = System.Drawing.Color.Black;
            this.label_swipe.Location = new System.Drawing.Point(80, 44);
            this.label_swipe.Name = "label_swipe";
            this.label_swipe.Size = new System.Drawing.Size(73, 24);
            this.label_swipe.TabIndex = 13;
            this.label_swipe.Text = "Swipe";
            // 
            // panel_editImage_link
            // 
            this.panel_editImage_link.BackColor = System.Drawing.Color.White;
            this.panel_editImage_link.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_editImage_link.Controls.Add(this.btn_close_linkPanel);
            this.panel_editImage_link.Controls.Add(this.fpanel_editImage_link);
            this.panel_editImage_link.ForeColor = System.Drawing.Color.Black;
            this.panel_editImage_link.Location = new System.Drawing.Point(43, 431);
            this.panel_editImage_link.Name = "panel_editImage_link";
            this.panel_editImage_link.Size = new System.Drawing.Size(635, 205);
            this.panel_editImage_link.TabIndex = 18;
            this.panel_editImage_link.Visible = false;
            // 
            // btn_close_linkPanel
            // 
            this.btn_close_linkPanel.BackColor = System.Drawing.Color.Transparent;
            this.btn_close_linkPanel.BackgroundImage = global::DropBox.Properties.Resources.close;
            this.btn_close_linkPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_linkPanel.FlatAppearance.BorderSize = 0;
            this.btn_close_linkPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_linkPanel.Location = new System.Drawing.Point(590, 8);
            this.btn_close_linkPanel.Name = "btn_close_linkPanel";
            this.btn_close_linkPanel.Size = new System.Drawing.Size(16, 16);
            this.btn_close_linkPanel.TabIndex = 18;
            this.btn_close_linkPanel.UseVisualStyleBackColor = false;
            this.btn_close_linkPanel.Click += new System.EventHandler(this.btn_close_linkPanel_Click);
            // 
            // link_alloc
            // 
            this.link_alloc.BackColor = System.Drawing.Color.Transparent;
            this.link_alloc.BackgroundImage = global::DropBox.Properties.Resources._10_link_pop;
            this.link_alloc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.link_alloc.FlatAppearance.BorderSize = 0;
            this.link_alloc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.link_alloc.Location = new System.Drawing.Point(435, 227);
            this.link_alloc.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.link_alloc.Name = "link_alloc";
            this.link_alloc.Size = new System.Drawing.Size(105, 43);
            this.link_alloc.TabIndex = 6;
            this.link_alloc.UseVisualStyleBackColor = false;
            this.link_alloc.Visible = false;
            this.link_alloc.Click += new System.EventHandler(this.button5_Click);
            // 
            // Edit_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1197, 739);
            this.Controls.Add(this.panel_editImage_link);
            this.Controls.Add(this.panel_editImage_total);
            this.Controls.Add(this.del_btn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel_image_link);
            this.Controls.Add(this.link_alloc);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Edit_Image";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Edit_Image_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel_for_pic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).EndInit();
            this.panel_editImage_total.ResumeLayout(false);
            this.panel_editImage_right.ResumeLayout(false);
            this.panel_detail_info_label.ResumeLayout(false);
            this.panel_detail_info.ResumeLayout(false);
            this.panel_detail_info.PerformLayout();
            this.panel_editImage_left2.ResumeLayout(false);
            this.panel_editImage_left2.PerformLayout();
            this.panel_editImage_link.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button link_alloc;
        private System.Windows.Forms.Panel panel_image_link;
        private System.Windows.Forms.PictureBox pictureBox_main;
        private System.Windows.Forms.Panel panel_for_pic;
        private System.Windows.Forms.Label label_info;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button del_btn;
        private System.Windows.Forms.FlowLayoutPanel fpanel_editImage_link;
        private System.Windows.Forms.Panel panel_editImage_total;
        private System.Windows.Forms.Panel panel_editImage_left2;
        private System.Windows.Forms.Label label_zoom;
        private System.Windows.Forms.Label label_swipe;
        private System.Windows.Forms.Panel panel_editImage_right;
        private System.Windows.Forms.Panel panel_detail_info_label;
        private System.Windows.Forms.Label label_detail_info_title;
        private System.Windows.Forms.Panel panel_detail_info;
        private System.Windows.Forms.Panel panel_editImage_link;
        private System.Windows.Forms.Button btn_close_linkPanel;
        private System.Windows.Forms.Button btn_swipe_down;
        private System.Windows.Forms.Button btn_swipe_up;
        private System.Windows.Forms.Button btn_swipe_right;
        private System.Windows.Forms.Button btn_swipe_left;
    }
}

