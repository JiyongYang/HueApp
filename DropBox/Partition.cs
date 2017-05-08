using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;


namespace DropBox
{
    public partial class Partition : Form
    {
        PictureBox pic;
        Graphics gr;
        Brush br;
        Bitmap bitmap;
        LinkData pData = new LinkData();
        List<EditProject.TotalData> pTotal_data;
        List<string> image_names;
        List<int> count;
        List<PictureBox> pictures;
        float each_width, each_height;
        int w_count, h_count;
        double ratio;

        AnalysisData aData = new AnalysisData();
        ScenarioData sData = new ScenarioData();

        string project_name;
        PrivateFontCollection pfc = new PrivateFontCollection();

        int r;
        int g;
        int b;

        string colorX;


        public Partition(List<EditProject.TotalData> _pTotal_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pTotal_data = _pTotal_data;
            sData = _sData;
            project_name = _project_name;
            count = new List<int>();
            pictures = new List<PictureBox>();

            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));

            string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name;
            IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

            image_names = imagenames.Cast<string>().ToList();

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_partition_selectScenario.Items.Add(sData.getSData()[i].title);
            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_partition_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_partition_selectScenario.SelectedIndex = 0;


            for (int i = 0; i < image_names.Count; i++)                       //이거 시나리오 콤보박스 인덱스 change에서 사용
            {
                cb_analysis_partition_selectImage.Items.Add(Path.GetFileName(image_names[i]));
            }
            cb_analysis_partition_selectImage.Sorted = true;

            if (image_names.Count == 0)
            {
                cb_analysis_partition_selectImage.Items.Add("데이터없음");
            }
            cb_analysis_partition_selectImage.SelectedIndex = 0;
            cb_analysis_partition_selectTest.SelectedIndex = 0;

            //이미지 가로 세로 분할 횟수 초기화
            tb_analysis_partition_hori.Text = "10";     //가로
            tb_analysis_partition_verti.Text = "20";    //세로



