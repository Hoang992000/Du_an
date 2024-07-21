namespace RFIDApp.Helper
{
    //public class ExcelHelper : ExcelHelperExtention
    //{
    //    public async Task<List<SyncPurchaseOrderModel>> ImportPurchaseOrder(string filePath, List<ProductModel> listProduct)
    //    {
    //        try
    //        {
    //            //Load Order Column
    //            string OrderPO = ConfigurationManager.AppSettings["OrderPO"];
    //            string OrderCtn = ConfigurationManager.AppSettings["OrderCtn"];
    //            string OrderDes = ConfigurationManager.AppSettings["OrderDes"];
    //            string OrderQuantity = ConfigurationManager.AppSettings["OrderQuantiy"];
    //            string OrderSku = ConfigurationManager.AppSettings["OrderSku"];

    //            if (
    //                string.IsNullOrEmpty(OrderPO) ||
    //                string.IsNullOrEmpty(OrderCtn) ||
    //                string.IsNullOrEmpty(OrderDes) ||
    //                string.IsNullOrEmpty(OrderQuantity) ||
    //                string.IsNullOrEmpty(OrderSku)
    //                )
    //            {
    //                throw new Exception("Vui lòng cài đặt cấu hình tệp tin mẫu");
    //            }


    //            List<SyncPurchaseOrderModel> poResult = new List<SyncPurchaseOrderModel>();
    //            TaskCompletionSource<List<SyncPurchaseOrderModel>> tcs = new TaskCompletionSource<List<SyncPurchaseOrderModel>>();
    //            List<string> listPO = new List<string>();
    //            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
    //            {
    //                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

    //                Workbook workbook = workbookPart.Workbook;
    //                Sheets sheets = workbook.Sheets;
    //                Sheet mySheet = null;
    //                List<Sheet> listSheet = new List<Sheet>();
    //                foreach (Sheet sheet in sheets)
    //                {
    //                    if(sheet.State == null)
    //                    {
    //                        listSheet.Add(sheet);
    //                    }
    //                }

    //                if (listSheet.Count > 1)
    //                {
    //                    throw new Exception("Có nhiều hơn 1 sheet trong tệp dữ liệu");
    //                }

    //                mySheet = listSheet[0];

    //                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(mySheet.Id);

    //                //WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

    //                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
    //                SharedStringTable sharedStringTable = sharedStringPart.SharedStringTable;

    //                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

    //                //List<SheetData> listsheetData = worksheetPart.Worksheet.Elements<SheetData>().ToList();

    //                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
    //                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

    //                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
    //                //int totalData = sheetData.Elements<Row>().Count();

    //                int current = 0;


    //                while (reader.Read())
    //                {
    //                    if (reader.ElementType == typeof(Row))
    //                    {
    //                        Row row = (Row)reader.LoadCurrentElement();
    //                        if (current == 0)
    //                        {
    //                            current++;
    //                            continue;
    //                        }

    //                        var cell = row.Descendants<Cell>().ToList();
    //                        IEnumerable<string> col = row.Descendants<Cell>()
    //                                    .Select(selectCell => selectCell.InnerText);
    //                        Cell cellCartonNumber = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == OrderCtn).FirstOrDefault();
    //                        Cell cellPo = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == OrderPO).FirstOrDefault();
    //                        Cell cellSession = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == OrderDes).FirstOrDefault();
    //                        Cell cellQuantity = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == OrderQuantity).FirstOrDefault();
    //                        Cell cellSku = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == OrderSku).FirstOrDefault();

    //                        string cartonNumber = CellValue(cellCartonNumber, sharedStringTable) != "" ? CellValue(cellCartonNumber, sharedStringTable).Replace(" ", string.Empty) : "";
    //                        string po = CellValue(cellPo, sharedStringTable) != "" ? CellValue(cellPo, sharedStringTable).Replace(" ", string.Empty) : "";
    //                        string description = CellValue(cellSession, sharedStringTable) != "" ? CellValue(cellSession, sharedStringTable) : "";
    //                        string qty = CellValue(cellQuantity, sharedStringTable) != "" ? CellValue(cellQuantity, sharedStringTable).Replace(" ", string.Empty) : "";
    //                        string sku = CellValue(cellSku, sharedStringTable) != "" ? CellValue(cellSku, sharedStringTable).Replace(" ", string.Empty) : "";

    //                        if(string.IsNullOrEmpty(cartonNumber) || string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(qty))
    //                        {
    //                            continue;
    //                        }

    //                        if (!Regex.IsMatch(cartonNumber, @"^\d+$"))
    //                        {
    //                            throw new Exception(string.Format("Vui lòng kiểm tra lại định dạng dữ liệu\r\nCột {0} - Dòng {1} - Dữ liệu: {2}", OrderCtn, current, cartonNumber));
    //                        }

    //                        if (!Regex.IsMatch(sku, @"^\d{6}-\d{3}$"))
    //                        {
    //                            throw new Exception(string.Format("Vui lòng kiểm tra lại định dạng dữ liệu\r\nCột {0} - Dòng {1} - Dữ liệu: {2}", OrderSku, current, sku));
    //                        }

    //                        if (!Regex.IsMatch(qty, @"^\d+$"))
    //                        {
    //                            throw new Exception(string.Format("Vui lòng kiểm tra lại định dạng dữ liệu\r\nCột {0} - Dòng {1} - Dữ liệu: {2}", OrderQuantity, current, qty));
    //                        }


    //                        ProductModel product = listProduct.Where(s => s.SKU == sku).FirstOrDefault();
    //                        int index = CheckIsExist(po, listPO);
    //                        if (index == -1)
    //                        {
    //                            listPO.Add(po);
    //                            SyncPurchaseOrderModel purchaseOrder = new SyncPurchaseOrderModel();
    //                            purchaseOrder.PurchaseOrderNumber = po;
    //                            purchaseOrder.PurchaseOrderDescription = description;
    //                            purchaseOrder.Items = new List<PurchaseOrderDetailModel>();
    //                            if (product == null)
    //                            {
    //                                purchaseOrder.Status = "Mã sản phẩm chưa được đồng bộ";
    //                            }

    //                            purchaseOrder.Items.Add(new PurchaseOrderDetailModel()
    //                            {
    //                                CartonNumber = Convert.ToInt16(cartonNumber),
    //                                Items = new ObservableCollection<CartonDetailModel>()
    //                                {
    //                                    new CartonDetailModel()
    //                                    {
    //                                        Product = product,
    //                                        Quantity = Convert.ToInt16(qty),
    //                                        ListScanned = new ObservableCollection<PurchaseOrderScannedModel>()
    //                                    }
    //                                },
    //                            });


    //                            poResult.Add(purchaseOrder);
    //                        }
    //                        else
    //                        {
    //                            PurchaseOrderDetailModel detail = poResult[index].Items.Where(x => x.CartonNumber == Convert.ToInt16(cartonNumber)).FirstOrDefault();
    //                            if (detail == null)
    //                            {
    //                                if(product == null)
    //                                {
    //                                    product = new ProductModel();
    //                                    poResult[index].Status = "Mã sản phẩm chưa được đồng bộ";
    //                                }

    //                                poResult[index].Items.Add(new PurchaseOrderDetailModel()
    //                                {
    //                                    CartonNumber = Convert.ToInt16(cartonNumber),
    //                                    Items = new ObservableCollection<CartonDetailModel>()
    //                                    {
    //                                        new CartonDetailModel()
    //                                        {
    //                                            Product = product,
    //                                            Quantity = Convert.ToInt16(qty),
    //                                            ListScanned = new ObservableCollection<PurchaseOrderScannedModel>()
    //                                        }
    //                                    }
    //                                });
    //                            }
    //                            else
    //                            {
    //                                CartonDetailModel checkProduct = null;
    //                                if (product != null)
    //                                {
    //                                    checkProduct = detail.Items.Where(x => x.Product.SKU != null && x.Product.SKU.Equals(product.SKU)).FirstOrDefault();
    //                                }

    //                                if (checkProduct == null) // Nếu sản phẩm chưa có trong carton
    //                                {
    //                                    detail.Items.Add(new CartonDetailModel()
    //                                    {
    //                                        Product = product,
    //                                        Quantity = Convert.ToInt16(qty),
    //                                        ListScanned = new ObservableCollection<PurchaseOrderScannedModel>()
    //                                    });
    //                                }
    //                                else
    //                                {
    //                                    checkProduct.Quantity += Convert.ToInt16(qty);
    //                                }
    //                            }
    //                        }
    //                        current++;
    //                    }

    //                }
    //                tcs.TrySetResult(poResult);

    //            }
    //            poResult = await tcs.Task;
    //            return poResult;
    //        }
    //        catch (IOException)
    //        {
    //            throw new Exception("Vui lòng đóng file excel trước khi thực hiện");
    //        }catch(Exception ex)
    //        {
    //            LogControl.WriteLog(ex.ToString());
    //            throw new Exception(ex.Message);
    //        }

    //    }

    //    public async Task<List<ProductModel>> ImportProduct(string filePath)
    //    {

    //        try
    //        {
    //            string ProductUPC = ConfigurationManager.AppSettings["ProductUpc"];
    //            string ProductSKU = ConfigurationManager.AppSettings["ProductSku"];
    //            string ProductSize = ConfigurationManager.AppSettings["ProductSize"];
    //            string ProductColor = ConfigurationManager.AppSettings["ProductColor"];
    //            string ProductInSeam = ConfigurationManager.AppSettings["ProductInseam"];

    //            if(
    //                string.IsNullOrEmpty(ProductUPC)||
    //                string.IsNullOrEmpty(ProductSKU)||
    //                string.IsNullOrEmpty(ProductSize)||
    //                string.IsNullOrEmpty(ProductColor)||
    //                string.IsNullOrEmpty(ProductInSeam)
    //                )
    //            {
    //                throw new Exception("Vui lòng cài đặt cấu hình tệp tin mẫu");
    //            }

    //            List<ProductModel> result = new List<ProductModel>();
    //            TaskCompletionSource<List<ProductModel>> tcs = new TaskCompletionSource<List<ProductModel>>();
    //            List<string> listUpc = new List<string>();
    //            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
    //            {
    //                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

    //                Workbook workbook = workbookPart.Workbook;
    //                Sheets sheets = workbook.Sheets;
    //                Sheet mySheet = null;
    //                List<Sheet> listSheet = new List<Sheet>();
    //                foreach (Sheet sheet in sheets)
    //                {
    //                    if (sheet.State == null)
    //                    {
    //                        listSheet.Add(sheet);
    //                        //mySheet = sheet;
    //                        //break;
    //                    }
    //                }

    //                if (listSheet.Count > 1)
    //                {
    //                    throw new Exception("Có nhiều hơn 1 sheet trong tệp dữ liệu");
    //                }

    //                mySheet = listSheet[0];

    //                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(mySheet.Id);

    //                //WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

    //                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
    //                SharedStringTable sharedStringTable = sharedStringPart.SharedStringTable;

    //                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();

    //                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
    //                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;

    //                OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);

    //                //int totalData = sheetData.Elements<Row>().Count();
    //                int current = 0;

    //                while (reader.Read())
    //                {
    //                    if (reader.ElementType == typeof(Row))
    //                    {
    //                        Row row = (Row)reader.LoadCurrentElement();
    //                        if (current == 0)
    //                        {
    //                            current++;
    //                            continue;
    //                        }

    //                        var cell = row.Descendants<Cell>().ToList();
    //                        IEnumerable<string> col = row.Descendants<Cell>()
    //                                    .Select(selectCell => selectCell.InnerText);
    //                        Cell cellUpc = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == ProductUPC).FirstOrDefault();
    //                        Cell cellColor = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == ProductColor).FirstOrDefault();
    //                        Cell cellSku = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == ProductSKU).FirstOrDefault();
    //                        Cell cellSize = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == ProductSize).FirstOrDefault();
    //                        Cell cellInSeam = cell.Where(selectCell => selectCell.CellReference.Value.Substring(0, 1) == ProductInSeam).FirstOrDefault();

    //                        string upc = CellValue(cellUpc, sharedStringTable) != "" ? CellValue(cellUpc, sharedStringTable).Replace(" ", string.Empty) : "";
    //                        string color = CellValue(cellColor, sharedStringTable) != "" ? CellValue(cellColor, sharedStringTable) : "";
    //                        string inseam = CellValue(cellInSeam, sharedStringTable) != "" ? CellValue(cellInSeam, sharedStringTable) : "";
    //                        string sku = CellValue(cellSku, sharedStringTable) != "" ? CellValue(cellSku, sharedStringTable).Replace(" ",string.Empty) : "";
    //                        string size = CellValue(cellSize, sharedStringTable) != "" ? CellValue(cellSize, sharedStringTable) : "";

    //                        if(string.IsNullOrEmpty(upc)|| string.IsNullOrEmpty(sku))
    //                        {
    //                            continue;
    //                        }    

    //                        if (!Regex.IsMatch(upc, @"^\d{12}$"))
    //                        {
    //                            throw new Exception(string.Format("Vui lòng kiểm tra lại định dạng dữ liệu\r\nCột {0} - Dòng {1} - Dữ liệu: {2}", ProductUPC,current,upc));
    //                        }

    //                        if (!Regex.IsMatch(sku, @"^\d{6}-\d{3}$"))
    //                        {
    //                            throw new Exception(string.Format("Vui lòng kiểm tra lại định dạng dữ liệu\r\nCột {0} - Dòng {1} - Dữ liệu: {2}", ProductSKU, current, sku));
    //                        }



    //                        if (!listUpc.Contains(upc))
    //                        {
    //                            listUpc.Add(upc);
    //                            result.Add(new ProductModel()
    //                            {
    //                                UPC = upc,
    //                                SKU = sku,
    //                                Size = size,
    //                                Color = color,
    //                                InSeam = inseam
    //                            });
    //                        }
    //                        current++;
    //                    }
    //                }
    //                tcs.TrySetResult(result);

    //            }
    //            result = await tcs.Task;
    //            return result;
    //        }
    //        catch (IOException)
    //        {
    //            //TraceLog.Message(ex.Message);
    //            throw new Exception("Vui lòng đóng file excel trước khi thực hiện");
    //        }
    //        catch(Exception ex)
    //        {
    //            LogControl.WriteLog(ex.ToString());

    //            throw new Exception("Vui lòng kiểm tra lại định dạng dữ liệu");
    //        }

    //    }

    //    public async Task<string> ExportDetailCarton(string filePath, PurchaseOrderModel item)
    //    {

    //        TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

    //        string result = string.Empty;

    //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
    //        {
    //            WorkbookPart workbookPart = document.AddWorkbookPart();
    //            workbookPart.Workbook = new Workbook();

    //            var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
    //            sharedStringTablePart.SharedStringTable = new SharedStringTable();

    //            //Load style
    //            WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
    //            stylePart.Stylesheet = GenerateStylesheet();
    //            stylePart.Stylesheet.Save();
    //            //End load style

    //            //Create first sheet
    //            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //            worksheetPart.Worksheet = new Worksheet();
    //            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    //            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "BoxScanning" };
    //            sheets.Append(sheet);
    //            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

    //            workbookPart.Workbook.Save();


    //            Row row = new Row();

    //            row.Append(ConstructCellWithReference("No", CellValues.String, 3, 1, 1));
    //            row.Append(ConstructCellWithReference("CartonNumber", CellValues.String, 3, 2, 1));
    //            row.Append(ConstructCellWithReference("UPC", CellValues.String, 3, 3, 1));
    //            row.Append(ConstructCellWithReference("SKU", CellValues.String, 3, 4, 1));
    //            row.Append(ConstructCellWithReference("Size", CellValues.String, 3, 5, 1));
    //            row.Append(ConstructCellWithReference("Color", CellValues.String, 3, 6, 1));
    //            row.Append(ConstructCellWithReference("InSeam", CellValues.String, 3, 7, 1));
    //            row.Append(ConstructCellWithReference("EPC", CellValues.String, 3, 8, 1));
    //            sheetData.AppendChild(row);

    //            int stt = 1;
    //            int rowIndex = 2;
    //            var list = item.Items;
    //            for (int i = 0; i < list.Count; i++) // Carton Number
    //            {
    //                for (int p = 0; p < list[i].Items.Count; p++) // Product
    //                {
    //                    if (list[i].Items[p].ListScanned.Count == 0)
    //                        continue;
    //                    for (int e = 0; e < list[i].Items[p].ListScanned.Count; e++)
    //                    {
    //                        Row rowDetail = new Row();
    //                        rowDetail.Append(ConstructCellWithReference(stt.ToString(), CellValues.Number, 3, 1, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].CartonNumber.ToString(), CellValues.String, 3, 2, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].Product.UPC.ToString(), CellValues.String, 3, 3, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].Product.SKU.ToString(), CellValues.String, 3, 4, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].Product.Size.ToString(), CellValues.String, 3, 5, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].Product.Color.ToString(), CellValues.String, 3, 6, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].Product.InSeam.ToString(), CellValues.String, 3, 7, rowIndex));
    //                        rowDetail.Append(ConstructCellWithReference(list[i].Items[p].ListScanned[e].EPC.ToString(), CellValues.String, 3, 8, rowIndex));
    //                        sheetData.AppendChild(rowDetail);
    //                        stt++;
    //                        rowIndex++;
    //                    }

    //                }

    //            }

    //            Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
    //            if (lstColumns == null)
    //            {
    //                lstColumns = new Columns();
    //            }

    //            lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 5, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 18, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 18, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 18, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 20, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 6, Max = 6, Width = 27, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 7, Max = 7, Width = 27, CustomWidth = true });
    //            lstColumns.Append(new Column() { Min = 8, Max = 8, Width = 27, CustomWidth = true });
    //            worksheetPart.Worksheet.InsertAt(lstColumns, 0);

    //            worksheetPart.Worksheet.Save();

    //            tcs.TrySetResult(filePath);
    //        }
    //        result = await tcs.Task;
    //        return result;
    //    }

    //    public async Task<string> ExportReport(List<ReportByTimeModel> listItem, string filePath,string start,string end, List<PurchaseOrderDetailModel> listDetail = null)
    //    {
    //        try
    //        {
    //            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
    //            DateTime now = DateTime.Now;

    //            string result = string.Empty;

    //            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
    //            {
    //                WorkbookPart workbookPart = document.AddWorkbookPart();
    //                workbookPart.Workbook = new Workbook();

    //                var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
    //                sharedStringTablePart.SharedStringTable = new SharedStringTable();

    //                //Load style
    //                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
    //                stylePart.Stylesheet = GenerateStylesheet();
    //                stylePart.Stylesheet.Save();
    //                //End load style

    //                //Create first sheet
    //                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //                worksheetPart.Worksheet = new Worksheet();
    //                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    //                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Report" };
    //                sheets.Append(sheet);

    //                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

    //                workbookPart.Workbook.Save();

    //                //Merge Cell
    //                MergeCells mergeCells = new MergeCells();
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:I1") });
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A2:I2") });
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A3:I3") });

    //                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());


    //                Row firstRow = new Row();
    //                firstRow.Append(ConstructCellWithReference("Ngày tạo: " + now.ToString("dd-MM-yyyy HH:mm:ss"), CellValues.String, 0, 1, 1));
    //                sheetData.AppendChild(firstRow);

    //                Row secondRow = new Row();
    //                secondRow.Height = 40;
    //                secondRow.Append(ConstructCellWithReference("BÁO CÁO THÙNG HÀNG", CellValues.String, 6, 1, 2));
    //                sheetData.AppendChild(secondRow);

    //                Row thirdRow = new Row();
    //                thirdRow.Append(ConstructCellWithReference("(" + start + " - " + end + ")", CellValues.String, 7, 1, 3));
    //                sheetData.AppendChild(thirdRow);

    //                Row row = new Row();

    //                row.Append(ConstructCellWithReference("STT", CellValues.String, 8, 1, 4));
    //                row.Append(ConstructCellWithReference("Mùa", CellValues.String, 8, 2, 4));
    //                row.Append(ConstructCellWithReference("PO", CellValues.String, 8, 3, 4));
    //                row.Append(ConstructCellWithReference("Mã thùng", CellValues.String, 8, 4, 4));
    //                row.Append(ConstructCellWithReference("SKU", CellValues.String, 8, 5, 4));
    //                row.Append(ConstructCellWithReference("Kích cỡ", CellValues.String, 8, 6, 4));
    //                row.Append(ConstructCellWithReference("Số lượng", CellValues.String, 8, 7, 4));
    //                row.Append(ConstructCellWithReference("Ngày kiểm tra", CellValues.String, 8, 8, 4));
    //                row.Append(ConstructCellWithReference("Kết quả kiểm tra", CellValues.String, 8, 9, 4));
    //                sheetData.AppendChild(row);

    //                int stt = 1;
    //                int rowIndex = 5;

    //                CultureInfo ci = new CultureInfo("en-EN");
    //                Thread.CurrentThread.CurrentCulture = ci;
    //                for (int i = 0; i < listItem.Count; i++) // Carton Number
    //                {
    //                    Row rowDetail = new Row();
    //                    rowDetail.Append(ConstructCellWithReference(stt.ToString(), CellValues.Number, 8, 1, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference("", CellValues.String, 3, 2, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].PurchaseOrderNumber, CellValues.String, 3, 3, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].PurchaseOrderNumber + "-" + listItem[i].CartonNumber.ToString(), CellValues.String, 3, 4, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].SKU, CellValues.String, 3, 5, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Size, CellValues.String, 8, 6, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Quantity.ToString(), CellValues.String, 8, 7, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].CheckDate.ToString("dd-MMM-yyyy HH:mm:ss"), CellValues.String, 8, 8, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Result, CellValues.String, 8, 9, rowIndex));
    //                    sheetData.AppendChild(rowDetail);
    //                    stt++;
    //                    rowIndex++;

    //                }

    //                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
    //                if (lstColumns == null)
    //                {
    //                    lstColumns = new Columns();
    //                }

    //                lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 5, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 15, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 18, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 18, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 12, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 6, Max = 6, Width = 14, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 7, Max = 7, Width = 9, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 8, Max = 8, Width = 21, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 9, Max = 9, Width = 12, CustomWidth = true });
    //                worksheetPart.Worksheet.InsertAt(lstColumns, 0);

    //                if (listDetail != null)
    //                {
    //                    #region Second Sheet
    //                    WorksheetPart secondWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //                    secondWorksheetPart.Worksheet = new Worksheet();
    //                    Sheet secondSheet = new Sheet() { Id = workbookPart.GetIdOfPart(secondWorksheetPart), SheetId = 2, Name = "Detail" };
    //                    sheets.Append(secondSheet);

    //                    SheetData secondSheetData = secondWorksheetPart.Worksheet.AppendChild(new SheetData());




    //                    Row rowSecond = new Row();

    //                    rowSecond.Append(ConstructCellWithReference("STT", CellValues.String, 3, 1, 1));
    //                    rowSecond.Append(ConstructCellWithReference("CartonNumber", CellValues.String, 3, 2, 1));
    //                    rowSecond.Append(ConstructCellWithReference("UPC", CellValues.String, 3, 3, 1));
    //                    rowSecond.Append(ConstructCellWithReference("SKU", CellValues.String, 3, 4, 1));
    //                    rowSecond.Append(ConstructCellWithReference("Size", CellValues.String, 3, 5, 1));
    //                    rowSecond.Append(ConstructCellWithReference("Color", CellValues.String, 3, 6, 1));
    //                    rowSecond.Append(ConstructCellWithReference("InSeam", CellValues.String, 3, 7, 1));
    //                    rowSecond.Append(ConstructCellWithReference("EPC", CellValues.String, 3, 8, 1));
    //                    secondSheetData.AppendChild(rowSecond);

    //                    int secondSheetStt = 1;
    //                    int secondRowIndex = 2;
    //                    for (int i = 0; i < listDetail.Count; i++) // Carton Number
    //                    {
    //                        for (int p = 0; p < listDetail[i].Items.Count; p++) // Product
    //                        {
    //                            if (listDetail[i].Items[p].ListScanned.Count == 0)
    //                                continue;
    //                            for (int e = 0; e < listDetail[i].Items[p].ListScanned.Count; e++)
    //                            {
    //                                Row rowDetail = new Row();
    //                                rowDetail.Append(ConstructCellWithReference(secondSheetStt.ToString(), CellValues.Number, 3, 1, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].CartonNumber.ToString(), CellValues.String, 3, 2, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.UPC.ToString(), CellValues.String, 3, 3, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.SKU.ToString(), CellValues.String, 3, 4, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.Size.ToString(), CellValues.String, 3, 5, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.Color.ToString(), CellValues.String, 3, 6, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.InSeam.ToString(), CellValues.String, 3, 7, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].ListScanned[e].EPC.ToString(), CellValues.String, 3, 8, secondRowIndex));
    //                                secondSheetData.AppendChild(rowDetail);
    //                                secondSheetStt++;
    //                                secondRowIndex++;
    //                            }

    //                        }

    //                    }

    //                    Columns secondLstColumns = secondWorksheetPart.Worksheet.GetFirstChild<Columns>();
    //                    if (secondLstColumns == null)
    //                    {
    //                        secondLstColumns = new Columns();
    //                    }

    //                    secondLstColumns.Append(new Column() { Min = 1, Max = 1, Width = 5, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 2, Max = 2, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 3, Max = 3, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 4, Max = 4, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 5, Max = 5, Width = 20, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 6, Max = 6, Width = 27, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 7, Max = 7, Width = 27, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 8, Max = 8, Width = 27, CustomWidth = true });
    //                    secondWorksheetPart.Worksheet.InsertAt(secondLstColumns, 0);

    //                    //worksheetPart.Worksheet.Save();

    //                    #endregion
    //                }


    //                worksheetPart.Worksheet.Save();

    //                tcs.TrySetResult(filePath);
    //            }
    //            result = await tcs.Task;
    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogControl.WriteLog(ex.ToString());
    //            throw new Exception(ex.Message);
    //        }

    //    }

    //    public async Task<string> ExportReportWithDetail(string po,string filePath, List<PurchaseOrderDetailModel> listDetail)
    //    {
    //        try
    //        {
    //            List<ReportByTimeModel> listItem = new List<ReportByTimeModel>();
    //            for (int i = 0; i < listDetail.Count; i++)
    //            {
    //                foreach (var item in listDetail[i].Items)
    //                {
    //                    if (listDetail[i].Items.Sum(x => x.ListScanned.Count) == 0)
    //                    {
    //                        continue;
    //                    }
    //                    listItem.Add(new ReportByTimeModel()
    //                    {
    //                        STT = i + 1,
    //                        PurchaseOrderNumber = po,
    //                        CartonNumber = listDetail[i].CartonNumber,
    //                        SKU = item.Product.SKU,
    //                        Size = item.Product.Size,
    //                        Quantity = item.Quantity,
    //                        CheckDate = item.ListScanned[0].CreateAt,
    //                        Result = item.ListScanned.Count != item.Quantity ? "Không hợp lệ" : "Hợp lệ"
    //                    });
    //                }

    //            }

    //            string start = listItem.Max(x => x.CheckDate).ToString("dd/MM/yyyy");
    //            string end = listItem.Min(x => x.CheckDate).ToString("dd/MM/yyyy");

    //            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
    //            DateTime now = DateTime.Now;

    //            string result = string.Empty;

    //            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
    //            {
    //                WorkbookPart workbookPart = document.AddWorkbookPart();
    //                workbookPart.Workbook = new Workbook();

    //                var sharedStringTablePart = workbookPart.AddNewPart<SharedStringTablePart>();
    //                sharedStringTablePart.SharedStringTable = new SharedStringTable();

    //                //Load style
    //                WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
    //                stylePart.Stylesheet = GenerateStylesheet();
    //                stylePart.Stylesheet.Save();
    //                //End load style

    //                //Create first sheet
    //                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //                worksheetPart.Worksheet = new Worksheet();
    //                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    //                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Report" };
    //                sheets.Append(sheet);

    //                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

    //                workbookPart.Workbook.Save();

    //                //Merge Cell
    //                MergeCells mergeCells = new MergeCells();
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:I1") });
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A2:I2") });
    //                mergeCells.Append(new MergeCell() { Reference = new StringValue("A3:I3") });

    //                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());


    //                Row firstRow = new Row();
    //                firstRow.Append(ConstructCellWithReference("Ngày tạo: " + now.ToString("dd-MM-yyyy HH:mm:ss"), CellValues.String, 0, 1, 1));
    //                sheetData.AppendChild(firstRow);

    //                Row secondRow = new Row();
    //                secondRow.Height = 40;
    //                secondRow.Append(ConstructCellWithReference("BÁO CÁO THÙNG HÀNG", CellValues.String, 6, 1, 2));
    //                sheetData.AppendChild(secondRow);

    //                Row thirdRow = new Row();
    //                thirdRow.Append(ConstructCellWithReference("(" + start + " - " + end + ")", CellValues.String, 7, 1, 3));
    //                sheetData.AppendChild(thirdRow);

    //                Row row = new Row();

    //                row.Append(ConstructCellWithReference("STT", CellValues.String, 8, 1, 4));
    //                row.Append(ConstructCellWithReference("Mùa", CellValues.String, 8, 2, 4));
    //                row.Append(ConstructCellWithReference("PO", CellValues.String, 8, 3, 4));
    //                row.Append(ConstructCellWithReference("Mã thùng", CellValues.String, 8, 4, 4));
    //                row.Append(ConstructCellWithReference("SKU", CellValues.String, 8, 5, 4));
    //                row.Append(ConstructCellWithReference("Kích cỡ", CellValues.String, 8, 6, 4));
    //                row.Append(ConstructCellWithReference("Số lượng", CellValues.String, 8, 7, 4));
    //                row.Append(ConstructCellWithReference("Ngày kiểm tra", CellValues.String, 8, 8, 4));
    //                row.Append(ConstructCellWithReference("Kết quả kiểm tra", CellValues.String, 8, 9, 4));
    //                sheetData.AppendChild(row);

    //                int stt = 1;
    //                int rowIndex = 5;

    //                CultureInfo ci = new CultureInfo("en-EN");
    //                Thread.CurrentThread.CurrentCulture = ci;
    //                for (int i = 0; i < listItem.Count; i++) // Carton Number
    //                {
    //                    Row rowDetail = new Row();
    //                    rowDetail.Append(ConstructCellWithReference(stt.ToString(), CellValues.Number, 8, 1, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference("", CellValues.String, 3, 2, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].PurchaseOrderNumber, CellValues.String, 3, 3, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].PurchaseOrderNumber + "-" + listItem[i].CartonNumber.ToString(), CellValues.String, 3, 4, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].SKU, CellValues.String, 3, 5, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Size, CellValues.String, 8, 6, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Quantity.ToString(), CellValues.Number, 8, 7, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].CheckDate.ToString("dd-MMM-yyyy HH:mm:ss"), CellValues.String, 8, 8, rowIndex));
    //                    rowDetail.Append(ConstructCellWithReference(listItem[i].Result, CellValues.String, 8, 9, rowIndex));
    //                    sheetData.AppendChild(rowDetail);
    //                    stt++;
    //                    rowIndex++;

    //                }

    //                Columns lstColumns = worksheetPart.Worksheet.GetFirstChild<Columns>();
    //                if (lstColumns == null)
    //                {
    //                    lstColumns = new Columns();
    //                }

    //                lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 5, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 15, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 18, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 18, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 12, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 6, Max = 6, Width = 14, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 7, Max = 7, Width = 9, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 8, Max = 8, Width = 21, CustomWidth = true });
    //                lstColumns.Append(new Column() { Min = 9, Max = 9, Width = 16, CustomWidth = true });
    //                worksheetPart.Worksheet.InsertAt(lstColumns, 0);

    //                if (listDetail != null)
    //                {
    //                    #region Second Sheet
    //                    WorksheetPart secondWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //                    secondWorksheetPart.Worksheet = new Worksheet();
    //                    Sheet secondSheet = new Sheet() { Id = workbookPart.GetIdOfPart(secondWorksheetPart), SheetId = 2, Name = "Detail" };
    //                    sheets.Append(secondSheet);

    //                    SheetData secondSheetData = secondWorksheetPart.Worksheet.AppendChild(new SheetData());




    //                    Row rowSecond = new Row();

    //                    rowSecond.Append(ConstructCellWithReference("STT", CellValues.String, 3, 1, 1));
    //                    rowSecond.Append(ConstructCellWithReference("CartonNumber", CellValues.String, 3, 2, 1));
    //                    rowSecond.Append(ConstructCellWithReference("UPC", CellValues.String, 3, 3, 1));
    //                    rowSecond.Append(ConstructCellWithReference("SKU", CellValues.String, 3, 4, 1));
    //                    rowSecond.Append(ConstructCellWithReference("Size", CellValues.String, 3, 5, 1));
    //                    rowSecond.Append(ConstructCellWithReference("Color", CellValues.String, 3, 6, 1));
    //                    rowSecond.Append(ConstructCellWithReference("InSeam", CellValues.String, 3, 7, 1));
    //                    rowSecond.Append(ConstructCellWithReference("EPC", CellValues.String, 3, 8, 1));
    //                    secondSheetData.AppendChild(rowSecond);

    //                    int secondSheetStt = 1;
    //                    int secondRowIndex = 2;
    //                    for (int i = 0; i < listDetail.Count; i++) // Carton Number
    //                    {
    //                        for (int p = 0; p < listDetail[i].Items.Count; p++) // Product
    //                        {
    //                            if (listDetail[i].Items[p].ListScanned.Count == 0)
    //                                continue;
    //                            for (int e = 0; e < listDetail[i].Items[p].ListScanned.Count; e++)
    //                            {
    //                                Row rowDetail = new Row();
    //                                rowDetail.Append(ConstructCellWithReference(secondSheetStt.ToString(), CellValues.Number, 3, 1, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].CartonNumber.ToString(), CellValues.Number, 3, 2, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.UPC.ToString(), CellValues.String, 3, 3, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.SKU.ToString(), CellValues.String, 3, 4, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.Size.ToString(), CellValues.String, 3, 5, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.Color.ToString(), CellValues.String, 3, 6, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].Product.InSeam.ToString(), CellValues.String, 3, 7, secondRowIndex));
    //                                rowDetail.Append(ConstructCellWithReference(listDetail[i].Items[p].ListScanned[e].EPC.ToString(), CellValues.String, 3, 8, secondRowIndex));
    //                                secondSheetData.AppendChild(rowDetail);
    //                                secondSheetStt++;
    //                                secondRowIndex++;
    //                            }

    //                        }

    //                    }

    //                    Columns secondLstColumns = secondWorksheetPart.Worksheet.GetFirstChild<Columns>();
    //                    if (secondLstColumns == null)
    //                    {
    //                        secondLstColumns = new Columns();
    //                    }

    //                    secondLstColumns.Append(new Column() { Min = 1, Max = 1, Width = 5, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 2, Max = 2, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 3, Max = 3, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 4, Max = 4, Width = 18, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 5, Max = 5, Width = 20, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 6, Max = 6, Width = 27, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 7, Max = 7, Width = 27, CustomWidth = true });
    //                    secondLstColumns.Append(new Column() { Min = 8, Max = 8, Width = 27, CustomWidth = true });
    //                    secondWorksheetPart.Worksheet.InsertAt(secondLstColumns, 0);

    //                    //worksheetPart.Worksheet.Save();

    //                    #endregion
    //                }


    //                worksheetPart.Worksheet.Save();

    //                tcs.TrySetResult(filePath);
    //            }
    //            result = await tcs.Task;
    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            LogControl.WriteLog(ex.ToString());
    //            throw new Exception(ex.Message);
    //        }

    //    }
    //}
}
