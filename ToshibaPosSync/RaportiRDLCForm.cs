using System;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ToshibaPosSinkronizimi
{
    public partial class RaportiRDLCForm : Form
    {
        public RaportiRDLCForm(
            bool? _PrintoAutomatikisht = false, int? NumriKopjeve = 1
            )
        {
            InitializeComponent();
            PrintoAutomatikisht = _PrintoAutomatikisht;
            numriKopjeve = NumriKopjeve;
        }
        bool? PrintoAutomatikisht = false;
        int? numriKopjeve = 1;
        private void RaportiRDLCForm_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.rptViewer.RefreshReport();
            
            PrinterSettings PrinterSettings = rptViewer.PrinterSettings;
        }
    }
}
