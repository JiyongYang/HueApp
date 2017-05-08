using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBox
{
    public class ResultData
    {

        public struct ResultInfo
        {
            public string projectName;
            public string taskName;
            public List<ImgTime> pathInfo;
            public bool isMin;
            public int idx;
        }

        public struct ImgTime
        {
            public string imgName;
            public string timeImg;
        }

        List<ResultInfo> rData = new List<ResultInfo>();

        public void AddResultInfo(string pProjectName, string pTaskName, List<ImgTime> pPathInfo, bool pIsMin, int pIdx)
        {
            rData.Add(new ResultInfo() { projectName = pProjectName, taskName = pTaskName, pathInfo = pPathInfo, isMin = pIsMin, idx = pIdx });
            Console.WriteLine("테스트 : " + pIsMin.ToString());
        }

        public List<ResultInfo> getRData()
        {
            return rData;
        }

    }
}
