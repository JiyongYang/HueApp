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
using System.Drawing.Text;

namespace DropBox
{
    public partial class Route : Form
    {
        List<EditProject.RouteData> pRoute_data;
        ScenarioData sData = new ScenarioData();
        string project_name;
        PrivateFontCollection pfc = new PrivateFontCollection();

        public Route(List<EditProject.RouteData> _pRoute_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pRoute_data = _pRoute_data;
            project_name = _project_name;
            sData = _sData;
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_route_selectScenario.Items.Add(sData.getSData()[i].title);
            }

            if (sData.getSData().Count == 0)
            {
                cb_analysis_route_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_route_selectScenario.SelectedIndex = 0;

            //font
            btn_analysis_show_route.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);

            label_1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_initial.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_user.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_user.ForeColor = Color.FromArgb(162, 29, 33);

            cb_analysis_route_selectScenario.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_route_selectTest.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            

            //location
            cb_analysis_route_selectScenario.Location = new Point(80, label_1.Location.Y + label_1.Height + 10);

            label_2.Location = new Point(80, cb_analysis_route_selectScenario.Location.Y + cb_analysis_route_selectScenario.Height + 20);
            cb_analysis_route_selectTest.Location = new Point(80, label_2.Location.Y + label_2.Height + 10);

        }

        public void DrawRoute(int index)
        {
            fpanel_initial.Controls.Clear();
            //initial route 채우기
            for (int i = 0; i < sData.getSData().Count; i++)
            {
                if (sData.getSData()[i].title.CompareTo(cb_analysis_route_selectScenario.SelectedItem.ToString()) == 0)
                {
                    for(int j = 0; j < sData.getSData()[i].paths.Count; j++)
                    {
                        
                        FlowLayoutPanel temp_fp_init = new FlowLayoutPanel();
                        temp_fp_init.FlowDirection = FlowDirection.TopDown;
                        temp_fp_init.Height = (int)(fpanel_initial.Height * 0.8);

                        PictureBox temp_pic_init = new PictureBox();
                        int temp_fp_init_height = (int)(temp_fp_init.Height * 0.85);
                        temp_pic_init.Size = new Size((int)(temp_fp_init_height*0.5625), temp_fp_init_height);

                        temp_pic_init.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + sData.getSData()[i].paths[j].path);
                        temp_pic_init.BackgroundImageLayout = ImageLayout.Stretch;

                        Label temp_label_init = new Label();
                        temp_label_init.Text = sData.getSData()[i].paths[j].path;
                        temp_label_init.Width = temp_pic_init.Width;
                        temp_label_init.TextAlign = ContentAlignment.TopCenter;
                        temp_label_init.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
                        temp_label_init.AutoSize = false;
                        temp_label_init.AutoEllipsis = true;


                        //temp_label_init.Size = temp_label_init.PreferredSize;

                        temp_fp_init.Controls.Add(temp_pic_init);
                        temp_fp_init.Controls.Add(temp_label_init);
                        temp_fp_init.Size = temp_fp_init.PreferredSize;

                        fpanel_initial.Controls.Add(temp_fp_init);

                        if (j == 0)
                        {
                            temp_fp_init.Margin = new Padding(30, 3, 3, 3);
                        }

                        if (j != sData.getSData()[i].paths.Count - 1)
                        {
                            PictureBox arrow = new PictureBox();
                            arrow.Parent = fpanel_initial;
                            fpanel_initial.Controls.Add(arrow);
                            arrow.Size = new Size(18, 18);
                            arrow.Anchor = AnchorStyles.None;

                            //arrow.BackgroundImage = new Bitmap(Properties.Resources.arrow);
                            arrow.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._9_arrow));
                            arrow.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            }



            //해당 패널의 모든 컨트롤을 클리어하고
            fpanel_user.Controls.Clear();

            //라우트 데이터의 특정 인덱스에 있는 이미지 파일들과 정보를 불러와서 저장한다.
            //새로운 flow panel을 만들고 처음 만들 시 오른쪽 방향, 그 다음은 왼쪽 방향으로 가는 패널을 만든다.
            //다음으로 넘어가는 조건은, 현재 아이템이 꽉 찼을 때.
            FlowLayoutPanel temp_fp = new FlowLayoutPanel();
            temp_fp.FlowDirection = FlowDirection.LeftToRight;
            fpanel_user.Controls.Add(temp_fp);
            int fp_height = (int)(fpanel_user.Height * 0.8);
            temp_fp.Height = fp_height;

            //for(int i = 0; i < route_data[index].images.Count; i++)
            //{
            //    PictureBox temp_pic = new PictureBox();

            //}

            PictureBox temp_pic = new PictureBox();
            FlowLayoutPanel temp_fp2 = new FlowLayoutPanel();

            int temp_height = (int)(temp_fp.Height * 0.85);


            for (int j = 0; j < pRoute_data[index].images.Count; j++)
            {
                //temp fp가 전체width - left panel 한 것보다 오버가 될 경우 다시 초기화 해서 새로운 fp생성 
                if ((panel_analysis_route_main.Width - panel_analysis_route_left.Width) > (temp_fp.Width + 530)) //나중에 해상도 받아와서 설정 다시
                {
                    RouteAddImage(temp_fp, temp_fp2, temp_pic, index, j, temp_height);
                }

                //flowlayoutpanel이 parent의 길이를 초과할 경우
                else
                {
                    temp_fp = new FlowLayoutPanel();
                    temp_fp.FlowDirection = FlowDirection.LeftToRight;
                    fpanel_user.Controls.Add(temp_fp);
                    RouteAddImage(temp_fp, temp_fp2, temp_pic, index, j, temp_height);
                }
            }
        }

