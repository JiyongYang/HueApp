using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using Ionic.Zip;
using System.Drawing.Text;

namespace DropBox
{
    public partial class Edit_Image : Form
    {
        ////%%%%%%%%%%%%%%%%%%%%%%%
        //public struct LINK
        //{
        //    public string fileName;
        //    public List<link_info_temp> link_data_temp;
        //};

        //public struct link_info_temp
        //{
        //    public int btn_id;
        //    public Button bttn;
        //    public string DstFile;
        //    public Point image_xy;
        //    public float image_width;
        //    public float image_height;
        //    public string input_type;
        //};
        ////%%%%%%%%%%%%%%%%%%%%%%%

        struct link_info
        {
            public int btn_id;
            public Button bttn;
            public string from;
            public string to;
            public Point image_xy;
            public float image_width;
            public float image_height;
        };

        LinkData pData;
        ScenarioData sData;
        List<EditProject.TotalData> pTotal_data;

        bool swipe = false;
        int swipe_type = 0;
        string image_name;
        string work_message;
        int btn_id_to_add = -1;
        int click_init = 0;
        int fileIndex = 0;
        
        link_info link_temp;
        List<string> str_work = new List<string>();             //화면 우측에 출력되는 링크정보

        List<Button> btnOnScreen = new List<Button>();

        Control[] btn_link;
        Button temp_delete_button, moveBtn;
        ContextMenu cm;

        //drawing
        int myShape;
        Point myPointStart;
        Point myPointEnd;
        Pen myPen;
        Pen myPenTemp;
        int sizeX;
        int sizeY;
        Boolean myPress;
        Graphics g;
        Rectangle rect;

        bool isDragged = false;
        Point ptOffset;
        //drawing

        Image mainImage, imageTemp;
        System.Drawing.Image img;
        float org_width = 0, org_height = 0, resize_ratio = 0;

        private string mPath;
        private string mfilePath;
        string user_id;
        PrivateFontCollection pfc = new PrivateFontCollection();
        FileInfo[] files;

        public Edit_Image() { InitializeComponent(); }

        public Edit_Image(string _mPath, string _filePath, LinkData _Data, ScenarioData _sData, string _user_id, List<EditProject.TotalData> _pTotal_data)
        {
            InitializeComponent();
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            pictureBox_main.Parent = panel_for_pic;
            mPath = _mPath;
            mfilePath = _filePath;
            pData = _Data;
            sData = _sData;
            user_id = _user_id;
            pTotal_data = _pTotal_data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Init for drawing
            this.myShape = 3;
            this.myPointStart = new Point(0, 0);
            this.myPointEnd = new Point(0, 0);
            this.myPen = new Pen(Color.Lime, 2);
            this.myPenTemp = new Pen(Color.Lime, 2);
            this.sizeX = 0;
            this.sizeY = 0;
            this.myPress = false;

            //clear
            this.link_temp = default(link_info);
            btn_id_to_add = 0;

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.MenuItemDeleteClick));

            //경로 받아오기 - 링크 정보를 추가 수정할 사진 한 장에 대한 경로
            string img_path = mfilePath; //@"C:\Users\lewis\Documents\Visual Studio 2015\Projects\workspace\workspace\pic\main.png";
            //경로 메인 이미지에 입력
            mainImage = Image.FromFile(img_path);
            image_name = Path.GetFileName(img_path);

            img = System.Drawing.Image.FromFile(mfilePath);
            org_width = img.Width;
            org_height = img.Height;

            btnOnScreen.Clear();

            label_detail_info_title.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_info.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
            panel_detail_info.Height = (int)(this.Height * 0.8);

            label_swipe.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_zoom.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

            //Edit_Image 처음 호출 했을 때 panel1의 사이즈 정하기
            if (img != null && (img.Height >= img.Width))
            {
                panel_for_pic.Height = (int)(this.Height * 0.9);
                panel_for_pic.Width = (int)(GetWidthOverHeight(pData.GetDeviceType()) * (this.Height * 0.9));      //여기에 디바이스정보 넣기
                panel_for_pic.Location = new Point(((this.Width / 2) - (panel_for_pic.Width / 2)), 0);
                resize_ratio = (float)panel_for_pic.Width / (float)img.Width;
            }
            else if(img != null && (img.Height < img.Width))
            {
                MessageBox.Show("Landscape는 추후 추가 예정");
            }

            //fpanel_editImage_link.Height = (int)(this.Height * 0.5);

