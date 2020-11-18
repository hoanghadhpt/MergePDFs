using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergePDFs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MergePDFs(textBox1.Text);
                MessageBox.Show("OK");
            }
            else
            {

            }
        }
        private void MergePDFs(string Path)
        {
            //File.Copy("ZZ.pdf", Path + "\\ZZ.PDF");

            string outputPdfPath = Path + "\\____HH.PDF";
            File.Delete(outputPdfPath);

            string[] PDFs = Directory.GetFiles(Path, "*.pdf", SearchOption.AllDirectories);
            

            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            

            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            sourceDocument.Open();

            try
            {
                //Loop through the files list
                for (int f = 0; f <= PDFs.Length-1; f++)
                {
                    label1.Text = String.Format("Đang ghép {0}/{1}",f+1,PDFs.Length);
                    this.Refresh();
                    int pages = get_pageCcount(PDFs[f]);

                    reader = new PdfReader(PDFs[f]);
                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        label1.Text = String.Format("Đang ghép {0}/{1} || Số trang: {2}/{3}", f + 1, PDFs.Length,i,pages);
                        this.Refresh();
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    reader.Close();
                }
                //At the end save the output file
                sourceDocument.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int get_pageCcount(string file)
        {
            PdfReader pdfReader = new PdfReader(file);
            int numberOfPages = pdfReader.NumberOfPages;
            return numberOfPages;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string usrName = Environment.UserName.ToString().ToUpper();
            string ValidUSER = "E1057 E1897 HOANG HA";
            if (ValidUSER.Contains(usrName))
            {
            }
            else
            {
                MessageBox.Show("OK Fine");
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
