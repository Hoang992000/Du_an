using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Linq;

namespace RFIDApp.Helper
{
    public abstract class ExcelHelperExtention
    {
        private string GetCellReference(int columnNumber, int rowNumber)
        {
            string columnLetter = "";
            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                columnLetter = (char)(65 + remainder) + columnLetter;
                columnNumber = (columnNumber - remainder) / 26;
            }

            return columnLetter + rowNumber.ToString();
        }

        protected Cell ConstructCellWithReference(string value, CellValues dataType, UInt32Value styleIndex, int col, int row)
        {

            Cell cell = new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex,
                CellReference = GetCellReference(col, row)
            };
            return cell;
        }

        protected Cell ConstructCell(string value, CellValues dataType, UInt32Value styleIndex)
        {

            Cell cell = new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex,
            };
            return cell;
        }

        protected Cell ConstructCell(int value, CellValues dataType, UInt32Value styleIndex)
        {

            Cell cell = new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex,
            };
            return cell;
        }


        protected Border GenerateBorder()
        {
            Border border2 = new Border();

            DocumentFormat.OpenXml.Spreadsheet.LeftBorder leftBorder2 = new DocumentFormat.OpenXml.Spreadsheet.LeftBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color1 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            leftBorder2.Append(color1);

            DocumentFormat.OpenXml.Spreadsheet.RightBorder rightBorder2 = new DocumentFormat.OpenXml.Spreadsheet.RightBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color2 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            rightBorder2.Append(color2);

            DocumentFormat.OpenXml.Spreadsheet.TopBorder topBorder2 = new DocumentFormat.OpenXml.Spreadsheet.TopBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color3 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            topBorder2.Append(color3);

            DocumentFormat.OpenXml.Spreadsheet.BottomBorder bottomBorder2 = new DocumentFormat.OpenXml.Spreadsheet.BottomBorder() { Style = BorderStyleValues.Thin };
            DocumentFormat.OpenXml.Spreadsheet.Color color4 = new DocumentFormat.OpenXml.Spreadsheet.Color() { Indexed = (UInt32Value)64U };

            bottomBorder2.Append(color4);
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border2.Append(leftBorder2);
            border2.Append(rightBorder2);
            border2.Append(topBorder2);
            border2.Append(bottomBorder2);
            border2.Append(diagonalBorder2);

            return border2;
        }

        protected Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            DocumentFormat.OpenXml.Spreadsheet.Fonts fonts = new DocumentFormat.OpenXml.Spreadsheet.Fonts(
                new DocumentFormat.OpenXml.Spreadsheet.Font(),
                new DocumentFormat.OpenXml.Spreadsheet.Font(new FontSize() { Val = 18 }),          
                new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "ff0000" }),
                new DocumentFormat.OpenXml.Spreadsheet.Font(new DocumentFormat.OpenXml.Spreadsheet.Color() { Rgb = "33cc00" }),
                new DocumentFormat.OpenXml.Spreadsheet.Font(new FontSize() { Val = 30 })
                );
            Borders borders = new Borders(new Border(), GenerateBorder());

            Fills fills = new Fills(
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = PatternValues.None }),
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill() { PatternType = PatternValues.Gray125 }),
                    new DocumentFormat.OpenXml.Spreadsheet.Fill(new DocumentFormat.OpenXml.Spreadsheet.PatternFill(
                        new DocumentFormat.OpenXml.Spreadsheet.ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } })
                    { PatternType = PatternValues.Solid })
                );

            Alignment alignment = new Alignment();
            alignment.WrapText = true;
            alignment.Horizontal = HorizontalAlignmentValues.Left;
            alignment.Vertical = VerticalAlignmentValues.Center;


            CellFormats cellFormats = new CellFormats(
                new CellFormat(),
                new CellFormat() { FontId = 0, BorderId = 1, FillId = 0 },
                new CellFormat() { FontId = 1, BorderId = 0, FillId = 0 },
                new CellFormat() { FontId = 0, BorderId = 1, Alignment = alignment, },
                new CellFormat() { FontId = 2, BorderId = 1, FillId = 0 },
                new CellFormat() { FontId = 3, BorderId = 1, FillId = 0 },
                new CellFormat() { FontId = 4, BorderId = 0, FillId = 0, 
                    Alignment = new Alignment() { WrapText = false, Horizontal= HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center } 
                }, new CellFormat() { FontId = 0, BorderId = 0, FillId = 0, 
                    Alignment = new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                }, new CellFormat()
                {
                    FontId = 0,
                    BorderId = 1,
                    FillId = 0,
                    Alignment = new Alignment() { WrapText = false, Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
                });

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);


            return styleSheet;
        }

        //protected WorksheetDrawing GenerateImage(WorksheetPart worksheetPart, int colNumber, int rowNumber)
        //{
        //    var drawingsPart = worksheetPart.DrawingsPart;
        //    if (drawingsPart == null)
        //        drawingsPart = worksheetPart.AddNewPart<DrawingsPart>();

        //    if (!worksheetPart.Worksheet.ChildElements.OfType<Drawing>().Any())
        //    {
        //        worksheetPart.Worksheet.Append(new Drawing { Id = worksheetPart.GetIdOfPart(drawingsPart) });
        //    }

        //    if (drawingsPart.WorksheetDrawing == null)
        //    {
        //        drawingsPart.WorksheetDrawing = new WorksheetDrawing();
        //    }

        //    var worksheetDrawing = drawingsPart.WorksheetDrawing;


        //    var imagePart = drawingsPart.AddImagePart(ImagePartType.Png);
        //    var resourceId = 0;/*Resource.Drawable.nhatminhlogo;*/
        //    var imageStream = Application.Context.Resources.OpenRawResource(resourceId);

        //    imagePart.FeedData(imageStream);


        //    Resources res = Application.Context.Resources;
        //    Bitmap bm = BitmapFactory.DecodeResource(res, resourceId);
        //    //DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();
        //    var extentsCx = (long)bm.Width * (long)((float)914400 / bm.Density);
        //    var extentsCy = (long)bm.Height * (long)((float)914400 / bm.Density);
        //    bm.Recycle();


        //    var nvps = worksheetDrawing.Descendants<Xdr.NonVisualDrawingProperties>();
        //    var drawingId = nvps.Count() > 0 ?
        //        (UInt32Value)worksheetDrawing.Descendants<Xdr.NonVisualDrawingProperties>().Max(p => p.Id.Value) + 1 : 1U;
        //    var colOffset = 0;
        //    var rowOffset = 0;

        //    var oneCellAnchor = new Xdr.OneCellAnchor(
        //        new Xdr.FromMarker
        //        {
        //            ColumnId = new Xdr.ColumnId((colNumber - 1).ToString()),
        //            RowId = new Xdr.RowId((rowNumber - 1).ToString()),
        //            ColumnOffset = new Xdr.ColumnOffset(colOffset.ToString()),
        //            RowOffset = new Xdr.RowOffset(rowOffset.ToString())
        //        },
        //        new Xdr.Extent { Cx = extentsCx, Cy = extentsCy },
        //        new Xdr.Picture(
        //            new Xdr.NonVisualPictureProperties(
        //                new Xdr.NonVisualDrawingProperties { Id = drawingId, Name = "Picture " + drawingId, Description = "Logo" },
        //                new Xdr.NonVisualPictureDrawingProperties(new A.PictureLocks { NoChangeAspect = true })
        //            ),
        //            new Xdr.BlipFill(
        //                new A.Blip { Embed = drawingsPart.GetIdOfPart(imagePart), CompressionState = A.BlipCompressionValues.Print },
        //                new A.Stretch(new A.FillRectangle())
        //            ),
        //            new Xdr.ShapeProperties(
        //                new A.Transform2D(
        //                    new A.Offset { X = 0, Y = 0 },
        //                    new A.Extents { Cx = extentsCx, Cy = extentsCy }
        //                ),
        //                new A.PresetGeometry { Preset = A.ShapeTypeValues.Rectangle }
        //            )
        //        ),
        //        new Xdr.ClientData()
        //    );
        //    worksheetDrawing.Append(oneCellAnchor);
        //    return worksheetDrawing;
        //}

        protected string InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            shareStringPart.SharedStringTable = new SharedStringTable();

            int i = 0;

            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i.ToString();
                }

                i++;
            }

            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i.ToString();
        }

        protected string CellValue(Cell cell, SharedStringTable sharedStringTable)
        {
            string result = "";
            if (cell != null)
            {
                string cellValue = cell.InnerText;

                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    int sharedStringIndex;
                    if (int.TryParse(cellValue, out sharedStringIndex))
                    {
                        cellValue = sharedStringTable.ElementAt(sharedStringIndex).InnerText;
                    }
                }

                return cellValue;

            }
            return result;
        }

        protected int CheckIsExist(string data, List<string> list)
        {
            int existFlag = -1;
            for (int i = 0; i < list.Count; i++)
            {
                string tempStr = list[i];
                if (data == tempStr)
                {
                    existFlag = i;
                    break;
                }
            }
            return existFlag;
        }
    }
}
