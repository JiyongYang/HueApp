using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DropBox
{
    class AnalysisData
    {
        public struct TotalData
        {
            public string image_name;
            public List<EventData> event_data;  //해당 xml의 모든 데이터들이 들어갈 리스트
        }

        public struct EventData
        {
            public string div;
            public int xcoord;
            public int ycoord;
            public string event_info;
            public string img;
            public double timeEntire;
            public double timeImg;
        }

        public struct RouteData
        {
            public int tag;
            public string div;
            public DateTime creation;
            public string scenario_name;
            public string device_id;
            public List<string> images;
            public List<double> visit_time;
        }

        public struct SurveyData
        {
            public int tag;
            public string div;
            public DateTime creation;
            public string scenario_name;
            public string device_id;
            public List<SurveyInternalInfo> survey_info;
        }

        public struct SurveyInternalInfo
        {
            public string question_type;
            public string question;
            public string answer;
            public string beforeImg;
            public string afterImg;
        }

        public List<TotalData> total_data;
        public List<PictureBox> pictures;
        public List<int> count;
        public List<int> under_bar_index;
        public List<RouteData> route_data;
        public List<SurveyData> survey_data;
        public LinkData pData = new LinkData();
        public List<string> image_names = new List<string>();

        string scenario_name;
        string[] filenames;
        string nudge_path = @"C:\Users\" + Environment.UserName + "\\Nudge";

        public void Analysis_Setup()
        {
            //처음 눌렀을 때 초기화 해주고 그 다음부터는 바로 보여주는 곳으로.

            total_data = new List<TotalData>();
            pictures = new List<PictureBox>();
            count = new List<int>();
            under_bar_index = new List<int>();
            route_data = new List<RouteData>();
            survey_data = new List<SurveyData>();

            scenario_name = "글 수정하기";           //combobox_scenario 에서 인덱스 0번으로 받아오기.

            DirectoryInfo di = new DirectoryInfo(nudge_path);

            if (di.Exists == false)
            {
                di.Create();
            }
            filenames = Directory.GetFiles(nudge_path, "*.xml");

            //파일 다운로스 시작
            //FileDownloader fd = new FileDownloader(filenames, scenario_name, project_name);
            XmlRead();        //다운로드 되어있는 Xml 읽어와서 데이터 저장
        }

        private void XmlRead()
        {
            //Xml파일들이 저장되는 폴더 경로
            DirectoryInfo dInfo = new DirectoryInfo(nudge_path);

            string div_temp;
            DateTime date_time;

            if (dInfo.Exists)
            {

                string cPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + pData.GetProjectName();
                IEnumerable<string> imagenames = Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png"));

                image_names = imagenames.Cast<string>().ToList();

                //다운로드 되고 나서 다시 xml 파일 정보 불러오기
                filenames = Directory.GetFiles(nudge_path, "*.xml");



                for (int i = 0; i < filenames.Length; i++)
                {
                    //경로에서 파일 이름만 추출
                    filenames[i] = Path.GetFileName(filenames[i]);
                }

                ////xml name
                ////number
                //Console.WriteLine(filenames[0][0]);
                ////user or peer
                //Console.WriteLine(filenames[0].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1));
                ////project
                //Console.WriteLine(filenames[0].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1));
                ////scenario
                //Console.WriteLine(filenames[0].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1));
                ////device id
                //Console.WriteLine(filenames[0].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1));
                ////touch data or survey result
                //Console.WriteLine(filenames[0].Substring(under_bar_index[4] + 1, (under_bar_index[5] - under_bar_index[4]) - 1));
                ////designer name
                //Console.WriteLine(filenames[0].Substring(under_bar_index[5] + 1, (filenames[0].Length - under_bar_index[5]) - 5));

                for (int i = 0; i < filenames.Length; i++)
                {
                    //생성일자
                    date_time = File.GetCreationTime(nudge_path + "\\" + filenames[i]);

                    //시나리오 추출 &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    under_bar_index.Clear();

                    //xml 이름에서 underbar 위치 찾아내서 각 정보 불러올 때 사용
                    for (int k = 0; k < filenames[i].Length; k++)
                    {
                        if (filenames[i][k].CompareTo('_') == 0)
                        {
                            under_bar_index.Add(k);
                        }
                    }

                    //peer // user 구분
                    div_temp = filenames[i].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                    string temp_scenario_tag = filenames[i][0].ToString();
                    string temp_scenario_name = filenames[i].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                    string temp_devicie_id = filenames[i].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);
                    string temp_project_name = filenames[i].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);
                    string temp_test_type = filenames[i].Substring(under_bar_index[4] + 1, (under_bar_index[5] - under_bar_index[4]) - 1);

                    if (temp_test_type.CompareTo("userData") == 0)
                    {
                        bool scenario_add = false;

                        for (int k = 0; k < route_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (route_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && route_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && route_data[k].device_id.CompareTo(temp_devicie_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }

                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false
                            && temp_project_name.CompareTo(pData.GetProjectName()) == 0
                            && temp_scenario_name.CompareTo(scenario_name) == 0)
                        {
                            route_data.Add(new RouteData() { tag = Convert.ToInt32(temp_scenario_tag), div = div_temp, creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, images = new List<string>(), visit_time = new List<double>() });
                        }
                        else continue;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        int x_cor = 0, y_cor = 0;
                        double pTimeEntire = 0, pTimeImg = 0;
                        string pEvent = string.Empty, pImg = string.Empty;
                        bool data_in = false;

                        double real1 = 0, real2 = 0;

                        xmlDoc.Load(nudge_path + "\\" + filenames[i]);
                        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/project/InputInfo");
                        //각 파일의 생성일자를 받아와서 이것을 기준으로 보여주는 순서 설정

                        foreach (XmlNode child_node in nodeList)
                        {

                            data_in = false;
                            x_cor = Convert.ToInt32(child_node.SelectSingleNode("xcoord").InnerText);
                            y_cor = Convert.ToInt32(child_node.SelectSingleNode("ycoord").InnerText);
                            pEvent = child_node.SelectSingleNode("event").InnerText;
                            pImg = child_node.SelectSingleNode("img").InnerText;
                            pTimeEntire = Convert.ToDouble(child_node.SelectSingleNode("timeEntire").InnerText);
                            pTimeImg = Convert.ToDouble(child_node.SelectSingleNode("timeImg").InnerText);

                            //이미지 이름 확인해서 해당 인덱스의 리스트에 넣기
                            for (int j = 0; j < total_data.Count; j++)
                            {
                                if (total_data[j].image_name.Equals(pImg))
                                {
                                    total_data[j].event_data.Add(new EventData() { div = div_temp, xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                                    data_in = true;
                                    break;
                                }
                            }

                            //total data안에 없으면 새로운 정보 입력
                            if (data_in == false)
                            {
                                total_data.Add(new TotalData() { image_name = pImg, event_data = new List<EventData>() });
                                total_data[total_data.Count - 1].event_data.Add(new EventData() { div = div_temp, xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                            }

                            //시나리오 정보 입력

                            //2번 이미지에서 3번 이미지로 넘어갈 경우, real1은 2->3의 시간을, real2는 1->2의 시간을 가지고 있는다.
                            //두 시간을 빼면 2번 이미지에 머물렀던 시간을 구할 수 있다.
                            real1 = pTimeEntire - pTimeImg;
                            try
                            {
                                if (route_data[route_data.Count - 1].images.ElementAt(route_data[route_data.Count - 1].images.Count - 1).CompareTo(pImg) != 0)
                                {
                                    route_data[route_data.Count - 1].images.Add(pImg);
                                    route_data[route_data.Count - 1].visit_time.Add(real1 - real2);
                                    real2 = real1;
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                //리스트가 비어있을 경우 처리
                                Console.WriteLine("초기값 입력");
                                route_data[route_data.Count - 1].images.Add(pImg);
                            }
                            continue;
                        }
                        //마지막은 더 이상 이미지의 전환이 없기 때문에 해당 이미지에 머문 시간을 입력한다.
                        route_data[route_data.Count - 1].visit_time.Add(pTimeImg);
                    }

                    //설문조사 데이터에 대한 것
                    else
                    {
                        bool scenario_add = false;

                        for (int k = 0; k < survey_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (survey_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && survey_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && survey_data[k].device_id.CompareTo(temp_devicie_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }

                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false
                            && temp_project_name.CompareTo(pData.GetProjectName()) == 0
                            && temp_scenario_name.CompareTo(scenario_name) == 0)
                        {
                            survey_data.Add(new SurveyData() { tag = Convert.ToInt32(temp_scenario_tag), div = div_temp, creation = date_time, scenario_name = temp_scenario_name, device_id = temp_devicie_id, survey_info = new List<SurveyInternalInfo>() });
                        }
                        else continue;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        string temp_question_type;
                        string temp_question, temp_answer;
                        string temp_beforeImg, temp_afterImg;

                        xmlDoc.Load(nudge_path + "\\" + filenames[i]);
                        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/survey/SurveyInfo");

                        foreach (XmlNode child_node in nodeList)
                        {
                            temp_question_type = string.Empty;
                            temp_question = string.Empty;
                            temp_answer = string.Empty;
                            temp_beforeImg = string.Empty;
                            temp_afterImg = string.Empty;

                            temp_question_type = child_node.SelectSingleNode("questionType").InnerText;
                            temp_question = child_node.SelectSingleNode("question").InnerText;
                            temp_answer = child_node.SelectSingleNode("answer").InnerText;

                            if (temp_question_type.CompareTo("resultTestQuestion_overTime") == 0)
                            {
                                temp_beforeImg = child_node.SelectSingleNode("beforeImg").InnerText;
                                temp_afterImg = child_node.SelectSingleNode("afterImg").InnerText;
                            }
                            //general question에 대한 것
                            else
                            {

                            }
                            survey_data[survey_data.Count - 1].survey_info.Add(new SurveyInternalInfo() { question_type = temp_question_type, question = temp_question, answer = temp_answer, afterImg = temp_afterImg, beforeImg = temp_beforeImg });
                        }
                    }
                }
            }
            else
            { //MessageBox.Show("no Info");
            }
        }
    }
}
