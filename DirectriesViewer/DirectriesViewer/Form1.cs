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

            this.Sdir.SelectedIndexChanged += new EventHandler(Sdir_SelectedIndexChanged);


        }

            

        private void button1_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
               


                var filePath = folderBrowserDialog1.SelectedPath;
               
                    var files = Path.GetDirectoryName(filePath);
                
                string[] fileEntries = Directory.GetFiles(filePath);//list of all the file names in the directory
                    displayText.Text = filePath;

                    
                    List<string> calcfile = new List<string>();

                    
                    foreach (string fileName in fileEntries)//method to sort each file in file names
                    {
                        StreamReader str = new StreamReader(fileName);



                        string FileContent = str.ReadToEnd();
                        

                    if (Path.GetExtension(fileName) == ".txt")
                    {
                        int count = 0;

                        string[] trimmed = FileContent.Split(' ');
                        string[] sorted = new string[trimmed.Length];

                        int trimCount = 0;
                        foreach (string test in trimmed)
                        {
                            sorted.SetValue(test.TrimEnd('.', ','), trimCount);

                            trimCount++;
                        }


                        Array.Sort(sorted);

                        string sortedFile = "\\Sorted" + Path.GetFileName(fileName);
                        string path = filePath + sortedFile;

                        if (!File.Exists(path))
                        {

                            int counter = 0;
                            StreamWriter sw = new StreamWriter(path);
                            foreach (string item in sorted)
                            {
                                
                                count = Sort.Count(item, sorted);
                                if (count > 1)
                                {


                                    if (item == sorted[counter])
                                    {
                                        continue;
                                        

                                    }
                                    else { sw.WriteLine(item + " ," + count); }
                                }

                                else if (count == 1)
                                {


                                    sw.WriteLine(item);
                                }
                                 

                           
                            counter++;
                            }
                        sw.Close();
                       
                            
                            AllFile.Items.Add(sortedFile + " in " + Directory.GetDirectoryRoot(path));
                            
                        }
                        else { MessageBox.Show("{0}  already eXists", Path.GetFileName(path)); }
                    }



                }

                Sdir.Items.Add(Path.GetFullPath(filePath));
            }
               
               
        }
      
        private void Sdir_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = Sdir.SelectedItem.ToString();
            MessageBox.Show(filename);
            string[] sortedfilesArrayInselected = Directory.GetFiles(filename, "*Sorted*");
            foreach(string longFilename in sortedfilesArrayInselected)
            {
                Sfile.Items.Add(Path.GetFileName(longFilename));
            }
          
        }

        
    }
}
