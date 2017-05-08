using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nevron;
using Nevron.Dom;
using Nevron.GraphicsCore;
using Nevron.Editors;
using Nevron.Chart;
using Nevron.Chart.Functions;
using Nevron.Chart.Windows;
using Nevron.Chart.WinForm;
using System.Drawing.Text;
using System.IO;

namespace DropBox
{
    public partial class Graph : Form
    {
        private ResultData rData;

        private NChart m_Chart;
        private NBarSeries m_Bar;
        private NLineSeries m_Line;
        private NFunctionCalculator m_FuncCalculator;

        private NHighLowSeries m_HighLow;

        private int curChartType;
        private String curProjectName;


        private List<ImgTime> ImgTimeInfo;

        public class ImgTime
        {
            public string imgName;
            public float Low { get; set; }
            public float High { get; set; }
        }

        private List<ImgAverageTime> ImgAverageInfo;

        public class ImgAverageTime
        {
            public string imgName;
            public float imgTime { get; set; }
            public int count { get; set; }
        }

        PrivateFontCollection pfc = new PrivateFontCollection();


        public Graph(ResultData _rData, string projectName, List<string> scenarioNames)
        {
            NLicense license = new NLicense("096ff936-7602-3c02-009c-5ea6176b006d");
            NLicenseManager.Instance.SetLicense(license);
            NLicenseManager.Instance.LockLicense = true;

            InitializeComponent();
            rData = _rData;
            curProjectName = projectName;

            //font and location of left bar
            pfc.AddFontFile(Path.Combine(Application.StartupPath, "KOPUBDOTUM_PRO_LIGHT.OTF"));
            label_1.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_2.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            label_3.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);
            chartType.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            taskNameBox.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            personalBox.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);


            chartType.SelectedIndex = 0;

            ImgTimeInfo = new List<ImgTime>();
            ImgAverageInfo = new List<ImgAverageTime>();

