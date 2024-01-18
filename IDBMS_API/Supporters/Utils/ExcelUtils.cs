using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace IDBMS_API.Supporters.Utils
{
    public class ExcelUtils
    {
        public enum Calculator
        {
            Sum = 1,
            Minus = 2,
            Multiple = 3,
            Divide = 4
        }
        public static double FindAndReplaceCalculatorMutipleRow(SpreadsheetDocument workbook, string sheetName, string cellResult, string cellName, int indexStart, int indexEnd)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);
            double total = 0;
            if (sheet != null)
            {

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellResult);
                Cell cell1 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellName + indexStart.ToString());
                if (cell != null && cell1 != null)
                {
                    string value1 = cell1.InnerText;
                    if (cell1.DataType != null && cell1.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value1);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value1 = sharedStringItem.Text.Text;
                        }
                    }
                    if (value1!= null || !value1.Equals("")) value1 = value1.Replace(".", "");
                    bool sc = Double.TryParse(value1, out total);
                    if (!sc)
                    {
                        value1 = cell1.LastChild.FirstChild.InnerText;
                        value1 = value1.Replace(".", "");
                        sc = Double.TryParse(value1, out total);
                    }
                    for (int i = indexStart + 1; i <= indexEnd; i++)
                    {
                        Cell cell2 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellName + i.ToString());
                        if (cell2 != null && cell1 != null)
                        {
                            string value2 = cell2.InnerText;

                            if (cell2.DataType != null && cell2.DataType == CellValues.SharedString)
                            {
                                int sharedStringIndex = int.Parse(value2);
                                SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                                if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                                {
                                    value2 = sharedStringItem.Text.Text;
                                }
                            }

                            if (value2 == null || value2.Equals("")) continue;
                            else value2 = value2.Replace(".", "");
                            double dvalue2 = 0;
                            sc = Double.TryParse(value2, out dvalue2);
                            if (!sc)
                            {
                                value2 = cell2.LastChild.FirstChild.InnerText;
                                value2 = value2.Replace(".", "");
                                sc = Double.TryParse(value2, out dvalue2);
                            }

                            total += dvalue2;
                        }
                    }
                    string cellFormula = "SUM(" + cellName + indexStart.ToString() + ":" + cellName + indexEnd.ToString() + ")";


                    cell.DataType = CellValues.Number;
                    cell.CellFormula = new CellFormula(cellFormula);
                    cell.CellValue = new CellValue(total);
                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
            return total;
        }
        public static void FindAndReplaceCalculatorMutipleMoneyRow(SpreadsheetDocument workbook, string sheetName, string cellResult, string cellName, int indexStart, int indexEnd)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellResult);
                Cell cell1 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellName + indexStart.ToString());
                string value1 = cell1.InnerText;
                if (cell != null && cell1 != null)
                {
                    if (cell1.DataType != null && cell1.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value1);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value1 = sharedStringItem.Text.Text;
                        }
                    }
                    double total = value1 == null ? 0 : Double.Parse(value1);

                    for (int i = indexStart + 1; i <= indexEnd; i++)
                    {
                        Cell cell2 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellName + i.ToString());
                        if (cell2 != null && cell1 != null)
                        {
                            string value2 = cell2.InnerText;

                            if (cell2.DataType != null && cell2.DataType == CellValues.SharedString)
                            {
                                int sharedStringIndex = int.Parse(value2);
                                SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                                if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                                {
                                    value2 = sharedStringItem.Text.Text;
                                }
                            }

                            if (value2 == null || value2.Equals("")) continue;
                            double dvalue2 = Double.Parse(value2);
                            total += dvalue2;
                        }
                    }
                    string cellFormula = "SUM(" + cellName + indexStart.ToString() + ":" + cellName + indexEnd.ToString() + ")";


                    cell.DataType = new EnumValue<CellValues>(CellValues.InlineString);
                    cell.CellFormula = new CellFormula(cellFormula);
                    cell.InlineString = new InlineString() { Text = new Text(IntUtils.ConvertStringToMoney((decimal)total)) };

                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
        }
        public static void FindAndReplaceCalculatorCellMultiple(SpreadsheetDocument workbook, string sheetName, string cellResult, string cellName, int percent)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellResult);
                Cell cell1 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellName);
                if (cell != null && cell1 != null)
                {
                    string value1 = cell1.LastChild.InnerText;
                    if (cell1.DataType != null && cell1.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value1);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value1 = sharedStringItem.Text.Text;
                        }
                    }
                    double total = value1 == null ? 0 : Double.Parse(value1);
                    total = total * (percent) / 100;

                    string cellFormula = "" + cellName + "*" + percent.ToString() + "%";


                    cell.DataType = CellValues.Number;
                    cell.CellFormula = new CellFormula(cellFormula);
                    cell.CellValue = new CellValue(total);
                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
        }
        public static Row InsertRow(SpreadsheetDocument document, string sheetname, uint originalRowIndex, uint rowIndex, bool isNewLastRow = false)
        {
            //WorkbookPart workbookPart = document.WorkbookPart;

            //Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetname);
            //WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            WorksheetPart worksheetPart = GetWorksheetPartByName(document, sheetname);
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            Row originalRow = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == originalRowIndex);
            Row insertRow = CloneRow(originalRow, (int)rowIndex);

            Row retRow = !isNewLastRow ? sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex) : null;
            if (retRow != null)
            {
                if (insertRow != null)
                {
                    UpdateRowIndexes(worksheetPart, rowIndex, false);
                    UpdateMergedCellReferences(worksheetPart, rowIndex, false);
                    UpdateHyperlinkReferences(worksheetPart, rowIndex, false);
                    retRow = sheetData.InsertBefore(insertRow, retRow);
                    string curIndex = retRow.RowIndex.ToString();
                    string newIndex = rowIndex.ToString();
                    foreach (Cell cell in retRow.Elements())
                    {
                        cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curIndex, newIndex));
                    }
                    retRow.RowIndex = rowIndex;
                }
            }
            else
            {
                Row refRow = !isNewLastRow ? sheetData.Elements<Row>().FirstOrDefault(row => row.RowIndex > rowIndex) : null;
                retRow = insertRow ?? new Row() { RowIndex = rowIndex };
                IEnumerable<Cell> cellsInRow = retRow.Elements<Cell>();
                if (cellsInRow.Any())
                {
                    string curIndex = retRow.RowIndex.ToString();
                    string newIndex = rowIndex.ToString();
                    foreach (Cell cell in cellsInRow)
                    {
                        cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curIndex, newIndex));
                    }
                    retRow.RowIndex = rowIndex;
                }
                sheetData.InsertBefore(retRow, refRow);
            }
            return retRow;
        }

        private static void UpdateRowIndexes(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            IEnumerable<Row> rows = worksheetPart.Worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= rowIndex);
            foreach (Row row in rows)
            {
                uint newIndex = (isDeletedRow ? row.RowIndex - 1 : row.RowIndex + 1);
                string curRowIndex = row.RowIndex.ToString();
                string newRowIndex = newIndex.ToString();
                foreach (Cell cell in row.Elements())
                {
                    cell.CellReference = new StringValue(cell.CellReference.Value.Replace(curRowIndex, newRowIndex));
                }
                row.RowIndex = newIndex;
            }
        }

        public static void UpdateMergedCellReferences(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            if (worksheetPart.Worksheet.Elements().Count() > 0)
            {
                MergeCells mergeCells = worksheetPart.Worksheet.Elements<MergeCells>().FirstOrDefault();
                if (mergeCells != null)
                {
                    List<MergeCell> mergeCellsList = mergeCells.Elements<MergeCell>().Where(r => r.Reference.HasValue)
                                                                                     .Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) >= rowIndex ||
                                                                                                 GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) >= rowIndex).ToList();
                    if (isDeletedRow)
                    {
                        List<MergeCell> mergeCellsToDelete = mergeCellsList.Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) == rowIndex ||
                                                                                       GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) == rowIndex).ToList();
                        foreach (MergeCell cellToDelete in mergeCellsToDelete)
                        {
                            cellToDelete.Remove();
                        }
                        mergeCellsList = mergeCells.Elements<MergeCell>().Where(r => r.Reference.HasValue)
                                                                         .Where(r => GetRowIndex(r.Reference.Value.Split(':').ElementAt(0)) > rowIndex ||
                                                                                     GetRowIndex(r.Reference.Value.Split(':').ElementAt(1)) > rowIndex).ToList();
                    }
                    foreach (MergeCell mergeCell in mergeCellsList)
                    {
                        string[] cellReference = mergeCell.Reference.Value.Split(':');
                        if (GetRowIndex(cellReference.ElementAt(0)) >= rowIndex)
                        {
                            string columnName = GetColumnName(cellReference.ElementAt(0));
                            cellReference[0] = isDeletedRow ? columnName + (GetRowIndex(cellReference.ElementAt(0)) - 1).ToString() : IncrementCellReference(cellReference.ElementAt(0), CellReferencePartEnum.Row);
                        }
                        if (GetRowIndex(cellReference.ElementAt(1)) >= rowIndex)
                        {
                            string columnName = GetColumnName(cellReference.ElementAt(1));
                            cellReference[1] = isDeletedRow ? columnName + (GetRowIndex(cellReference.ElementAt(1)) - 1).ToString() : IncrementCellReference(cellReference.ElementAt(1), CellReferencePartEnum.Row);
                        }
                        mergeCell.Reference = new StringValue(cellReference[0] + ":" + cellReference[1]);
                    }
                }
            }
        }
        private static void UpdateHyperlinkReferences(WorksheetPart worksheetPart, uint rowIndex, bool isDeletedRow)
        {
            Hyperlinks hyperlinks = (Hyperlinks)worksheetPart.Worksheet.Elements<Hyperlinks>().FirstOrDefault();
            if (hyperlinks != null)
            {
                Match hyperlinkRowIndexMatch;
                uint hyperlinkRowIndex;
                foreach (DocumentFormat.OpenXml.Office2016.Excel.Hyperlink hyperlink in hyperlinks.Elements())
                {
                    hyperlinkRowIndexMatch = Regex.Match(hyperlink.Reference.Value, "[0-9]+");
                    if (hyperlinkRowIndexMatch.Success && uint.TryParse(hyperlinkRowIndexMatch.Value, out hyperlinkRowIndex) && hyperlinkRowIndex >= rowIndex)
                    {
                        if (isDeletedRow)
                        {
                            if (hyperlinkRowIndex == rowIndex)
                            {
                                hyperlink.Remove();
                            }
                            else
                            {
                                hyperlink.Reference.Value = hyperlink.Reference.Value.Replace(hyperlinkRowIndexMatch.Value, (hyperlinkRowIndex - 1).ToString());
                            }
                        }
                        else
                        {
                            hyperlink.Reference.Value = hyperlink.Reference.Value.Replace(hyperlinkRowIndexMatch.Value, (hyperlinkRowIndex + 1).ToString());
                        }
                    }
                }
                if (hyperlinks.Elements().Count() == 0)
                {
                    hyperlinks.Remove();
                }
            }
        }

        public static uint GetRowIndex(string cellReference)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellReference);
            return uint.Parse(match.Value);
        }

        public static string IncrementCellReference(string reference, CellReferencePartEnum cellRefPart)
        {
            string newReference = reference;
            if (cellRefPart != CellReferencePartEnum.None && !String.IsNullOrEmpty(reference))
            {
                string[] parts = Regex.Split(reference, "([A-Z]+)");
                if (cellRefPart == CellReferencePartEnum.Column || cellRefPart == CellReferencePartEnum.Both)
                {
                    List<char> col = parts[1].ToCharArray().ToList();
                    bool needsIncrement = true;
                    int index = col.Count - 1;
                    do
                    {
                        col[index] = Letters[Letters.IndexOf(col[index]) + 1];
                        if (col[index] == Letters[Letters.Count - 1])
                        {
                            col[index] = Letters[0];
                        }
                        else
                        {
                            needsIncrement = false;
                        }
                    } while (needsIncrement && --index >= 0);
                    if (needsIncrement)
                    {
                        col.Add(Letters[0]);
                    }
                    parts[1] = new String(col.ToArray());
                }
                if (cellRefPart == CellReferencePartEnum.Row || cellRefPart == CellReferencePartEnum.Both)
                {
                    parts[2] = (int.Parse(parts[2]) + 1).ToString();
                }
                newReference = parts[1] + parts[2];
            }
            return newReference;
        }

        /// Address of the cell (ie. B2)
        /// Column name (ie. A2)
        //private static string GetColumnName(string cellName)
        //{
        //    // Create a regular expression to match the column name portion of the cell name.
        //    Regex regex = new Regex("[A-Za-z]+");
        //    Match match = regex.Match(cellName);
        //    return match.Value;
        //}
        public enum CellReferencePartEnum
        {
            None,
            Column,
            Row,
            Both
        }
        private static List<char> Letters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };

        public static void RenameSheet(SpreadsheetDocument document, string sheetName, string newName)
        {
            Sheet sheet = document.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);
            sheet.Name = newName;
            document.Save();
        }

        public static Row CloneRow(Row originalRow, int newIndex)
        {
            Row newRow = new Row();

            foreach (Cell originalCell in originalRow.Elements<Cell>())
            {
                Cell newCell = new Cell(originalCell.OuterXml)
                {
                    CellReference = new StringValue(originalCell.CellReference.Value.Replace(originalRow.RowIndex.ToString(), newIndex.ToString()))
                };

                newRow.Append(newCell);
            }

            newRow.RowIndex = (uint)newIndex;

            return newRow;
        }

        public static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {
                return (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            }

            return null;
        }
        public static void FindAndReplaceString(SpreadsheetDocument workbook, string sheetName, string cellReference, string value)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
                if (cell != null)
                {
                    cell.InlineString = new InlineString() { Text = new Text(value) };
                    //cell.CellValue = new CellValue(value);
                    cell.DataType = new EnumValue<CellValues>(CellValues.InlineString);
                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
        }
        public static double FindAndReplaceCalculatorCell(SpreadsheetDocument workbook, string sheetName, string cellReference, string cellReference1, string cellReference2, Calculator calculator)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
                Cell cell1 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference1);
                Cell cell2 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference2);
                if (cell2 != null && cell1 != null)
                {
                    //string value1 = cell1.InlineString.Text.Text;
                    string value1 = cell1.CellValue.InnerText;

                    if (cell1.DataType != null && cell1.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value1);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value1 = sharedStringItem.Text.Text;
                        }
                    }


                    string value2 = cell2.CellValue.InnerText;

                    if (cell2.DataType != null && cell2.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value2);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value2 = sharedStringItem.Text.Text;
                        }
                    }

                    if (value1 == null || value2 == null) return 0;
                    double dvalue1 = Double.Parse(value1);
                    double dvalue2 = Double.Parse(value2);
                    string cellFormula = "";
                    double result = 0;
                    switch (calculator)
                    {
                        case Calculator.Sum:
                            cellFormula += cellReference1 + "+" + cellReference2;
                            result = dvalue1 + dvalue2;
                            break;
                        case Calculator.Minus:
                            cellFormula += cellReference1 + "-" + cellReference2;
                            result = dvalue1 - dvalue2;
                            break;
                        case Calculator.Multiple:
                            cellFormula += cellReference1 + "*" + cellReference2;
                            result = dvalue1 * dvalue2;
                            break;
                        case Calculator.Divide:
                            if (cellReference2 == "0") break;
                            cellFormula += cellReference1 + "/" + cellReference2;
                            result = dvalue1 / dvalue2;
                            break;
                    }
                    cell.DataType = CellValues.Number;
                    cell.CellFormula = new CellFormula(cellFormula);
                    cell.CellValue = new CellValue(result);
                    workbook.Save();
                    return result;
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
            return 0;
        }

        public static double FindAndReplaceCalculatorCharCell(SpreadsheetDocument workbook, string sheetName, string cellReference, char charStart, char charEnd, int index, Calculator calculator)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {

                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
                string cellReference1 = (Char.ToString(charStart) + index);
                string cellReference2 = (Char.ToString(charEnd) + index);
                Cell cell1 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference1);
                Cell cell2 = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference2);
                if (cell2 != null && cell1 != null)
                {
                    string value1 = cell1.InlineString.InnerText;
                    //string value1 = cell1.CellValue.InnerText;

                    if (cell1.DataType != null && cell1.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value1);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value1 = sharedStringItem.Text.Text;
                        }
                    }


                    string value2 = cell2.InlineString.InnerText;

                    if (cell2.DataType != null && cell2.DataType == CellValues.SharedString)
                    {
                        int sharedStringIndex = int.Parse(value2);
                        SharedStringTablePart sharedStringTablePart = workbookPart.SharedStringTablePart;
                        if (sharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAtOrDefault(sharedStringIndex) is SharedStringItem sharedStringItem)
                        {
                            value2 = sharedStringItem.Text.Text;
                        }
                    }

                    if (value1 == null || value2 == null) return 0;
                    else
                    {
                        value1 = value1.Replace(".", "");
                        value2 = value2.Replace(".", "");
                    }
                    double dvalue1 = Double.Parse(value1);
                    double dvalue2 = Double.Parse(value2);
                    string cellFormula = "";
                    double result = 0;
                    switch (calculator)
                    {
                        case Calculator.Sum:
                            cellFormula += cellReference1 + "+" + cellReference2;
                            result = dvalue1 + dvalue2;
                            break;
                        case Calculator.Minus:
                            cellFormula += cellReference1 + "-" + cellReference2;
                            result = dvalue1 - dvalue2;
                            break;
                        case Calculator.Multiple:
                            cellFormula += cellReference1 + "*" + cellReference2;
                            result = dvalue1 * dvalue2;
                            break;
                        case Calculator.Divide:
                            if (cellReference2 == "0") break;
                            cellFormula += cellReference1 + "/" + cellReference2;
                            result = dvalue1 / dvalue2;
                            break;
                    }
                    cell.DataType = CellValues.Number;
                    cell.CellFormula = new CellFormula(cellFormula);
                    cell.CellValue = new CellValue(result);
                    workbook.Save();
                    return result;
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
            return 0;
        }

        public static void FindAndReplaceNumber(SpreadsheetDocument workbook, string sheetName, string cellReference, string value)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
                if (cell != null)
                {
                    double doubleValue = 0;
                    doubleValue = Double.Parse(value);
                    cell.DataType = CellValues.Number;
                    //cell.InlineString = new InlineString() {  = new DoubleValue(doubleValue) };
                    cell.CellValue = new CellValue(value);
                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
        }
        public static string GetColumnName(string cellReference)
        {
            return System.Text.RegularExpressions.Regex.Replace(cellReference.ToUpper(), "[0-9]", "");
        }
        public static void RemoveSheet(SpreadsheetDocument document, string sheetNameToDelete)
        {

            Sheet sheetToDelete = document.WorkbookPart.Workbook.Sheets.Elements<Sheet>().FirstOrDefault(s => s.Name == sheetNameToDelete);

            if (sheetToDelete != null)
            {
                var workbookPart = document.WorkbookPart;
                int sheetCount = workbookPart.Workbook.Sheets.Count();

                var theSheet = workbookPart.Workbook.Descendants<Sheet>()
                                           .FirstOrDefault(s => s.Name == sheetNameToDelete);

                if (sheetCount <= 1 || theSheet == null)
                {
                    return;
                }

                var worksheetPart = (WorksheetPart)(workbookPart.GetPartById(theSheet.Id));
                workbookPart.DeletePart(worksheetPart);
                theSheet.Remove();

                WorkbookView view = document.WorkbookPart.Workbook.Descendants<WorkbookView>().FirstOrDefault();
                view.ActiveTab = 0;
                document.Save();
            }
            else
            {
                Console.WriteLine($"Sheet '{sheetNameToDelete}' not found in the workbook.");
            }
        }
        public static void CloneSheet(SpreadsheetDocument document, string sheetName, string newSheetName)
        {
            Sheet sourceSheet = document.WorkbookPart.Workbook.Sheets.Elements<Sheet>().Where(a => a.Name.Equals(sheetName)).FirstOrDefault();

            if (sourceSheet != null)
            {
                Sheet clonedSheet = (Sheet)sourceSheet.CloneNode(true);
                clonedSheet.Name = newSheetName;
                clonedSheet.SheetId = NewSheetId(document.WorkbookPart);
                //clonedSheet.Id = newId(document.WorkbookPart);
                
                Sheets sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                sheets.Append(clonedSheet);
                document.Save();
            }
            else
            {
                Console.WriteLine("Source sheet not found in the workbook.");
            }

        }
        private static UInt32Value NewSheetId(WorkbookPart workbookPart)
        {
            UInt32Value resultat = 0;
            foreach (Sheet sheet in workbookPart.Workbook.Descendants<Sheet>())
            {
                if (sheet.SheetId > resultat)
                    resultat = sheet.SheetId;
            }
            return resultat + 1;
        }

        
        public static void FindAndReplaceFormula(SpreadsheetDocument workbook, string sheetName, string cellReference, string value ,string formula)
        {
            WorkbookPart workbookPart = workbook.WorkbookPart;

            Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == sheetName);

            if (sheet != null)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                Cell cell = worksheetPart.Worksheet.Descendants<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
                if (cell != null)
                {
                    cell.DataType = new EnumValue<CellValues>(CellValues.InlineString);
                    cell.CellFormula = new CellFormula(formula);
                    cell.InlineString = new InlineString() { Text = new Text(value) };
                    //cell.CellValue = new CellValue(value);
                    workbook.Save();
                }
                else
                {
                    Console.WriteLine($"Sheet with name '{sheetName}' not found.");
                }
            }
            else
            {
                Console.WriteLine($"Sheet with name '{sheetName}' not found.");
            }
        }
    }
}
