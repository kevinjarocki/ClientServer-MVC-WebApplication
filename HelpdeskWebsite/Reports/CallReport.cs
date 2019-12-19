using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using HelpdeskViewModels;
using System.Collections.Generic;

namespace HelpdeskWebsite.Reports
{
    public class CallReport
    {
        static string mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        static Font hdgFont = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD);
        static Font catFont = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD);
        static Font subFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD);
        static Font smallFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        static string IMG = "img/report.jpg";


        private static void addEmptyLine(Paragraph para, int num)
        {
            for (int i = 0; i < num; i++)
            {
                para.Add(new Paragraph(" "));
            }
        }

        private static void addEmptyLine(PdfPTable para, int num)
        {
            for (int i = 0; i < num; i++)
            {
                para.AddCell(" ");
            }
        }

        private PdfPCell addCell(string data, string celltype = "d")
        {
            PdfPCell cell;
            if (celltype == "h")
            {
                cell = new PdfPCell(new Phrase(data, smallFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }
            else
            {
                cell = new PdfPCell(new Phrase(data));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }

            return cell;
        }

        public void doIt()
        {
            try
            {


                Document document = new Document();
                PdfWriter.GetInstance(document,
                    new FileStream(mappedPath + "pdfs/Call.pdf", FileMode.Create));
                document.Open();
                Paragraph para = new Paragraph();
                PdfPTable titleTable = new PdfPTable(2);
                titleTable.DefaultCell.Border = Rectangle.NO_BORDER;
                // Image Stuff
                Image image1 = Image.GetInstance(mappedPath + IMG);
                image1.SetAbsolutePosition(100f, 100f);
                //image1.Alignment = Image.ALIGN_LEFT | Image.TEXTWRAP;
                image1.ScaleToFit(100, 100);



                // ALign para for image
                //para.Alignment = Element.ALIGN_RIGHT;

                Paragraph mainHead = new Paragraph("Calls", catFont);
                Paragraph imgHead = new Paragraph();
                //mainHead.Add(logocell);
                mainHead.Alignment = Element.ALIGN_CENTER;
                imgHead.Alignment = Element.ALIGN_CENTER;
                // mainHead.PaddingTop = Element.

                imgHead.Add(new Chunk(image1, 0, 0, true));

                PdfPCell imgCell = new PdfPCell(imgHead);
                PdfPCell titlCell = new PdfPCell(mainHead);
                imgCell.Border = Rectangle.NO_BORDER;
                titlCell.Border = Rectangle.NO_BORDER;
                titleTable.AddCell(imgCell);
                titleTable.AddCell(titlCell);
                // para.Add(imgHead);
                //para.Add(mainHead);
                para.Add(titleTable);


                addEmptyLine(para, 2);
                // Table stuff
                PdfPTable table = new PdfPTable(6);

                table.WidthPercentage = 100.00F;
                table.AddCell(addCell("Opened", "h"));
                table.AddCell(addCell("Lastname", "h"));
                table.AddCell(addCell("Tech", "h"));
                table.AddCell(addCell("Problem", "h"));
                table.AddCell(addCell("Status", "h"));
                table.AddCell(addCell("Closed", "h"));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                CallViewModel callVm = new CallViewModel();
                List<CallViewModel> calls = callVm.GetAll();

                foreach (CallViewModel call in calls)
                {

                   table.AddCell(addCell(call.DateClosed.ToString()));
                    table.AddCell(addCell(call.DateOpened.ToShortDateString()));

                    table.AddCell(addCell(call.EmployeeName));
                    table.AddCell(addCell(call.TechName));

                    table.AddCell(addCell(call.ProblemDescription));
                    if (call.OpenStatus == true)
                    {
                        table.AddCell(addCell("Open"));
                    }
                    else if (call.OpenStatus == false)
                    {
                        table.AddCell(addCell("Closed"));
                    }

                }

                para.Add(table);
                addEmptyLine(para, 3);
                para.Alignment = Element.ALIGN_CENTER;
                Paragraph footer = new Paragraph("Call Report written on - " + DateTime.Now, smallFont);
                footer.Alignment = Element.ALIGN_CENTER;
                para.Add(footer);

                document.Add(para);
                document.Close();

            }
            catch (Exception e)
            {
                Trace.WriteLine("Error: " + e.Message);
            }
        }
    }
}