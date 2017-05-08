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
using System.Xml.Linq;
using System.Xml;
using System.Drawing.Text;

namespace DropBox
{
    public partial class CreateNewProject : Form
    {
        string user_id;
        PrivateFontCollection pfc = new PrivateFontCollection();

        public CreateNewProject(main temp, string _id)
        {
            InitializeComponent();
            textBoxProjectName_SetText();
            form1 = temp;
            user_id = _id;

            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));
            label_projectname.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_size.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_newProject_title.Font = new Font(pfc.Families[0], 18, FontStyle.Bold);
            label_newProject_title.Location = new Point((int)(this.Width / 2) - (int)(label_newProject_title.Width / 2), label_newProject_title.Location.Y);
            comboBox_size.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            textBox_projectname.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
        }

        protected void textBoxProjectName_SetText()
        {
            this.textBox_projectname.Text = "  Your project name";
            textBox_projectname.ForeColor = Color.Gray;
        }

        private void textBoxProjectName_Enter(object sender, EventArgs e)
        {
            if (textBox_projectname.ForeColor == Color.Black)
                return;
            textBox_projectname.Text = "";
            textBox_projectname.ForeColor = Color.Black;
        }

        private void textBoxProjectName_Leave(object sender, EventArgs e)
        {
            if (textBox_projectname.Text.Trim() == "")
                textBoxProjectName_SetText();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox_size.SelectedIndex < 0)
            {
                comboBox_size.Text = "What device are you prototyping for?";
            }
            else
            {
                comboBox_size.Text = comboBox_size.SelectedText;
            }
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (textBox_projectname.Text.Contains("_"))
            {
                MessageBox.Show("제목에 '_'를 포함할 수 없습니다.");
            }
            else if(textBox_projectname.Text.Contains("Your project name"))
            {
                MessageBox.Show("제목을 입력하세요.");
            }
            else if(comboBox_size.Text.CompareTo("") ==0)
            {
                MessageBox.Show("사이즈를 선택하세요");
            }
            else
            {
                //string mPath = @"C:\Prototype\project\" + textBoxProjectName.Text + "\\";
                string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + textBox_projectname.Text + "\\";
                DirectoryInfo di = new DirectoryInfo(mPath);

                if (di.Exists == false)
                {
                    di.Create();
                    //프로젝트를 만들면서 디바이스에 대한 정보만 가지고 있는 xml을 만든다.

                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(mPath + "\\" + "link.xml", xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("LinkTable");

                        xmlWriter.WriteStartElement("UserId");
                        xmlWriter.WriteString(user_id);       //재정의된 device info
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("DeviceInfo");
                        xmlWriter.WriteString(GetDeviceName(comboBox_size.Text));       //재정의된 device info
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteStartElement("DeviceResolution");
                        xmlWriter.WriteString(comboBox_size.Text);                      //ex) iPhone5 (640 x 1280)
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                        //MessageBox.Show("create");
                    }
                }
                else
                {
                    MessageBox.Show("A project with this name already exists", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //Image mImage = Image.FromFile((string)di.GetFiles()[0].Name);

                //@@@@ imagelistview
                //System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CreateNewProject));
                //(System.Drawing.Image)(resources.GetObject("jiyong"));

                using (Stream resource = GetType().Assembly.GetManifestResourceStream("DropBox.Resources.DoNotDelete.bmp"))
                {
                    if (resource == null)
                    {
                        throw new ArgumentException("No such resource", "resourceName");
                    }
                    using (Stream output = File.OpenWrite(mPath + "\\DoNotDelete.bmp"))
                    {
                        resource.CopyTo(output);
                    }
                    //File.Copy(DropBox.Properties.Resources.jiyong.bmp, mPath);

                    File.SetAttributes(mPath + "\\DoNotDelete.bmp", FileAttributes.Hidden);
                }
                form1.imageListView_Main.Items.Add(mPath + "\\DoNotDelete.bmp", textBox_projectname.Text, comboBox_size.Text);
                form1.imageListView_Main.ThumbnailSize = new Size(120, 120);

                /*string pResolution;
                Button newButton = new Button();
                newButton.Name = textBoxProjectName.Text;
                pResolution = comboBox1.Text.Replace(" (", System.Environment.NewLine + "(");
                newButton.Text = textBoxProjectName.Text + "\n" + pResolution;
                newButton.TextAlign = ContentAlignment.BottomCenter;
                newButton.Size = new Size(128, 128);
                newButton.Margin = new Padding(10, 10, 10, 10);
                newButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.eachButton_Click);

                form1.flowLayoutPanel1.Controls.Add(newButton);*/
                this.Dispose();
            }
        }

        //새로 만든 프로젝트를 눌렀을 때
        private void eachButton_Click(object sender, MouseEventArgs e)
        {
            //MouseEventArgs me = (MouseEventArgs)e;
            

            switch (e.Button)
            {
                case MouseButtons.Left:

                    // 여기에 정의
                    // 이 주소를 통하여서 안에 있는 그림 파일 혹은 기타 파일 가져 오기
                    Button temp_btn_left = sender as Button;

                    string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + temp_btn_left.Name + "\\";
                    //string mPath = @"C:\Prototype\\project\\" + temp_btn.Name + "\\";

                    //MessageBox.Show(temp, "Warning",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    EditProject editProject = new EditProject(mPath, user_id, temp_btn_left.Name);
                    editProject.Show();
                    //MessageBox.Show("Left click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case MouseButtons.Right:

                    //MessageBox.Show("Right click", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Button temp_btn_right = sender as Button;
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Delete", new System.EventHandler(this.menuItem_delete_click));
                    cm.MenuItems.Add("Item 2");
                    temp_btn_right.ContextMenu = cm;
                    main.TempDeleteButton = temp_btn_right;

                    break;
            }

        }

        //device 리스트를 만들어서 체크해서 넘기는 것
        private string getDevice(string temp)
        {
            string[] device_list = {"iPhone4", "iPhone5", "iPhone6", "GalaxyS4", "GalaxyS5"};
            string device = string.Empty;
            
            for(int i = 0; i < device_list.Length; i++)
            {
                if(temp.Contains(device_list[i]) == true)
                {
                    device = device_list[i];
                    break;
                }
            }

            return device;
        }

        private void menuItem_delete_click(object sender, EventArgs e)
        {
            /*string mPath = @"C:\Users\" + Environment.UserName + "\\Dropbox\\IMAGE\\" + main.TempDeleteButton.Name + "\\";

            //get control hovered with mouse
            //Button buttonToRemove = (this.GetChildAtPoint(this.PointToClient(Cursor.Position)) as Button);
            MessageBox.Show(main.TempDeleteButton.Text, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if it's a Button, remove it from the form

            DirectoryInfo Info = new DirectoryInfo(mPath);

            //Info.Delete(true);

            form1.flowLayoutPanel1.Controls.Remove(main.TempDeleteButton);

            //$$$$$$*/
        }

        private string GetDeviceName(string user_selection)
        {
            if (user_selection.CompareTo("iPhone4 (640 x 960)") == 0)
            {
                return "iphone4";//거의 안 씀
            }
            else if (user_selection.CompareTo("iPhone5 (640 x 1136)") == 0)
            {
                return "iphone5";//
            }
            else if(user_selection.CompareTo("iPhone6 (750 x 1334)") == 0)
            {
                return "iphone6";//
            }
            else if (user_selection.CompareTo("iPhone6S (750 x 1334)") == 0)
            {
                return "iphone6s";//
            }
            else if (user_selection.CompareTo("iPhone6 + (1080 x 1920)") == 0)
            {
                return "iphone6p";//
            }
            else if (user_selection.CompareTo("iPhone6S+ (1080 x 1920)") == 0)
            {
                return "iphone6sp";//
            }
            else if (user_selection.CompareTo("GalaxyS2 (480 x 800)") == 0)
            {
                return "galaxyS2";//거의 안 씀
            }
            else if (user_selection.CompareTo("GalaxyS2_HD (720 x 1280)") == 0)
            {
                return "galaxyS2HD";//
            }

            else if (user_selection.CompareTo("GalaxyS3 (720 x 1280)") == 0)
            {
                return "galaxyS3";//
            }
            else if (user_selection.CompareTo("GalaxyS4 (1080 x 1920)") == 0)
            {
                return "galaxyS4";//
            }
            else if (user_selection.CompareTo("GalaxyS5 (1080 x 1920)") == 0)
            {
                return "galaxyS5";//
            }
            else if (user_selection.CompareTo("GalaxyS6 (1440 x 2560)") == 0)
            {
                return "galaxyS6";//
            }
            else if (user_selection.CompareTo("GalaxyNote1 (800 x 1280)") == 0)
            {
                return "note1";//거의 안 씀
            }
            else if (user_selection.CompareTo("GalaxyNote2 (720 x 1280)") == 0)
            {
                return "note2";//
            }
            else if (user_selection.CompareTo("GalaxyNote3 (1080 x 1920)") == 0)
            {
                return "note3";//
            }
            else if (user_selection.CompareTo("GalaxyNote4 (1440 x 2560)") == 0)
            {
                return "note4";//
            }
            else if (user_selection.CompareTo("GalaxyNote5 (1440 x 2560)") == 0)
            {
                return "note5";//
            }
            else if (user_selection.CompareTo("GalaxyTabS (1600 x 2560)") == 0)
            {
                return "tabS";//거의 안 씀
            }
            else if (user_selection.CompareTo("OptimusG_Pro (1080 x 1920)") == 0)
            {
                return "gPro";//
            }
            else if (user_selection.CompareTo("G2 (1080 x 1920)") == 0)
            {
                return "g2";//
            }
            else if (user_selection.CompareTo("G3 (1440 x 2560)") == 0)
            {
                return "g3";//
            }
            else if (user_selection.CompareTo("G4 (1440 x 2560)") == 0)
            {
                return "g4";//
            }
            else if (user_selection.CompareTo("GalaxyNexus (720 x 1280)") == 0)
            {
                return "galaxyNexus";//거의 안 씀
            }
            else if (user_selection.CompareTo("Nexus4 (768 x 1280)") == 0)
            {
                return "nexus4";//거의 안 씀
            }
            else if (user_selection.CompareTo("Nexus5(1080 x 1920)") == 0)
            {
                return "nexus5";//거의 안 씀
            }
            else if (user_selection.CompareTo("Nexus7(2013) (1200 x 1920)") == 0)
            {
                return "nexus7_2013";//거의 안 씀
            }
            else if (user_selection.CompareTo("Nexus7 (800 x 1280)") == 0)
            {
                return "nexus7";//거의 안 씀
            }
            else if (user_selection.CompareTo("Nexus10 (1600 x 2560)") == 0)
            {
                return "nexus10";//거의 안 씀
            }

            return "undefined";
        }

        main form1;

        private void button_create_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
