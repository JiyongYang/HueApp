using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Reflection;
using Manina.Windows.Forms;
using Ionic.Zip;
using System.Drawing.Text;

namespace DropBox
{
    public partial class EditProject : Form
    {
        LinkData pData = new LinkData();
        ScenarioData sData = new ScenarioData();
        AnalysisData aData = new AnalysisData();
        ResultData rData = new ResultData();

        private string myPath;
        private Button TempDeleteButton;
        private ContextMenu cm;

        main _main;

        public struct SCENARIO
        {
            public int tag;
            public string title;
            public string purpose;
            public string level;
            public string time;
        }

        public struct LINK
        {
            public string fileName;
            public List<link_info_temp> link_data_temp;
        }

        public struct link_info_temp
        {
            public int btn_id;
            public Button bttn;
            public string DstFile;
            public Point image_xy;
            public float image_width;
            public float image_height;
            public string input_type;
        }

        List<SCENARIO> scenarios = new List<SCENARIO>();
        List<LINK> links = new List<LINK>() { };
        List<string> image_list = new List<string>();
        LINK temp_link;
        int last_index = 0;
        public ListBox listBox;
        string user_id;

        /// <summary>
        /// Data for analysis
        /// </summary>
        public struct TotalData
        {
            public string image_name;
            public string scenario_name;
            public List<EventData> event_data;  //해당 xml의 모든 데이터들이 들어갈 리스트
        }

        public struct EventData
        {
            public int test_num;
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

        //public struct AnalysisInfo
        //{
        //    public string scenario;
        //    public string image;
        //    public int clicks;
        //    public float time;

        //}

        List<TotalData> total_data;
        List<string> sName;

        float width, height;
        int w_count, h_count;
        float each_width, each_height;
        List<PictureBox> pictures;
        List<int> count;
        Image mainImage;
        PictureBox pic;
        double ratio;
        Graphics g;
        Panel panel_dots;
        Bitmap bitmap;
        SolidBrush br;
        List<int> under_bar_index;
        List<RouteData> route_data;
        List<SurveyData> survey_data;

        string scenario_name;
        string[] filenames;

        //data for analysis

        PrivateFontCollection pfc = new PrivateFontCollection();


