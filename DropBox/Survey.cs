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
    public partial class Survey : Form
    {
        List<EditProject.SurveyData> pSurvey_data;
        ScenarioData sData;
        string project_name;
        PrivateFontCollection pfc = new PrivateFontCollection();

        public Survey(List<EditProject.SurveyData> _pSurvey_data, string _project_name, ScenarioData _sData)
        {
            InitializeComponent();
            pSurvey_data = _pSurvey_data;
            project_name = _project_name;
            sData = _sData;
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));

            for (int i = 0; i < sData.getSData().Count; i++)
            {
                cb_analysis_survey_selectScenario.Items.Add(sData.getSData()[i].title);
            }
            if (sData.getSData().Count == 0)
            {
                cb_analysis_survey_selectScenario.Items.Add("데이터없음");
            }
            cb_analysis_survey_selectScenario.SelectedIndex = 0;


            btn_analysis_show_survey.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);

            //font
            label_1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_result.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            cb_analysis_survey_selectScenario.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            cb_analysis_survey_selectTest.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);

            //location
            cb_analysis_survey_selectScenario.Location = new Point(80, label_1.Location.Y + label_1.Height + 10);

            label_2.Location = new Point(80, cb_analysis_survey_selectScenario.Location.Y + cb_analysis_survey_selectScenario.Height + 20);
            cb_analysis_survey_selectTest.Location = new Point(80, label_2.Location.Y + label_2.Height + 10);

        }

        private void DrawSurvey(int index)
        {
            fpanel_analysis_survey.Controls.Clear();
            for (int i = 0; i < pSurvey_data[index].survey_info.Count; i++)
            {
                if (pSurvey_data[index].survey_info[i].question_type.CompareTo("resultTestQuestion_overTime") == 0)
                {
                    FlowLayoutPanel new_flowlayout = new FlowLayoutPanel();
                    new_flowlayout.FlowDirection = FlowDirection.LeftToRight;
                    fpanel_analysis_survey.Controls.Add(new_flowlayout);

                    PictureBox pBeforeImg = new PictureBox();
                    try
                    {
                        pBeforeImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pSurvey_data[index].survey_info[i].beforeImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pBeforeImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = pSurvey_data[index].survey_info[i].beforeImg + "\nnot found";
                        pBeforeImg.Controls.Add(notFound);
                        notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
                    }
                    pBeforeImg.Margin = new Padding(30, 0, 50, 0);
                    pBeforeImg.BackgroundImageLayout = ImageLayout.Stretch;
                    pBeforeImg.Size = new Size((int)(200 * 0.5625), 200);

                    PictureBox arrow = new PictureBox();
                    //arrow.BackgroundImage = Properties.Resources.arrow;
                    arrow.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._9_arrow));
                    //arrow.Margin = new Padding(50);
                    arrow.Size = new Size(20, 20);
                    arrow.BackgroundImageLayout = ImageLayout.Stretch;


                    PictureBox pAfterImg = new PictureBox();
                    try
                    {
                        pAfterImg.BackgroundImage = Image.FromFile(@"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + project_name + "\\" + pSurvey_data[index].survey_info[i].afterImg);
                    }
                    catch (FileNotFoundException fe)
                    {
                        pAfterImg.BackColor = Color.Gray;
                        Label notFound = new Label();
                        notFound.Text = pSurvey_data[index].survey_info[i].afterImg + "\nnot found";
                        pAfterImg.Controls.Add(notFound);
                        notFound.Anchor = AnchorStyles.None | AnchorStyles.Left;
                    }
                    pAfterImg.Margin = new Padding(50, 0, 0, 0);
                    pAfterImg.BackgroundImageLayout = ImageLayout.Stretch;
                    pAfterImg.Size = new Size((int)(200 * 0.5625), 200);

                    new_flowlayout.Controls.Add(pBeforeImg);
                    new_flowlayout.Controls.Add(arrow);
                    new_flowlayout.Controls.Add(pAfterImg);

                    new_flowlayout.Size = new_flowlayout.PreferredSize;

                    //가운데 화살표 위치 정할 때 상단 여백 주기
                    arrow.Margin = new Padding(0, (int)(new_flowlayout.Height / 2 - arrow.Height / 2), 0, 0);
                }
                //general question
                else
                {

                }
                Label label_q = new Label();
                label_q.Text = "Q. " + pSurvey_data[index].survey_info[i].question;
                label_q.Margin = new Padding(30, 10, 0, 0);
                label_q.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
                label_q.ForeColor = Color.Black;
                fpanel_analysis_survey.Controls.Add(label_q);
                label_q.Size = label_q.PreferredSize;

                Label label_a = new Label();
                label_a.Text = "A. " + pSurvey_data[index].survey_info[i].answer;
                label_a.ForeColor = Color.FromArgb(162, 29, 33);
                label_a.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
                label_a.Margin = new Padding(30, 10, 0, 0);
                fpanel_analysis_survey.Controls.Add(label_a);
                label_a.AutoSize = true;

                PictureBox line = new PictureBox();
                line.Size = new Size(this.Width - panel_analysis_survey_left.Width - 200, 1);
                line.BackColor = Color.Silver;
                line.Margin = new Padding(30, 20, 0, 10);
                fpanel_analysis_survey.Controls.Add(line);

            }

            PictureBox margin = new PictureBox();
            margin.Size = new Size(200, 50);
            margin.BackColor = Color.Transparent;
            fpanel_analysis_survey.Controls.Add(margin);
        }

        private void btn_analysis_show_survey_Click(object sender, EventArgs e)
        {
            bool exist = false;
            int index = 0;
            //여기서 인덱스 찾아서 넘겨야 할 듯
            for (int i = 0; i < pSurvey_data.Count; i++)
            {
                //비교 : tag, div, scenario_name              //태그가 모두 다르다는게 확정이면 div는 비교할 필요 없음
                if ((pSurvey_data[i].tag.CompareTo(cb_analysis_survey_selectTest.SelectedItem) == 0)
                    && (pSurvey_data[i].scenario_name.CompareTo(cb_analysis_survey_selectScenario.SelectedItem) == 0))
                {
                    index = i;
                    exist = true;
                    break;
                }
            }

            if (exist == true)
            {
                DrawSurvey(index);
            }
        }

        private void cb_analysis_survey_selectScenario_SelectedIndexChanged(object sender, EventArgs e)
        {
            //테스트 콤보박스 리셋
            cb_analysis_survey_selectTest.Items.Clear();


            //테스트 콤보박스에 해당하는 시나리오의 테스트만 추가
            for (int i = 0; i < pSurvey_data.Count; i++)
            {
                if (pSurvey_data[i].scenario_name.CompareTo(cb_analysis_survey_selectScenario.SelectedItem.ToString()) == 0)
                {
                    if (!cb_analysis_survey_selectTest.Items.Contains(pSurvey_data[i].tag))
                    {
                        cb_analysis_survey_selectTest.Items.Add(pSurvey_data[i].tag);
                    }
                }
                cb_analysis_survey_selectTest.Sorted = true;
            }

            if (cb_analysis_survey_selectTest.Items.Count == 0)
            {
                cb_analysis_survey_selectTest.Items.Add("no data");
            }
            cb_analysis_survey_selectTest.SelectedIndex = 0;
        }

        private void Survey_SizeChanged(object sender, EventArgs e)
        {
            btn_analysis_show_survey.Location = new Point(80, (int)(this.Height * 0.7));
            fpanel_analysis_survey.Width = this.Width;
            fpanel_analysis_survey.Height = this.Height - 100;
        }
    }
}
