using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using System.IO.Compression;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DropBox
{
    public partial class Scenario : Form
    {
        int show_type = 0;    // 0 : new, 1 : edit, 2 : show

        const int NEW = 0;
        const int EDIT = 1;
        const int SHOW = 2;

        ScenarioData sData;
        //Control[] btn_link;
        string mPath;
        int edit_index;
        ListBox listBox;
        int temp_height;
        List<ScenarioData.PATH_DATA> sPath;
        FlowLayoutPanel fpanel_path;
        PrivateFontCollection pfc = new PrivateFontCollection();

        public Scenario()
        {
            InitializeComponent();
        }

        public Scenario(int _show_type, string _path, ScenarioData _Data, int _edit_index, ListBox _listBox)
        {
            /*
            sData : 현재 프로젝트의 시나리오 데이터
            show_type : 작업 종류  ( 0 : NEW, 1 : EDIT, 2 : SHOW )  default : 0
            mPath : 작업 중인 프로젝트의 경로
            edit_index : 시나리오의 내용을 수정하거나 보여주는 작업을 할 때 해당 시나리오만 접근할 수 있도록 미리 인덱스값을 받아온다.
            listBox : 시나리오의 목록을 담고 있는 리스트박스, 작업내용을 반영하는 용도
            sPath : 시나리오의 경로와 시간 정보 리스트
            */

            InitializeComponent();
            sData = _Data;
            show_type = _show_type;
            mPath = _path;
            edit_index = _edit_index;
            listBox = _listBox;
            sPath = new List<ScenarioData.PATH_DATA>();
            
            Setup();

            //EDIT과 SHOW의 경우 현재 인덱스의 기존 정보 불러오기
            if (show_type == EDIT || show_type == SHOW)
            {
                GetInfo();
            }
        }

        private void Setup()
        {
            //폰트 설정
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));

            //사이즈 조정
            this.Size = new Size((int)(Screen.PrimaryScreen.Bounds.Width * 0.8), (int)(Screen.PrimaryScreen.Bounds.Height * 0.8));

            //right wing
            panel_right.Width = (int)(this.Width * 0.3);
            panel_for_picturebox.Size = new Size((int)((this.Height - panel_top.Height - panel_bottom.Height - 200) * 0.7), (int)(this.Height - panel_top.Height - panel_bottom.Height - 200));
            pictureBox_selectedimage.Size = new Size((int)((this.Height - panel_top.Height - panel_bottom.Height - 200)*0.5625), (int)(this.Height - panel_top.Height - panel_bottom.Height - 200));
            button_add.Location = new Point(5, panel_right.Height - panel_bottom.Height - 30 - button_add.Height);
            button_reset.Location = new Point(5 + button_add.Width + 20, button_add.Location.Y);
            label5.Location = new Point(5, label1.Location.Y + panel_top.Height);
            label5.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label5.AutoSize = true;
            comboBox_images.Location = new Point(5, label5.Location.Y + label5.Height + 20);
            comboBox_images.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            panel_for_picturebox.Location = new Point(5, comboBox_images.Location.Y + comboBox_images.Height + 20);
            pictureBox_selectedimage.Location = new Point((int)(panel_for_picturebox.Width / 2) - (int)(pictureBox_selectedimage.Width / 2), 0);

            //left wing
            panel_left.Size = new Size(this.Width - panel_right.Width, this.Height - panel_bottom.Height - panel_top.Height);
            fp_route.Location = new Point(30, 230);
            fp_route.Size = new Size(panel_left.Width - 60, panel_left.Height - 260);
            label1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label3.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label1.Location = new Point(30, label1.Location.Y);
            label2.Location = new Point(30, label1.Location.Y + 60);
            label3.Location = new Point(30, label2.Location.Y + 60);
            textBox1.Location = new Point(label1.Location.X + label1.Width + 30, label1.Location.Y - 5);
            textBox2.Location = new Point(label2.Location.X + label2.Width + 30, label2.Location.Y - 5);
            textBox3.Location = new Point(label3.Location.X + label3.Width + 30, label3.Location.Y - 5);
            textBox1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            textBox2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            textBox3.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

            //temp_height: 시나리오 작업 화면에서 이미지의 사이즈를 설정할 때 사용한다.
            temp_height = (int)(panel_left.Height * 0.5);

            //title
            label_title.Font = new Font(pfc.Families[0], 19, FontStyle.Bold);
            label_title.Location = new Point((int)(this.Width / 2) - (int)(label_title.Width / 2), label_title.Location.Y);

            //bottom
            button_done.Location = new Point((int)(this.Width / 2) + (int)(this.Width * 0.1), button_done.Location.Y);
            button_cancel.Location = new Point((int)(this.Width / 2) - (int)(this.Width * 0.1) - button_cancel.Width, button_cancel.Location.Y);

            //프로젝트 내에 있는 파일이름 가져오기
            DirectoryInfo dinfo = new DirectoryInfo(mPath);
            string[] extensions = new[] { ".jpg", ".tiff", ".bmp", ".png" };
            FileInfo[] files =
                dinfo.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            //btn_link = new Control[files.Length];

            for(int i = 0; i < files.Length; i++)
            {
                comboBox_images.Items.Add(files[i]);
            }
        }

        private void GetInfo()
        {
            /*
            작업하려고 하는 인덱스의 시나리오 경로 정보를 읽어와서 sPath에 추가한다.
            이후에 sPath에 수정사항을 반영하고 최종적으로 기존의 인덱스에 sPath의 정보를 덮어쓴다.
            */
            for(int i = 0; i < sData.getSData()[edit_index].paths.Count; i++)
            {
                sPath.Add(new ScenarioData.PATH_DATA() { tag = sData.getSData()[edit_index].paths[i].tag, path = sData.getSData()[edit_index].paths[i].path, stay_time = sData.getSData()[edit_index].paths[i].stay_time, auto_change_time = sData.getSData()[edit_index].paths[i].auto_change_time });
            }

            //edit box에 데이터 미리 불러오기
            textBox1.Text = sData.getSData()[edit_index].title;
            textBox2.Text = sData.getSData()[edit_index].purpose;

            if (show_type == SHOW)
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }

            /*
            임시로 Button 변수를 만들어서 각 경로의 정보를 Button으로 만들고 패널에 추가한다.
            해당 변수를 재활용하여 모든 경로에 대한 정보를 패널에 추가한다.

            가로방향 FlowLayoutPanel(fpanel_path)위에 세로방향 FlowLayoutPanel(fpanel_path)을 올린다.
            각 fpanel_path에는 1개의 버튼, 2개의 레이블이 포함된다.
            버튼의 배경이미지는 경로정보에서 해당되는 이미지로 설정하고
            2개의 레이블은 각각 이미지 이름과 시간 정보를 나타낸다.
            */
            Button path_pic;
            
            for (int i = 0; i < sPath.Count; i++)
            {
                fpanel_path = new FlowLayoutPanel();
                fpanel_path.FlowDirection = FlowDirection.TopDown;

                path_pic = new Button();
                path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
                path_pic.BackgroundImage = Image.FromFile(mPath + sPath[i].path);
                path_pic.BackgroundImageLayout = ImageLayout.Stretch;
                path_pic.Click += new EventHandler(this.picture_click);
                path_pic.FlatStyle = FlatStyle.Flat;
                path_pic.FlatAppearance.BorderSize = 0;
                path_pic.Name = "path_pic" + sPath[i].tag.ToString();
                path_pic.TabIndex = sPath[i].tag;
                fpanel_path.Controls.Add(path_pic);

                //label
                Label path_name;
                path_name = new Label();
                path_name.Width = fpanel_path.Width;
                path_name.Text = sPath[i].path;
                path_name.BorderStyle = BorderStyle.FixedSingle;
                path_name.ForeColor = Color.FromArgb(96, 96, 96);
                path_name.TextAlign = ContentAlignment.MiddleCenter;
                fpanel_path.Controls.Add(path_name);

                Button path_time;
                path_time = new Button();
                path_time.Width = fpanel_path.Width;
                path_time.Text = "예상체류시간 : " + sPath[sPath.Count - 1].stay_time + "초";
                path_time.TabIndex = sPath[i].tag;
                path_time.FlatStyle = FlatStyle.Flat;
                path_time.ForeColor = Color.FromArgb(96, 96, 96);
                path_time.Name = "path_time" + sPath[i].tag.ToString();
                path_time.Click += new EventHandler(this.picture_click);
                fpanel_path.Controls.Add(path_time);

                Button path_change_time;
                path_change_time = new Button();
                path_change_time.Width = fpanel_path.Width;
                path_change_time.Text = "자동변경시간 : " + sPath[sPath.Count - 1].auto_change_time + "초";
                path_change_time.TabIndex = sPath[sPath.Count - 1].tag;
                path_change_time.Click += new EventHandler(this.picture_click);
                path_change_time.FlatStyle = FlatStyle.Flat;
                path_change_time.ForeColor = Color.FromArgb(96, 96, 96);
                path_change_time.Name = "path_change_time" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path.Controls.Add(path_change_time);

                //fpanel_path2.WrapContents = false;
                fpanel_path.Size = fpanel_path.PreferredSize;
                fpanel_path.Name = "path_pic" + sPath[i].tag.ToString();
                fpanel_path.TabIndex = sPath[i].tag;
                fpanel_path.BackColor = Color.Transparent;

                fp_route.Controls.Add(fpanel_path);
            }
        }

        private void picture_click(object sender, EventArgs e)
        {
            /*
            시나리오 추가, 수정 모드에서 이미지버튼을 클릭하면 시간정보를 입력할 수 있도록 한다.
            이미지버튼을 클릭하면 시간정보를 입력하는 팝업창이 뜨며 입력시 해당 이미지 인덱스에 반영된다.
            경로 리스트에서 해당 이미지의 인덱스 정보는 이름을 비교해서(GetControlByName) 찾는다.
            */

            if (show_type != SHOW)
            {
                Button path_btn = sender as Button;

                Button button1 = new Button();
                Control ctl = GetControlByName(path_btn);

                string var = "";

                if (PopupInput(button1, 4, 75, ref var) == System.Windows.Forms.DialogResult.OK)
                {
                    //((Button)ctl).Text = var;

                    for(int i = 0; i < sPath.Count; i++)
                    {
                        if(sPath[i].tag == path_btn.TabIndex)
                        {
                            string temp_path = sPath[i].path;
                            
                            if (ctl.Name.Contains("change"))
                            {
                                sPath.Insert(i, new ScenarioData.PATH_DATA() { tag = path_btn.TabIndex, path = temp_path, stay_time = sPath[i].stay_time, auto_change_time = var });
                                ((Button)ctl).Text = "자동변경시간 : " + var + "초";
                            }
                            else
                            {
                                sPath.Insert(i, new ScenarioData.PATH_DATA() { tag = path_btn.TabIndex, path = temp_path, stay_time = var, auto_change_time = sPath[i].auto_change_time });
                                ((Button)ctl).Text = "예상체류시간 : " + var + "초";
                            }
                            sPath.RemoveAt(i+1);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("읽기전용 모드입니다.");
            }
        }

        Control GetControlByName(Button clicked_btn)
        {
            foreach (Control c in this.fp_route.Controls)
            {
                foreach(Control ct in c.Controls)
                {
                    if (ct.Name == clicked_btn.Name)
                    {
                        if (ct is Button && ct.TabIndex == clicked_btn.TabIndex)
                        {
                            return ct;
                        }
                    }
                }
            }
            return null;
        }

        public DialogResult PopupInput(Control ctrl, int border, int length, ref string output)
        {
            Point ctrlpt = Control.MousePosition;

            TextBox input = new TextBox { Height = 20, Width = length, Top = border / 2, Left = border / 2 };
            input.BorderStyle = BorderStyle.FixedSingle;
            //######## SetColor to your preference
            input.BackColor = Color.Azure;

            Button btnok = new Button { DialogResult = System.Windows.Forms.DialogResult.OK, Top = 25 };
            Button btncn = new Button { DialogResult = System.Windows.Forms.DialogResult.Cancel, Top = 25 };

            btnok.Text = "확인";
            btncn.Text = "취소";
            btnok.BackColor = Color.Wheat;
            btncn.BackColor = Color.Wheat;
            btnok.Location = new Point(btncn.Location.X + btncn.Width, btncn.Location.Y);

            Form frm = new Form { ControlBox = false, AcceptButton = btnok, CancelButton = btncn, StartPosition = FormStartPosition.Manual, Location = ctrlpt };
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //######## SetColor to your preference
            frm.BackColor = Color.Navy;

            RectangleF rec = new RectangleF(0, 0, (length + border) * 2, (20 + border) * 2);
            GraphicsPath GP = new GraphicsPath(); //GetRoundedRect(rec, 4.0F);
            float diameter = 8.0F;
            SizeF sizef = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(rec.Location, sizef);
            GP.AddArc(arc, 180, 90);
            arc.X = rec.Right - diameter;
            GP.AddArc(arc, 270, 90);
            arc.Y = rec.Bottom - diameter;
            GP.AddArc(arc, 0, 90);
            arc.X = rec.Left;
            GP.AddArc(arc, 90, 90);
            GP.CloseFigure();

            frm.Region = new Region(GP);
            frm.Controls.AddRange(new Control[] { input, btncn, btnok });
            DialogResult rst = frm.ShowDialog();
            output = input.Text;
            return rst;
        }
       
        private void path_time_click(object sender, EventArgs e)
        {
            MessageBox.Show("haha");
        }

        //private void SelectImage(object sender, EventArgs e)
        //{
        //    Control ctl = sender as Control;
        //    Button btn = sender as Button;
        //    if (ctl != null)
        //    {
        //        fpanel_path = new FlowLayoutPanel();
        //        fpanel_path.FlowDirection = FlowDirection.TopDown;


        //        //여기서 에러나면 맨 처음은 0으로 설정
        //        try
        //        {
        //            sPath.Add(new ScenarioData.PATH_DATA() { tag = sPath[sPath.Count - 1].tag + 1, path = btn.Name, time = "-1" });
        //        }
        //        catch(ArgumentOutOfRangeException ae)
        //        {
        //            sPath.Add(new ScenarioData.PATH_DATA() { tag = 1, path = btn.Name, time = "-1" });
        //        }

        //        MessageBox.Show(btn.Name);
        //        Button path_pic = new Button();
        //        path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
        //        path_pic.BackgroundImage = Image.FromFile(mPath + sPath[sPath.Count-1].path);
        //        path_pic.BackgroundImageLayout = ImageLayout.Stretch;
        //        path_pic.Click += new EventHandler(this.picture_click);
        //        path_pic.FlatStyle = FlatStyle.Flat;
        //        path_pic.FlatAppearance.BorderSize = 0;
        //        path_pic.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
        //        path_pic.TabIndex = sPath[sPath.Count - 1].tag;

        //        fpanel_path.Controls.Add(path_pic);
        //        fp_route.AutoScroll = true;

        //        //label
        //        Label path_name;
        //        path_name = new Label();
        //        path_name.Text = sPath[sPath.Count - 1].path;
        //        fpanel_path.Controls.Add(path_name);

        //        Button path_time;
        //        path_time = new Button();
        //        path_time.Text = sPath[sPath.Count - 1].time;
        //        path_time.TabIndex = sPath[sPath.Count - 1].tag;
        //        path_pic.Click += new EventHandler(this.path_time_click);
        //        path_time.Name = "path_time" + sPath[sPath.Count - 1].tag.ToString();
        //        fpanel_path.Controls.Add(path_time);

        //        fpanel_path.WrapContents = false;
        //        fpanel_path.Size = fpanel_path.PreferredSize;
        //        fpanel_path.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
        //        fpanel_path.TabIndex = sPath[sPath.Count - 1].tag;
        //        fpanel_path.BackColor = Color.Transparent;
        //        fp_route.Controls.Add(fpanel_path);
        //        fp_route.WrapContents = false;
        //        fp_route.AutoScroll = true;
        //    }
        //    //완료 버튼 누르면 종료하는 것으로.
        //}

        private void comboBox_images_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control ctl = sender as Control;

            pictureBox_selectedimage.BackgroundImage = Image.FromFile(mPath + ctl.Text);
            pictureBox_selectedimage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void textBoxPurpose_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_add_click(object sender, EventArgs e)
        {
            if (show_type == SHOW)
            {
                MessageBox.Show("읽기전용 모드입니다.");
                return;
            }

            //여기서 flowpanel에 추가
            Control ctl = sender as Control;
            Button btn = sender as Button;

            //fpanel_scenario_image.Visible = false;
            //this.Height = panel1.Height;
            if (ctl != null)
            {
                fpanel_path = new FlowLayoutPanel();
                fpanel_path.FlowDirection = FlowDirection.TopDown;

                //여기서 에러나면 맨 처음은 0으로 설정
                try
                {
                    sPath.Add(new ScenarioData.PATH_DATA() { tag = sPath[sPath.Count - 1].tag + 1, path = comboBox_images.SelectedItem.ToString(), stay_time = "3", auto_change_time = "0" });
                }
                catch (ArgumentOutOfRangeException ae)
                {
                    sPath.Add(new ScenarioData.PATH_DATA() { tag = 1, path = comboBox_images.SelectedItem.ToString(), stay_time = "3", auto_change_time = "0" });
                }

                Button path_pic = new Button();
                path_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);
                path_pic.BackgroundImage = Image.FromFile(mPath + sPath[sPath.Count - 1].path);
                path_pic.BackgroundImageLayout = ImageLayout.Stretch;
                path_pic.Click += new EventHandler(this.picture_click);
                path_pic.FlatStyle = FlatStyle.Flat;
                path_pic.FlatAppearance.BorderSize = 0;
                path_pic.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
                path_pic.TabIndex = sPath[sPath.Count - 1].tag;

                fpanel_path.Controls.Add(path_pic);

                //label
                Label path_name;
                path_name = new Label();
                path_name.Width = fpanel_path.Width;
                path_name.BorderStyle = BorderStyle.FixedSingle;
                path_name.Text = sPath[sPath.Count - 1].path;
                path_name.ForeColor = Color.FromArgb(96, 96, 96);
                path_name.TextAlign = ContentAlignment.MiddleCenter;
                fpanel_path.Controls.Add(path_name);

                Button path_time;
                path_time = new Button();
                path_time.Width = fpanel_path.Width;
                path_time.Text = "예상체류시간 : " + sPath[sPath.Count - 1].stay_time + "초";
                path_time.TabIndex = sPath[sPath.Count - 1].tag;
                path_time.Click += new EventHandler(this.picture_click);
                path_time.FlatStyle = FlatStyle.Flat;
                path_time.ForeColor = Color.FromArgb(96, 96, 96);
                path_time.Name = "path_time" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path.Controls.Add(path_time);

                Button path_change_time;
                path_change_time = new Button();
                path_change_time.Width = fpanel_path.Width;
                path_change_time.Text = "자동변경시간 : " + sPath[sPath.Count - 1].auto_change_time + "초";
                path_change_time.TabIndex = sPath[sPath.Count - 1].tag;
                path_change_time.Click += new EventHandler(this.picture_click);
                path_change_time.FlatStyle = FlatStyle.Flat;
                path_change_time.ForeColor = Color.FromArgb(96, 96, 96);
                path_change_time.Name = "path_change_time" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path.Controls.Add(path_change_time);

                fpanel_path.WrapContents = false;
                fpanel_path.Size = fpanel_path.PreferredSize;
                fpanel_path.Name = "path_pic" + sPath[sPath.Count - 1].tag.ToString();
                fpanel_path.TabIndex = sPath[sPath.Count - 1].tag;
                fpanel_path.BackColor = Color.Transparent;
                fp_route.Controls.Add(fpanel_path);
                fp_route.WrapContents = false;
                fp_route.AutoScroll = true;

            }
            //완료 버튼 누르면 종료하는 것으로.
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            if(show_type == SHOW)
            {
                MessageBox.Show("읽기전용 모드입니다.");
            }
            else
            {
                //리셋
            }
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            //this.Height = panel_left.Height;
            //저장한 거 어레이에 두었다가 한 번에 저장하면서 xml파일 만들기

            //pTemp_data.id = 어레이의 카운트 값을 입력
            string pTitle = textBox1.Text;
            string pPurpose = textBox2.Text;
            string pTime = textBox3.Text;


            //시간은 초단위로만 받기
            try
            {
                if (pTime.CompareTo("") == 0)
                {
                    pTime = "0";
                }
                int temp = Convert.ToInt32(pTime);

            }
            catch (FormatException fe)
            {
                MessageBox.Show("숫자만 입력해주세요");
            }



            //시나리오 이름에 언더바 있으면 안 됨!!!!

            //팝업창 띄워서 내용 확인해서 오케이 누르면 아래 진행

            if (pTitle.Contains("_"))
            {
                MessageBox.Show("제목에 '_' 기호를 포함시킬 수 없습니다.");
            }

            else if (pTitle.CompareTo("") == 0)
            {
                MessageBox.Show("제목을 입력하세요.");
            }

            else if (show_type == NEW)
            {
                for (int i = 0; i < sPath.Count; i++)
                {
                    Console.WriteLine(sPath[i].path);
                }
                sData.AddScenario((sData.getSLastIndex() + 1), pTitle, pPurpose, pTime, "1", sPath);
                ShowDialog_save();
                listBox.Items.Add(pTitle);
                sData.changed = 1;
                this.Close();
            }
            else if (show_type == EDIT)
            {
                sData.getSData()[edit_index] = new ScenarioData.SCENARIO_DATA() { tag = sData.getSData()[edit_index].tag, title = pTitle, purpose = pPurpose, time = pTime, level = "2", paths = sPath };
                MessageBox.Show("수정되었습니다.");
                listBox.Items.RemoveAt(edit_index);
                listBox.Items.Insert(edit_index, pTitle);
                sData.changed = 1;
                this.Close();
            }
            else if (show_type == SHOW)
            {
                this.Close();
            }
        }

        public void ShowDialog_save()
        {
            Form prompt = new Form();
            prompt.Width = 598;
            prompt.Height = 278;
            prompt.BackColor = Color.White;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.ControlBox = false;
            prompt.FormBorderStyle = FormBorderStyle.None;
            prompt.ForeColor = Color.Maroon;

            Panel panel_dialog = new Panel();
            panel_dialog.Size = prompt.Size;
            panel_dialog.Location = new Point(0, 0);
            panel_dialog.BorderStyle = BorderStyle.FixedSingle;
            panel_dialog.ForeColor = Color.Maroon;

            Panel title = new Panel();
            title.Size = new Size(prompt.Width, 50);
            title.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._1_bg_35_35));
            title.BackgroundImageLayout = ImageLayout.Stretch;
            title.Location = new Point(0, 0);

            Label label_title = new Label();
            label_title.Text = "Save";
            label_title.Size = new Size(300, 40);
            label_title.TextAlign = ContentAlignment.MiddleCenter;
            label_title.BringToFront();
            label_title.Location = new Point((int)(prompt.Width / 2) - (int)(label_title.Width / 2), 5);
            label_title.BackColor = Color.Transparent;

            label_title.Font = new Font(pfc.Families[0], 18, FontStyle.Bold);
            label_title.ForeColor = Color.White;
            title.Controls.Add(label_title);

            Label label_question = new Label();
            label_question.Text = "저장되었습니다.";
            label_question.TextAlign = ContentAlignment.MiddleCenter;
            label_question.BackColor = Color.Transparent;
            label_question.Size = new Size(500, 50);
            label_question.Location = new Point((int)(prompt.Width / 2) - (int)(label_question.Width / 2), 100);
            label_question.Font = new Font(pfc.Families[0], 17, FontStyle.Bold);
            label_question.ForeColor = Color.Black;

            Button confirm = new Button();
            confirm.Size = new Size(140, 47);
            confirm.Location = new Point((int)(prompt.Width * 0.5) - (int)(confirm.Width / 2), 180);
            confirm.FlatStyle = FlatStyle.Flat;
            confirm.BackColor = Color.Transparent;
            confirm.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._3_ok));
            confirm.BackgroundImageLayout = ImageLayout.Stretch;

            confirm.Click += (sender, e) => {
                prompt.Close();
            };

            prompt.Controls.Add(panel_dialog);
            panel_dialog.Controls.Add(title);
            panel_dialog.Controls.Add(confirm);
            panel_dialog.Controls.Add(label_question);
            prompt.ShowDialog();
        }

    }
}
