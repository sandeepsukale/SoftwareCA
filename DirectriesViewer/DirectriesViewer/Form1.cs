using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace DirectriesViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Multiselect = true;
           openFileDialog1.RestoreDirectory = false;
           
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
               


                var filePath = folderBrowserDialog1.SelectedPath;
               
                    var files = Path.GetDirectoryName(filePath);

                    string[] fileEntries = Directory.GetFiles(filePath);//list of all the file names in the directory
                    displayText.Text = filePath;

                    List<string> items = new List<string>();
                    List<string> sortedfiles = new List<string>();
                    List<string> Allsortedfiles = new List<string>();
                    List<string> calcfile = new List<string>();
                    items.Add(Path.GetFullPath(filePath));


                    foreach (string fileName in fileEntries)//method to sort each file in file names
                    {
                        StreamReader str = new StreamReader(fileName);



                        string FileContent = str.ReadToEnd();
                        

                    if (Path.GetExtension(fileName) == ".txt")
                    {
                        
                         string[] sorted= FileContent.Split(' ');

                        Array.Sort(sorted);

                        string sortedFile = "\\Sorted" + Path.GetFileName(fileName);
                        string path = filePath + sortedFile;

                        if (!File.Exists(path))
                        {

                           
                            StreamWriter sw = new StreamWriter(path);
                            foreach (string item in sorted)
                            {
                                
                                    sw.WriteLine(item);
                                
                               
                            }


                            sortedfiles.Add(sortedFile);
                            sw.Close();

                            Allsortedfiles.Add(sortedFile + " in " + Directory.GetDirectoryRoot(path));
                        }
                        else { MessageBox.Show("{0}  already eXists", Path.GetFileName(path)); }
                    }

                    
                 

                    Sdir.DataSource = items;
                    Sfile.DataSource = sortedfiles;
                    AllFile.DataSource = Allsortedfiles;

           }
             
            }
            
        }
      
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
