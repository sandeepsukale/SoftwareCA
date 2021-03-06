﻿using System;
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

            label5.Text = char.ConvertFromUtf32(0x2193);

            this.Sdir.SelectedIndexChanged += new EventHandler(Sdir_SelectedIndexChanged);


        }

        List<string> SearchList = new List<string>();
        string filePath;
        private void button1_Click(object sender, EventArgs e)
        {

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {



                filePath = folderBrowserDialog1.SelectedPath;

                var files = Path.GetDirectoryName(filePath);

                Sdir.Items.Add(Path.GetFullPath(filePath));

                string[] filters = new[] { "*.txt", "*.calc" };
                string[] fileEntries = filters.SelectMany(f => Directory.GetFiles(filePath, f)).ToArray();

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
                                counter++;
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

                            }
                            sw.Close();


                            AllFile.Items.Add(Path.GetFileName(path) + " in " + Directory.GetDirectoryRoot(path));

                        }
                        else {
                            MessageBox.Show("{0}  already eXists", Path.GetFileName(path));
                            break;
                        }
                    }
                    else if (Path.GetExtension(fileName) == ".calc")
                    {
                        AllFile.Items.Add(Path.GetFileName(fileName));
                    }


                }


            }


        }

        private void Sdir_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sfile.Items.Clear();
            string filename = Sdir.SelectedItem.ToString();
            displayText.Text = filename;

            string[] sortedfilesArrayInselected = Directory.GetFiles(filename, "*Sorted*");
            foreach (string longFilename in sortedfilesArrayInselected)
            {

                Sfile.Items.Add(Path.GetFileName(longFilename));
            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void Searchbutton_Click(object sender, EventArgs e)
        {

            AllFile.Items.Clear();
            string[] listItems = new string[Sdir.Items.Count];

            for (int i = 0; i < Sdir.Items.Count; i++)
            {
                listItems[i] = Sdir.Items[i].ToString();
            }
            
            // MessageBox.Show(string.Join(" ",listItems));
            string wordsearch = SearchBox.Text;
            if (SearchBox.Text != "")
            {


                SearchList = Sort.search(wordsearch, listItems).ToList();
                AllFile.DataSource = SearchList;
                
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //string filename = Sdir.SelectedItem.ToString();
            List<string> calcItems = new List<string>();
            var dirs = Sdir.Items.Cast<String>().ToArray();


            foreach (string dir in dirs)
            {
                if ((Directory.GetFiles(dir, "*.calc").Length > 0))
                {

                    string[] calcfilesArray = Directory.GetFiles(dir, "*.calc");

                    string answerName = DateTime.Now.ToString("MM/dd/yyyy HH:mm").Replace("/", "_").Replace(" ", "_").Replace(":", "_");
                    string simplifiedAnswName = $"currentData_{answerName}.answ";

                    foreach (string calFilename in calcfilesArray)
                    {


                        string[] calcwords = File.ReadAllLines(calFilename);
                        foreach (string Calcword in calcwords)
                        {
                            calcItems.Add(Calculator.calculate(Calcword));

                        }


                    }
                    File.WriteAllLines(filePath + "\\" + simplifiedAnswName, calcItems.ToArray());
                    MessageBox.Show($"Answers to the calculated file is written into  {simplifiedAnswName} \n of {Path.GetFullPath(dir)} ", "Calculated!!!");
                }

                else { MessageBox.Show("No .Calc file available in {dir}"); }
            }
        }

    }

  


}

