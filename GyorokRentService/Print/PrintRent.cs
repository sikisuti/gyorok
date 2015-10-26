using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using SQLConnectionLib;
using System.IO;
using MiddleLayer.Representations;

namespace GyorokRentService.Print
{
    class PrintRent
    {
        RentalGroup_Representation rentalsToPrint;
        RentCalculates calc = new RentCalculates();
        //long totalCost;
        //long deposit;
        string contract;
        //string comment;

        //public PrintRent(List<RentalSum> r)
        //{
        //    rentalsToPrint = r;
        //    Printing();
        //}

        public PrintRent(RentalGroup_Representation rentalGroup)
        {
            rentalsToPrint = rentalGroup;
            try
            {
                Printing();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void CalcTotalCost()
        //{
        //    long gID = rentalsToPrint[0].groupID;
        //    deposit = (long)SQLConnection.Execute.RentalGroupsTable.Single(rg => rg.groupID == gID).deposit;
        //    totalCost = 0;
        //    foreach (var item in rentalsToPrint)
        //    {
        //        //if (item.isPaid != true)
        //        //{
        //            totalCost += calc.getRentCost(item.rentalStart, item.rentalEnd, (long)item.actualPrice, (float)item.discount); // (long)((item.rentalEnd - item.rentalStart).Days * item.actualPrice * (1 - item.discount));
        //        //}
        //    }
        //    //totalCost -= deposit;
        //}

        //private string CalcPartCost(DateTime start, DateTime end, long price, float discount)
        //{
        //    int hours;

        //    hours = calc.getIntervalInHours(start, end);
        //    if (hours == 0)
        //    {
        //        hours = 1;
        //    }

        //    return ((long)Math.Round((double)(hours * (price / 10) * (1 - discount)), 0)).ToString("C0");

        //}

        public void Printing()
        {
            //long rtpID;
            PrintDialog pDialog = new PrintDialog();
            try
            {
                FileStream fs = new FileStream(@"Kölcsönzés_Feltételek.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                contract = sr.ReadToEnd();
            }
            catch (Exception)
            {
                contract = string.Empty;
            }

            //CalcTotalCost();

            try
            {
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintRent);

                    if (pDialog.ShowDialog() == DialogResult.OK)
                    {
                        pd.PrinterSettings = pDialog.PrinterSettings;
                        // Print the document.
                        pd.Print();
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void pd_PrintRent(object sender, PrintPageEventArgs ev)
        {
            int leftMargin = ev.PageBounds.Left + 50;
            int topMargin = ev.PageBounds.Top + 50;
            int rightMargin = ev.PageBounds.Right - 50;
            int bottomMargin = ev.PageBounds.Bottom - 50;
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            StringFormat currencyFormat = new StringFormat();
            currencyFormat.Alignment = StringAlignment.Far;
            //StringFormat justifiedFormat = new StringFormat();
            //justifiedFormat. = StringAlignment

            Point address = new Point(leftMargin, topMargin);
            Point openTime = new Point(rightMargin-250, topMargin);
            RectangleF title = new RectangleF(leftMargin, topMargin + 100, ev.PageBounds.Width - 100, 50);
            RectangleF subTitle = new RectangleF(title.Left, title.Top + 30, title.Width, 50);
            Point personDatas = new Point(leftMargin, topMargin + 180);
            Point rentDatas = new Point(leftMargin, topMargin + 310);
            Point sumDatas = new Point(leftMargin, bottomMargin - 600);
            RectangleF commentRect = new RectangleF(leftMargin + 50, bottomMargin - 490, ev.PageBounds.Width - 100, 50);
            RectangleF contractRect = new RectangleF(leftMargin, bottomMargin - 400, ev.PageBounds.Width - 100, 250);
            Point signs = new Point(ev.PageBounds.Left + 50, ev.PageBounds.Bottom - 80);
            
            ev.Graphics.DrawString(Properties.Settings.Default.CompanyName, new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, address, new StringFormat());
            ev.Graphics.DrawString(Properties.Settings.Default.CompanyAddress + 
                "\nTel.: " + Properties.Settings.Default.CompanyPhone1 + "\n        " + Properties.Settings.Default.CompanyPhone2, 
                new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, address.X, address.Y + 30, new StringFormat());            
            ev.Graphics.DrawString("Nyitva:", new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, openTime, new StringFormat());
            ev.Graphics.DrawString(Properties.Settings.Default.OpenTime.Replace("|NL|", Environment.NewLine), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, openTime.X, openTime.Y + 30, new StringFormat());
            ev.Graphics.DrawString("BÉRLETI SZERZŐDÉS", new Font(FontFamily.GenericSerif, 16, FontStyle.Bold), Brushes.Black, title, centerFormat);
            ev.Graphics.DrawString("Mely létrejött a " + Properties.Settings.Default.CompanyName + " - továbbiakban bérbeadó -, és Bérlő között",
                new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, subTitle, centerFormat);
            ev.Graphics.DrawString("Bérlő neve:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.rentals[0].customer.customerName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Címe:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 20, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.rentals[0].customer.GetAddressString(), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 20, new StringFormat());
            ev.Graphics.DrawString("Szem. ig. sz., tel.:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.rentals[0].customer.IDNumber + ", " + rentalsToPrint.rentals[0].customer.customerPhone, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 150, personDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString("Cég (megbízó):", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 70, new StringFormat());
            if (rentalsToPrint.rentals[0].contact != null)
            {
                ev.Graphics.DrawString(rentalsToPrint.rentals[0].contact.customerName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 150, personDatas.Y + 70, new StringFormat()); 
            }
            ev.Graphics.DrawString("Bérleti idő kezdete:                            napján                     órától", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 100, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.rentals[0].rentalStart.ToString("d"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 150, personDatas.Y + 100, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.rentals[0].rentalStart.ToString("t"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 350, personDatas.Y + 100, new StringFormat());
            ev.Graphics.DrawString("Bérleti tárgy", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas, new StringFormat());
            ev.Graphics.DrawString("Gyártó", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 140, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Azonosító", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 270, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Bérleti idő lejár", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 400, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Nap", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 500, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Óra", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 530, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Egységár", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 560, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Eng. %", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 630, rentDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Díj", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, rentDatas.X + 700, rentDatas.Y, new StringFormat());
            int posY = rentDatas.Y + 30;
            foreach (var item in rentalsToPrint.rentals)
            {
                //if (item.isPaid != true)
                //{
                    ev.Graphics.DrawString(item.tool.toolName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X, posY, new StringFormat());
                    ev.Graphics.DrawString(item.tool.toolManufacturer, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 140, posY, new StringFormat());
                    if (item.tool.serialNumber == null)
                    {
                        ev.Graphics.DrawString(item.tool.IDNumber, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 270, posY, new StringFormat()); 
                    }
                    else
                    {
                        ev.Graphics.DrawString(item.tool.serialNumber, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 270, posY, new StringFormat()); 
                    }
                    ev.Graphics.DrawString(item.rentalEnd.ToShortDateString(), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 410, posY, new StringFormat());
                    ev.Graphics.DrawString(calc.getIntervalDays(item.rentalStart, item.rentalEnd).ToString(), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 500, posY, new StringFormat());
                    ev.Graphics.DrawString(calc.getIntervalHours(item.rentalStart, item.rentalEnd).ToString(), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 530, posY, new StringFormat());
                    ev.Graphics.DrawString(((long)item.actualPrice).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(rentDatas.X + 530, posY, 90, 20), currencyFormat);
                    ev.Graphics.DrawString(((float)item.discount).ToString("0 %"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, rentDatas.X + 630, posY, new StringFormat());
                    ev.Graphics.DrawString(calc.getRentCost(item.rentalStart, item.rentalEnd, (long)item.actualPrice, (float)item.discount).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(rentDatas.X + 660, posY, 90, 20), currencyFormat);
                    posY += 20; 
                //}

            }
            ev.Graphics.DrawString("Díj a lejárat idejéig:  " + rentalsToPrint.TotalCost.ToString("C0") + "\nLetét:  " + rentalsToPrint.deposit.ToString("C0") + ", azaz", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas, new StringFormat());
            ev.Graphics.DrawString("A bérleti díj a bérelt tárgy visszaszállításának napján került számlázásra, addig letétként szerepel.", new Font(FontFamily.GenericSerif, 10, FontStyle.Regular), Brushes.Black, sumDatas.X, sumDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString("A bérelt tárgyat megtisztítva kérjük visszaszállítani!", new Font(FontFamily.GenericSerif, 10, FontStyle.Regular), Brushes.Black, sumDatas.X, sumDatas.Y + 67, new StringFormat());
            ev.Graphics.DrawString("A Tisztítás felára: " + Properties.Settings.Default.CostOfClean + ",- Ft.", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas.X + 290, sumDatas.Y + 65, new StringFormat());
            ev.Graphics.DrawString("Megjegyzés:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, sumDatas.X, sumDatas.Y + 85, new StringFormat());
            ev.Graphics.DrawString(rentalsToPrint.comment, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, commentRect, new StringFormat());
            ev.Graphics.DrawString(contract, new Font(FontFamily.GenericSerif, 8, FontStyle.Regular), Brushes.Black, contractRect, new StringFormat());
            ev.Graphics.DrawString("Fent leírt feltételeket elolvastam, és magamra nézve tudomásul vettem:", new Font(FontFamily.GenericSerif, 8, FontStyle.Italic), Brushes.Black, contractRect.Left + 20, contractRect.Top + contractRect.Height + 20, new StringFormat());
            ev.Graphics.DrawString("Visszavétel:", new Font(FontFamily.GenericSerif, 10, FontStyle.Italic), Brushes.Black, contractRect.Left + 450, contractRect.Top + contractRect.Height + 50, new StringFormat());
            ev.Graphics.DrawString(".................................            .................................            .................................            .................................", new Font(FontFamily.GenericSerif, 12, FontStyle.Italic), Brushes.Black, signs, new StringFormat());
            ev.Graphics.DrawString("            bérlő                                   bérbeadó                                bérlő                                   bérbeadó", new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, signs.X, signs.Y + 20, new StringFormat());





            ev.HasMorePages = false;
        }
    }
}