            //메인이미지를 pictureBox1의 배경으로 설정
            listBox1.Height = listBox1.PreferredHeight;
            pictureBox_main.BackgroundImage = mainImage;
            groupBox1_Load();
            SetLinks();
        }

        private double GetWidthOverHeight(string deviceType)
        {
            double ratio = 0;
            switch (deviceType)
            {
                case "gPro":
                case "g2":
                case "g3":
                case "g4":
                case "galaxyS2HD":

                case "galaxyS3":
                case "galaxyS4":
                case "galaxyS5":
                case "galaxyS6":
                case "note2":

                case "note3":
                case "note4":
                case "note5":
                case "iphone5": 
                case "iphone6":

                case "iphone6s":
                case "iphone6p":
                case "iphone6sp": ratio = 0.5625; break;  // 9 : 16

                // 기타 비율은 탭이나 현재 거의 쓰지 않는 기종들. 나중에 비율 다른 기종이 나오면 여기에 추가
            }
            return ratio;
        }

        private void groupBox1_Load()
        {
            //디렉토리 주소를 받아와서 저장
            string path_folder;
            path_folder = mPath; //지용이가 넘겨준 주소값

            //프로젝트 내에 있는 파일이름 가져오기
            DirectoryInfo dinfo = new DirectoryInfo(mPath);

            string[] extensions = new[] { ".jpg", ".tiff", ".png" };

            files =
                dinfo.EnumerateFiles()
                     .Where(f => extensions.Contains(f.Extension.ToLower()))
                     .ToArray();

            btn_link = new Control[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                //링크연결을 위한 이미지
                btn_link[i] = new Button();
                btn_link[i].Parent = this;
                btn_link[i].Size = new Size((int)(GetWidthOverHeight(pData.GetDeviceType()) * (int)(fpanel_editImage_link.Height * 0.9)), (int)(fpanel_editImage_link.Height * 0.9));
                btn_link[i].Tag = i.ToString();

                btn_link[i].Click += new EventHandler(ButtonClickToLink);
                btn_link[i].Name = files[i].Name;
                btn_link[i].Text = i.ToString();
                btn_link[i].ForeColor = Color.Lime;
                btn_link[i].Font = new Font(btn_link[i].Font.Name, 10, FontStyle.Bold);
                btn_link[i].BackgroundImage = Image.FromFile(mPath + files[i].Name);
                btn_link[i].BackgroundImageLayout = ImageLayout.Stretch;
                fpanel_editImage_link.WrapContents = false;
                fpanel_editImage_link.AutoScroll = true;

                fpanel_editImage_link.Controls.Add(btn_link[i]);
            }
        }

        //이미지 내부에 구현되어 있는 버튼에 대한.
        public void ButtonClickInImage(object sender, MouseEventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            if (e.Button == MouseButtons.Right)
            {
                temp_delete_button = btn;
                temp_delete_button.TabIndex = btn.TabIndex;
            }
            else
            {
                //if (e.Button == MouseButtons.Left)
                //{
                //    isDragged = true;
                //    moveBtn = btn;
                //    Point ptStartPosition = moveBtn.PointToScreen(new Point(e.X, e.Y));

                //    ptOffset = new Point();
                //    ptOffset.X = btn.Location.X - ptStartPosition.X;
                //    ptOffset.Y = btn.Location.Y - ptStartPosition.Y;
                //}
                //else
                //{
                //    isDragged = false;
                //}

                panel_editImage_link.Visible = false;

                if (btn != null)
                {
                    for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                    {
                        if (btn.TabIndex == pData.GetLinks()[fileIndex].link_data[i].btn_id)
                        {
                            work_message =
                                     "Destination Image : " + pData.GetLinks()[fileIndex].link_data[i].dst_file
                                    + "\nXY coordinate : " + pData.GetLinks()[fileIndex].link_data[i].image_xy.X + ", " + pData.GetLinks()[fileIndex].link_data[i].image_xy.Y
                                    + "\nRectangle Width : " + pData.GetLinks()[fileIndex].link_data[i].image_width
                                    + "\nRectangle Height : " + pData.GetLinks()[fileIndex].link_data[i].image_height
                                    + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                    + "\n------------------------\n";
                            str_work.Add(work_message);
                            break;
                            //이후에 버튼을 눌러서 수정하는 작업 여기서 구현
                        }
                    }
                    this.label_info.Text = string.Empty;

                    for (int j = str_work.Count; j > 0; j--)
                    {
                        this.label_info.Text += str_work[j - 1];
                    }
                }
            }
        }

        private void MenuItemDeleteClick(object sender, EventArgs e)
        {
            //get control hovered with mouse
            Button button_to_remove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);
            //if it's a Button, remove it from the form
            DeleteLink(temp_delete_button.TabIndex);
            pictureBox_main.Controls.Remove(temp_delete_button);
            //$$$$$$
            this.Invalidate();
        }

        //마우스관련
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //this.isDragged = true;
            this.myPress = true; //마우스가 눌러짐
            this.link_alloc.Visible = false;
            this.myPointStart.X = e.X; //마우스가 눌러진 X 좌표
            this.myPointStart.Y = e.Y; //마우스가 눌러진 Y 좌표
            this.click_init = 1;
            this.panel_editImage_link.Visible = false;
            //pictureBox1.BringToFront();
            this.g = Graphics.FromHwnd(pictureBox_main.Handle);
            //MessageBox.Show(pictureBox1.Width.ToString() + "//" + pictureBox1.Height.ToString());
            //MessageBox.Show(panel1.Width.ToString() + "//" + panel1.Height.ToString());

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (isDragged)
            //{
            //    //Point newPoint = moveBtn.PointToScreen(new Point(e.X, e.Y));
            //    //newPoint.Offset(ptOffset);
            //    moveBtn.Location = new Point(e.X, e.Y);
            //    moveBtn.FlatAppearance.BorderColor = Color.Lime;
            //    //Console.WriteLine(e.X.ToString() + ", " + e.Y.ToString());
            //}

            if (myShape == 3)      //사각형
            {
                if (this.myPress)
                {
                    g.DrawImage(mainImage, 0, 0, this.panel_for_pic.Width, this.panel_for_pic.Height);

                    this.sizeX = Math.Abs(e.X - this.myPointStart.X);
                    this.sizeY = Math.Abs(e.Y - this.myPointStart.Y);
                    if (this.sizeX == 0){ this.sizeX = 1;}
                    if (this.sizeY == 0){ this.sizeY = 1;}

                    if (e.X < this.myPointStart.X && e.Y < this.myPointStart.Y)         //우측하단 시작
                    {
                        this.rect = new Rectangle(e.X, e.Y, this.sizeX, this.sizeY);
                    }
                    else if (e.X > this.myPointStart.X && e.Y < this.myPointStart.Y)    //좌측하단 시작
                    {
                        this.rect = new Rectangle(this.myPointStart.X, e.Y, this.sizeX, this.sizeY);
                    }
                    else if (e.X < this.myPointStart.X && e.Y > this.myPointStart.Y)    //우측상단 시작
                    {
                        this.rect = new Rectangle(e.X, this.myPointStart.Y, this.sizeX, this.sizeY);
                    }
                    else                                                                //좌측상단 시작
                    {
                        this.rect = new Rectangle(this.myPointStart.X, this.myPointStart.Y, this.sizeX, this.sizeY);
                    }
                    g.DrawRectangle(this.myPen, this.rect);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //isDragged = false;
            //MessageBox.Show(moveBtn.Location.X.ToString() + ", " + moveBtn.Location.Y.ToString());
            if (this.myShape == 3)
            {
                this.sizeX = Math.Abs(e.X - this.myPointStart.X);
                this.sizeY = Math.Abs(e.Y - this.myPointStart.Y);
                if (this.sizeX == 0)
                {
                    this.sizeX = 1;
                }
                if (this.sizeY == 0)
                {
                    this.sizeY = 1;
                }
                this.rect = new Rectangle(this.myPointStart.X, this.myPointStart.Y, this.sizeX, this.sizeY);

                link_temp.image_width = this.sizeX;
                link_temp.image_height = this.sizeY;

                //그냥 클릭한 것에 대해서는 반응하지 않도록
                if (Math.Abs(this.myPointStart.X - e.X) > 5 && Math.Abs(this.myPointStart.Y - e.Y) > 5 && click_init == 1)
                {
                    this.link_alloc.Visible = true;
                    this.link_alloc.Parent = pictureBox_main;
                    //this.link_alloc.BringToFront();
                    //사각형의 어느 꼭짓점에서 시작하는지에 따라 그려지는 과정이 다름
                    if (e.X < this.myPointStart.X && e.Y < this.myPointStart.Y)         //우측하단 시작
                    {
                        //this.link_alloc.Location = new Point(this.rect.X + (this.Width / 2) - (panel_for_pic.Width / 2), this.rect.Y);
                        this.link_alloc.Location = new Point(e.X + 1, e.Y + 1);
                        link_temp.image_xy = new Point(e.X, e.Y);
                    }
                    else if (e.X > this.myPointStart.X && e.Y < this.myPointStart.Y)    //좌측하단 시작
                    {
                        //this.link_alloc.Location = new Point(this.rect.X + this.sizeX + (this.Width / 2) - (panel_for_pic.Width / 2), this.rect.Y);
                        this.link_alloc.Location = new Point(e.X - this.rect.Width + 1, e.Y + 1);
                        link_temp.image_xy = new Point(this.rect.X, e.Y);
                    }
                    else if (e.X < this.myPointStart.X && e.Y > this.myPointStart.Y)    //우측상단 시작
                    {
                        //this.link_alloc.Location = new Point(this.rect.X + (this.Width / 2) - (panel_for_pic.Width / 2), this.rect.Y + this.sizeY);
                        this.link_alloc.Location = new Point(e.X + 1, e.Y - this.rect.Height + 1);
                        link_temp.image_xy = new Point(e.X, this.rect.Y);
                    }
                    else                                                                //좌측상단 시작
                    {
                        //this.link_alloc.Location = new Point(this.rect.X + this.sizeX + (this.Width / 2) - (panel_for_pic.Width / 2), this.rect.Y + this.sizeY);
                        this.link_alloc.Location = new Point(e.X - this.rect.Width + 1, e.Y - this.rect.Height + 1);
                        link_temp.image_xy = new Point(this.rect.X, this.rect.Y);
                    }
                }
                else
                {
                    link_alloc.Visible = false;
                    this.Invalidate();
                }
            }
            click_init = 0;
            this.myPress = false;
        }
        //마우스관련 끝

        //링크연동버튼
        private void button5_Click(object sender, EventArgs e)
        {
            this.panel_editImage_link.Visible = true;
            this.panel_editImage_link.BringToFront();
            this.link_alloc.Visible = false;
            g.DrawImage(mainImage, 0, 0, this.panel_for_pic.Width, this.panel_for_pic.Height);
            g.DrawRectangle(this.myPen, this.rect);
            this.Invalidate();
            fpanel_editImage_link.Visible = true;
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //콤보박스에서 선택한 것이 리스트 박스 안에 없다면 추가한다.
        //    if(!listBox1.Items.Contains(cb_swipe.SelectedItem))
        //    {
        //        fpanel_editImage_link.Visible = true;
        //        fpanel_editImage_link.BringToFront();
        //        swipe = true;
        //        //MessageBox.Show("swipe3");
        //        //밑에 링크박스 열리면서 선택할 경우 추가하도록
        //        //listBox1.Items.Add(comboBox1.SelectedItem);
        //        //ADD하면서 pData에도 추가
        //        //pData.AddLink(index, btn_id_to_add, pDstFile, new Point(-1, -1), -1, -1, comboBox1.SelectedItem);
        //    }
        //}

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.panel_editImage_link.Size = new Size((int)(this.Width * 0.7), (int)(this.Height * 0.5));
            this.panel_editImage_link.Location = new Point(0, this.Height - panel_editImage_link.Height - 50);
            fpanel_editImage_link.Height = panel_editImage_link.Height - 40;
            btn_close_linkPanel.Location = new Point(panel_editImage_link.Width - btn_close_linkPanel.Width - 5, 3);

            for (int i = 0; i < files.Length; i++)
            {
                btn_link[i].Size = new Size((int)(GetWidthOverHeight(pData.GetDeviceType()) * (int)(fpanel_editImage_link.Height * 0.9)), (int)(fpanel_editImage_link.Height * 0.9));
            }

            pictureBox_main.Location = new Point((int)(this.Width / 2) - panel_editImage_left2.Width, 20);

            //세로가 더 긴 경우
            if (img != null && (img.Height > img.Width))
            {
                //원본 이미지 파일의 가로 세로 비율을 통해 새로운 resolution에서 같은 비율 적용
                panel_for_pic.Height = (int)(this.Height * 0.9);                           //화면 아래로 잘려나가는 것을 방지하기 위함
                double WidthOverHeight = (double)img.Width / (double)img.Height;    //이미지의 가로세로 비율
                panel_for_pic.Width = (int)(WidthOverHeight * (this.Height * 0.9));
                panel_for_pic.Location = new Point((panel_editImage_total.Width / 2) - panel_editImage_left2.Width, 20);

                //resize시 원본대비 변경된 사이즈의 비율
                resize_ratio =  (float)panel_for_pic.Width / (float)img.Width;

                try
                {
                    for (int i = 0, j = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                    {
                   
                        //해당 링크 정보가 터치일 경우에 버튼의 위치와 크기 resize.
                        if (pData.GetLinks()[fileIndex].link_data[i].input_type.Contains("Touch"))
                        {
                            btnOnScreen[j].Location = new Point((int)(pData.GetLinks()[fileIndex].link_data[i].image_xy.X * resize_ratio), (int)(pData.GetLinks()[fileIndex].link_data[i].image_xy.Y * resize_ratio));
                            btnOnScreen[j].Width = (int)(pData.GetLinks()[fileIndex].link_data[i].image_width * resize_ratio);
                            btnOnScreen[j].Height = (int)(pData.GetLinks()[fileIndex].link_data[i].image_height * resize_ratio);
                            btnOnScreen[j].Visible = true;
                            j++;
                        }
                  
                    }
                }catch (ArgumentOutOfRangeException ae)
                {
                }
            }
            link_alloc.Visible = false;
            fpanel_editImage_link.Visible = false;
        }

        //delete
        private void DeleteLink(int pDeleteIndex)
        {
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
            {
                if (pData.GetLinks()[fileIndex].link_data[i].btn_id == pDeleteIndex)
                {
                    
                    //삭제 메시지
                    work_message =
                                    "링크가 삭제되었습니다."
                                    + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                    + "\n------------------------\n";
                    str_work.Add(work_message);

                    this.label_info.Text = string.Empty;

                    for (int j = str_work.Count; j > 0; j--)
                    {
                        this.label_info.Text += str_work[j - 1];
                    }
                    pData.GetLinks()[fileIndex].link_data.RemoveAt(i);
                    break;
                }
            }
        }

        //리스트박스 내 아이템 클릭
        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Control ctl = sender as Control;

            //우클릭시 context menu 호출하여 delete 할 수 있도록
            if (e.Button == MouseButtons.Right)
            {

            }
            //좌클릭
            else if(e.Button == MouseButtons.Left && listBox1.SelectedItem != null)
            {
                for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                {
                    if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo(listBox1.SelectedItem.ToString()) == 0)
                    {
                        work_message =
                                    "Destination Image : " + pData.GetLinks()[fileIndex].link_data[i].dst_file
                                    + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                    + "\n------------------------\n";
                        str_work.Add(work_message);
                        break;
                        //이후에 버튼을 눌러서 수정하는 작업 여기서 구현
                    }
                }

                this.label_info.Text = string.Empty;

                for (int j = str_work.Count; j > 0; j--)
                {
                    this.label_info.Text += str_work[j - 1];
                }
            }

        }

        //삭제
        private void del_btn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
            {
                if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo(listBox1.SelectedItem.ToString()) == 0)
                {
                    listBox1.Items.Remove(listBox1.SelectedItem);
                    listBox1.Height = listBox1.PreferredHeight;

                    work_message =
                                    "링크가 삭제되었습니다."
                                    + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                    + "\n------------------------\n";
                    str_work.Add(work_message);

                    this.label_info.Text = string.Empty;

                    for (int j = str_work.Count; j > 0; j--)
                    {
                        this.label_info.Text += str_work[j - 1];
                    }

                    pData.GetLinks()[fileIndex].link_data.RemoveAt(i);

                    break;
                }
            }
        }

        private void Edit_Image_FormClosed(object sender, FormClosedEventArgs e)
        {
            EditProject editProject = new EditProject(mPath, pData, sData, user_id, pTotal_data);
            editProject.Show();
        }
        
        private void BACK_btn_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void btn_close_linkPanel_Click(object sender, EventArgs e)
        {
            panel_editImage_link.Visible = false;
            g.DrawImage(mainImage, 0, 0, this.panel_for_pic.Width, this.panel_for_pic.Height);
        }

        private void btn_swipe_left_Click(object sender, EventArgs e)
        {
            if(btn_swipe_left.BackColor == Color.Silver)
            {
                for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                {
                    if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo("Swipe_Left") == 0)
                    {
                        work_message =
                                        "링크가 삭제되었습니다."
                                        + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                        + "\n------------------------\n";
                        str_work.Add(work_message);

                        this.label_info.Text = string.Empty;

                        for (int j = str_work.Count; j > 0; j--)
                        {
                            this.label_info.Text += str_work[j - 1];
                        }
                        pData.GetLinks()[fileIndex].link_data.RemoveAt(i);
                        break;
                    }
                }
                btn_swipe_left.BackColor = Color.White;
            }
            else
            {
                swipe = true;
                swipe_type = 1;
                this.panel_editImage_link.Visible = true;
                this.panel_editImage_link.BringToFront();
                fpanel_editImage_link.Visible = true;
            }
        }

        private void btn_swipe_right_Click(object sender, EventArgs e)
        {
            if (btn_swipe_right.BackColor == Color.Silver)
            {
                for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                {
                    if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo("Swipe_Right") == 0)
                    {
                        work_message =
                                        "링크가 삭제되었습니다."
                                        + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                        + "\n------------------------\n";
                        str_work.Add(work_message);

                        this.label_info.Text = string.Empty;

                        for (int j = str_work.Count; j > 0; j--)
                        {
                            this.label_info.Text += str_work[j - 1];
                        }
                        pData.GetLinks()[fileIndex].link_data.RemoveAt(i);
                        break;
                    }
                }
                btn_swipe_right.BackColor = Color.White;
            }
            else
            {
                swipe = true;
                swipe_type = 2;
                this.panel_editImage_link.Visible = true;
                this.panel_editImage_link.BringToFront();
                fpanel_editImage_link.Visible = true;
            }
        }

        private void btn_swipe_up_Click(object sender, EventArgs e)
        {
            if (btn_swipe_up.BackColor == Color.Silver)
            {
                for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                {
                    if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo("Swipe_Up") == 0)
                    {
                        work_message =
                                        "링크가 삭제되었습니다."
                                        + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                        + "\n------------------------\n";
                        str_work.Add(work_message);

                        this.label_info.Text = string.Empty;

                        for (int j = str_work.Count; j > 0; j--)
                        {
                            this.label_info.Text += str_work[j - 1];
                        }
                        pData.GetLinks()[fileIndex].link_data.RemoveAt(i);
                        break;
                    }
                }
                btn_swipe_up.BackColor = Color.White;
            }
            else
            {
                swipe = true;
                swipe_type = 3;
                this.panel_editImage_link.Visible = true;
                this.panel_editImage_link.BringToFront();
                fpanel_editImage_link.Visible = true;
            }
        }

        private void btn_swipe_down_Click(object sender, EventArgs e)
        {
            if (btn_swipe_down.BackColor == Color.Silver)
            {
                for (int i = 0; i < pData.GetLinks()[fileIndex].link_data.Count; i++)
                {
                    if (pData.GetLinks()[fileIndex].link_data[i].input_type.CompareTo("Swipe_Down") == 0)
                    {
                        work_message =
                                        "링크가 삭제되었습니다."
                                        + "\nLink ID : " + pData.GetLinks()[fileIndex].link_data[i].btn_id
                                        + "\n------------------------\n";
                        str_work.Add(work_message);

                        this.label_info.Text = string.Empty;

                        for (int j = str_work.Count; j > 0; j--)
                        {
                            this.label_info.Text += str_work[j - 1];
                        }
                        pData.GetLinks()[fileIndex].link_data.RemoveAt(i);
                        break;
                    }
                }
                btn_swipe_down.BackColor = Color.White;
            }
            else
            {
                swipe = true;
                swipe_type = 4;
                this.panel_editImage_link.Visible = true;
                this.panel_editImage_link.BringToFront();
                fpanel_editImage_link.Visible = true;
            }
        }

        //Xml 저장
        private void SaveBtnClick(object sender, EventArgs e)
        {
            DirectoryInfo difo = new DirectoryInfo(mPath);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(mPath + "\\" + "link.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("LinkTable");
                    xmlWriter.WriteStartElement("UserId");
                    xmlWriter.WriteString(pData.GetUserId());               //user id
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("DeviceInfo");
                    xmlWriter.WriteString(pData.GetDeviceType());               //device info
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("DeviceResolution");
                    xmlWriter.WriteString(pData.GetDeviceResolution());                      //ex) iPhone5 (640 x 1280)
                    xmlWriter.WriteEndElement();

                CreateNodeTemp(xmlWriter);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
                MessageBox.Show("create");


                //기존의 ZIP파일 지우고 다시 생성하기
                if (File.Exists(mPath + difo.Name + ".zip"))
                {
                    File.Delete(mPath + difo.Name + ".zip");
                }

                string[] filenames = Directory.GetFiles(mPath, "*.*");

                bool zipped = false;
                using(ZipFile zip = new ZipFile(mPath))
                {
                    zip.AddFiles(filenames, false, "");
                    zip.Save(string.Format("{0}{1}.zip", mPath, difo.Name));
                    zipped = true;
                }
            }
        }

        private void CreateNodeTemp(XmlWriter writer)
        {
            //editProject에서 받아온 정보(create_links)에서 전체 링크 정보를 저장, 파일명으로 링크 구분
            int i = 0, j = 0;
            for (i = 0; i < pData.GetLinks().Count; i++)
            {
                //해당 인덱스에 링크정보가 하나도 없으면 저장하지 않는다.
                if (pData.GetLinks()[i].link_data.Count != 0)
                {
                    writer.WriteStartElement("Link");
                    writer.WriteStartAttribute("fileName");
                    writer.WriteString(pData.GetLinks()[i].file_name);             //image name
                    for (j = 0; j < pData.GetLinks()[i].link_data.Count; j++)
                    {
                        writer.WriteStartElement("LinkInfo");
                        writer.WriteStartElement("Tag");
                        writer.WriteString(pData.GetLinks()[i].link_data[j].btn_id.ToString());
                        writer.WriteEndElement();
                        writer.WriteStartElement("InputType");
                        writer.WriteString(pData.GetLinks()[i].link_data[j].input_type);
                        writer.WriteEndElement();
                        writer.WriteStartElement("StartFile");
                        writer.WriteString(pData.GetLinks()[i].file_name);
                        writer.WriteEndElement();

                        //case : touch (swipe는 아래 부분을 쓰지 않음)
                        if (pData.GetLinks()[i].link_data[j].input_type.Contains("Touch"))
                        {
                            writer.WriteStartElement("LinkX");
                            writer.WriteString(pData.GetLinks()[i].link_data[j].image_xy.X.ToString());
                            writer.WriteEndElement();
                            writer.WriteStartElement("LinkY");
                            writer.WriteString(pData.GetLinks()[i].link_data[j].image_xy.Y.ToString());
                            writer.WriteEndElement();
                            writer.WriteStartElement("LinkWidth");
                            writer.WriteString(pData.GetLinks()[i].link_data[j].image_width.ToString());
                            writer.WriteEndElement();
                            writer.WriteStartElement("LinkHeight");
                            writer.WriteString(pData.GetLinks()[i].link_data[j].image_height.ToString());
                            writer.WriteEndElement();
                        }
                        writer.WriteStartElement("DstFile");
                        writer.WriteString(pData.GetLinks()[i].link_data[j].dst_file);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                
            }
        }
        //Xml 저장 끝

        private void SetLinks()
        {
            int i = 0, j = 0, k = 0;
            string pTag = "", pDstFile = "", pInputType = "";
            string pLinkX = "", pLinkY = "", pLinkWidth = "", pLinkHeight = "";
            btn_id_to_add = 0;

            fileIndex = pData.GetLinks().Count;  //링크정보가 없는 fileIndex를 맨 끝 번호로 지정
            //선택된 파일의 인덱스 찾기

            for (i = 0; i < pData.GetLinks().Count; i++)
            {
                //MessageBox.Show(image_name + "//" + pData.GetLinks()[i].file_name);
                if (image_name.CompareTo(pData.GetLinks()[i].file_name) == 0)
                {
                    fileIndex = i;
                    break;
                }
            }

            //링크가 비어있지 않은 경우
            try {
                //해당 인덱스의 링크정보만 띄워주기
                for (j = 0; j < pData.GetLinks()[fileIndex].link_data.Count; j++)
                {
                    pInputType = pData.GetLinks()[fileIndex].link_data[j].input_type;
                    pTag = pData.GetLinks()[fileIndex].link_data[j].btn_id.ToString();
                    pDstFile = pData.GetLinks()[fileIndex].link_data[j].dst_file;

                    //case : button
                    if (pInputType.CompareTo("Single_Touch") == 0 || pInputType.CompareTo("Long_Touch") == 0)
                    {
                        pLinkX = pData.GetLinks()[fileIndex].link_data[j].image_xy.X.ToString();
                        pLinkY = pData.GetLinks()[fileIndex].link_data[j].image_xy.Y.ToString();
                        pLinkWidth = pData.GetLinks()[fileIndex].link_data[j].image_width.ToString();
                        pLinkHeight = pData.GetLinks()[fileIndex].link_data[j].image_height.ToString();

                        btnOnScreen.Add(new Button());
                        btnOnScreen[k].TabIndex = Convert.ToInt32(pTag);
                        btnOnScreen[k].Parent = this.pictureBox_main;
                        btnOnScreen[k].MouseUp += new MouseEventHandler(ButtonClickInImage);
                        btnOnScreen[k].Visible = true;
                        btnOnScreen[k].FlatStyle = FlatStyle.Flat;
                        btnOnScreen[k].BackColor = Color.Transparent;
                        btnOnScreen[k].FlatAppearance.BorderColor = Color.Lime;
                        btnOnScreen[k].FlatAppearance.MouseDownBackColor = Color.Transparent;
                        btnOnScreen[k].FlatAppearance.MouseOverBackColor = Color.Transparent;
                        btnOnScreen[k].Location = new Point(((int)((Convert.ToInt32(pLinkX)) * resize_ratio)), ((int)((Convert.ToInt32(pLinkY)) * resize_ratio)));
                        btnOnScreen[k].Width = (int)((Convert.ToInt32(pLinkWidth)) * resize_ratio);
                        btnOnScreen[k].Height = (int)((Convert.ToInt32(pLinkHeight)) * resize_ratio);
                        btnOnScreen[k].BringToFront();
                        btnOnScreen[k].ContextMenu = cm;
                        k++;
                    }
                    //case : swipe --> id, type, dstFile만 있으면 되기 때문에 나머지는 -1로 초기화
                    if (pInputType.Contains("Swipe"))
                    {
                        pLinkX = "-1";
                        pLinkY = "-1";
                        pLinkWidth = "-1";
                        pLinkHeight = "-1";

                        if (pInputType.CompareTo("Swipe_Left") == 0)
                        {
                            btn_swipe_left.BackColor = Color.Silver;
                        }
                        else if (pInputType.CompareTo("Swipe_Right") == 0)
                        {
                            btn_swipe_right.BackColor = Color.Silver;
                        }
                        else if (pInputType.CompareTo("Swipe_Up") == 0)
                        {
                            btn_swipe_up.BackColor = Color.Silver;
                        }
                        else if (pInputType.CompareTo("Swipe_Down") == 0)
                        {
                            btn_swipe_down.BackColor = Color.Silver;
                        }

                    }
                    //링크정보를 추가할 경우 부여되는 ID
                    if (btn_id_to_add <= Convert.ToInt32(pTag))
                    {
                        btn_id_to_add = Convert.ToInt32(pTag);
                        btn_id_to_add += 1;
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                //MessageBox.Show("empty");
            }
        }

        //네모박스 그린 이 후 링크연동 시 이미지 파일을 클릭할 때 동작하는 버튼
        public void ButtonClickToLink(object sender, EventArgs e)
        {
            Control ctl = sender as Control;
            Button btn = sender as Button;

            link_alloc.Visible = false;
            panel_editImage_link.Visible = false;

            try
            {
                g.DrawImage(mainImage, 0, 0, this.panel_for_pic.Width, this.panel_for_pic.Height);
            }catch(NullReferenceException ne)
            {

            }
            this.Invalidate();

            if (ctl != null)
            {
                //추가할 인덱스 찾기
                int addIndex = 0;
                //해당 이미지 이름이 없으면 새로운 index를 부여해서 전체 어레이에 추가하도록 합니다.
                for (int i = 0; i < pData.GetLinks().Count; i++)
                {
                    if (pData.GetLinks()[i].file_name.CompareTo(image_name) == 0)
                    {
                        addIndex = i;
                        break;
                    }
                    else
                    {
                        addIndex = i + 1;
                    }
                }

                int btn_index = 0;
                //추가하기
                if (swipe == true)
                {
                    //이미 클릭되어 있던 버튼인 경우
                    //이 경우는 버튼 클릭단계에서 취소 하는 것으로.

                    //새로 추가하는 경우

                    string swipe_type_input = string.Empty;
                    if(swipe_type == 1)
                    {
                        swipe_type_input = "Swipe_Left";
                        btn_swipe_left.BackColor = Color.Silver;
                    }
                    else if(swipe_type == 2)
                    {
                        swipe_type_input = "Swipe_Right";
                        btn_swipe_right.BackColor = Color.Silver;
                    }
                    else if (swipe_type == 3)
                    {
                        swipe_type_input = "Swipe_Up";
                        btn_swipe_up.BackColor = Color.Silver;
                    }
                    else if(swipe_type == 4)
                    {
                        swipe_type_input = "Swipe_Down";
                        btn_swipe_down.BackColor = Color.Silver;
                    }

                    btn_index = pData.AddLink(image_name, addIndex, btn_id_to_add, btn.Name, new Point(-1, -1), -1, -1, swipe_type_input);
                    //listBox1.Items.Add(cb_swipe.SelectedItem);
                    //listBox1.Height = listBox1.PreferredHeight;
                    work_message =
                               "저장되었습니다."
                           + "\nDestination Index : " + pData.GetLinks()[addIndex].link_data[btn_index].dst_file
                           + "\nLink ID : " + pData.GetLinks()[addIndex].link_data[btn_index].btn_id
                           + "\n------------------------\n"; 
                    swipe = false;
                    swipe_type = 0;
                }
                //case : button
                else
                {
                    btn_index = pData.AddLink(image_name, addIndex, btn_id_to_add, btn.Name, new Point((int)(link_temp.image_xy.X / resize_ratio), (int)(link_temp.image_xy.Y / resize_ratio)),
                                        (int)(link_temp.image_width / resize_ratio), (int)(link_temp.image_height / resize_ratio), "Single_Touch"); //single touch 부분 수정
                                                                                                                                                    //보여주기
                    Button temp_btn = new Button();
                    temp_btn.Parent = pictureBox_main;
                    temp_btn.TabIndex = btn_id_to_add;
                    temp_btn.MouseUp += new MouseEventHandler(ButtonClickInImage);
                    temp_btn.Location = link_temp.image_xy;
                    temp_btn.Size = new Size((int)link_temp.image_width, (int)link_temp.image_height);
                    this.pictureBox_main.Controls.Add(temp_btn);
                    temp_btn.FlatStyle = FlatStyle.Flat;
                    temp_btn.BackColor = Color.Transparent;
                    temp_btn.FlatAppearance.BorderColor = Color.Lime;
                    temp_btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    temp_btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    temp_btn.ContextMenu = cm;
                    btnOnScreen.Add(temp_btn);

                    work_message =
                                "저장되었습니다."
                            + "\nDestination Index : " + pData.GetLinks()[addIndex].link_data[btn_index].dst_file
                            + "\nXY coordination : " + pData.GetLinks()[addIndex].link_data[btn_index].image_xy.X + ", " + pData.GetLinks()[addIndex].link_data[btn_index].image_xy.Y
                            + "\nRectangle Width : " + pData.GetLinks()[addIndex].link_data[btn_index].image_width
                            + "\nRectangle Height : " + pData.GetLinks()[addIndex].link_data[btn_index].image_height
                            + "\nLink ID : " + pData.GetLinks()[addIndex].link_data[btn_index].btn_id
                            + "\n------------------------\n";
                }
                btn_id_to_add++;
                str_work.Add(work_message);

                //이후에 버튼을 눌러서 수정하는 작업 여기서 구현
                this.label_info.Text = "";

                //우측에 출력되는 메시지
                for (int j = str_work.Count; j > 0; j--)
                {
                    this.label_info.Text += str_work[j - 1];
                }
                fpanel_editImage_link.Visible = false;

                //MessageBox.Show(pData.GetLinks()[fileIndex).link_data.Count.ToString());
            }
            
        }
    }
}
