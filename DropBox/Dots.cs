using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;


namespace DropBox
{
    public partial class Dots : Form
    {
        PictureBox pic;
        Graphics gr;
        Brush br;
        Bitmap bitmap;
        LinkData pData = new LinkData();
        List<EditProject.TotalData> pTotal_data;
        List<string> image_names;

        AnalysisData aData = new AnalysisData();
        ScenarioData sData = new ScenarioData();

        string project_name;

        PrivateFontCollection pfc = new PrivateFontCollection();

        public Dots(List<EditProject.TotalData> _pTotal_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pTotal_data = _pTotal_data;
            sData = _sData;
            project_name = _project_name;

            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));

            string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name;
            IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

            image_names = imagenames.Cast<string>().ToList();

            for(int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_dots_selectScenario.Items.Add(sData.getSData()[i].title);

            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_dots_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_dots_selectScenario.SelectedIndex = 0;


            for (int i = 0; i < image_names.Count; i++)                       //이거 시나리오 콤보박스 인덱스 change에서 사용
            {
                cb_analysis_dots_selectImage.Items.Add(Path.GetFileName(image_names[i]));
            }
            cb_analysis_dots_selectImage.Sorted = true;

            if (image_names.Count == 0)
            {
                cb_analysis_dots_selectImage.Items.Add("데이터없음");
            } 
            cb_analysis_dots_selectImage.SelectedIndex = 0;

         


            btn_analysis_show_dot.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);

            label_1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_3.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            cb_analysis_dots_selectScenario.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_dots_selectTest.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_dots_selectImage.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            label_detail_info_title.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_click.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_time.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_shortest.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_longest.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_visit.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_testdate.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

            cb_analysis_dots_selectScenario.Location = new Point(80, label_1.Location.Y + label_1.Height + 10);

            label_2.Location = new Point(80, cb_analysis_dots_selectScenario.Location.Y + cb_analysis_dots_selectScenario.Height + 20);
            cb_analysis_dots_selectTest.Location = new Point(80, label_2.Location.Y + label_2.Height + 10);

            label_3.Location = new Point(80, cb_analysis_dots_selectTest.Location.Y + cb_analysis_dots_selectTest.Height + 20);
            cb_analysis_dots_selectImage.Location = new Point(80, label_3.Location.Y + label_3.Height + 10);



