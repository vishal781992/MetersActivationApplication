using System.IO;
using System.Windows.Forms;
//using System.Windows.Forms;
//using System.ComponentModel;

namespace SimAutomation
{
    public partial class ConfirmationBox : Form
    {
        string Filename = string.Empty;
        public ConfirmationBox(string FileName)
        {
            this.Filename = FileName;
            //label2.Text = this.Filename;
            InitializeComponent();
            UpdateTextFileData();
        }

        public void UpdateTextFileData()
        {
            try
            {
                string[] FileString = File.ReadAllLines(Filename); label2.Text = Filename;
                richTextBox_CB.Clear();
                foreach (string LineSt in FileString)
                {
                    richTextBox_CB.AppendText(LineSt + "\r\n");
                }
            }
            catch { }
        }
    }
}
