using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Text;

namespace sCollect
{
    public partial class Form1 : Form
    {
        WebClient _WC = new WebClient();
        Defaults _DF = new Defaults();
        bool doneYet = false;
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            gatherTheProxies();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                
                backgroundWorker1.RunWorkerAsync();
                if (txtOut.Checked)
                {
                    if (dlg.ShowDialog() == DialogResult.OK) 
                    { 
                        StreamWriter writer = new StreamWriter(dlg.FileName);

                        for (int i = 0; i < listBox1.Items.Count; i++) 
                        {
                            writer.WriteLine((String)listBox1.Items[i]);
                        }

                        writer.Close();
                    }
                    
             
                }

                dlg.Dispose();

            }
            catch (Exception i)
            {
                MessageBox.Show("error : " + i.Message, "lol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gatherTheProxies()
        {

            try
            {
                foreach (string Source in textBox1.Lines)
                {
                    string unparsedWebSource = _WC.DownloadString(Source);


                    MatchCollection _MC = _DF.REGEX.Matches(unparsedWebSource);
                    foreach (Match Proxy in _MC)
                    {
                        string t = Proxy.ToString();
                        listBox1.Items.Add(t);
                        
                    }
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("error : " + e.Message, "!");
            }
         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