        public EditProject()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));

            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);
        }

        //from edit_image
        public EditProject(string _myPath, LinkData _pData, ScenarioData _sData, string _user_id, List<TotalData> _total_data)
        {
            InitializeComponent();

            _main = new main();
            myPath = _myPath;
            pData = _pData;
            sData = _sData;
            user_id = _user_id;
            pData.SetDownload(false);       //edit image에서 edit project로 넘어오면 download를 다시 하지 않게 함
            total_data = _total_data;

            SetupButton();

            for(int i = 0; i < sData.getSData().Count; i++)
            {
                listBox_scenario.Items.Add(sData.getSData()[i].title);
            }

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));

            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.imageListView_menuItem_delete_click));
        }

        //from main
        public EditProject(string _myPath, string _id, string _project_name)
        {
            
            InitializeComponent();

            _main = new main();
            myPath = _myPath;
            user_id = _id;
            pData.SetProjectName(_project_name);
            pData.SetUserId(_id);
            pData.SetDownload(true);        //main에서부터 올 경우 다운로드 하도록 함

            SetupButton();
            ReadLink();
            ReadScenario();
            Analysis_Setup();


            for(int i = 0; i < sData.getSData().Count; i++)
            {
                Console.WriteLine("받아온거 : " + sData.getSData()[i].title);
                for (int j = 0; j < sData.getSData()[i].paths.Count; j++)
                {
                    Console.WriteLine("받아온거 : " + sData.getSData()[i].paths[j].path);
                }
            }

            Assembly assembly = Assembly.GetAssembly(typeof(ImageListView));
            //imageListView_EditProject.Width = this.Width;
            imageListView_EditProject.SetRenderer(new ImageListViewRenderers.DefaultRenderer());
            imageListView_EditProject.SortColumn = 0;
            imageListView_EditProject.SortOrder = Manina.Windows.Forms.SortOrder.AscendingNatural;

            string cacheDir = Path.Combine(
                Path.GetDirectoryName(new Uri(assembly.GetName().CodeBase).LocalPath),
                "Cache"
                );
            if (!Directory.Exists(cacheDir))
                Directory.CreateDirectory(cacheDir);
            imageListView_EditProject.PersistentCacheDirectory = cacheDir;
            imageListView_EditProject.Columns.Add(ColumnType.Name);
            imageListView_EditProject.Columns.Add(ColumnType.Dimensions);
            imageListView_EditProject.Columns.Add(ColumnType.FileSize);
            imageListView_EditProject.Columns.Add(ColumnType.FolderName);
            imageListView_EditProject.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);

            cm = new ContextMenu();
            cm.MenuItems.Add("Delete", new System.EventHandler(this.imageListView_menuItem_delete_click));
        }


        private void SetupButton()
        {
            temp_link.link_data_temp = new List<link_info_temp>();
            panel_scenario.Visible = false;
            panel_analysis.Visible = false;
            listBox = this.listBox_scenario;
            panel_content.BringToFront();
            imageListView_EditProject.BringToFront();
            btn_editImage_back.Visible = false;
            btn_editImage_save.Visible = false;
            btnAddImages.Visible = true;
            

            //폰트 변경
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));
            label_content_projectname.Font = new Font(pfc.Families[0], 40, FontStyle.Bold);
            label_content_projectname.ForeColor = Color.FromArgb(104, 104, 104);

            label_scenario_projectname.Font = new Font(pfc.Families[0], 40, FontStyle.Bold);
            label_scenario_projectname.ForeColor = Color.FromArgb(104, 104, 104);

            label_analysis_projectname.Font = new Font(pfc.Families[0], 40, FontStyle.Bold);
            label_analysis_projectname.ForeColor = Color.FromArgb(104, 104, 104);

            //listBox_scenario.Items.Font = new Font(pfc.Families[0], 40, FontStyle.Bold);
            //listBox_scenario.ForeColor = Color.FromArgb(104, 104, 104);

            DirectoryInfo Info = new DirectoryInfo(myPath + "\\");

            //프로젝트 이름
            label_content_projectname.Text = Info.Name;
            label_scenario_projectname.Text = Info.Name;
            label_analysis_projectname.Text = Info.Name;

            panel_scenario_left_title.BackColor = Color.FromArgb(204, 204, 204);
            panel_scenario_right_title.BackColor = Color.FromArgb(204, 204, 204);


            //시나리오 - 테이블 상단 탭
            label_scenario_left_title.Font = new Font(pfc.Families[0], 20, FontStyle.Regular);
            label_scenario_right_title.Font = new Font(pfc.Families[0], 20, FontStyle.Regular);

            //좌측 패널
            panel_analysis_left.BackColor = Color.FromArgb(246, 247, 248);
            panel_content_left.BackColor = Color.FromArgb(246, 247, 248);
            panel_scenario_left.BackColor = Color.FromArgb(246, 247, 248);

            DateTime creationTime = File.GetCreationTime(myPath);
            string creationTime_revised = creationTime.ToShortDateString();

            label_content_left_info1.Text = "최초 제작     " + creationTime_revised.Substring(0, 4) + " ." + creationTime_revised.Substring(5, 2) + " ." + creationTime_revised.Substring(8, 2);
            label_scenario_left_info1.Text = "최초 제작     " + creationTime_revised.Substring(0, 4) + " ." + creationTime_revised.Substring(5, 2) + " ." + creationTime_revised.Substring(8, 2);
            label_analysis_left_info1.Text = "최초 제작     " + creationTime_revised.Substring(0, 4) + " ." + creationTime_revised.Substring(5, 2) + " ." + creationTime_revised.Substring(8, 2);

            DateTime writeTime = File.GetLastWriteTime(myPath);
            string writeTime_revised = writeTime.ToShortDateString();
            label_content_left_info2.Text = "마지막 수정  " + writeTime_revised.Substring(0, 4) + " ." + writeTime_revised.Substring(5, 2) + " ." + writeTime_revised.Substring(8, 2);
            label_scenario_left_info2.Text = "마지막 수정  " + writeTime_revised.Substring(0, 4) + " ." + writeTime_revised.Substring(5, 2) + " ." + writeTime_revised.Substring(8, 2);
            label_analysis_left_info2.Text = "마지막 수정  " + writeTime_revised.Substring(0, 4) + " ." + writeTime_revised.Substring(5, 2) + " ." + writeTime_revised.Substring(8, 2);

            XmlDocument xmlResolution = new XmlDocument();

            xmlResolution.Load(myPath + "\\" + "link.xml");
            XmlNode nodeDevice = xmlResolution.DocumentElement.SelectSingleNode("/LinkTable");
            string resolution = nodeDevice.SelectSingleNode("DeviceResolution").InnerText;

            int first = 0, second = 0;
         
            first = resolution.IndexOf("(");
            second = resolution.IndexOf(")");

            resolution = resolution.Substring(first + 1, second - first - 1);

            label_content_left_info3.Text = "작업환경      " + resolution;
            label_scenario_left_info3.Text = "작업환경      " + resolution;
            label_analysis_left_info3.Text = "작업환경      " + resolution;

            string[] fileCount = Directory.GetFiles(myPath, "*.*");
            string[] fileNotCount1 = Directory.GetFiles(myPath, "*.zip");
            string[] fileNotCount2 = Directory.GetFiles(myPath, "*.xml");

            string NumOfFiles;
            FileInfo fInfo = new FileInfo(myPath + "\\DoNotDelete.bmp");
            if (fInfo.Exists)
            {
                NumOfFiles = (fileCount.Length - fileNotCount1.Length - fileNotCount2.Length - 1).ToString();
            }
            else
            {
                NumOfFiles = (fileCount.Length - fileNotCount1.Length - fileNotCount2.Length).ToString();
            }

            label_content_left_info4.Text = "총 이미지수  " + NumOfFiles + "장";
            label_scenario_left_info4.Text = "총 이미지수  " + NumOfFiles + "장";
            label_analysis_left_info4.Text = "총 이미지수  " + NumOfFiles + "장";

            label_content_left_info1.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_content_left_info2.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_content_left_info3.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_content_left_info4.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_content_left_info1.ForeColor = Color.FromArgb(104, 104, 104);
            label_content_left_info2.ForeColor = Color.FromArgb(104, 104, 104);
            label_content_left_info3.ForeColor = Color.FromArgb(104, 104, 104);
            label_content_left_info4.ForeColor = Color.FromArgb(104, 104, 104);

            label_scenario_left_info1.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_scenario_left_info2.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_scenario_left_info3.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_scenario_left_info4.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_scenario_left_info1.ForeColor = Color.FromArgb(104, 104, 104);
            label_scenario_left_info2.ForeColor = Color.FromArgb(104, 104, 104);
            label_scenario_left_info3.ForeColor = Color.FromArgb(104, 104, 104);
            label_scenario_left_info4.ForeColor = Color.FromArgb(104, 104, 104);

            label_analysis_left_info1.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_analysis_left_info2.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_analysis_left_info3.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_analysis_left_info4.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);
            label_analysis_left_info1.ForeColor = Color.FromArgb(104, 104, 104);
            label_analysis_left_info2.ForeColor = Color.FromArgb(104, 104, 104);
            label_analysis_left_info3.ForeColor = Color.FromArgb(104, 104, 104);
            label_analysis_left_info4.ForeColor = Color.FromArgb(104, 104, 104);

            if (Info.Exists)
            {
                foreach (System.IO.FileInfo _file in Info.GetFiles())
                {
                    string fileName = _file.Name.Substring(0, _file.Name.LastIndexOf('.'));
                    string fileExt = _file.Name.Substring(_file.Name.LastIndexOf('.'));
                    string overName = String.Empty;

                    if (fileName.Length > 10)
                    {
                        overName = fileName.Substring(0, 8) + "....." + fileExt;
                    }
                    else
                    {
                        overName = fileName + fileExt;
                    }
                    if (Regex.IsMatch(_file.Extension, "jpg", RegexOptions.IgnoreCase) ||
                        Regex.IsMatch(_file.Extension, "png", RegexOptions.IgnoreCase))             //여기서 extension 추가하기
                    {

                        imageListView_EditProject.Items.Add(_file.FullName);
                    }
                }
            }
        }

        //read
        private void ReadLink()
        {
            FileInfo Info = new FileInfo(myPath + "\\" + "link.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(myPath + "\\" + "link.xml");
                XmlNode nodeDevice = xmlDoc.DocumentElement.SelectSingleNode("/LinkTable");
                pData.SetDeviceType(nodeDevice.SelectSingleNode("DeviceInfo").InnerText);
                pData.SetDeviceResolution(nodeDevice.SelectSingleNode("DeviceResolution").InnerText);
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/LinkTable/Link");

                string pFile_name;
                int pTag, pImage_width, pImage_height;
                string pDst_file, pInput_type;
                Point pImage_xy;

                foreach (XmlNode child_node in nodeList)
                {
                    pFile_name = child_node.Attributes["fileName"].Value.ToString();
                    foreach (XmlNode grand_child_node in child_node)
                    {
                        pTag = Convert.ToInt32(grand_child_node.SelectSingleNode("Tag").InnerText);
                        pDst_file = grand_child_node.SelectSingleNode("DstFile").InnerText;
                        pInput_type = grand_child_node.SelectSingleNode("InputType").InnerText;

                        if (pInput_type.CompareTo("Single_Touch") == 0 || pInput_type.CompareTo("Long_Touch") == 0)
                        {
                            pImage_xy = new Point(Convert.ToInt32(grand_child_node.SelectSingleNode("LinkX").InnerText), Convert.ToInt32(grand_child_node.SelectSingleNode("LinkY").InnerText));
                            pImage_width = Convert.ToInt32(grand_child_node.SelectSingleNode("LinkWidth").InnerText);
                            pImage_height = Convert.ToInt32(grand_child_node.SelectSingleNode("LinkHeight").InnerText);
                        }
                        else
                        {
                            pImage_xy = new Point(-1, -1);
                            pImage_width = -1;
                            pImage_height = -1;
                        }
                        pData.SetLink(pFile_name, pTag, pDst_file, pImage_xy, pImage_width, pImage_height, pInput_type);
                    }
                }
            }
            else
            {
                //MessageBox.Show("no Info");
            }
            //링크정보가 없는 이미지에 대해서도 미리 리스트에 공간을 확보 해두기(첫 버튼 생성시 outofbound exception이 나기 때문)
            int check = 0;
            for (int i = 0; i < image_list.Count; i++)
            {
                check = 0;
                for(int j = 0; j < pData.GetLinks().Count; j++)
                {
                    //i번째의 이미지 이름이 링크정보가 만들어진 데이터에 존재하지 않는다면 추가하기
                    if(pData.GetLinks()[j].file_name.CompareTo(image_list[i]) == 0)
                    {
                        check = 1;
                        break;
                    }
                }
                if(check == 0)
                {
                    //추가하기
                    pData.GetLinks().Add(new LinkData.LINK() { file_name = image_list[i], link_data = new List<LinkData.link_info>() });
                }
            }
        }

        private void ReadScenario()
        {
            //Scenrio.xml 읽어와서 정보 읽어오기
            //데이터 저장
            FileInfo Info = new FileInfo(myPath + "\\" + "scenario.xml");
            if (Info.Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(myPath + "\\" + "scenario.xml");

                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/ScenarioTable/Scenario");

                string sTitle, sPurpose, sTime, sLevel;
                int sTag = 0;
                List<ScenarioData.PATH_DATA> sPath = new List<ScenarioData.PATH_DATA>();
                string pPath, pStayTime, pAutoChangeTime;

                foreach (XmlNode child_node in nodeList)
                {
                    sPath = new List<ScenarioData.PATH_DATA>();
                    sTag = Convert.ToInt32(child_node.SelectSingleNode("Tag").InnerText);
                    sTitle = child_node.SelectSingleNode("Title").InnerText;
                    sPurpose = child_node.SelectSingleNode("Purpose").InnerText;
                    sLevel = child_node.SelectSingleNode("Level").InnerText;

                    XmlNode new_node = child_node.SelectSingleNode("Path");

                    for(int j = 0; j < Convert.ToInt16(new_node.Attributes["count"].Value.ToString()); j++)
                    {
                        XmlNode temp = new_node.SelectSingleNode("Route" + (j+1).ToString());
                        pPath = temp.Attributes["img"].Value.ToString();

                        XmlNode stayTime = temp.SelectSingleNode("StayTime");
                        pStayTime = stayTime.InnerText;
                        XmlNode autoChangeTime = temp.SelectSingleNode("AutoChangeTime");
                        pAutoChangeTime = autoChangeTime.InnerText;

                        sPath.Add(new ScenarioData.PATH_DATA() { tag = j, path = pPath, stay_time = pStayTime, auto_change_time = pAutoChangeTime });
                    }
                    sTime = child_node.SelectSingleNode("TotalTime").InnerText;
                    Console.WriteLine("추가");
                    sData.AddScenario(sTag, sTitle, sPurpose, sTitle, sLevel, sPath);
                    
                    listBox_scenario.Items.Add(sTitle);

                    if (last_index > sTag)
                    {
                        last_index = sTag;
                    }
                }
            }
            else
            {
                //MessageBox.Show("no Info");
            }
        }


        private void btnAddImages_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string destFile = "";
            DialogResult dr = this.openFileDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string temp in openFileDialog.FileNames)
                {
                    fileName = System.IO.Path.GetFileName(temp);
                    destFile = System.IO.Path.Combine(myPath, fileName);

                    pData.GetLinks().Add(new LinkData.LINK() { file_name = fileName, link_data = new List<LinkData.link_info>() });

                    if (!System.IO.Directory.Exists(myPath))
                    {
                        System.IO.Directory.CreateDirectory(myPath);
                    }

                    try
                    {
                        System.IO.File.Copy(temp, destFile);
                    }catch(IOException ie)
                    {
                        MessageBox.Show("파일이름이 중복되거나 이미 파일이 있습니다.");
                    }
                    

                    imageListView_EditProject.Items.Add(destFile);

                    Image img;
                    using (var bmpTemp = new Bitmap(destFile))
                    {
                        img = new Bitmap(bmpTemp);
                    }

                    //Bitmap tempImage = new Bitmap(destFile);
                    using (Graphics gr = Graphics.FromImage(img))
                    {
                        gr.SmoothingMode = SmoothingMode.HighQuality;
                        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        gr.DrawImage(img, new Rectangle(0, 0, 64, 64));
                    }

                    string _fileName = fileName.Substring(0, fileName.LastIndexOf('.'));
                    string _fileExt = fileName.Substring(fileName.LastIndexOf('.'));
                    string _overName = String.Empty;

                    if (_fileName.Length > 10)
                    {
                        _overName = _fileName.Substring(0, 8) + "....." + _fileExt;
                    }
                    else
                    {
                        _overName = _fileName + _fileExt;
                    }
                }
            }
        }

        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:

                    Button temp_btn = sender as Button;

                    string mPath = myPath + "\\" + temp_btn.Name;

                    Edit_Image editImage = new Edit_Image(myPath, mPath, pData, sData, user_id, total_data);
                    editImage.TopLevel = false;
                    editImage.AutoScroll = true;
                    this.panel_content_right.Controls.Add(editImage);
                    editImage.FormBorderStyle = FormBorderStyle.None;
                    editImage.Location = new Point(0, 0);
                    editImage.Dock = DockStyle.Fill;
                    editImage.Show();
                    editImage.BringToFront();
                    break;

                case MouseButtons.Right:
                    Button temp_btn_right = sender as Button;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));
                    cm.MenuItems.Add("Item 2");
                    temp_btn_right.ContextMenu = cm;
                    TempDeleteButton = temp_btn_right;
                    break;
            }

        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {

            string mPath = myPath + TempDeleteButton.Name;

            //get control hovered with mouse
            //Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);

            //if it's a Button, remove it from the form

            if (File.Exists(mPath))
            {
                MessageBox.Show(mPath, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Delete(mPath);
            }

            /*if (Info.Exists)
            {
                foreach (FileInfo file in Info.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in Info.GetDirectories())
                {
                    dir.Delete(true);
                }
            }*/

            //Info.Delete();

            //this.flowLayoutPanel_home.Controls.Remove(TempDeleteButton);
        }

        private void Scenario_btn_Click(object sender, EventArgs e)
        {
            panel_scenario.Visible = true;
            panel_content.Visible = false;
            //flowLayoutPanel_home.Visible = false;
            panel_analysis.Visible = false;
            btnAddImages.Visible = false;
            btn_update.Visible = false;
            Scenario_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_scenario_on));

            Content_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_contents_off));
            Analysis_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_analysis_off));
            //시나리오 페이지 띄우기
            //Data.cs에 시나리오 부분 필요.
            //총 소요시간, 목적, 이름, 시작.png, 도착.png
        }

        private void Project_btn_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel_home.Visible = true;
            panel_scenario.Visible = false;
            panel_analysis.Visible = false;
            panel_content.Visible = true;
            btnAddImages.Visible = true;
            btn_update.Visible = true;
            Content_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_contents_on));

            Analysis_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_analysis_off));
            Scenario_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_scenario_off));

        }

        private void Analysis_btn_Click(object sender, EventArgs e)
        {
            panel_analysis.Location = new Point(0, panel_main_tapbar.Location.Y + panel_main_tapbar.Height);
            panel_analysis.Size = new Size(this.Width, this.Height - panel_main_tapbar.Height - panel_main_top.Height);

            panel_analysis.Visible = true;
            panel_content.Visible = false;
            //flowLayoutPanel_home.Visible = false;
            panel_scenario.Visible = false;
            btnAddImages.Visible = false;
            btn_update.Visible = false;

            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part_off));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph_off));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route_off));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey_off));

            Analysis_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_analysis_on));

            Scenario_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_scenario_off));
            Content_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._01_contents_off));

            string front = panel_analysis.Controls[0].Name;

            //원래 열려있던 창 다시 열기
            //현재 해당 창이 다시 열리긴 하는데 초기화 되어버려서 보던 자료가 뜨지 않음.
            if (front.CompareTo("Dots") == 0 || front.CompareTo("Partition") == 0 ||
                front.CompareTo("Route") == 0 || front.CompareTo("Survey") == 0)
            {
                panel_analysis.Controls[0].Show();
            }   
            else
            {
                Dots dots = new Dots(total_data, pData.GetProjectName(), sData);
                dots.TopLevel = false;
                dots.AutoScroll = true;
                this.panel_analysis_forms.Controls.Add(dots);
                dots.FormBorderStyle = FormBorderStyle.None;
                dots.Dock = DockStyle.Fill;
                dots.Location = new Point(0, 0);
                dots.Show();
                dots.BringToFront();
            }
        }

        private void EDIT_btn_Click(object sender, EventArgs e)
        {
            //리스트박스에서 선택된 항목 수정하기
            if(listBox_scenario.SelectedItem == null)
            {
                MessageBox.Show("please select an item");
            }
            else
            {
                Scenario scenario = new Scenario(1, myPath, sData, listBox_scenario.SelectedIndex, listBox_scenario);
                scenario.Show();
            }
        }

        private void NEW_btn_Click(object sender, EventArgs e)
        {
            //시나리오 등록하는 창 새로 띄워서 추가할 수 있도록
            Scenario scenario = new Scenario(0, myPath, sData, 0, listBox_scenario);
            scenario.Show();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox_scenario.SelectedItem != null)
            {
                //리스트박스에서 선택된 항목 보여주기 수정x
                Scenario scenario = new Scenario(2, myPath, sData, listBox_scenario.SelectedIndex, listBox_scenario);
                scenario.Show();
            }
            
        }

        private void SAVE_btn_Click(object sender, EventArgs e)
        {

            for (int a = 0; a < sData.getSData().Count; a++)
            {
                for (int b = 0; b < sData.getSData()[a].paths.Count; b++)
                {
                    Console.WriteLine(a.ToString() + " : " + sData.getSData()[a].paths[b].path);
                }
            }

            ////xml 만들기
            DirectoryInfo difo = new DirectoryInfo(myPath);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(myPath + "\\" + "scenario.xml", xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ScenarioTable");

                //xmlWriter.WriteStartElement("ProjectName");
                //xmlWriter.WriteString(pData.GetDeviceType());               //project name
                //xmlWriter.WriteEndElement();

                CreateNode(xmlWriter);

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();
                MessageBox.Show("create");

                //기존의 ZIP파일 지우고 다시 생성하기
                if (File.Exists(myPath + difo.Name + ".zip"))  //프로젝트 이름
                {
                    File.Delete(myPath + difo.Name + ".zip");
                }

                string[] filenames = Directory.GetFiles(myPath, "*.*");

                bool zipped = false;
                using (ZipFile zip = new ZipFile(myPath))
                {
                    zip.AddFiles(filenames, false, "");
                    zip.Save(string.Format("{0}{1}.zip", myPath, difo.Name));
                    zipped = true;
                }
            }
        }

        private void CreateNode(XmlWriter writer)
        {
            //editProject에서 받아온 정보(create_links)에서 전체 링크 정보를 저장, 파일명으로 링크 구분
            int i = 0, j = 0, totalTime = 0;
            //해당 인덱스에 링크정보가 하나도 없으면 저장하지 않는다.

            

            if (sData.getSData().Count != 0)
            {
                for (j = 0; j < sData.getSData().Count; j++)
                {
                    totalTime = 0;
                    writer.WriteStartElement("Scenario");

                    writer.WriteStartElement("Tag");
                    writer.WriteString(sData.getSData()[j].tag.ToString());
                    writer.WriteEndElement();
                    writer.WriteStartElement("Title");
                    writer.WriteString(sData.getSData()[j].title);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Purpose");
                    writer.WriteString(sData.getSData()[j].purpose);
                    writer.WriteEndElement();
                    writer.WriteStartElement("Level");
                    writer.WriteString(sData.getSData()[j].level);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Path");
                    writer.WriteStartAttribute("count");
                    writer.WriteString(sData.getSData()[j].paths.Count.ToString());

                    //경로 xml 저장하는 부분!
                    for (int k = 0; k < sData.getSData()[j].paths.Count; k++)
                    {
                        writer.WriteStartElement("Route" + (k+1).ToString());
                        writer.WriteStartAttribute("img");
                        writer.WriteString(sData.getSData()[j].paths[k].path);
                        writer.WriteEndAttribute();

                        writer.WriteStartElement("StayTime");
                        writer.WriteString(sData.getSData()[j].paths[k].stay_time);
                        writer.WriteEndElement();

                        writer.WriteStartElement("AutoChangeTime");
                        writer.WriteString(sData.getSData()[j].paths[k].auto_change_time);
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                        totalTime += Convert.ToInt16(sData.getSData()[j].paths[k].stay_time);
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("TotalTime");
                    writer.WriteString(totalTime.ToString());
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }
        }

        private void DEL_btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Deleted!");
            //시나리오 삭제할 때 인덱스값을.. 찾아서 지우기?
            sData.getSData().RemoveAt(listBox_scenario.SelectedIndex);
            this.listBox_scenario.Items.Remove(listBox_scenario.SelectedItem);
        }


        private void imageListView1_itemDoubleClick(object sender, ItemClickEventArgs e)
        {
            ImageListViewItem item = imageListView_EditProject.SelectedItems[0];

            //string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + item.FolderName + "\\";
            Edit_Image editImage = new Edit_Image(myPath, item.FileName, pData, sData, user_id, total_data);
            editImage.TopLevel = false;
            editImage.AutoScroll = true;
            this.panel_content_right.Controls.Add(editImage);
            editImage.FormBorderStyle = FormBorderStyle.None;
            editImage.Location = new Point(0, 0);
            editImage.Dock = DockStyle.Fill;
            editImage.Show();
            editImage.BringToFront();
            btn_editImage_back.Visible = true;
            btn_editImage_save.Visible = true;
            btnAddImages.Visible = false;


            //panel_editImage_forms.BringToFront();
        }

        private void imageListView1_itemClick(object sender, ItemClickEventArgs e)
        {
            if ((e.Buttons & MouseButtons.Right) != MouseButtons.None)
            {
                
                imageListView_EditProject.ContextMenu = cm;
            }
        }

        private void imageListView_menuItem_delete_click(object sender, EventArgs e)
        {
            ShowDialog_Delete();
        }

        public void ShowDialog_Delete()
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
            label_title.Text = "Project Delete";
            label_title.Size = new Size(300, 40);
            label_title.TextAlign = ContentAlignment.MiddleCenter;
            label_title.BringToFront();
            label_title.Location = new Point((int)(prompt.Width / 2) - (int)(label_title.Width / 2), 5);
            label_title.BackColor = Color.Transparent;

            label_title.Font = new Font(pfc.Families[0], 18, FontStyle.Bold);
            label_title.ForeColor = Color.White;
            title.Controls.Add(label_title);

            Label label_question = new Label();
            label_question.Text = "해당 이미지를 삭제하면 관련된 데이터는\n모두 삭제됩니다. 계속하시겠습니까?";
            label_question.TextAlign = ContentAlignment.MiddleCenter;
            label_question.BackColor = Color.Transparent;
            label_question.Size = new Size(500, 50);
            label_question.Location = new Point((int)(prompt.Width / 2) - (int)(label_question.Width / 2), 100);
            label_question.Font = new Font(pfc.Families[0], 17, FontStyle.Bold);
            label_question.ForeColor = Color.Black;

            Button delete = new Button();
            delete.Size = new Size(140, 47);
            delete.Location = new Point((int)(prompt.Width * 0.7) - (int)(delete.Width / 2), 180);
            delete.FlatStyle = FlatStyle.Flat;
            delete.BackColor = Color.Transparent;
            delete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._4_delete));
            delete.BackgroundImageLayout = ImageLayout.Stretch;

            delete.Click += (sender, e) => {
                foreach (ImageListViewItem item in imageListView_EditProject.SelectedItems)
                {
                    if (File.Exists(item.FileName))
                    {

                        //MessageBox.Show(item.FileName, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            File.Delete(item.FileName);
                        }catch(IOException ioe)
                        {

                        }
                        imageListView_EditProject.Items.Remove(item);

                    }
                }
                prompt.Close();
            };

            Button cancel = new Button();
            cancel.Size = new Size(140, 47);
            cancel.Location = new Point((int)(prompt.Width * 0.3) - (int)(cancel.Width / 2), 180);
            cancel.FlatStyle = FlatStyle.Flat;
            cancel.BackColor = Color.Transparent;
            cancel.ForeColor = Color.Black;
            cancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._2_cancel));
            cancel.BackgroundImageLayout = ImageLayout.Stretch;

            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(panel_dialog);
            panel_dialog.Controls.Add(title);
            panel_dialog.Controls.Add(cancel);
            panel_dialog.Controls.Add(delete);
            panel_dialog.Controls.Add(label_question);
            prompt.ShowDialog();

            //drag and drop.. 
        }

        

        private void EditProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_main.refresh();
        }

        private void Analysis_Setup()
        {
            //처음 눌렀을 때 초기화 해주고 그 다음부터는 바로 보여주는 곳으로.

            total_data = new List<TotalData>();
            pictures = new List<PictureBox>();
            count = new List<int>();
            under_bar_index = new List<int>();
            route_data = new List<RouteData>();
            survey_data = new List<SurveyData>();
            sName = new List<string>();

            for (int i = 0; i < listBox_scenario.Items.Count; i++)
            {
                Console.WriteLine(listBox_scenario.Items[i].ToString());
                sName.Add(listBox_scenario.Items[i].ToString());
            }

            string[] download_filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\" + Environment.UserName + "\\Nudge");

            if (di.Exists == false)
            {
                di.Create();
            }

            if (pData.GetDownload() == true)
            {
                //파일 다운로스 시작
                FileDownloader fd = new FileDownloader(download_filenames, sData, pData.GetProjectName(), user_id);
            }

            XmlRead();        //다운로드 되어있는 Xml 읽어와서 데이터 저장
        }

        private void XmlRead()
        {
            //Xml파일들이 저장되는 폴더 경로
            string mPath = @"C:\Users\" + Environment.UserName + "\\Nudge";
            DirectoryInfo dInfo = new DirectoryInfo(mPath);
            List<ResultData.ImgTime> riData = new List<ResultData.ImgTime>();

            string div_temp;
            DateTime date_time;

            if (dInfo.Exists)
            {
                //다운로드 되고 나서 다시 xml 파일 정보 불러오기
                filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");

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
                    date_time = File.GetCreationTime(mPath + "\\" + filenames[i]);

                    //시나리오 추출 &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                    under_bar_index.Clear();

                    riData = new List<ResultData.ImgTime>();

                    //xml 이름에서 underbar 위치 찾아내서 각 정보 불러올 때 사용
                    for (int k = 0; k < filenames[i].Length; k++)
                    {
                        if (filenames[i][k].CompareTo('_') == 0)
                        {
                            under_bar_index.Add(k);
                        }
                    }

                    //peer // user 구분
                    string temp_scenario_tag = filenames[i].Substring(0, under_bar_index[0]);
                    string temp_scenario_name = filenames[i].Substring(under_bar_index[1] + 1, (under_bar_index[2] - under_bar_index[1]) - 1);
                    string temp_device_id = filenames[i].Substring(under_bar_index[2] + 1, (under_bar_index[3] - under_bar_index[2]) - 1);
                    string temp_project_name = filenames[i].Substring(under_bar_index[0] + 1, (under_bar_index[1] - under_bar_index[0]) - 1);
                    string temp_test_type = filenames[i].Substring(under_bar_index[3] + 1, (under_bar_index[4] - under_bar_index[3]) - 1);

                    

                    //Console.WriteLine("시작");
                    //Console.WriteLine(temp_scenario_tag);
                    //Console.WriteLine(temp_scenario_name);
                    //Console.WriteLine(temp_device_id);
                    //Console.WriteLine(temp_project_name);
                    //Console.WriteLine(temp_test_type);
                    if (temp_test_type.CompareTo("userData") == 0)
                    {
                        bool scenario_add = false;

                        for (int k = 0; k < route_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (route_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && route_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && route_data[k].device_id.CompareTo(temp_device_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }
                        Console.WriteLine("add" + scenario_add.ToString());
                        Console.WriteLine(pData.GetProjectName());
                        Console.WriteLine(scenario_name);
                        //위에서 중복된 정보가 아니더라도 tag, scenario_name, project_name 중 하나라도 다르면 입력 x
                        if (scenario_add == false && temp_project_name.CompareTo(pData.GetProjectName()) == 0)
                        {
                            for (int p = 0; p < sData.getSData().Count; p++)
                            {
                                if(temp_scenario_name.CompareTo(sData.getSData()[p].title) == 0)
                                {
                                    route_data.Add(new RouteData() { tag = Convert.ToInt32(temp_scenario_tag), creation = date_time, scenario_name = temp_scenario_name, device_id = temp_device_id, images = new List<string>(), visit_time = new List<double>() });
                                    break;
                                }
                            }
                        }
                        else continue;

                        if (sData.getSData().Count == 0) continue;
                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        int x_cor = 0, y_cor = 0;
                        double pTimeEntire = 0, pTimeImg = 0;
                        string pEvent = string.Empty, pImg = string.Empty;
                        bool data_in = false;

                        double real1 = 0, real2 = 0;

                        xmlDoc.Load(mPath + "\\" + filenames[i]);
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
                                    total_data[j].event_data.Add(new EventData() { test_num = Convert.ToInt16(temp_scenario_tag), xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
                                    data_in = true;
                                    break;
                                }
                            }

                            //total data안에 없으면 새로운 정보 입력
                            if (data_in == false)
                            {
                                total_data.Add(new TotalData() { image_name = pImg, scenario_name = temp_scenario_name, event_data = new List<EventData>() });
                                total_data[total_data.Count - 1].event_data.Add(new EventData() { xcoord = x_cor, ycoord = y_cor, event_info = pEvent, img = pImg, timeEntire = pTimeEntire, timeImg = pTimeImg });
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
                                //MessageBox.Show(pImg + "//" + route_data.Count.ToString());
                                Console.WriteLine("초기값 입력");
                                route_data[route_data.Count-1].images.Add(pImg); //여기 오류 왜 나는지 체크
                            }
                            continue;
                        }
                        //마지막은 더 이상 이미지의 전환이 없기 때문에 해당 이미지에 머문 시간을 입력한다.
                        route_data[route_data.Count - 1].visit_time.Add(pTimeImg);

                        Console.WriteLine("count : " + route_data[route_data.Count - 1].images.Count.ToString());
                        for (int k = 0; k < route_data[route_data.Count - 1].images.Count; k++)
                        {
                            
                            riData.Add(new ResultData.ImgTime() { imgName = route_data[route_data.Count - 1].images[k], timeImg = route_data[route_data.Count - 1].visit_time[k].ToString() });
                        }

                        bool isMin = false;
                        for(int j = 0; j < sName.Count; j++)
                        {
                            Console.WriteLine("비교1 : " + sName[j] + "==" + temp_scenario_name);
                            Console.WriteLine("비교2 : " + sData.getSData()[j].paths.Count.ToString() + "==" + riData.Count.ToString());
                            if (sName[j].CompareTo(temp_scenario_name) == 0 && sData.getSData()[j].paths.Count == riData.Count)
                            {
                                isMin = true;
                                for(int h = 0; h < riData.Count; h++)
                                {
                                    Console.WriteLine("비교3 : " + riData[h].imgName + "==" + sData.getSData()[j].paths[h].path);
                                    if(riData[h].imgName.CompareTo(sData.getSData()[j].paths[h].path) == 0)
                                    {

                                    }
                                    else
                                    {
                                        isMin = false;
                                        break;
                                    }
                                }
                                if (isMin == false) break;
                            }
                        }

                        Console.WriteLine("입력한 것");
                        Console.WriteLine(temp_scenario_name);
                        rData.AddResultInfo(pData.GetProjectName(), temp_scenario_name, riData, isMin, Convert.ToInt16(temp_scenario_tag));

                    }

                    //설문조사 데이터에 대한 것
                    else
                    {
                        bool scenario_add = false, scenario_empty = true;

                        for (int k = 0; k < survey_data.Count; k++)
                        {
                            //번호, 시나리오 이름, 디바이스 아이디 같으면 이미 존재 add = true; 
                            if (survey_data[k].tag == Convert.ToInt32(temp_scenario_tag)
                                && survey_data[k].scenario_name.CompareTo(temp_scenario_name) == 0
                                && survey_data[k].device_id.CompareTo(temp_device_id) == 0)
                            {
                                scenario_add = true;
                                break;
                            }
                        }

                        if (scenario_add == false && temp_project_name.CompareTo(pData.GetProjectName()) == 0)
                        {
                            for (int p = 0; p < sData.getSData().Count; p++)
                            {
                                if (temp_scenario_name.CompareTo(sData.getSData()[p].title) == 0)
                                {
                                    survey_data.Add(new SurveyData() { tag = Convert.ToInt32(temp_scenario_tag), creation = date_time, scenario_name = temp_scenario_name, device_id = temp_device_id, survey_info = new List<SurveyInternalInfo>() });
                                    break;
                                }
                            }
                        }
                        else continue;
                        if (sData.getSData().Count == 0) continue;

                        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                        XmlDocument xmlDoc = new XmlDocument();
                        string temp_question_type;
                        string temp_question, temp_answer;
                        string temp_beforeImg, temp_afterImg;

                        xmlDoc.Load(mPath + "\\" + filenames[i]);
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

            for (int p = 0; p < rData.getRData().Count; p++)
            {
                Console.WriteLine("projectName : " + rData.getRData()[p].projectName);
                Console.WriteLine("taskName : " + rData.getRData()[p].taskName);
                Console.WriteLine("isMin : " + rData.getRData()[p].isMin);
                Console.WriteLine("idx : " + rData.getRData()[p].idx);
                for(int u = 0; u < rData.getRData()[p].pathInfo.Count; u++)
                {
                    Console.WriteLine("imgName : " + rData.getRData()[p].pathInfo[u].imgName);
                    Console.WriteLine("timeImg : " + rData.getRData()[p].pathInfo[u].timeImg);
                }
            }
        }

        private void Dots_btn_Click(object sender, EventArgs e)
        {
            Dots dots = new Dots(total_data, pData.GetProjectName(), sData);
            dots.TopLevel = false;
            dots.AutoScroll = true;
            this.panel_analysis_forms.Controls.Add(dots);
            dots.FormBorderStyle = FormBorderStyle.None;
            dots.Location = new Point(0, 0);
            dots.Dock = DockStyle.Fill;
            dots.Show();
            dots.BringToFront();

            //버튼 이미지 변경
            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part_off));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph_off));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route_off));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey_off));
        }
        
        private void Partition_btn_Click(object sender, EventArgs e)
        {
            Partition partition = new Partition(total_data, pData.GetProjectName(), sData);
            partition.TopLevel = false;
            partition.AutoScroll = true;
            this.panel_analysis_forms.Controls.Add(partition);
            partition.FormBorderStyle = FormBorderStyle.None;
            partition.Dock = DockStyle.Fill;
            partition.Location = new Point(0, 0);
            partition.Show();
            partition.BringToFront();

            //버튼 이미지 변경
            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot_off));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph_off));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route_off));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey_off));
        }

        private void Graph_btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(rData.getRData().Count.ToString());

            //Graph graph = new Graph(rData,);
            Graph graph = new Graph(rData, pData.GetProjectName(), sName);
            graph.TopLevel = false;
            graph.AutoScroll = true;
            this.panel_analysis_forms.Controls.Add(graph);
            graph.FormBorderStyle = FormBorderStyle.None;
            graph.Location = new Point(0, 0);
            graph.Dock = DockStyle.Fill;
            graph.Show();
            graph.BringToFront();

            //버튼 이미지 변경
            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot_off));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part_off));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route_off));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey_off));
        }

        private void Route_btn_Click(object sender, EventArgs e)
        {
            Route route = new Route(route_data, pData.GetProjectName(), sData);
            route.TopLevel = false;
            route.AutoScroll = true;
            this.panel_analysis_forms.Controls.Add(route);
            route.FormBorderStyle = FormBorderStyle.None;
            route.Location = new Point(0, 0);
            route.Dock = DockStyle.Fill;
            route.Show();
            route.BringToFront();

            //버튼 이미지 변경
            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot_off));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part_off));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph_off));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey_off));
        }

        private void Survey_btn_Click(object sender, EventArgs e)
        {
            Survey survey = new Survey(survey_data, pData.GetProjectName(), sData);
            survey.TopLevel = false;
            survey.AutoScroll = true;
            this.panel_analysis_forms.Controls.Add(survey);
            survey.FormBorderStyle = FormBorderStyle.None;
            survey.Location = new Point(0, 0);
            survey.Dock = DockStyle.Fill;
            survey.Show();
            survey.BringToFront();

            //버튼 이미지 변경
            Dots_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_dot_off));
            Partition_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_part_off));
            Graph_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_graph_off));
            Route_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_route_off));
            Survey_btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._8_1_survey));
        }

        private void btn_analysis_dot_delete_Click(object sender, EventArgs e)
        {
            //string dPath = @"C:\Users\" + Environment.UserName + "\\Nudge\\" + delete_file;

            //if (File.Exists(dPath))
            //{
            //    //데이터를 삭제하시면 복구가 불가능합니다. 그래도 진행하시겠습니까? 메시지 띄우기
            //    MessageBox.Show("삭제 : " + delete_file);

            //    File.Delete(dPath);

            //    //삭제하기전에 RouteData 수정하기
            //}


            //Delete delete = new Delete(route_data, survey_data, filenames, pData.GetProjectName(),
            //    cb_analysis_route_selectGroup, cb_analysis_route_selectTest, cb_analysis_survey_selectGroup, cb_analysis_survey_selectTest,
            //    cb_analysis_dots_selectGroup, cb_analysis_dots_selectTest, cb_analysis_dots_selectImage,
            //    cb_analysis_partition_selectGroup, cb_analysis_partition_selectTest, cb_analysis_partition_selectImage);
            //delete.Show();
        }


        public void SetSurveyData(List<SurveyData> _survey_data)
        {
            this.survey_data = _survey_data;
            //cb_analysis_route_selectTest.Items.Clear();

            //for(int i = 0; i < route_data.Count; i++)
            //{
            //    //cb를 파라미터로 보내서 수정해야하나.. 
            //    cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
            //    MessageBox.Show(route_data[i].tag.ToString());
            //}
            //MessageBox.Show("완료");
        }

        private void EditProject_SizeChanged(object sender, EventArgs e)
        {
            panel_main_tapbar.Width = this.Width;

            //content화면 사이즈 조정
            
            panel_content.Width = this.Width;
            panel_content.Height = this.Height - 123;
            panel_content_left.Height = panel_content.Height;
            panel_content_top.Width = panel_content.Width - panel_content_left.Width;
            btnAddImages.Location = new Point(panel_content_top.Width - 200, btnAddImages.Location.Y);
            btn_update.Location = new Point(panel_main_tapbar.Width - 200, btn_update.Location.Y);
            btn_editImage_save.Location = new Point(panel_content_top.Width - 200, btn_editImage_save.Location.Y);
            btn_editImage_back.Location = new Point(btnAddImages.Location.X - 20 - btn_editImage_back.Width, btn_editImage_back.Location.Y);
            panel_content_left_info.Location = new Point(2, this.Height - panel_content_left_info.Height - 180);

            //scenario화면 사이즈 조정
            panel_scenario.Size = panel_content.Size;
            panel_scenario_left.Height = panel_scenario.Height;
            panel_scenario_top.Width = panel_scenario.Width - panel_scenario_left.Width;

            panel_scenario_box.Size = new Size(panel_scenario_top.Width - 500, panel_scenario.Height - panel_scenario_top.Height - 200);
            panel_scenario_box_left.Size = new Size((int)(panel_scenario_box.Width / 2), panel_scenario_box.Height);

            listBox_scenario.Location = new Point(0, 50);
            listBox_scenario.Height = panel_scenario_box.Height - panel_scenario_left_title.Height;
            panel_scenario_info.Location = new Point(0, 50);
            panel_scenario_info.Height = panel_scenario_box.Height - panel_scenario_left_title.Height;
            label_scenario_left_title.Location = new Point((int)(panel_scenario_box_left.Width / 2 - label_scenario_left_title.Width / 2), label_scenario_left_title.Location.Y);
            label_scenario_right_title.Location = new Point((int)(panel_scenario_box_right.Width / 2 - label_scenario_right_title.Width / 2), label_scenario_right_title.Location.Y);

            SAVE_btn.Location = new Point(panel_scenario_top.Width - 200, btnAddImages.Location.Y); //이거 나중에 바꾸기
            NEW_btn.Location = new Point(SAVE_btn.Location.X + panel_scenario_left.Width, panel_scenario_box.Location.Y);
            EDIT_btn.Location = new Point(SAVE_btn.Location.X + panel_scenario_left.Width, NEW_btn.Location.Y + NEW_btn.Height + 20);
            DEL_btn.Location = new Point(SAVE_btn.Location.X + panel_scenario_left.Width, EDIT_btn.Location.Y + EDIT_btn.Height + 20);

            panel_scenario_left_info.Location = new Point(2, this.Height - panel_scenario_left_info.Height - 180);
            panel_analysis_left_info.Location = new Point(2, this.Height - panel_analysis_left_info.Height - 180);
            //panel_editImage_left_info.Location = new Point(2, this.Height - panel_editImage_left_info.Height - 180);
            //scenario화면 사이즈 조정 끝

            //panel_editImage.Size = panel_content.Size;



            imageListView_EditProject.Location = new Point(175, 117);
            imageListView_EditProject.Width = panel_content_top.Width;
            imageListView_EditProject.Height = panel_content.Height - panel_content_top.Height;

            label_ID.Text = user_id + "님, 반갑습니다.";
            label_ID.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);

            label_ID.Location = new Point(this.Width - (label_ID.Width + 72), label_ID.Location.Y);
        }

        private void Home_btn_Click(object sender, EventArgs e)
        {
            //저장할 지 안 할지 정하고, 홈 화면으로 이동.
            this.Close();
        }

        private void listBox_scenario_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;

            if (e.Index < 0) return;

            //e = new DrawItemEventArgs(e.Graphics,
            //                            e.Font,
            //                            e.Bounds,
            //                            e.Index,
            //                            e.State ^ DrawItemState.Default,
            //                            e.ForeColor,
            //                            Color.White);//Choose the color

            //if the item state is selected them change the back color
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.FromArgb(204, 204, 204));//Choose the color

            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Draw the current item text
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));
            object item = list.Items[e.Index];
            SizeF size = e.Graphics.MeasureString(item.ToString(), e.Font);
            e.Graphics.DrawString(listBox_scenario.Items[e.Index].ToString(), new Font(pfc.Families[0], 15, FontStyle.Regular), Brushes.Black, e.Bounds.Left + 10, e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2), StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void listBox_scenario_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void BACK_btn_Click(object sender, EventArgs e)
        {
            btn_editImage_back.Visible = false;
            btn_editImage_save.Visible = false;
            panel_editImage.Visible = false;
            btnAddImages.Visible = true;
            imageListView_EditProject.BringToFront();
        }

        private void btn_editImage_save_Click(object sender, EventArgs e)
        {
            ShowDialog_save();
        }

        public void Save()
        {
            string mPath = myPath;

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
                //MessageBox.Show("create");


                //기존의 ZIP파일 지우고 다시 생성하기
                if (File.Exists(mPath + difo.Name + ".zip"))
                {
                    File.Delete(mPath + difo.Name + ".zip");
                }

                string[] filenames = Directory.GetFiles(mPath, "*.*");

                bool zipped = false;
                using (ZipFile zip = new ZipFile(mPath))
                {
                    zip.AddFiles(filenames, false, "");
                    zip.Save(string.Format("{0}{1}.zip", mPath, difo.Name));
                    zipped = true;
                }
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
            label_question.Text = "변경내용을 저장하시겠습니까?";
            label_question.TextAlign = ContentAlignment.MiddleCenter;
            label_question.BackColor = Color.Transparent;
            label_question.Size = new Size(500, 50);
            label_question.Location = new Point((int)(prompt.Width / 2) - (int)(label_question.Width / 2), 100);
            label_question.Font = new Font(pfc.Families[0], 17, FontStyle.Bold);
            label_question.ForeColor = Color.Black;

            Button confirm = new Button();
            confirm.Size = new Size(140, 47);
            confirm.Location = new Point((int)(prompt.Width * 0.7) - (int)(confirm.Width / 2), 180);
            confirm.FlatStyle = FlatStyle.Flat;
            confirm.BackColor = Color.Transparent;
            confirm.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._3_ok));
            confirm.BackgroundImageLayout = ImageLayout.Stretch;

            confirm.Click += (sender, e) => {
                Save();
                prompt.Close();
            };

            Button cancel = new Button();
            cancel.Size = new Size(140, 47);
            cancel.Location = new Point((int)(prompt.Width * 0.3) - (int)(cancel.Width / 2), 180);
            cancel.FlatStyle = FlatStyle.Flat;
            cancel.BackColor = Color.Transparent;
            cancel.ForeColor = Color.Black;
            cancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._2_cancel));
            cancel.BackgroundImageLayout = ImageLayout.Stretch;

            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(panel_dialog);
            panel_dialog.Controls.Add(title);
            panel_dialog.Controls.Add(cancel);
            panel_dialog.Controls.Add(confirm);
            panel_dialog.Controls.Add(label_question);
            prompt.ShowDialog();
        }

        private void panel_scenario_VisibleChanged(object sender, EventArgs e)
        {
            //scanario change count가 1이면 저장 팝업
            if(sData.changed == 1)
            {
                //MessageBox.Show("변경 내용을 저장하시겠습니까?");
                sData.changed = 0;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string[] download_filenames = Directory.GetFiles(@"C:\Users\" + Environment.UserName + "\\Nudge", "*.xml");
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\" + Environment.UserName + "\\Nudge");

            if (di.Exists == false)
            {
                di.Create();
            }

            if (pData.GetDownload() == true)
            {
                //파일 다운로스 시작
                FileDownloader fd = new FileDownloader(download_filenames, sData, pData.GetProjectName(), user_id);
            }

            route_data = new List<RouteData>();
            survey_data = new List<SurveyData>();
            total_data = new List<TotalData>();

            XmlRead();        //다운로드 되어있는 Xml 읽어와서 데이터 저장
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
        //survey//

        

        public void SetRouteData(List<RouteData> _route_data)
        {
            this.route_data = _route_data;
            //cb_analysis_route_selectTest.Items.Clear();

            //for(int i = 0; i < route_data.Count; i++)
            //{
            //    //cb를 파라미터로 보내서 수정해야하나.. 
            //    cb_analysis_route_selectTest.Items.Add(route_data[i].tag);
            //    MessageBox.Show(route_data[i].tag.ToString());
            //}
            //MessageBox.Show("완료");
        }



        public List<RouteData> GetRouteData()
        {
            return this.route_data;
        }
        
    }
}