            label_click.Text = "클릭 수 : ";
            label_time.Text = "체류 시간 : ";
            label_visit.Text = "방문 횟수 : ";
            label_shortest.Text = "최단 체류 시간 : ";
            label_longest.Text = "최장 체류 시간 : ";
            label_testdate.Text = "테스트 일자 : ";

        }

        public void DrawDots(string selected, string test, string scenario)
        {
            panel_analysis_picture.Height = (int)(this.Height * 0.85);

            //backgroundimage width height를 해상도 받아온 것으로 해야함 - 뒤에 숫자.
            panel_analysis_picture.Width = (int)(panel_analysis_picture.Height * ((double)720 / (double)1280));
            bitmap = new Bitmap(panel_analysis_picture.Width, panel_analysis_picture.Height);
            panel_analysis_picture.Location = new Point((int)(panel_analysis_dot_main.Width / 2) - panel_analysis_dots_left.Width, 20);


            //기존에 설정되어 있던 컨트롤 다 지우기
            panel_analysis_picture_dot.Controls.Remove(pic);
            //panel_analysis_picture_dot.BackColor = Color.White;
            pic = new PictureBox();
            pic.Parent = panel_analysis_picture_dot;
            panel_analysis_picture_dot.Controls.Add(pic);
            pic.Location = new Point(0, 0);

            //Image img = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            //MessageBox.Show(panel_analysis_picture_dot.Width.ToString() + "//" + img.Width.ToString());
            //float ratio = (float)panel_analysis_picture_dot.Width / (float)img.Width;

            //panel_analysis_picture_dot.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            //panel_analysis_picture_dot.BackgroundImageLayout = ImageLayout.Stretch;

            //Image img_temp = AlterTransparency(Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected), 80);

            pic.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + selected);
            pic.BackgroundImageLayout = ImageLayout.Stretch;
            pic.Size = panel_analysis_picture.Size;
            //pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_mousemove);
            //pic.BackColor = Color.White;
            //pic.BackgroundImage = img_temp;
            //pic.BackgroundImageLayout = ImageLayout.Stretch;
            //gr = panel_analysis_picture_dot.CreateGraphics();
            gr = pic.CreateGraphics();
            //gr = Graphics.FromHwnd(panel_analysis_picture_dot.Handle);
            gr = Graphics.FromImage(pic.BackgroundImage);

            br = new SolidBrush(SetColor(30, Color.Red));

            int index = 0;

            try
            {
                for (int i = 0; i < pTotal_data.Count; i++)
                {
                    //MessageBox.Show(pTotal_data[i].image_name + "//" + selected + "//"+ pTotal_data.Count.ToString());
                    if (pTotal_data[i].image_name.CompareTo(selected) == 0)
                    {
                        index = i;
                        break;
                    }
                }

                if (test.CompareTo("모두보기") == 0)
                {
                    try
                    {
                        for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
                        {
                            //시나리오, 이미지 이름만 맞으면 다 보여주기
                            if (pTotal_data[index].scenario_name.CompareTo(scenario) == 0 && pTotal_data[index].image_name.CompareTo(selected) == 0)
                            {
                                gr.FillPie(br, new Rectangle(new Point(((int)(pTotal_data[index].event_data[j].xcoord) - 25), (int)(pTotal_data[index].event_data[j].ycoord) - 25), new Size(50, 50)), 0, 360);
                            }
                        }
                    }
                    catch (ArgumentOutOfRangeException ae)
                    {

                    }
                }

                else
                {
                    for (int j = 0; j < pTotal_data[index].event_data.Count; j++)
                    {
                        if (pTotal_data[index].scenario_name.CompareTo(scenario) == 0 && pTotal_data[index].image_name.CompareTo(selected) == 0 && pTotal_data[index].event_data[j].test_num == Convert.ToInt16(test))
                        {
                            gr.FillPie(br, new Rectangle(new Point(((int)(pTotal_data[index].event_data[j].xcoord) - 25), (int)(pTotal_data[index].event_data[j].ycoord) - 25), new Size(50, 50)), 0, 360);
                        }
                    }
                }
            }
            catch (NullReferenceException ne)
            {
                MessageBox.Show("null");
            }
        }
        
        public static Bitmap AlterTransparency(Image image, Byte Alpha)
        {
            Bitmap Original = new Bitmap(image);
            Bitmap TransparentImage = new Bitmap(image.Width, image.Height);

            Color c = Color.Black;
            Color v = Color.Black;

            int av = 0;

            for(int i = 0; i < image.Width; i++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    c = Original.GetPixel(i, y);
                    v = Color.FromArgb(Alpha, c.R, c.G, c.B);
                    TransparentImage.SetPixel(i, y, v);
                }
            }

            return TransparentImage;
        }

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }

        private void cb_analysis_dots_selectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //테스트 콤보박스 리셋
            cb_analysis_dots_selectTest.Items.Clear();
            cb_analysis_dots_selectTest.Items.Add("모두보기");

            //테스트 콤보박스에 해당하는 시나리오의 테스트만 추가
            for (int i = 0; i < pTotal_data.Count; i++)
            {
                if (pTotal_data[i].scenario_name.CompareTo(cb_analysis_dots_selectScenario.SelectedItem.ToString()) == 0)
                {
                    for(int j = 0; j < pTotal_data[i].event_data.Count; j++)
                    {
                        if (!cb_analysis_dots_selectTest.Items.Contains(pTotal_data[i].event_data[j].test_num))
                        {
                            cb_analysis_dots_selectTest.Items.Add(pTotal_data[i].event_data[j].test_num);
                        }
                    }
                }
            }
            cb_analysis_dots_selectTest.Sorted = true;
            cb_analysis_dots_selectTest.SelectedIndex = 0;
        }

        private void Dots_SizeChanged(object sender, EventArgs e)
        {
            btn_analysis_show_dot.Location = new Point(80, (int)(this.Height * 0.7));
        }

        private void btn_analysis_show_dot_Click_1(object sender, EventArgs e)
        {
            string s = cb_analysis_dots_selectScenario.SelectedItem.ToString();
            string t = cb_analysis_dots_selectTest.SelectedItem.ToString();
            string i = cb_analysis_dots_selectImage.SelectedItem.ToString();

            DrawDots(i, t, s);
        }

        public void getDetailInfo()
        {
            int click = 0;
            int visit = 0;
        }
    }
}
