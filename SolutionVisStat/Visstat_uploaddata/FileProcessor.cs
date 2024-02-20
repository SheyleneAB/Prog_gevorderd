using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisStatsBL.interfaces;

namespace Visstat_uploaddata
{
    public class FileProcessor : IFileProcessor
    {
        public List<string> LeesSoorten(string fileName)
        {
            try
            {
                List<string> soorten = new List<string>();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        soorten.Add(line.Trim());
                    }
                }
                return soorten;
            }
            catch (Exception ex) { throw new Exception($"FileProcessor-LeesSoorten [{fileName}]"); }
        }
    }
}
