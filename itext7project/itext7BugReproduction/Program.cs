using iText.Forms;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace itext7BugReproduction
{
    class Program
    {
        static void Main(string[] args)
        {
            //change these accourdin your machine
            var src = @"C:\Users\Viktor.Jakovlev\Desktop\itext7\itext7project\itext7BugReproduction\templates\sample.pdf";
            var dest = @"C:\Users\Viktor.Jakovlev\Desktop\itext7\itext7project\itext7BugReproduction\pdfs\sample_filled.pdf";

            FileInfo file = new FileInfo(dest);
            file.Directory.Create();

            //var fields = GetFieldNames(src);

            FillPdf(src, dest);
        }

        public static List<string> GetFieldNames(string src)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(src));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            return form.GetFormFields().Keys.ToList();
        }

        public static void FillPdf(string src, string dest)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(src), new PdfWriter(dest));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

            //here is where the bug occurs, when the razor runtime compliation library is installed
            //remove the razor runtime compilation package, and the next line will pass
            form.GetField("First Name").SetValue("Viktor");
            form.GetField("Last Name").SetValue("Jakovlev");

            pdfDoc.Close();
        }
    }
}