            //font
            btn_analysis_show_partition.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);

            label_1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_3.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_4.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_5.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_6.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            tb_analysis_partition_hori.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            tb_analysis_partition_verti.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_partition_selectScenario.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_partition_selectTest.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_partition_selectImage.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            label_detail_info_title.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_click.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_time.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_shortest.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_longest.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_visit.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_testdate.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_click.Text = "클릭 수 : ";
            label_time.Text = "체류 시간 : ";
            label_visit.Text = "방문 횟수 : ";
            label_shortest.Text = "최단 체류 시간 : ";
            label_longest.Text = "최장 체류 시간 : ";
            label_testdate.Text = "테스트 일자 : ";

            /////////
            cb_analysis_partition_selectScenario.Location = new Point(80, label_1.Location.Y + label_1.Height + 10);

            label_2.Location = new Point(80, cb_analysis_partition_selectScenario.Location.Y + cb_analysis_partition_selectScenario.Height + 20);
            cb_analysis_partition_selectTest.Location = new Point(80, label_2.Location.Y + label_2.Height + 10);

            label_3.Location = new Point(80, cb_analysis_partition_selectTest.Location.Y + cb_analysis_partition_selectTest.Height + 20);
            cb_analysis_partition_selectImage.Location = new Point(80, label_3.Location.Y + label_3.Height + 10);

            label_4.Location = new Point(80, cb_analysis_partition_selectImage.Location.Y + cb_analysis_partition_selectImage.Height + 20);
            label_5.Location = new Point(80, label_4.Location.Y + label_4.Height + 10);
            label_6.Location = new Point(80, label_5.Location.Y + label_5.Height + 10);

            tb_analysis_partition_hori.Location = new Point(label_5.Location.X + label_5.Width + 10, label_5.Location.Y);
            tb_analysis_partition_verti.Location = new Point(label_6.Location.X + label_6.Width + 10, label_6.Location.Y);

        }

        public void DrawPartition(string selected, string test, string scenario)
        {
            panel_analysis_partition_picture.Height = (int)(this.Height * 0.85);

            //backgroundimage width height를 해상도 받아온 것으로 해야함 - 뒤에 숫자.
            panel_analysis_partition_picture.Width = (int)(panel_analysis_partition_picture.Height * ((double)720 / (double)1280)); //0.5625
            bitmap = new Bitmap(panel_analysis_partition_picture.Width, panel_analysis_partition_picture.Height);
            panel_analysis_partition_picture.Location = new Point((int)(this.Width / 2) - panel_analysis_partition_left.Width, 20);

            //기존에 설정되어 있던 컨트롤 다 지우기
            panel_analysis_partition_picture2.Controls.Remove(pic);
            pic = new PictureBox();
            pic.Parent = panel_analysis_partition_picture2;
            panel_analysis_partition_picture2.Controls.Add(pic);
            pic.Location = new Point(0, 0);




            Image image;
            image = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + cb_analysis_partition_selectImage.SelectedItem);
            pic.BackgroundImage = Bitmap.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + cb_analysis_partition_selectImage.SelectedItem); ;
            pic.Size = panel_analysis_partition_picture.Size;
            pic.BackgroundImageLayout = ImageLayout.Stretch;

            pic.BringToFront();
            gr = pic.CreateGraphics();
            gr = Graphics.FromImage(pic.BackgroundImage);

            //gr.FillRectangle(br, new RectangleF(new PointF((float)100.1, (float)100.4), new Size((int)each_width, (int)each_height)));


            //초기화
            //--panel_analysis_partition_picture2.Controls.Clear();

            count.Clear();
            //pictures.Clear();

            //--Image image;
            //--image = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + cb_analysis_partition_selectImage.SelectedItem);
            //panel_analysis_partition_picture2.BackgroundImage = image;
            //panel_analysis_partition_picture2.BackgroundImageLayout = ImageLayout.Stretch;

            //화면분할할 때 각 칸의 가로세로 길이
            w_count = Convert.ToInt16(tb_analysis_partition_hori.Text);
            h_count = Convert.ToInt16(tb_analysis_partition_verti.Text);
            //each_width = (float)panel_analysis_partition_picture.Width / (float)w_count;
            //each_height = (float)panel_analysis_partition_picture.Height / (float)h_count;
            each_width = (float)image.Width / (float)w_count;
            each_height = (float)image.Height / (float)h_count;
            ratio = (double)panel_analysis_partition_picture.Width / (double)image.Width;

            for (int j = 0; j < h_count; j++)
            {
                for (int i = 0; i < w_count; i++)
                {
                    ////네모칸의 횟수만큼 0 카운트 추가
                    count.Add(0);
                }
            }


            //combobox에서 정해준 이미지의 인덱스 찾기
            int index = 0;
            for (int i = 0; i < pTotal_data.Count; i++)
            {
                if (pTotal_data[i].image_name.CompareTo(selected) == 0)
                {
                    index = i;
                    break;
                }
            }

            int click = 0;

            if (test.CompareTo("모두보기") == 0)
            {
                try
                {
                    for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
                    {
                        if (pTotal_data[index].scenario_name.CompareTo(scenario) == 0)
                        {
                            for (int j = 0, k = 0; j < h_count; j++)
                            {
                                for (int i = 0; i < w_count; i++)
                                {
                                    if ((pTotal_data[index].event_data[u].xcoord >= (i * each_width)) && (pTotal_data[index].event_data[u].xcoord <= ((i + 1) * each_width)))
                                    {
                                        if ((pTotal_data[index].event_data[u].ycoord >= (j * each_height)) && (pTotal_data[index].event_data[u].ycoord <= ((j + 1) * each_height)))
                                        {
                                            click++;
                                            count[k]++;
                                            break;
                                        }
                                    }
                                    k++;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ae)
                {

                }

            }
            else
            {
                try
                {
                    for (int u = 0; u < pTotal_data[index].event_data.Count; u++)
                    {
                        //선택한 test 번호에 맞는 것만.
                        if (pTotal_data[index].scenario_name.CompareTo(scenario) == 0 && pTotal_data[index].event_data[u].test_num == Convert.ToInt16(test))
                        {
                            for (int j = 0, k = 0; j < h_count; j++)
                            {
                                for (int i = 0; i < w_count; i++)
                                {
                                    if ((pTotal_data[index].event_data[u].xcoord >= (i * each_width)) && (pTotal_data[index].event_data[u].xcoord <= ((i + 1) * each_width)))
                                    {
                                        if ((pTotal_data[index].event_data[u].ycoord >= (j * each_height)) && (pTotal_data[index].event_data[u].ycoord <= ((j + 1) * each_height)))
                                        {
                                            click++;
                                            count[k]++;
                                            break;
                                        }
                                    }
                                    k++;
                                }
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ae)
                {

                }
            }

            label_click.Text = "클릭 수 : " + click.ToString() + "회";

            //for (int i = 0; i < pTotal_data[index].event_data.Count; i++)
            //{
            //    MessageBox.Show(pTotal_data[index].event_data[i].img);
            //}
            //체류시간, 방문횟수
            double entireTime = 0, shortestTime = 99999, longestTime = 0;

            //for (int i = 0; i < pTotal_data[index].event_data.Count; i++)
            //{

            //    if(pTotal_data[index].scenario_name.CompareTo(scenario) == 0 && pTotal_data[index].event_data[i].test_num == Convert.ToInt16(test))
            //    {
            //        entireTime += pTotal_data[index].event_data[i].timeImg;
            //        //최장체류시간
            //        if (longestTime < pTotal_data[index].event_data[i].timeImg)
            //        {
            //            longestTime = pTotal_data[index].event_data[i].timeImg;
            //        }
            //        //최단체류시간
            //        try
            //        {
            //            if (pTotal_data[index].event_data[i].img.CompareTo(pTotal_data[index].event_data[i + 1].img) != 0)
            //            {
            //                if (shortestTime > pTotal_data[index].event_data[i].timeImg)
            //                {
            //                    shortestTime = pTotal_data[index].event_data[i].timeImg;
            //                }
            //            }
            //        }
            //        catch (ArgumentOutOfRangeException ae)
            //        {
            //            if (shortestTime > pTotal_data[index].event_data[i].timeImg)
            //            {
            //                shortestTime = pTotal_data[index].event_data[i].timeImg;
            //            }
            //        }
            //    }
            //}

            //모두보기에서는 현재 에러남
            //int visit = 0;
            //bool visit_flag = false;
            //for (int i = 0; i < pTotal_data[index].event_data.Count; i++)
            //{
            //    if (pTotal_data[index].scenario_name.CompareTo(scenario) == 0 && pTotal_data[index].event_data[i].img.CompareTo(selected) == 0 && pTotal_data[index].event_data[i].test_num == Convert.ToInt16(test))
            //    {
            //        if(pTotal_data[index].event_data[i].timeEntire - pTotal_data[index].event_data[i].timeImg != pTotal_data[index].event_data[i - 1].timeImg)
            //        {
            //            //if (visit_flag == false)
            //            //{
            //                visit++;
            //            //}
            //            //visit_flag = true;
            //        }
            //    }
            //    else
            //    {
            //        //visit_flag = false;
            //    }
            //}

            //if (click == 0)
            //{
            //    label_time.Text = "체류 시간 : 0z초";
            //    label_longest.Text = "최장 체류 시간 : 0초";
            //    label_shortest.Text = "최단 체류 시간 : 0초";
            //    //label_visit.Text = "방문 횟수 : 0회";
            //}
            //else
            //{
            //    label_time.Text = "체류 시간 : " + entireTime + "초";
            //    label_longest.Text = "최장 체류 시간 : " + longestTime + "초";
            //    label_shortest.Text = "최단 체류 시간 : " + shortestTime + "초";
            //    //label_visit.Text = "방문 횟수 : " + visit + "회";
            //}

            RectangleF[] rects = { new RectangleF(0,0, each_width, each_height) };
            
            int count_index = 0;
            for (int j = 0; j < h_count; j++)
            {
                for (int i = 0; i < w_count; i++)
                {
                    rects[0].Location = new PointF((float)(i * each_width), (float)(j * each_height));
                    gr.DrawRectangles(new Pen(Color.Red, 1), rects);
                    if (count[count_index] > 0)
                    {
                        if (count[count_index] < (pTotal_data[index].event_data.Count / 9))
                        {
                            br = new SolidBrush(SetColor(180, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 8))
                        {
                            br = new SolidBrush(SetColor(160, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 7))
                        {
                            br = new SolidBrush(SetColor(140, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 6))
                        {
                            br = new SolidBrush(SetColor(120, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 5))
                        {
                            br = new SolidBrush(SetColor(100, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 4))
                        {
                            br = new SolidBrush(SetColor(80, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 3))
                        {
                            br = new SolidBrush(SetColor(60, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 2))
                        {
                            br = new SolidBrush(SetColor(40, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                        else if (count[count_index] < (pTotal_data[index].event_data.Count / 1))
                        {
                            br = new SolidBrush(SetColor(20, Color.Red));
                            gr.FillRectangle(br, new RectangleF(new PointF((float)(i * each_width), (float)(j * each_height)), new Size((int)each_width, (int)each_height)));
                        }
                    }
                    count_index++;
                }
            }
        }

        private void Partition_SizeChanged(object sender, EventArgs e)
        {
            btn_analysis_show_partition.Location = new Point(80, (int)(this.Height * 0.7));
        }

        private void cb_analysis_partition_selectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //테스트 콤보박스 리셋
            cb_analysis_partition_selectTest.Items.Clear();
            cb_analysis_partition_selectTest.Items.Add("모두보기");
            cb_analysis_partition_selectTest.SelectedIndex = 0;

            //테스트 콤보박스에 해당하는 시나리오의 테스트만 추가
            for (int i = 0; i < pTotal_data.Count; i++)
            {
                if (pTotal_data[i].scenario_name.CompareTo(cb_analysis_partition_selectScenario.SelectedItem.ToString()) == 0)
                {
                    for (int j = 0; j < pTotal_data[i].event_data.Count; j++)
                    {
                        if (!cb_analysis_partition_selectTest.Items.Contains(pTotal_data[i].event_data[j].test_num))
                        {
                            cb_analysis_partition_selectTest.Items.Add(pTotal_data[i].event_data[j].test_num);
                        }
                    }
                }
            }
            cb_analysis_partition_selectTest.Sorted = true;

            
        }

        private void btn_analysis_show_partition_Click(object sender, EventArgs e)
        {
            string s = cb_analysis_partition_selectScenario.SelectedItem.ToString();
            string t = cb_analysis_partition_selectTest.SelectedItem.ToString();
            string i = cb_analysis_partition_selectImage.SelectedItem.ToString();

            DrawPartition(i, t, s);
        }

        public Color SetColor(int A, Color color)
        {
            return Color.FromArgb(A, color.R, color.G, color.B);
        }
    }
}
