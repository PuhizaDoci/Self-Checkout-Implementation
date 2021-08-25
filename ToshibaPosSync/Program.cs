using ToshibaPos.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToshibaPosSinkronizimi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SinkronizimiClass.Starto(true);
            try
            {
                
                Application.Run(new SinkronizimiForm());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