            if (rData == null)
            {
                MessageBox.Show("NULL!!!");
            }

            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                if (ListBox.NoMatches == taskNameBox.FindStringExact(temp.taskName) && curProjectName == temp.projectName)
                {
                    taskNameBox.Items.Add(temp.taskName);
                }
            }

        }




        private void callPersonalGraph(string projectName, string ScenarioName, int idx)
        {
            nChartControl1.Clear();

            //rData.getRData()[0].taskName 이런식으로 인덱스 줘서 사용하면 될 듯
            m_FuncCalculator = new NFunctionCalculator();

            nChartControl1.Legends.Clear();



            // set a chart title
            NLabel title = nChartControl1.Labels.AddHeader("Task x 소요 시간");
            //title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 18, FontStyle.Regular);
            title.TextStyle.FontStyle = new NFontStyle(pfc.Families[0].ToString(), 18, FontStyle.Regular);
            title.ContentAlignment = ContentAlignment.BottomCenter;
            title.Location = new NPointL(new NLength(50, NRelativeUnit.ParentPercentage), new NLength(2, NRelativeUnit.ParentPercentage));

            // setup chart
            m_Chart = nChartControl1.Charts[0];
            m_Chart.BoundsMode = BoundsMode.Stretch;
            m_Chart.Location = new NPointL(new NLength(10, NRelativeUnit.ParentPercentage), new NLength(15, NRelativeUnit.ParentPercentage));
            m_Chart.Size = new NSizeL(new NLength(80, NRelativeUnit.ParentPercentage), new NLength(75, NRelativeUnit.ParentPercentage));
            


            //m_Chart.Axis(StandardAxis.Depth).Visible = false;


            // add a line series for the function
            m_Line = (NLineSeries)m_Chart.Series.Add(SeriesType.Line);
            m_Line.MarkerStyle.Visible = true;
            m_Line.MarkerStyle.BorderStyle.Color = Color.DarkGreen;
            m_Line.MarkerStyle.BorderStyle.Width = new NLength(2, NGraphicsUnit.Pixel);
            m_Line.MarkerStyle.Width = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Line.MarkerStyle.Height = new NLength(1.2f, NRelativeUnit.ParentPercentage);
            m_Line.MarkerStyle.PointShape = PointShape.Cylinder;
            m_Line.MarkerStyle.FillStyle = new NColorFillStyle(Color.Gold);
            m_Line.BorderStyle.Color = Color.DarkGreen;
            m_Line.BorderStyle.Width = new NLength(2, NGraphicsUnit.Pixel);
            m_Line.Legend.Mode = SeriesLegendMode.None;
            m_Line.DataLabelStyle.Format = "<value>";
            m_Line.Values.ValueFormatter = new NNumericValueFormatter("0.0");
            m_Line.ShadowStyle.Type = ShadowType.GaussianBlur;
            m_Line.ShadowStyle.Offset = new NPointL(2, 2);
            m_Line.ShadowStyle.Color = Color.FromArgb(120, 0, 0, 0);
            m_Line.ShadowStyle.FadeLength = new NLength(5);


            // add the bar series
            m_Bar = (NBarSeries)m_Chart.Series.Add(SeriesType.Bar);
            m_Bar.Name = "Bar1";
            m_Bar.Values.Name = "values";
            m_Bar.Values.ValueFormatter = new NNumericValueFormatter("0.0");
            m_Bar.MultiBarMode = MultiBarMode.Stacked;
            m_Bar.DataLabelStyle.Visible = true; // 바에 보이는 값
            //m_Bar.Legend.Mode = SeriesLegendMode.None; // 범례 안보이게 하는 거
            m_Bar.BarShape = BarShape.Cylinder;
            m_Bar.BorderStyle.Width = new NLength(0, NGraphicsUnit.Pixel);
            m_Bar.FillStyle = new NColorFillStyle(Color.DarkKhaki);
            m_Bar.ShadowStyle.Type = ShadowType.Solid;
            m_Bar.ShadowStyle.Offset = new NPointL(2, 2);
            m_Bar.ShadowStyle.Color = Color.FromArgb(80, 0, 0, 0);
            //m_Bar.Values.FillRandomRange(new Random(), 14, 0, 100);

            NLinearScaleConfigurator linearScale = new NLinearScaleConfigurator();
            //m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScale;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = false;

            NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(0, 10));
            m_Chart.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;
            nChartControl1.Controller.Tools.Add(new NAxisScrollTool());

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;

            scaleConfiguratorX.AutoLabels = false;

            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                // 현재 찾고자 하는 프로젝트와 시나리오랑 같으면
                if (temp.projectName == projectName && temp.taskName == ScenarioName)
                {
                    if (idx == -1 /*&& temp.isMin == true*/) // ***************************************************************************************************************** check
                    {
                        ImgAverageInfo.Clear();

                        // 해당 시나리오 값에서 이동한 path 이미지들 값 가져오기
                        foreach (ResultData.ImgTime imgTemp in temp.pathInfo)
                        {
                            bool inList = false;

                            float timeValue = System.Convert.ToSingle(imgTemp.timeImg);

                            // 리스트 내에 같은 값 찾기

                            if (ImgAverageInfo.Count != 0)
                            {
                                foreach (ImgAverageTime inListImg in ImgAverageInfo)
                                {
                                    // 같은게 있다면
                                    if (inListImg.imgName == imgTemp.imgName)
                                    {
                                        inListImg.count += 1;
                                        inListImg.imgTime += timeValue;
                                        inList = true;
                                        break;
                                    }
                                }
                            }

                            // 리스트 내에 없다면
                            if (inList == false)
                                ImgAverageInfo.Add(new ImgAverageTime() { imgName = imgTemp.imgName, imgTime = timeValue, count = 1 });
                        }
                    }
                    // Idx랑 같으면
                    else if (temp.idx == idx)
                    {
                        // 해당 시나리오 값에서 이동한 path 이미지들 값 가져오기
                        foreach (ResultData.ImgTime imgTemp in temp.pathInfo)
                        {
                            scaleConfiguratorX.Labels.Add(imgTemp.imgName);
                            m_Bar.Values.Add(imgTemp.timeImg);
                        }
                    }
                }
            }


            if (idx == -1)
            {
                foreach (ImgAverageTime listImg in ImgAverageInfo)
                {
                    scaleConfiguratorX.Labels.Add(listImg.imgName);
                    m_Bar.Values.Add(listImg.imgTime / listImg.count);
                }
            }





            //m_FuncCalculator.Arguments.Add(m_Line.Values);
            m_FuncCalculator.Arguments.Add(m_Bar.Values);
            m_FuncCalculator.Expression = "CUMSUM(values)";
            m_Line.Values = m_FuncCalculator.Calculate();

            m_Line.Values.ValueFormatter = new NNumericValueFormatter("0.0");


            // form controls
            /*m_FunctionCombo.Items.Add("Power");
            m_FunctionCombo.Items.Add("Cumulative");
            m_FunctionCombo.Items.Add("Exponential Average");
            m_FunctionCombo.SelectedIndex = 0;*/


            nChartControl1.Refresh();
        }

        private void callLowHighGraph(string projectName, string ScenarioName)
        {
            nChartControl1.Clear();
            nChartControl1.Legends.Clear();

            NLabel title = nChartControl1.Labels.AddHeader("이미지별 최대 최소 시간");
            //title.TextStyle.FontStyle = new NFontStyle("Times New Roman", 18, FontStyle.Bold);
            title.TextStyle.FontStyle = new NFontStyle(pfc.Families[0].ToString(), 18, FontStyle.Regular);
            //title.TextStyle.FillStyle = new NColorFillStyle(GreyBlue);

            // configure the chart
            m_Chart = nChartControl1.Charts[0];
            m_Chart.Axis(StandardAxis.Depth).Visible = false;

            // add a High-Low series
            m_HighLow = (NHighLowSeries)m_Chart.Series.Add(SeriesType.HighLow);
            m_HighLow.Name = "High-Low Series";
            m_HighLow.HighFillStyle = new NColorFillStyle(Color.FromArgb(68, 90, 108));
            m_HighLow.HighBorderStyle = new NStrokeStyle(Color.FromArgb(254, 181, 25));
            //m_HighLow.LowFillStyle = new NColorFillStyle(DarkOrange);
            m_HighLow.DataLabelStyle.Visible = true;
            m_HighLow.DataLabelStyle.Format = " <high_value>\n<low_value>";
            m_HighLow.LowValues.ValueFormatter = new NNumericValueFormatter("0.##");
            m_HighLow.HighValues.ValueFormatter = new NNumericValueFormatter("0.##");


            m_HighLow.DropLines = true;

            m_HighLow.ClearDataPoints();


            NLinearScaleConfigurator linearScale = new NLinearScaleConfigurator();
            //m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator = linearScale;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.ResetButton.Visible = false;

            NNumericAxisPagingView numericPagingView = new NNumericAxisPagingView(new NRange1DD(0, 10));
            m_Chart.Axis(StandardAxis.PrimaryX).PagingView = numericPagingView;
            m_Chart.Axis(StandardAxis.PrimaryX).ScrollBar.Visible = true;
            nChartControl1.Controller.Tools.Add(new NAxisScrollTool());

            NStandardScaleConfigurator scaleConfiguratorX = (NStandardScaleConfigurator)m_Chart.Axis(StandardAxis.PrimaryX).ScaleConfigurator;
            scaleConfiguratorX.MajorTickMode = MajorTickMode.AutoMaxCount;

            scaleConfiguratorX.AutoLabels = false;

            // data 값을 다 불러와서
            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                // 현재 찾고자 하는 프로젝트와 시나리오랑 같으면
                if (temp.projectName == projectName && temp.taskName == ScenarioName)
                {
                    // 해당 시나리오 값에서 이동한 path 이미지들 값 가져오기
                    foreach (ResultData.ImgTime imgTemp in temp.pathInfo)
                    {
                        bool inList = false;

                        float timeValue = System.Convert.ToSingle(imgTemp.timeImg);

                        // 리스트 내에 같은 값 찾기

                        if (ImgTimeInfo.Count != 0)
                        {
                            foreach (ImgTime inListImg in ImgTimeInfo)
                            {
                                // 같은게 있다면
                                if (inListImg.imgName == imgTemp.imgName)
                                {
                                    // 현재 값이 high 보다 크다면
                                    if (inListImg.High <= timeValue)
                                        inListImg.High = timeValue;
                                    else if (inListImg.Low >= timeValue)
                                        inListImg.Low = timeValue;

                                    inList = true;

                                    break;
                                }
                            }
                        }

                        // 리스트 내에 없다면
                        if (inList == false)
                            ImgTimeInfo.Add(new ImgTime() { imgName = imgTemp.imgName, High = timeValue, Low = timeValue });
                    }
                }
            }

            foreach (ImgTime listImg in ImgTimeInfo)
            {
                scaleConfiguratorX.Labels.Add(listImg.imgName);
                m_HighLow.HighValues.Add(listImg.High);
                m_HighLow.LowValues.Add(listImg.Low);
            }

            nChartControl1.Refresh();
        }


        private void chartType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (chartType.SelectedIndex >= 0)
            {
                curChartType = chartType.SelectedIndex;
                // 작업필요
            }
        }

        private void projectNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskNameBox.Items.Clear();

            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                if (ListBox.NoMatches == taskNameBox.FindStringExact(temp.taskName) && curProjectName == temp.projectName)
                {
                    taskNameBox.Items.Add(temp.taskName);
                }
            }
        }

        private void chartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskNameBox.Items.Clear();

            if (chartType.SelectedIndex == 1)
            {
                personalBox.Enabled = false;
            }
            else
            {
                personalBox.Enabled = true;
            }

            taskNameBox.Items.Clear();

            foreach (ResultData.ResultInfo temp in rData.getRData())
            {
                if (ListBox.NoMatches == taskNameBox.FindStringExact(temp.taskName) && curProjectName == temp.projectName)
                {
                    taskNameBox.Items.Add(temp.taskName);
                }
            }

        }

        private void taskNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (chartType.SelectedIndex == 0)
            {
                personalBox.Items.Clear();

                personalBox.Items.Add("Entire Average");

                foreach (ResultData.ResultInfo temp in rData.getRData())
                {
                    if (taskNameBox.SelectedItem == temp.taskName && personalBox.Enabled == true)
                        personalBox.Items.Add("Tester " + temp.idx);
                }
            }
            else
            {
                callLowHighGraph(curProjectName, taskNameBox.SelectedItem.ToString());
            }
        }

        private void personalBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (personalBox.SelectedIndex == 0)
                callPersonalGraph(curProjectName, taskNameBox.SelectedItem.ToString(), -1);
            else
                callPersonalGraph(curProjectName, taskNameBox.SelectedItem.ToString(), Int32.Parse(personalBox.SelectedItem.ToString().Split(' ')[1]));
        }

    }
}