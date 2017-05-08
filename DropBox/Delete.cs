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

namespace DropBox
{
    public partial class Delete : Form
    {
        List<EditProject.RouteData> temp_route_data;
        List<EditProject.SurveyData> temp_survey_data;
        EditProject editProject = new EditProject();
        ComboBox cb_route_group, cb_route_test;
        ComboBox cb_survey_group, cb_survey_test;
        ComboBox cb_dots_group, cb_dots_test, cb_dots_image;
        ComboBox cb_partition_group, cb_partition_test, cb_partition_image;
        string[] filenames;
        List<int> under_bar_index;
        string project_name;
        int clicked = 0;

        public Delete(List<EditProject.RouteData> _route_data, List<EditProject.SurveyData> _survey_data, string[] _filenames, string _project_name, 
            ComboBox _cb_route_group, ComboBox _cb_route_test, ComboBox _cb_survey_group, ComboBox _cb_survey_test,
            ComboBox _cb_dots_group, ComboBox _cb_dots_test, ComboBox _cb_dots_image,
            ComboBox _cb_partition_group, ComboBox _cb_partition_test, ComboBox _cb_partition_image)
        {
            InitializeComponent();
            temp_route_data = _route_data;
            temp_survey_data = _survey_data;
            cb_route_group = _cb_route_group;
            cb_route_test = _cb_route_test;
            cb_survey_group = _cb_survey_group;
            cb_survey_test = _cb_survey_test;
            cb_dots_group = _cb_dots_group;
            cb_dots_test = _cb_dots_test;
            cb_dots_image = _cb_dots_image;
            cb_partition_group = _cb_partition_group;
            cb_partition_test = _cb_partition_test;
            cb_partition_image = _cb_partition_image;

            filenames = _filenames;
            project_name = _project_name;

            under_bar_index = new List<int>();

            //초기화 하면서 일반데이터 버튼이 눌러졌다는 것을 표시할 수 있어야함.
            for(int i = 0; i < temp_route_data.Count; i++)
            {
                listBox1.Items.Add(temp_route_data[i].tag);     //개인 테스트 정보에 대한 것.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection seletedItems = new ListBox.SelectedObjectCollection(listBox1);
            seletedItems = listBox1.SelectedItems;
            int range = seletedItems.Count;
            bool find = false;
            string dPath = string.Empty;

            //일반데이터 삭제시
            if(clicked == 0)
            {
                for(int i = 0; i < range; i++)
                {
                    for (int j = 0; j < temp_route_data.Count; j++)
                    {
                        if (temp_route_data[j].tag.CompareTo(seletedItems[0]) == 0)
                        {
                            under_bar_index.Clear();
                            find = false;
                            //해당 태그와 같은 파일 이름을 먼저 찾고. 
                            for (int k = 0; k < filenames.Length; k++)
                            {
                                for (int h = 0; h < filenames[k].Length; h++)
                                {
                                    if (filenames[k][h].CompareTo('_') == 0)
                                    {
                                        under_bar_index.Add(h);
                                    }
                                }

                                //peer // user 구분
                                string div_temp = filenames[k].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                                string temp_scenario_tag = filenames[k][0].ToString();
                                string temp_scenario_name = filenames[k].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                                string temp_devicie_id = filenames[k].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);
                                string temp_project_name = filenames[k].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);

                                if ((temp_scenario_tag.CompareTo(temp_route_data[j].tag.ToString())) == 0
                                && (temp_scenario_name.CompareTo(temp_route_data[j].scenario_name)) == 0
                                && (temp_project_name.CompareTo(project_name) == 0))
                                {
                                    dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\" + filenames[k];
                                    //dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\test.txt";
                                    find = true;
                                    break;
                                }
                            }

                            //여기서 삭제 하고 나가기 
                            if (find == true)
                            {
                                listBox1.Items.Remove(seletedItems[0]);
                                temp_route_data.RemoveAt(j);
                                MessageBox.Show("삭제할 데이터 : " + dPath);
                                //File.Delete(dPath);                       
                                break;
                            }
                        }
                    }
                }
                RefreshRoute();
            }
            else
            {
                for (int i = 0; i < range; i++)
                {
                    for (int j = 0; j < temp_survey_data.Count; j++)
                    {
                        if (temp_survey_data[j].tag.CompareTo(seletedItems[0]) == 0)
                        {
                            under_bar_index.Clear();
                            find = false;
                            //해당 태그와 같은 파일 이름을 먼저 찾고. 
                            for (int k = 0; k < filenames.Length; k++)
                            {
                                for (int h = 0; h < filenames[k].Length; h++)
                                {
                                    if (filenames[k][h].CompareTo('_') == 0)
                                    {
                                        under_bar_index.Add(h);
                                    }
                                }

                                //peer // user 구분
                                string div_temp = filenames[k].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                                string temp_scenario_tag = filenames[k][0].ToString();
                                string temp_scenario_name = filenames[k].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                                string temp_devicie_id = filenames[k].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);
                                string temp_project_name = filenames[k].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);

                                if ((temp_scenario_tag.CompareTo(temp_survey_data[j].tag.ToString())) == 0
                                && (temp_scenario_name.CompareTo(temp_survey_data[j].scenario_name)) == 0
                                && (temp_project_name.CompareTo(project_name) == 0))
                                {
                                    dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\" + filenames[k];
                                    //dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\test.txt";
                                    find = true;
                                    break;
                                }
                            }

                            //여기서 삭제 하고 나가기 
                            if (find == true)
                            {
                                listBox1.Items.Remove(seletedItems[0]);
                                temp_survey_data.RemoveAt(j);
                                MessageBox.Show("삭제할 데이터 : " + dPath);
                                //File.Delete(dPath);                       
                                break;
                            }
                        }
                    }
                }
                RefreshSurvey();
            }

            
        }

        private void btn_delete_normData_Click(object sender, EventArgs e)
        {
            clicked = 0;
            listBox1.Items.Clear();
            for (int i = 0; i < temp_route_data.Count; i++)
            {
                listBox1.Items.Add(temp_route_data[i].tag);     //개인 테스트 정보에 대한 것.
            }
        }

        private void btn_delete_survey_Click(object sender, EventArgs e)
        {
            clicked = 1;
            listBox1.Items.Clear();
            for (int i = 0; i < temp_survey_data.Count; i++)
            {
                listBox1.Items.Add(temp_survey_data[i].tag);     //개인 테스트 정보에 대한 것.
            }
        }

        private void RefreshRoute()
        {
            //editProject.SetRouteData(temp_route_data);
            cb_route_test.Items.Clear();
            cb_partition_test.Items.Clear();                    //dots와 partition의 테스트별 정보는 지용이한테서 얻음
            cb_dots_test.Items.Clear();

            //피어목록보여주기
            if (cb_route_group.SelectedIndex == 0)
            {
                for (int i = 0; i < temp_route_data.Count; i++)
                {
                    if (temp_route_data[i].div.CompareTo("p") == 0)
                    {
                        cb_route_test.Items.Add(temp_route_data[i].tag);
                    }
                }
            }
            //유저
            else
            {
                for (int i = 0; i < temp_route_data.Count; i++)
                {
                    if (temp_route_data[i].div.CompareTo("u") == 0)
                    {
                        cb_route_test.Items.Add(temp_route_data[i].tag);
                    }
                }
            }
            if (cb_route_test.Items.Count == 0)
            {
                cb_route_test.Items.Add("데이터없음");
            }
            cb_route_test.Sorted = true;
            cb_route_test.SelectedIndex = 0;
        }

        //설문조사 탭의 콤보박스 받아와서 변경 설정하기
        private void RefreshSurvey()
        {
            editProject.SetSurveyData(temp_survey_data);
            cb_survey_test.Items.Clear();

            //피어목록보여주기
            if (cb_survey_group.SelectedIndex == 0)
            {
                for (int i = 0; i < temp_survey_data.Count; i++)
                {
                    if (temp_survey_data[i].div.CompareTo("p") == 0)
                    {
                        cb_survey_test.Items.Add(temp_survey_data[i].tag);
                    }
                }
            }
            //유저
            else
            {
                for (int i = 0; i < temp_survey_data.Count; i++)
                {
                    if (temp_survey_data[i].div.CompareTo("u") == 0)
                    {
                        cb_survey_test.Items.Add(temp_survey_data[i].tag);
                    }
                }
            }
            if (cb_survey_test.Items.Count == 0)
            {
                cb_survey_test.Items.Add("데이터없음");
            }
            cb_survey_test.Sorted = true;
            cb_survey_test.SelectedIndex = 0;
        }
    }
}
