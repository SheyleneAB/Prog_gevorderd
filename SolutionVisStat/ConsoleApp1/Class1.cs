using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visstat_uploaddata;
using VisStatsBL.Manager;
using Visstat_SQL;
using VisStatsBL.interfaces;


namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\elyne\OneDrive\Documents\School-Documenten\Programmeren gevorderd\Vis\vissoorten1.txt";
            string connectionString = @"Data Source=Elyne\SQLEXPRESS;Initial Catalog=PQValue_B;Integrated Security=True;Trust Server Certificate=True";
            FileProcessor processor = new FileProcessor();

            IVisStatRepository visStatsRepository = new VisStatRepository(connectionString);
            VisStatManager visStatsManager = new VisStatManager(processor, visStatsRepository);
            visStatsManager.UploadVissoorten(filePath);

        }
    }
}
