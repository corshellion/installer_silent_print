using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Insall_Print
{
    public partial class Form1 : Form
    {
        // string path = Directory.GetCurrentDirectory();
        string mycomputer = Environment.UserName;


        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show("C:\\Users\\user\\Documents\\SilentPrint\\SilentPrint\\bin\\SilentPrint.exe" + "<< ini adalah pathnya");
        }

        Microsoft.Win32.RegistryKey key;

        public void createRegistry()
        {
            key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey("slsprint");
            key.SetValue("URL Protocol", "");
            key.Close();
            progressBar1.Value = 10;
            string programFiles = "";

            if (IntPtr.Size == 8)
            {
                //64 bit
                programFiles = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");
            }
            else
            {
                //32bit
                programFiles = Environment.ExpandEnvironmentVariables("%ProgramFiles%");
            }
            string tambahan = "%1";
            string f = programFiles + "\\Silent Print\\SilentPrint.exe";
            string total = "\"" + f + "\"" + " " + "\"" + tambahan + "\"";

            key = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey("slsprint\\shell\\open\\command");
            key.SetValue("", total);
            key.Close();
            progressBar1.Value = 30;

        }

        public void createDirectory()
        {
            //detect domain + version
            string localvar = Environment.GetCommandLineArgs()[1].ToString();
            string[] tokens = localvar.Split(' ');
            string domain = tokens[0];
            string version = tokens[1];
            //
            string path = "C:\\Users\\" + mycomputer.ToString() + "\\Documents\\SilentPrint\\SilentPrint("+version+")";
            string userProfile = System.Environment.GetEnvironmentVariable("USERPROFILE");
            //string sourceAsal = path + "\\bin\\SilentPrint.exe";
            string sourceAsal = path+"\\bin\\SilentPrint.exe";
            string sourceDir = "\\bin\\";
            string shdocvwDll = "Interop.SHDocVw.dll";
            string programFiles = "";
            if (IntPtr.Size == 8)
            {
                progressBar1.Value = 50;
                //run in windows 64bit and file is 64bit
                //programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
                //run in windows 64bit but file is 32bit
                programFiles = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");

                
            }
            else
            {
                //run in windows 32bit
                programFiles = Environment.ExpandEnvironmentVariables("%ProgramFiles%");
                
            }
            string sourceTujuan = programFiles + "\\Silent Print\\SilentPrint.exe";
            string folder = programFiles + "\\Silent Print";

            progressBar1.Value = 70;
            if (Directory.Exists(folder))
            {

            }
            else
            {
                System.IO.Directory.CreateDirectory(folder);
            }
            File.Copy(sourceAsal, sourceTujuan, true);
            File.Copy(path + sourceDir + shdocvwDll, folder + "\\" + shdocvwDll, true);
            progressBar1.Value = 80;

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                createRegistry();
                progressBar1.Value = 90;
                createDirectory();
                progressBar1.Value = 100;
                MessageBox.Show("Install Success");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                this.Close();
            }


        }
    }
}
