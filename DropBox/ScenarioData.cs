using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBox
{
    public class ScenarioData
    {
        public int changed;

        public struct SCENARIO_DATA
        {
            public int tag;
            public string title;
            public string time;
            public string purpose;
            public string level;
            public List<PATH_DATA> paths;
        }

        public struct PATH_DATA
        {
            public int tag;
            public string path;
            public string stay_time;
            public string auto_change_time;
        }

        List<SCENARIO_DATA> sData = new List<SCENARIO_DATA>();

        public void AddScenario(int sTag, string sTitle, string sPurpose, string sTime, string sLevel, List<PATH_DATA> sPaths)
        {
            sData.Add(new SCENARIO_DATA() { tag = sTag, title = sTitle, purpose = sPurpose, time = sTime, level = sLevel, paths = sPaths });
            for (int i = 0; i < sData.Count; i++)
            {
                for(int j = 0; j < sData[i].paths.Count; j++)
                {
                    Console.WriteLine(sData[i].paths[j].path);
                }
            }
            Console.WriteLine("\n");
        }

        public List<SCENARIO_DATA> getSData()
        {
            return sData;
        }

        
        public int getSLastIndex()
        {
            int last_index = 0;
            for (int i = 0; i < sData.Count; i++)
            {
                if(last_index < sData[i].tag)
                {
                    last_index = sData[i].tag;
                }
            }
            return last_index;
        }

        public void resetTime(int index, string pStayTime, string pPath, int pTag, string pAutoCgTime)
        {
            for(int i = 0; i < sData[index].paths.Count; i++)
            {
                if(sData[index].paths[i].tag == pTag)
                {
                    sData[index].paths.RemoveAt(i);
                    sData[index].paths.Insert(i, new PATH_DATA() { tag = pTag, path = pPath, stay_time = pStayTime, auto_change_time = pAutoCgTime });
                }
            }
        }

    }
}
