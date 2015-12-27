using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Configuration;
using SQLConnectionLib;
using System.IO;

namespace GyorokRentService.Print
{
    class printService
    {
        ServiceSum serviceToPrint;
        List<Parts> builtInParts;
        servicePrintType pType;
        string contract;
        //dbGyorokEntities db;

        public printService(ServiceSum sw, servicePrintType printType)
        {
            pType = printType;
            serviceToPrint = sw;
            Printing();
        }

        public printService(ServiceWorksheets sw, servicePrintType printType)
        {
            //db = new dbGyorokEntities(); 
            pType = printType;
            //serviceToPrint = SQLConnection.Execute.ServiceSumView.Single(ss => ss.worksheetID == sw.worksheetID);
            Printing();
        }

        public void Printing()
        {
            PrintDialog pDialog = new PrintDialog();
            FileStream fs = new FileStream(@"Szervíz_Feltételek.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            contract = sr.ReadToEnd();

            try
            {
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
            int yPos;
            long materialCost;
            builtInParts = new List<Parts>();
            //builtInParts = SQLConnection.Execute.PartsTable.Where(pt => pt.serviceID == serviceToPrint.worksheetID).ToList();
            materialCost = 0;
            foreach (var item in builtInParts)
            {
                materialCost += item.partQuantity * item.partPrice;
            }

            int leftMargin = ev.PageBounds.Left + 50;
            int topMargin = ev.PageBounds.Top + 50;
            int rightMargin = ev.PageBounds.Right - 50;
            int bottomMargin = ev.PageBounds.Bottom - 50;
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            StringFormat currencyFormat = new StringFormat();
            currencyFormat.Alignment = StringAlignment.Far;
            Pen p = new Pen(Color.Black, 1);

            Point address = new Point(leftMargin, topMargin);
            Point openTime = new Point(rightMargin - 250, topMargin);
            RectangleF title = new RectangleF(leftMargin, topMargin + 100, ev.PageBounds.Width - 100, 50);
            //RectangleF subTitle = new RectangleF(title.Left, title.Top + 30, title.Width, 50);
            Point personDatas = new Point(leftMargin, topMargin + 180);
            Point serviceDatas = new Point(leftMargin, topMargin + 310);
            Point orderDatas = new Point(leftMargin, topMargin + 490);
            Point builtDatas = new Point(leftMargin, topMargin + 540);
            RectangleF contractRect = new RectangleF(leftMargin, bottomMargin - 400, ev.PageBounds.Width - 100, 250);
            Point comment = new Point(leftMargin, topMargin + 830);
            Point signs = new Point(leftMargin, bottomMargin - 80);

            string hasWarranty = "NEM";
            string quotReq = "NEM";

            if (serviceToPrint.hasWarranty) { hasWarranty = "IGEN"; }
            if (serviceToPrint.quotRequested) { quotReq = "IGEN"; }

            ev.Graphics.DrawString(Properties.Settings.Default.CompanyName, new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, address, new StringFormat());
            ev.Graphics.DrawString(Properties.Settings.Default.CompanyAddress +
                "\nTel.: " + Properties.Settings.Default.CompanyPhone1 + "\n        " + Properties.Settings.Default.CompanyPhone2,
                new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, address.X, address.Y + 30, new StringFormat());
            ev.Graphics.DrawString("Nyitva:", new Font(FontFamily.GenericSerif, 14, FontStyle.Bold), Brushes.Black, openTime, new StringFormat());
            ev.Graphics.DrawString(Properties.Settings.Default.OpenTime.Replace("|NL|", Environment.NewLine), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, openTime.X, openTime.Y + 30, new StringFormat());
            switch (pType)
            {
                case servicePrintType.bringIn:
                    ev.Graphics.DrawString("RENDELÉS FELVÉTEL", new Font(FontFamily.GenericSerif, 16, FontStyle.Bold), Brushes.Black, title, centerFormat); 
                    break;
                case servicePrintType.takesAway:
                    ev.Graphics.DrawString("MUNKALAP", new Font(FontFamily.GenericSerif, 16, FontStyle.Bold), Brushes.Black, title, centerFormat); 
                    break;
                case servicePrintType.quotation:
                    ev.Graphics.DrawString("AJÁNLAT", new Font(FontFamily.GenericSerif, 16, FontStyle.Bold), Brushes.Black, title, centerFormat); 
                    break;
                default:
                    break;
            }
            

            ev.Graphics.DrawString("Munkalap száma:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.serviceStart.Year.ToString() + "/" + serviceToPrint.yearCounter.ToString("D4"), new Font(FontFamily.GenericSerif, 16, FontStyle.Regular), Brushes.Black, personDatas.X + 150, personDatas.Y - 5, new StringFormat());
            ev.Graphics.DrawString("Felvéve:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.serviceStart.ToString("D"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString("Vevő:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, personDatas.X, personDatas.Y + 60, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.customerName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 60, new StringFormat());
            if (serviceToPrint.cityID != null)
            {
                ev.Graphics.DrawString(serviceToPrint.postalCode + " " + serviceToPrint.city + ", " + serviceToPrint.customerAddress, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 80, new StringFormat());
            }
            else
            {
                ev.Graphics.DrawString(serviceToPrint.customerAddress, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 80, new StringFormat());
            }            
            ev.Graphics.DrawString(serviceToPrint.customerPhone, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, personDatas.X + 100, personDatas.Y + 100, new StringFormat());
            ev.Graphics.DrawLine(p, leftMargin, serviceDatas.Y, rightMargin, serviceDatas.Y);
            ev.Graphics.DrawString("Gép típus:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 10, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.deviceName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 10, new StringFormat());
            ev.Graphics.DrawString("Gyártó:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.deviceManufacturer, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString("Azonosító:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.deviceID, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString("Gyári szám:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 70, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.serialID, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 70, new StringFormat());
            ev.Graphics.DrawString("Tartozékok:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 90, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.plusParts, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 90, new StringFormat());
            ev.Graphics.DrawString("Vásárlás kelte:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 400, serviceDatas.Y + 10, new StringFormat());
            if (serviceToPrint.buyDate != null)
            {
                ev.Graphics.DrawString(((DateTime)serviceToPrint.buyDate).ToString("D"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 550, serviceDatas.Y + 10, new StringFormat()); 
            }
            ev.Graphics.DrawString("Garanciajegy:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 400, serviceDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString(hasWarranty, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 550, serviceDatas.Y + 30, new StringFormat());
            ev.Graphics.DrawString("száma:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 450, serviceDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.warrantyNumber, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 550, serviceDatas.Y + 50, new StringFormat());
            ev.Graphics.DrawLine(p, leftMargin, serviceDatas.Y + 120, rightMargin, serviceDatas.Y + 120);
            ev.Graphics.DrawString("Hiba:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, serviceDatas.X + 20, serviceDatas.Y + 130, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.errorDescription, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, serviceDatas.X + 120, serviceDatas.Y + 130, new StringFormat());
            ev.Graphics.DrawLine(p, leftMargin, serviceDatas.Y + 160, rightMargin, serviceDatas.Y + 160);
            ev.Graphics.DrawString("Megrendelés:            Árajánlat - ", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, orderDatas, new StringFormat());
            ev.Graphics.DrawString(quotReq, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, orderDatas.X + 250, orderDatas.Y, new StringFormat());
            ev.Graphics.DrawString("Max. jav. összeg:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, orderDatas.X + 400, orderDatas.Y, new StringFormat());
            if (serviceToPrint.maxCost != null)
            {
                ev.Graphics.DrawString(((long)serviceToPrint.maxCost).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, orderDatas.X + 550, orderDatas.Y, new StringFormat());
            }
            
            switch (pType)
            {
                case servicePrintType.bringIn:
                    ev.Graphics.DrawString(contract, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, contractRect, new StringFormat());            
                    break;
                case servicePrintType.takesAway:
                case servicePrintType.quotation:
                    ev.Graphics.DrawString("Beépített alkatrészek:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, builtDatas, new StringFormat());
                    ev.Graphics.DrawString("Alkatrész", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 30, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawString("Gyártó", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 230, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawString("Azonosító", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 370, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawString("Egységár", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 480, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawString("db", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 580, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawString("Összeg", new Font(FontFamily.GenericSerif, 10, FontStyle.Bold), Brushes.Black, builtDatas.X + 650, builtDatas.Y + 30, new StringFormat());
                    ev.Graphics.DrawLine(p, leftMargin, builtDatas.Y + 45, rightMargin, builtDatas.Y + 45);
                    yPos = builtDatas.Y + 55;
                    foreach (var item in builtInParts)
                    {
                        ev.Graphics.DrawString(item.partName, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, builtDatas.X, yPos, new StringFormat());
                        ev.Graphics.DrawString(item.partManufacturer, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, builtDatas.X + 200, yPos, new StringFormat());
                        ev.Graphics.DrawString(item.partIDNumber, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, builtDatas.X + 360, yPos, new StringFormat());
                        ev.Graphics.DrawString(item.partPrice.ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 450, yPos, 90, 20), currencyFormat);
                        ev.Graphics.DrawString(item.partQuantity.ToString(), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 520, yPos, 90, 20), currencyFormat);
                        ev.Graphics.DrawString((item.partPrice * item.partQuantity).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 630, yPos, 90, 20), currencyFormat);
                
                        yPos += 20;
                    }
                    ev.Graphics.DrawLine(p, leftMargin, yPos + 5, rightMargin, yPos + 5);
                    ev.Graphics.DrawString(materialCost.ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 630, yPos + 10, 90, 20), currencyFormat);
                    ev.Graphics.DrawString("Munkadíj:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, builtDatas.X + 500, yPos + 30, new StringFormat());
                    if (serviceToPrint.serviceCost != null)
                    {
                        ev.Graphics.DrawString(((long)serviceToPrint.serviceCost).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 630, yPos + 30, 90, 20), currencyFormat);
                        ev.Graphics.DrawString(((long)serviceToPrint.serviceCost * (1 - serviceToPrint.discount) + materialCost).ToString("C0"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 630, yPos + 80, 90, 20), currencyFormat); 
                    }
                    ev.Graphics.DrawString("Kedvezmény:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, builtDatas.X + 500, yPos + 50, new StringFormat());
                    ev.Graphics.DrawString(serviceToPrint.discount.ToString("0 %"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(builtDatas.X + 630, yPos + 50, 90, 20), currencyFormat);
                    ev.Graphics.DrawLine(p, leftMargin + 630, yPos + 70, rightMargin, yPos + 70);                    
                    break;
                default:
                    break;
            }
            ev.Graphics.DrawString("Megjegyzés:", new Font(FontFamily.GenericSerif, 12, FontStyle.Bold), Brushes.Black, comment, new StringFormat());
            ev.Graphics.DrawString(serviceToPrint.wsComment, new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, new RectangleF(comment.X, comment.Y + 20, ev.PageBounds.Width - 100, 100), new StringFormat());
            ev.Graphics.DrawString("                            .................................                                         .................................", new Font(FontFamily.GenericSerif, 12, FontStyle.Italic), Brushes.Black, signs, new StringFormat());
            ev.Graphics.DrawString("                                   átvevő                                                             megrendelő", new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, signs.X, signs.Y + 20, new StringFormat());
            ev.Graphics.DrawString("Dunaújváros, " + DateTime.Today.ToString("D"), new Font(FontFamily.GenericSerif, 12, FontStyle.Regular), Brushes.Black, signs.X, signs.Y + 60, new StringFormat());

            
            
            ev.HasMorePages = false;
        }
    }

    public enum servicePrintType { bringIn, takesAway, quotation }
}