        private void RouteAddImage(FlowLayoutPanel temp_fp, FlowLayoutPanel temp_fp2, PictureBox temp_pic, int index, int j, int temp_height)
        {
            temp_fp2 = new FlowLayoutPanel();
            temp_fp.Controls.Add(temp_fp2);
            temp_fp2.Parent = temp_fp;
            temp_fp2.FlowDirection = FlowDirection.TopDown;

            temp_pic = new PictureBox();
            try
            {
                temp_pic.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pRoute_data[index].images[j]);
            }
            catch (FileNotFoundException fe)
            {
                temp_pic.BackColor = Color.Gray;
                Label notFound = new Label();
                notFound.Text = pRoute_data[index].images[j] + "\nnot found";
                temp_pic.Controls.Add(notFound);
                notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
            }
            temp_pic.Parent = temp_fp2;
            temp_fp2.Controls.Add(temp_pic);
            temp_pic.Size = new Size((int)(temp_height * 0.5625), temp_height);     //나중에 해상도 받아와서 설정 다시
            temp_pic.BackgroundImageLayout = ImageLayout.Stretch;
            temp_pic.Text = pRoute_data[index].images[j];

            Label temp_label = new Label();
            temp_label.Text = pRoute_data[index].images[j];
            temp_fp2.Controls.Add(temp_label);
            temp_label.Width = temp_pic.Width;
            temp_label.TextAlign = ContentAlignment.TopCenter;
            temp_label.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
            temp_label.Height = temp_label.PreferredHeight;
            temp_label.AutoSize = false;
            temp_label.AutoEllipsis = true;

            Label temp_time = new Label();
            temp_time.Text = pRoute_data[index].visit_time[j].ToString();
            temp_fp2.Controls.Add(temp_time);
            temp_time.Width = temp_pic.Width;
            temp_time.TextAlign = ContentAlignment.TopCenter;
            temp_time.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
            temp_time.Height = temp_label.PreferredHeight;
            temp_time.AutoSize = false;
            temp_time.AutoEllipsis = true;

            temp_pic.BackColor = Color.Transparent;
            temp_label.BackColor = Color.Transparent;
            temp_time.BackColor = Color.Transparent;

            temp_fp2.Size = temp_fp2.PreferredSize;
            
            //시작 여백
            if (j == 0)
            {
                temp_fp2.Margin = new Padding(30, 3, 3, 3);
            }

            if (j != pRoute_data[index].images.Count - 1)
            {
                Console.WriteLine("화살표");
                PictureBox arrow = new PictureBox();
                arrow.Parent = temp_fp;
                temp_fp.Controls.Add(arrow);
                arrow.Size = new Size(18, 18);
                arrow.Anchor = AnchorStyles.None;

                //arrow.BackgroundImage = new Bitmap(Properties.Resources.arrow);
                arrow.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._9_arrow));
                arrow.BackgroundImageLayout = ImageLayout.Stretch;
            }
            temp_fp.Size = temp_fp.PreferredSize;
        }

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }

        private void btn_analysis_show_route_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < pRoute_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((pRoute_data[i].tag.CompareTo(cb_analysis_route_selectTest.SelectedItem) == 0)
                    && (pRoute_data[i].scenario_name.CompareTo(cb_analysis_route_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                DrawRoute(index);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cb_analysis_route_selectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //테스트 콤보박스 리셋
            cb_analysis_route_selectTest.Items.Clear();


            //테스트 콤보박스에 해당하는 시나리오의 테스트만 추가
            for (int i = 0; i < pRoute_data.Count; i++)
            {
                if (pRoute_data[i].scenario_name.CompareTo(cb_analysis_route_selectScenario.SelectedItem.ToString()) == 0)
                {
                    if (!cb_analysis_route_selectTest.Items.Contains(pRoute_data[i].tag))
                    {
                        cb_analysis_route_selectTest.Items.Add(pRoute_data[i].tag);
                    }
                }
                cb_analysis_route_selectTest.Sorted = true;
            }

            if (cb_analysis_route_selectTest.Items.Count == 0)
            {
                cb_analysis_route_selectTest.Items.Add("no data");
            }
            cb_analysis_route_selectTest.SelectedIndex = 0;
        }

        private void panel_user_SizeChanged(object sender, EventArgs e)
        {
            btn_analysis_show_route.Location = new Point(80, (int)(this.Height * 0.7));
            pictureBox_line.Width = this.Width - panel_analysis_route_left.Width - 200;

            //사이즈 조정 패널.
            int h = (int)(panel_for_fpanels.Height * 0.4); //상단 패널 두개 높이 뺀 것
            panel_user.Location = new Point(0, h);

            fpanel_user.Height = panel_user.Height - 60;
        }
    }
}
