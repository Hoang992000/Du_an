using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDApp.Helper
{
    public class LogControl
    {
        public static void WriteLog(string str)
        {
            string logFile = DateTime.Now.ToString("dd-MM-yyyy");
            if (!Directory.Exists(@"Log\" + DateTime.Now.Year + "\\" + DateTime.Now.Month))
            {
                Directory.CreateDirectory(@"Log\" + DateTime.Now.Year + "\\" + DateTime.Now.Month);
            }
            using (StreamWriter sw = new StreamWriter(@"Log\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + logFile + ".txt", true))
            {
                DateTime datetime = DateTime.Now;
                
                sw.WriteLine(datetime.ToString("dd/MM/yyyy HH:mm:ss")+": "+str);
                sw.Close();
            }
        }
    }
}
