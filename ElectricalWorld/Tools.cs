using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

namespace ElectricalWorld
{
    class Tools
    {
        public static void CreateInvoice(PO.Order order)
        {
            Document document = new Document();

            //document.Info.Title = "invoice";
            //document.Info.Subject = "Demonstrates how to create an invoice.";
            //document.Info.Author = "Stefan Lange";
            // Get the predefined style Normal.

            Style style = document.Styles["Normal"];

            // Because all styles are derived from Normal, the next line changes the
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.

            style.Font.Name = "Verdana";

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            // Create a new style called Reference based on style Normal

            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);




            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();

            

            // Create footer
            Paragraph paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("Household Goods World ltd · 9 Egerton Road · N16 6UE London · England");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the text frame for the address
            TextFrame addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "5.5cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;


            // Create the text frame for the address
            TextFrame companyFrame = section.AddTextFrame();
            companyFrame.Height = "3.0cm";
            companyFrame.Width = "7.0cm";
            companyFrame.Left = ShapePosition.Outside;
            companyFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            companyFrame.Top = "4.5cm";
            companyFrame.RelativeVertical = RelativeVertical.Page;

            // Put sender in address frame
            //paragraph = addressFrame.AddParagraph("Household Goods World ltd · 9 Egerton Road · N16 6UE London");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("INVOICE #" + order.OrderID, TextFormat.Bold);
            paragraph.AddTab();
            //paragraph.AddText("London, ");
            //paragraph.AddDateField("dd.MM.yyyy");
            paragraph.AddText(order.OrderTime.ToShortDateString());

            // Create the item table
            Table table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Colors.Black;
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("9cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Left;


            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.LightGray;
            row.Cells[0].AddParagraph("Item");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].AddParagraph("Brand");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph("Model Number");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph("Description");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].AddParagraph("Price");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;



            //row = table.AddRow();
            //row.HeadingFormat = true;
            //row.Format.Alignment = ParagraphAlignment.Center;
            //row.Format.Font.Bold = true;
            //row.Shading.Color = Colors.LightGray;
            //row.Cells[1].AddParagraph("Quantity");
            //row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[2].AddParagraph("Unit Price");
            //row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[3].AddParagraph("Discount (%)");
            //row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            //row.Cells[4].AddParagraph("Taxable");
            //row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            table.SetEdge(0, 0, 5, 1, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);



            // Fill address in address text frame
            Paragraph paragraph1 = addressFrame.AddParagraph();
            paragraph1.AddText(order.Customer.ToString());
            paragraph1.AddLineBreak();
            paragraph1.AddText(order.Customer.Address);
            paragraph1.AddLineBreak();
            paragraph1.AddText(order.Customer.PostCode + " " + "London");


            // Fill address in company text frame
            Paragraph paragraph2 = companyFrame.AddParagraph();
            paragraph2.Format.Alignment = ParagraphAlignment.Right;
            paragraph2.Format.Font.Size = 12;
            paragraph2.AddFormattedText("Household Goods World ltd", TextFormat.Bold);
            paragraph2.AddLineBreak();
            paragraph2.AddText("9 Egerton Road");
            paragraph2.AddLineBreak();
            paragraph2.AddText("London N16 6UE");

            // Iterate the invoice items

            //XPathNodeIterator iter = navigator.Select("/invoice/items/*");
            var iter = order.Items.GetEnumerator();
            while (iter.MoveNext())
            {
                var item = iter.Current;
                double price = item.Price;

                // Each item fills two rows
                Row row1 = table.AddRow();
                row1.TopPadding = 1.5;
                row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;

                row1.Cells[0].AddParagraph(item.ItemID);
                row1.Cells[1].AddParagraph(item.Brand);
                row1.Cells[2].AddParagraph(item.ModelNumber);
                row1.Cells[3].AddParagraph(item.Description);
                row1.Cells[4].AddParagraph(item.Price.ToString("C", new CultureInfo("en-GB", false).NumberFormat));
                

                table.SetEdge(0, table.Rows.Count - 2, 5, 2, Edge.Box, BorderStyle.Single, 0.75);
            }

            // Add an invisible row as a space line to the table
            Row row2 = table.AddRow();
            row.Borders.Visible = false;

            // Add the total price row
            row2 = table.AddRow();
            row2.Cells[0].Borders.Visible = false;
            row2.Cells[0].AddParagraph("Total Price");
            row2.Cells[0].Format.Font.Bold = true;
            row2.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row2.Cells[0].MergeRight = 3;
            row2.Cells[4].AddParagraph(order.TotalPrice.ToString("C", new CultureInfo("en-GB", false).NumberFormat));

            //// Add the VAT row
            //row = table.AddRow();
            //row.Cells[0].Borders.Visible = false;
            //row.Cells[0].AddParagraph("VAT (19%)");
            //row.Cells[0].Format.Font.Bold = true;
            //row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            //row.Cells[0].MergeRight = 4;
            //row.Cells[5].AddParagraph((0.19 * totalExtendedPrice).ToString("0.00") + " €");

            // Add the additional fee row
            Row row3 = table.AddRow();
            row3.Cells[0].Borders.Visible = false;
            row3.Cells[0].AddParagraph("Shipping and Handling");
            row3.Cells[4].AddParagraph(0.ToString("C", new CultureInfo("en-GB", false).NumberFormat));
            row3.Cells[0].Format.Font.Bold = true;
            row3.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row3.Cells[0].MergeRight = 3;

            // Add the total due row
            Row row4 = table.AddRow();
            row4.Cells[0].AddParagraph("Total Due");
            row4.Cells[0].Borders.Visible = false;
            row4.Cells[0].Format.Font.Bold = true;
            row4.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            row4.Cells[0].MergeRight = 3;
            row4.Cells[4].AddParagraph(order.TotalPrice.ToString("C", new CultureInfo("en-GB", false).NumberFormat));

            // Set the borders of the specified cell range
            table.SetEdge(4, table.Rows.Count - 3, 1, 3, Edge.Box, BorderStyle.Single, 0.75);

            // Add the notes paragraph
            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.SpaceBefore = "1cm";
            paragraph.Format.Borders.Width = 0.75;
            paragraph.Format.Borders.Distance = 3;
            //paragraph.Format.Borders.Color = TableBorder;
            //paragraph.Format.Shading.Color = TableGray;
            //item = SelectItem("/invoice");
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddFormattedText("Thank you for your business - we appreciate it very much.", TextFormat.Italic);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            string fileName = order.OrderID + ".pdf";
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\Invoice\")) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\Invoice\");
            pdfRenderer.PdfDocument.Save(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\Invoice\" + fileName);
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\ElectricalWorld\Invoice\" + fileName);
        }
    }
}
