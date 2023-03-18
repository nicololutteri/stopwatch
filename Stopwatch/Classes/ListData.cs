using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;

namespace Stopwatch
{
    public class ListData
    {
        public List<Lap> list;

        public ListData()
        {
            list = new List<Lap>();
        }

        public void Add(Lap x)
        {
            list.Add(x);
        }

        public void Modify(int number, Lap x)
        {
            list[number] = x;
        }

        public void Delete(int number)
        {
            list.RemoveAt(number);
        }

        public void SaveXml(string fileName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName);
                XmlSerializer xs = new XmlSerializer(typeof(List<Lap>));
                xs.Serialize(sw, list);
                sw.Close();
            }
            catch
            {
                
            }
        }

        public void LoadXML(string fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName);
                XmlSerializer xs = new XmlSerializer(typeof(List<Lap>));
                list = (List<Lap>)xs.Deserialize(sr);
                sr.Close();
            }
            catch
            {

            }
        }
    }
}
