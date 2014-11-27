using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.Run(new Form1(args.Length > 0 ? args[0] : "25.12.2013"));
            var calendar = new Calendar(args.Length > 0 ? args[0] : "25.12.2013");
            calendar.SaveToFile("calendar.bmp");
        }
    }
}
