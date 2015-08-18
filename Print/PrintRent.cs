using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace Print
{
    class PrintRent
    {
        public PrintRent()
        {
            Printing();
        }

        public void Printing()
        {
            try
            {
                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintRent);
                    // Print the document.
                    pd.Print();
                }
                finally
                {
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void pd_PrintRent(object sender, PrintPageEventArgs ev)
        {
            //float linesPerPage = 0;
            //float yPos = 0;
            //int count = 0;
            int leftMargin = ev.PageBounds.Left + 50;
            int topMargin = ev.PageBounds.Top + 50;
            int rightMargin = ev.PageBounds.Right - 50;
            int bottomMargin = ev.PageBounds.Bottom - 50;
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            Point address = new Point(leftMargin, topMargin);
            Point openTime = new Point(rightMargin-250, topMargin);
            RectangleF title = new RectangleF(leftMargin, topMargin + 100, ev.PageBounds.Width - 100, 50);
            RectangleF subTitle = new RectangleF(title.Left, title.Top + 30, title.Width, 50);
            Point personDatas = new Point(leftMargin, topMargin + 180);
            Point rentDatas = new Point(leftMargin, topMargin + 310);
            Point sumDatas = new Point(leftMargin, bottomMargin - 500);
            //String line = null;
            
            ev.Graphics.DrawString(ConfigurationManager.AppSettings["CompanyName"], new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, address, new StringFormat());
            ev.Graphics.DrawString(ConfigurationManager.AppSettings["CompanyAddress"] + 
                "\nTel.: " + ConfigurationManager.AppSettings["CompanyPhone1"] + "\n        " + ConfigurationManager.AppSettings["CompanyPhone2"], 
                new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, address.X, address.Y + 30, new StringFormat());            
            ev.Graphics.DrawString("Nyitva:", new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, openTime, new StringFormat());
            ev.Graphics.DrawString(ConfigurationManager.AppSettings["OpenTime"].Replace("|NL|", Environment.NewLine), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, openTime.X, openTime.Y + 30, new StringFormat());
            ev.Graphics.DrawString("BÉRLETI SZERZŐDÉS", new Font(FontFamily.GenericSerif, 16, FontStyle.Bold), Brushes.Black, title, centerFormat);
            ev.Graphics.DrawString("Mely létrejött a " + ConfigurationManager.AppSettings["CompanyName"] + " - továbbiakban bérbeadó -, és Bérlő között",
                new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, subTitle, centerFormat);
            ev.Graphics.DrawString("Bérlő neve:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas, new StringFormat());
            ev.Graphics.DrawString("Címe:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 20, new StringFormat());
            ev.Graphics.DrawString("Szem. ig. sz., tel.:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString("Cég (megbízó):", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 70, new StringFormat());
            ev.Graphics.DrawString("Bérleti idő kezdete:            napján              órától", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 100, new StringFormat());
            ev.Graphics.DrawString("Bérleti tárgy megnevezése, azonosító száma          db   Bérleti idő lejár     Nap    Egységár      Eng. %      Díj    .", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold | FontStyle.Underline), Brushes.Black, rentDatas, new StringFormat());
            ev.Graphics.DrawString("Díj a lejárat idejéig:                      Ft\nLetét:              Ft, azaz", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas, new StringFormat());
            ev.Graphics.DrawString("A bérleti díj a bérelt tárgy visszaszállításának napján került számlázásra, addig letétként szerepel.", new Font(FontFamily.GenericSerif, 10, FontStyle.Regular), Brushes.Black, sumDatas.X, sumDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString("A bérelt tárgyat megtisztítva kérjük visszaszállítani!", new Font(FontFamily.GenericSerif, 10, FontStyle.Regular), Brushes.Black, sumDatas.X, sumDatas.Y + 67, new StringFormat());
            ev.Graphics.DrawString("A Tisztítás felára: " + ConfigurationManager.AppSettings["CostOfClean"] + ",- Ft.", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas.X + 290, sumDatas.Y + 65, new StringFormat());
            ev.Graphics.DrawString("Megjegyzés:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas.X, sumDatas.Y + 85, new StringFormat());

            

            

            // Calculate the number of lines per page.
            //linesPerPage = ev.MarginBounds.Height /
            //   printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            //while (count < linesPerPage &&
            //   ((line = streamToPrint.ReadLine()) != null))
            //{
            //    yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
            //    ev.Graphics.DrawString(line, printFont, Brushes.Black,
            //       leftMargin, yPos, new StringFormat());
            //    count++;
            //}

            // If more lines exist, print another page.
            //if (line != null)
            //    ev.HasMorePages = true;
            //else
                ev.HasMorePages = false;
        }
    }
}
