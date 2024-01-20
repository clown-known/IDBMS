using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using BusinessObject.Models;
using IDBMS_API.Supporters.Utils;
using Repository.Implements;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
using Microsoft.AspNetCore.OData.Formatter.Wrapper;
using Microsoft.OData.Edm;

namespace IDBMS_API.Supporters.File
{
    public class ExcelSupporter
    {
        public class ExcelResult
        {
            public string name { get; set; }
            public string sheetName { get; set; }
            public int sheetIndex { get; set; }
            public char resultColumn { get; set; }
            public int resultRow { get; set; }

            public double value1 { get; set; }
            public double value2 { get; set; }
            public double value3 { get; set; }
        }
        public static byte[] GenExcelFileBytes(byte[] excelBytes, Guid projectId)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(excelBytes, 0, excelBytes.Length);
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, true))
                {

                    ProjectRepository projectRepository = new ProjectRepository();
                    Project project = projectRepository.GetById(projectId);
                    if (project == null) return null;
                    string projectName = project.Name;
                    if (project.Site == null) throw new Exception("Site is null");
                    string address = project.Site.Address;
                    

                    ProjectTaskRepository projectTaskRepository = new ProjectTaskRepository();
                    List<ProjectTask> projectTask = projectTaskRepository.GetByProjectId(projectId).Where(t => t.ParentTaskId == null).ToList();
                    List<TaskCategory> taskCategory = new List<TaskCategory>();
                    List<ProjectTask> taskListWithoutCategory = new List<ProjectTask>();
                    List<ProjectTask> taskListInterior = new List<ProjectTask>();
                    foreach (var task in projectTask) {
                        if (task.TaskCategory == null)
                        {
                            taskListWithoutCategory.Add(task);
                        }else if (task.TaskCategoryId == 8)
                        {
                            taskListInterior.Add(task);
                        }else {
                            bool flag = true;
                            foreach(TaskCategory tcate in taskCategory)
                            {
                                if (tcate.Id == task.TaskCategory.Id) flag = false;
                            }
                            if (flag) taskCategory.Add(task.TaskCategory); 
                        }
                    }
                    taskCategory.Sort((x, y) =>
                    {
                        if (x.Id == 5) return 1;
                        else if (y.Id == 5) return -1;
                        else return 0;
                    });

                    int sheetCount = 3;
                    string listTaskName = "";

                    List<ExcelResult> excelResults = new List<ExcelResult>();
                    ExcelResult current;
                    foreach (var item in taskCategory)
                    {
                        switch (item.Id)
                        {
                            case 4:
                                current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount-2, "PHA DO", projectName, address, "PHÁ DỠ", 
                                    projectTask.Where(t => t.TaskCategoryId == item.Id && t.ParentTaskId == null).ToList());
                                excelResults.Add(current);
                                break;
                            case 5:
                                current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount-2, "XAY DUNG", projectName, address, "XÂY DỰNG", 
                                    projectTask.Where(t => t.TaskCategoryId == item.Id && t.ParentTaskId == null).ToList());
                                excelResults.Add(current);
                                break;
                            case 6:
                                current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount-2, "DIEN CHIEU SANG", projectName, address, "ĐIỆN CHIẾU SÁNG", 
                                    projectTask.Where(t => t.TaskCategoryId == item.Id && t.ParentTaskId == null).ToList());
                                excelResults.Add(current);
                                break;
                            case 7:
                                current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount-2, "MANG DIEN THOAI", projectName, address, "MẠNG - ĐIỆN THOẠI", 
                                    projectTask.Where(t => t.TaskCategoryId == item.Id && t.ParentTaskId == null).ToList());
                                excelResults.Add(current);
                                break;
                            case 9:
                                current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount-2, "VAN CHUYEN", projectName, address, "VẬN CHUYỂN", 
                                    projectTask.Where(t => t.TaskCategoryId == item.Id && t.ParentTaskId == null).ToList());
                                excelResults.Add(current);
                                break;
                        }
                        listTaskName += item.Name + " - ";
                    }
                    if (taskListWithoutCategory.Count > 0)
                    {
                        current = GenSheetTaskBaseCategory(doc, "Sheet" + sheetCount++.ToString(), sheetCount - 2, "CHUA PHAN LOAI", projectName, address, "CHƯA PHÂN LOẠI", projectTask.Where(t => t.TaskCategoryId == null && t.ParentTaskId == null).ToList());
                        excelResults.Add(current);
                    }
                    if(taskListInterior.Count > 0)
                    {
                        current = GenSheetInterior(doc, (++sheetCount - 2), projectName, address,
                                    projectTask.Where(t => t.TaskCategoryId == 8d && t.ParentTaskId == null).ToList());
                        excelResults.Add(current);
                    }
                    listTaskName = listTaskName.Substring(0, listTaskName.LastIndexOf("-"));


                    ItemInTaskRepository itemInTaskRepository = new ItemInTaskRepository();
                    List<ItemInTask> interiorItems = itemInTaskRepository.GetByProjectId(projectId).ToList();
                    List<ProjectTask> incurredTask = projectTask.Where(t => t.ParentTaskId == null && t.IsIncurred == true).ToList();
                    List<TaskCategory> taskCategoryIncurred = new List<TaskCategory>();
                    foreach (var task in incurredTask) {
                        bool flag = true;
                        foreach(var c in taskCategoryIncurred)
                        {
                            if (task.TaskCategoryId != null && task.TaskCategoryId == c.Id) flag = false;
                        }
                        if (flag && task.TaskCategoryId!=null) taskCategoryIncurred.Add(task.TaskCategory); 
                    }
                    if (incurredTask.Count > 0)
                    {
                        current = GenSheetIncurred(doc, (++sheetCount - 2), projectName, address, taskCategoryIncurred, incurredTask);
                        excelResults.Add(current);
                    }
                    if(interiorItems.Count > 0)
                        GenSheetInteriorInProject(doc,"Sheet11", (++sheetCount - 2), "CHUNG LOAT VAT TU",projectName,listTaskName,interiorItems);

                    excelResults.Sort((x, y) =>
                    {
                        return x.sheetIndex - y.sheetIndex;
                    });

                    var (sum,incrs) = GenSheetTHKP(doc,"Sheet2","HTKP",projectName, listTaskName, excelResults);

                    GenSheet1ForCompany(doc,"Sheet1","BANG XĐGTHT",project,listTaskName,sum,incrs);
                    for (int i = 1; i <= 11; i++)
                    {
                        ExcelUtils.RemoveSheet(doc, "Sheet" + i.ToString());
                    }
                    doc.Save();

                }
                stream.Position = 0;
                return stream.ToArray();
            }
        }
        public static void GenSheet1ForCompany(SpreadsheetDocument doc, string templateName, string sheetName, Project project, string categoryName, ExcelResult total, ExcelResult incr)
        {
            ExcelUtils.RenameSheet(doc, templateName, sheetName);

            User owner;
            var ownerParti = project.ProjectParticipations.FirstOrDefault(p => p.Role == BusinessObject.Enums.ParticipationRole.ProductOwner);
            if(ownerParti == null)
            {
                throw new Exception("Owner is null!");
            }
            owner = ownerParti.User;

            string A4data = "Tên gói thầu:  " + categoryName;

            ExcelUtils.FindAndReplaceString(doc, sheetName, "A3", "Tên dự án:   " + project.Name.ToUpper());

            ExcelUtils.FindAndReplaceString(doc, sheetName, "A4", A4data);
            if(owner.CompanyName != null)
                ExcelUtils.FindAndReplaceString(doc, sheetName, "A6", "Chủ đầu tư: " + owner.CompanyName.ToUpper());
            else
                ExcelUtils.FindAndReplaceString(doc, sheetName, "A6", "Chủ đầu tư: " + owner.Name.ToUpper());
            PaymentStageRepository paymentStageRepository = new PaymentStageRepository();
            List<PaymentStage> paymentStage = paymentStageRepository.GetAll().Where(p => p.ProjectId == project.Id).ToList();

            if(project.EstimatedPrice != null)
                ExcelUtils.FindAndReplaceString(doc, sheetName, "C13", IntUtils.ConvertStringToMoney(project.EstimatedPrice.Value));

            if (incr != null)
            {
                string formula = "'" + incr.sheetName + "'!" + Char.ToString((char)incr.resultColumn++) + incr.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, sheetName, "D13", IntUtils.ConvertStringToMoney((decimal)incr.value1), formula);
            }
            else
                ExcelUtils.FindAndReplaceString(doc, sheetName, "D13", "0");

            //ExcelUtils.FindAndReplaceFormula(doc, sheetName, "E13", "", "D13+C13");
            ExcelUtils.FindAndReplaceCalculatorCharCell(doc, sheetName, "E13", 'D', 'C', 13, ExcelUtils.Calculator.Sum);
            string formula2 = "'" + total.sheetName + "'!" + Char.ToString((char)total.resultColumn++) + total.resultRow;
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "F13", IntUtils.ConvertStringToMoney((decimal)total.value1), formula2);
            if(project.AmountPaid != 0)
                ExcelUtils.FindAndReplaceString(doc, sheetName, "G13", IntUtils.ConvertStringToMoney(project.AmountPaid));
            else
                ExcelUtils.FindAndReplaceString(doc, sheetName, "G13", "0");

            //ExcelUtils.FindAndReplaceFormula(doc, sheetName, "H13", "", "F13-G13");
            ExcelUtils.FindAndReplaceCalculatorCharCell(doc, sheetName, "H13", 'F', 'G', 13, ExcelUtils.Calculator.Minus);

            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "C14", "", "C13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "D14", "", "D13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "E14", "", "E13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "F14", "", "F13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "G14", "", "G13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "H14", "", "H13");

            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "G16", "", "E14");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "G17", "", "F13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "G18", "", "G13");
            ExcelUtils.FindAndReplaceFormula(doc, sheetName, "G19", "", "H13");

            var result = (int)total.value1 -  (int)project.AmountPaid;
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A20", " Số tiền bằng chữ: "+ IntUtils.ConvertNumberToVietnamese(result) + " đồng");


        }

        public static (ExcelResult sum, ExcelResult? incrs) GenSheetTHKP(SpreadsheetDocument doc,string templateSheetName, string sheetName, string projectTitle, string categoryName, List<ExcelResult> excelResults)
        {

            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A1", "BẢNG 1 :  BẢNG TỔNG HỢP GIÁ TRỊ QUYẾT TOÁN");
            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A2", "CÔNG TRÌNH : " + projectTitle.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A6", "I");
            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "B6", "Hợp đồng thi công lắp đặt công trình");
            int currentIndex = 7;
            int currentNo = 1;
            var incr = excelResults.Where(i => i.sheetName.Equals("PHAT SINH")).FirstOrDefault();


            ExcelResult sum = new ExcelResult();
            ExcelResult incrs = null;


            foreach (var item in excelResults.Where(i=>!i.sheetName.Equals("PHAT SINH")))
            {
                char startColumn = 'A';
                if (currentNo < excelResults.Count-1) 
                    ExcelUtils.InsertRow(doc, templateSheetName, (uint)currentIndex, ((uint)currentIndex), false);
                ExcelUtils.FindAndReplaceNumber(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), (currentNo++).ToString());
                ExcelUtils.FindAndReplaceString(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "Phần " + item.name);

                string formula = "='" + item.sheetName + "'!" + Char.ToString((char)item.resultColumn++) + item.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)item.value1), formula);
                formula = "'" + item.sheetName + "'!" + Char.ToString((char)item.resultColumn++) + item.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)item.value2), formula);
                formula = "'" + item.sheetName + "'!" + Char.ToString((char)item.resultColumn++) + item.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)item.value3), formula);

                ExcelUtils.FindAndReplaceString(doc, templateSheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "BẢNG " + item.sheetIndex.ToString());

                currentIndex++;
            }

            char startColumn2 = 'A';
            double valueResult = 0;
            if (incr != null)
            {
                ExcelUtils.InsertRow(doc, templateSheetName, 6, ((uint)currentIndex), false);
                ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A" + currentIndex.ToString(), "II");
                ExcelUtils.FindAndReplaceString(doc, templateSheetName, "B" + currentIndex.ToString(), "Phát sinh");
                currentIndex++;

                ExcelUtils.FindAndReplaceNumber(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), (currentNo++).ToString());
                ExcelUtils.FindAndReplaceString(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), "Phần " + incr.name);
                string formula = "'" + incr.sheetName + "'!" + Char.ToString((char)incr.resultColumn++) + incr.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)incr.value1), formula);
                formula = "'" + incr.sheetName + "'!" + Char.ToString((char)incr.resultColumn++) + incr.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)incr.value2), formula);
                formula = "'" + incr.sheetName + "'!" + Char.ToString((char)incr.resultColumn++) + incr.resultRow;
                ExcelUtils.FindAndReplaceFormula(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney((decimal)incr.value3), formula);

                incrs = new ExcelResult
                {
                    sheetName = sheetName,
                    resultColumn = 'C',
                    resultRow = currentIndex,
                    value1 = incr.value1
                };

                ExcelUtils.FindAndReplaceString(doc, templateSheetName, Char.ToString((char)startColumn2++) + currentIndex.ToString(), "BẢNG " + incr.sheetIndex.ToString());
                currentIndex++;

                
            }
            if(currentIndex < 9)
            {
                if (incr != null)
                {
                    currentIndex = currentIndex - 2;
                }
                ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "C9", "C", 7, currentIndex - 1);
                ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "D9", "D", 7, currentIndex - 1);
                valueResult = ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "E9", "E", 7, currentIndex - 1);

                sum = new ExcelResult
                {
                    sheetName = sheetName,
                    resultColumn = 'E',
                    resultRow = currentIndex,
                    value1 = valueResult
                };

                currentIndex = 10;
            }
            else
            {
                int minusIndex = 1;
                if (incr != null)
                {
                    minusIndex = minusIndex + 2;
                }
                ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "C" + currentIndex.ToString(), "C", 7, currentIndex - minusIndex);
                ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "D" + currentIndex.ToString(), "D", 7, currentIndex - minusIndex);
                valueResult = ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, templateSheetName, "E" + currentIndex.ToString(), "E", 7, currentIndex - minusIndex);

                sum = new ExcelResult
                {
                    sheetName = sheetName,
                    resultColumn = 'E',
                    resultRow = currentIndex,
                    value1 = valueResult
                };
                currentIndex++;
            }
            

            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A" + currentIndex.ToString(), "BẰNG CHỮ :  " + IntUtils.ConvertNumberToVietnamese((int)valueResult).ToUpper() + " ĐỒNG");


            var cate = incr != null ? categoryName + " & PHẦN PHÁT SINH CÓ DUYỆT GIÁ" : categoryName;
            ExcelUtils.FindAndReplaceString(doc, templateSheetName, "A3", "HẠNG MỤC : " + cate);

            ExcelUtils.RenameSheet(doc, templateSheetName, sheetName);
            return (sum,incrs);
        }

        public static ExcelResult GenSheetIncurred(SpreadsheetDocument doc, int sheetcount, string projectTitle, string address,List<TaskCategory> cateList, List<ProjectTask> taskList)
        {
            string sheetName = "Sheet10";
            bool cloneRow = taskList.Count + cateList.Count > 3;
            List<ProjectTask> projectTasksIncurred = new List<ProjectTask>();
            foreach(var task in taskList)
            {
                if (task.TaskCategoryId == null) projectTasksIncurred.Add(task);
            }
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A1", "BẢNG " + sheetcount.ToString() + " : BẢNG QUYẾT TOÁN ");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A2", "CÔNG TRÌNH : " + projectTitle.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A3", "HẠNG MỤC : PHÁT SINH");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A4", "ĐỊA ĐIỂM : " + address.ToUpper());

            int currentIndex = 9;
            int count = 1;
            foreach (var cate in cateList)
            {
                if (currentIndex != 9) ExcelUtils.InsertRow(doc, sheetName, 9, ((uint)currentIndex), false);

                ExcelUtils.FindAndReplaceString(doc, sheetName, "A" + currentIndex.ToString(), IntUtils.IntToRoman(count++));

                string cateName = "PHẦN "+cate.Name.ToUpper();
                ExcelUtils.FindAndReplaceString(doc, sheetName, "B" + currentIndex.ToString(), cateName);
                currentIndex++;


                    int currentNo = 1;
                    foreach (var task in taskList.Where(t => t.TaskCategoryId == cate.Id).ToList())
                    {
                        char startColumn = 'A';
                    if(cloneRow)
                        ExcelUtils.InsertRow(doc, sheetName, (uint)currentIndex, ((uint)currentIndex), false);

                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), (currentNo++).ToString());
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Name);
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), CalculationUnitUtils.ConvertVietnamese(task.CalculationUnit));
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitInContract.ToString());
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                        if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                            ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                        else
                            ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");

                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit));
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitInContract));
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                        if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                            ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                        else
                            ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");
                        currentIndex++;

                    }
            }
            if (projectTasksIncurred.Count > 0)
            {

                if (currentIndex != 9) ExcelUtils.InsertRow(doc, sheetName, 9, ((uint)currentIndex), false);

                ExcelUtils.FindAndReplaceString(doc, sheetName, "A" + currentIndex.ToString(), IntUtils.IntToRoman(count++));

                string cateName = "PHẦN CHƯA PHÂN LOẠI";
                ExcelUtils.FindAndReplaceString(doc, sheetName, "B" + currentIndex.ToString(), cateName);
                currentIndex++;
                int i = 0;


                int currentNo = 1;
                foreach (var task in projectTasksIncurred)
                {
                    char startColumn = 'A';
                    ExcelUtils.InsertRow(doc, sheetName, (uint)currentIndex, ((uint)currentIndex), false);

                    ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), (currentNo++).ToString());
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Name);
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), CalculationUnitUtils.ConvertVietnamese(task.CalculationUnit));
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitInContract.ToString());
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                    if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                    else
                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");

                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit));
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitInContract));
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                    if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                    else
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");
                    currentIndex++;

                }


            }
            if (currentIndex < 12) currentIndex = 12;
            else if(cloneRow) currentIndex+=2;
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "H" + currentIndex.ToString(), "H", 10, currentIndex - 3);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "I" + currentIndex.ToString(), "I", 10, currentIndex - 3);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "J" + currentIndex.ToString(), "J", 10, currentIndex - 3);

            currentIndex++;
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "H" + currentIndex.ToString(), "H" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "I" + currentIndex.ToString(), "I" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), 10);

            currentIndex++;
            double value1 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "H" + currentIndex.ToString(), "H" + (currentIndex - 1).ToString(), "H" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value2 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "I" + currentIndex.ToString(), "I" + (currentIndex - 1).ToString(), "I" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value3 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), "J" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);


            string newSheetName = "PHAT SINH";
            ExcelUtils.RenameSheet(doc, sheetName, newSheetName);

            ExcelResult result = new ExcelResult
            {
                name = "phát sinh",
                sheetName =  newSheetName,
                sheetIndex = sheetcount,
                resultColumn = 'H',
                resultRow = currentIndex,
                value1 = value1,
                value2 = value2,
                value3 = value3
            };
            return result;
        }

        public static void GenSheetInteriorInProject(SpreadsheetDocument doc, string templateName, int sheetcount, string sheetName, string projectTitle, string categoryName, List<ItemInTask> interiorList)
        {
            ExcelUtils.RenameSheet(doc, templateName, sheetName);

            ExcelUtils.FindAndReplaceString(doc, sheetName, "A1", "BẢNG " + sheetcount.ToString() + " : BẢNG QUYẾT TOÁN ");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A2", "CÔNG TRÌNH : " + projectTitle.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A3", "HẠNG MỤC : " + categoryName.ToUpper());
            int currentIndex = 6;
            int order = 1;
            foreach (var item in interiorList)
            {
                char startColumn = 'A';
                if (order + 2 <= interiorList.Count)
                    ExcelUtils.InsertRow(doc, sheetName, (uint)currentIndex + 1, ((uint)currentIndex + 1), false);

                ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), order++.ToString());
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), item.InteriorItem.Name);
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), item.InteriorItem.Origin ?? "");

                currentIndex++;
            }
        }
        public static ExcelResult GenSheetTaskBaseCategory(SpreadsheetDocument doc, string templateName, int sheetcount, string sheetName, string projectTitle,string address, string categoryName , List<ProjectTask> taskList)
        {
            if (taskList.Count == 0) return null;
            ExcelUtils.RenameSheet(doc, templateName, sheetName);

            ExcelUtils.FindAndReplaceString(doc, sheetName, "A1", "BẢNG "+sheetcount.ToString()+" : BẢNG QUYẾT TOÁN ");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A2", "CÔNG TRÌNH : " +projectTitle.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A3", "HẠNG MỤC : " + categoryName.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A4", "ĐỊA ĐIỂM : " + address.ToUpper());

            int currentIndex = 9;
            int order = 1;
            foreach(var task in taskList)
            {
                char startColumn = 'A';
                if( order + 3 <= taskList.Count)
                ExcelUtils.InsertRow(doc, sheetName, (uint)currentIndex+1, ((uint)currentIndex+1), false);

                ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), order++.ToString());
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Name);
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), CalculationUnitUtils.ConvertVietnamese(task.CalculationUnit));
                ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitInContract.ToString());
                ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                if(task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                    ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                else
                    ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit));
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitInContract));
                ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                else
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");
                if(task.Room != null)
                {
                    ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Room.UsePurpose);
                }
                currentIndex++;

            }
            if (currentIndex < 12) currentIndex = 12;
            //else currentIndex++;
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "H" + currentIndex.ToString(), "H", 9, currentIndex - 1);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "I" + currentIndex.ToString(), "I", 9, currentIndex - 1);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "J" + currentIndex.ToString(), "J", 9, currentIndex - 1);

            currentIndex++;
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "H" + currentIndex.ToString(), "H" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "I" + currentIndex.ToString(), "I" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), 10);

            currentIndex++;
            double value1 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "H" + currentIndex.ToString(), "H" + (currentIndex - 1).ToString(), "H" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value2 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "I" + currentIndex.ToString(), "I" + (currentIndex - 1).ToString(), "I" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value3 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), "J" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);

            ExcelResult result = new ExcelResult
            {
                name = categoryName.ToLower(),
                sheetName= sheetName.ToUpper(),
                sheetIndex = sheetcount,
                resultColumn = 'H',
                resultRow = currentIndex,
                value1 = value1,
                value2 = value2,
                value3 = value3
            };
            return result;
        }
        public static ExcelResult GenSheetInterior(SpreadsheetDocument doc, int sheetcount, string projectTitle, string address, List<ProjectTask> taskList)
        {
            string sheetName = "Sheet9";

            ExcelUtils.FindAndReplaceString(doc, sheetName, "A1", "BẢNG " + sheetcount.ToString() + " : BẢNG QUYẾT TOÁN ");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A2", "CÔNG TRÌNH : " + projectTitle.ToUpper());
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A3", "HẠNG MỤC : CUNG CẤP LẮP ĐẶT NỘI THẤT");
            ExcelUtils.FindAndReplaceString(doc, sheetName, "A4", "ĐỊA ĐIỂM : " + address.ToUpper());

            List<Floor> floors = new List<Floor>();
            foreach (var task in taskList) {
                bool flag = true;
                foreach(var f in floors)
                {
                    if (task.Room.FloorId == f.Id) flag = false;
                }
                if (flag) floors.Add(task.Room.Floor); 
            }
            floors = floors.OrderBy(p=>p.FloorNo).ToList();
            List<Room> rooms = new List<Room>();
            foreach (var task in taskList)
            {
                bool flag = true;
                foreach (var r in rooms)
                {
                    if (task.RoomId == r.Id) flag = false;
                }
                if (flag) rooms.Add(task.Room);
            }

            int currentIndex = 9;
            int countFloor = 1;
            foreach (var floor in floors)
            {
                if (currentIndex != 9) ExcelUtils.InsertRow(doc, sheetName, 9, ((uint)currentIndex), false);

                ExcelUtils.FindAndReplaceString(doc, sheetName, "A" + currentIndex.ToString(), IntUtils.IntToRoman(countFloor++));
                string floorName = floor.FloorNo == 0 ? "TẦNG TRỆT" : "TẦNG " + floor.FloorNo;
                ExcelUtils.FindAndReplaceString(doc, sheetName, "B" + currentIndex.ToString(), floorName);
                currentIndex++;
                int i = 0;
                foreach (var room in rooms)
                {
                    string alphabelt = Char.ToString((char)('A' + i++));
                    if (currentIndex != 10) ExcelUtils.InsertRow(doc, sheetName, 10, ((uint)currentIndex), false);

                    ExcelUtils.FindAndReplaceString(doc, sheetName, "A" + currentIndex.ToString(), alphabelt);
                    ExcelUtils.FindAndReplaceString(doc, sheetName, "B" + currentIndex.ToString(), room.UsePurpose.ToUpper());

                    currentIndex++;
                    int currentNo = 1;
                    foreach (var task in taskList.Where(t => t.RoomId == room.Id).ToList())
                    {
                        char startColumn = 'A';
                        ExcelUtils.InsertRow(doc, sheetName, (uint)currentIndex, ((uint)currentIndex), false);

                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), (currentNo++).ToString());
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Name);
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Description);
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.Code);
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), CalculationUnitUtils.ConvertVietnamese(task.CalculationUnit));
                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitInContract.ToString());
                        ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                        if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                            ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), task.UnitUsed.ToString());
                        else
                            ExcelUtils.FindAndReplaceNumber(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");

                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit));
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitInContract));
                        ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                        if (task.Status == BusinessObject.Enums.ProjectTaskStatus.Done)
                            ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), IntUtils.ConvertStringToMoney(task.PricePerUnit * (decimal)task.UnitUsed));
                        else
                            ExcelUtils.FindAndReplaceString(doc, sheetName, Char.ToString((char)startColumn++) + currentIndex.ToString(), "0");
                        currentIndex++;

                    }

                }
            }

            currentIndex += 2;
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "J" + currentIndex.ToString(), "J", 11, currentIndex - 3);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "K" + currentIndex.ToString(), "K", 11, currentIndex - 3);
            ExcelUtils.FindAndReplaceCalculatorMutipleRow(doc, sheetName, "L" + currentIndex.ToString(), "L", 11, currentIndex - 3);

            currentIndex++;
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "K" + currentIndex.ToString(), "K" + (currentIndex - 1).ToString(), 10);
            ExcelUtils.FindAndReplaceCalculatorCellMultiple(doc, sheetName, "L" + currentIndex.ToString(), "L" + (currentIndex - 1).ToString(), 10);

            currentIndex++;
            double value1 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "J" + currentIndex.ToString(), "J" + (currentIndex - 1).ToString(), "J" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value2 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "K" + currentIndex.ToString(), "K" + (currentIndex - 1).ToString(), "K" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);
            double value3 = ExcelUtils.FindAndReplaceCalculatorCell(doc, sheetName, "L" + currentIndex.ToString(), "L" + (currentIndex - 1).ToString(), "L" + (currentIndex - 2).ToString(), ExcelUtils.Calculator.Sum);


            string newSheetName = "NOI THAT HOP DONG";
            ExcelUtils.RenameSheet(doc, sheetName, newSheetName);


            ExcelResult result = new ExcelResult
            {
                name = "nội thất",
                sheetName = newSheetName,
                sheetIndex = sheetcount,
                resultColumn = 'J',
                resultRow = currentIndex,
                value1 = value1,
                value2 = value2,
                value3 = value3
            };
            return result;
        }
        

    }
}
