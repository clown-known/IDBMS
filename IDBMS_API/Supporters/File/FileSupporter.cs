using Azure.Core;
using BusinessObject.Enums;
using BusinessObject.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.Constants;
using IDBMS_API.DTOs.Request;
using IDBMS_API.Supporters.Utils;
using Repository.Implements;
using System.IO;

namespace IDBMS_API.Supporters.File
{
    public class FileSupporter
    {
        public static byte[] GenContractForCompanyFileBytes(byte[] docxBytes, ContractRequest request)
        { 
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(docxBytes, 0, docxBytes.Length);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream,true ))
                {
                    DateTime time = DateTime.Now;

                    FindAndReplaceText(doc, "[Code]", GetCode());
                    FindAndReplaceText(doc, "[CreatedDate]", time.Day.ToString());
                    FindAndReplaceText(doc, "[CreatedMonth]", time.Month.ToString());
                    FindAndReplaceText(doc, "[CreatedYear]", time.Year.ToString());
                    FindAndReplaceText(doc, "[ACompanyName]", request.ACompanyName);
                    FindAndReplaceText(doc, "[ACompanyAddress]", request.ACompanyAddress);
                    FindAndReplaceText(doc, "[CompanyCode]", request.ACompanyCode);
                    FindAndReplaceText(doc, "[ARepresentative]", request.AOwnerName);
                    FindAndReplaceText(doc, "[APhone]", request.APhone);
                    FindAndReplaceText(doc, "[Position]", request.APosition);
                    FindAndReplaceText(doc, "[AEmail]", request.AEmail);

                    FindAndReplaceText(doc, "[BName]", request.BCompanyName);
                    FindAndReplaceText(doc, "[BAddress]", request.BCompanyAddress);
                    FindAndReplaceText(doc, "[BCompanyCode]", request.BSwiftCode);
                    FindAndReplaceText(doc, "[BRepresentative]", request.BRepresentedBy);
                    FindAndReplaceText(doc, "[BPhone]",request.BCompanyPhone);
                    FindAndReplaceText(doc, "[BPosition]", request.BPosition);
                    FindAndReplaceText(doc, "[BEmail]",request.BEmail);
                    FindAndReplaceText(doc, "[NumOfCopies]", request.NumOfCopie.ToString());
                    FindAndReplaceText(doc, "[NumOfA]", request.NumOfA.ToString());
                    FindAndReplaceText(doc, "[NumOfB]", request.NumOfB.ToString());
                    FindAndReplaceText(doc, "[NumOfPages]", ""+ CountPages(doc));
                    FindAndReplaceText(doc, "[ProjectName]",request.ProjectName);
                    FindAndReplaceText(doc, "[PaymentMethod]", request.PaymentMethod);
                    FindAndReplaceText(doc, "[Value]", ""+ IntUtils.ConvertStringToMoney(request.Value));
                    FindAndReplaceText(doc, "[Money]", ""+ IntUtils.ConvertNumberToVietnamese((int)request.Value));
                    FindAndReplaceText(doc, "[StartedDate]", time.Day.ToString()+"/"+time.Month.ToString()+"/"+time.Year.ToString());
                    FindAndReplaceText(doc, "[EstimateBusinessDay]", request.EstimateDays.ToString());

                    doc.Save();
                }
                stream.Position = 0;
                return stream.ToArray();
            }
        }
        public static byte[] GenContractForCustomerFileBytes(byte[] docxBytes, ContractForCustomerRequest request)
        {
            using (MemoryStream stream = new MemoryStream(docxBytes))
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    DateTime time = DateTime.Now;
                    string stringDate = time.Day.ToString() + "/" + time.Month.ToString() + "/" + time.Year.ToString();
                    FindAndReplaceText(doc, "[Code]", GetCode());
                    FindAndReplaceText(doc, "[CreatedDate]", time.Day.ToString());
                    FindAndReplaceText(doc, "[CreatedMonth]", time.Month.ToString());
                    FindAndReplaceText(doc, "[CreatedYear]", time.Year.ToString());
                    FindAndReplaceText(doc, "[CustomerName]", request.CustomerName);
                    FindAndReplaceText(doc, "[YearOfBirth]", DateParse(request.DateOfBirth));
                    FindAndReplaceText(doc, "[Address]", request.Address);
                    FindAndReplaceText(doc, "[Phone]", request.Phone);
                    FindAndReplaceText(doc, "[Email]", request.Email);
                    FindAndReplaceText(doc, "[IdentityCode]", request.IdentityCode);
                    FindAndReplaceText(doc, "[Created]", DateParse(request.CodeCreatedDate));
                    FindAndReplaceText(doc, "[IssuedBy]", request.IssuedBy);
                    FindAndReplaceText(doc, "[RegisteredPlaceOfPermanentResidence]", request.RegisteredPlaceOfPermanentResidence);

                    FindAndReplaceText(doc, "[BName]", request.BCompanyName);
                    FindAndReplaceText(doc, "[BAddress]", request.BCompanyAddress);
                    FindAndReplaceText(doc, "[BCompanyCode]", request.BSwiftCode);
                    FindAndReplaceText(doc, "[BRepresentative]", request.BRepresentedBy);
                    FindAndReplaceText(doc, "[BPhone]", request.BCompanyPhone);
                    FindAndReplaceText(doc, "[BPosition]", request.BPosition);
                    FindAndReplaceText(doc, "[BEmail]", request.BEmail);
                    FindAndReplaceText(doc, "[NumOfCopies]", request.NumOfCopie.ToString());
                    FindAndReplaceText(doc, "[NumOfA]", request.NumOfA.ToString());
                    FindAndReplaceText(doc, "[NumOfB]", request.NumOfB.ToString());
                    FindAndReplaceText(doc, "[NumOfPages]", "" + CountPages(doc));
                    FindAndReplaceText(doc, "[ProjectName]", request.ProjectName);
                    FindAndReplaceText(doc, "[PaymentMethod]", request.PaymentMethod);
                    FindAndReplaceText(doc, "[Value]", "" + IntUtils.ConvertStringToMoney(request.Value));
                    FindAndReplaceText(doc, "[Money]", "" + IntUtils.ConvertNumberToVietnamese((int)request.Value));
                    FindAndReplaceText(doc, "[StartedDate]", stringDate);
                    FindAndReplaceText(doc, "[EstimateBusinessDay]", request.EstimateDays.ToString());
                    doc.Save();
                }
            }
            return docxBytes;
        }
        static private string DateParse(DateTime input)
        {
            string day = input.Day > 10 ? input.Day.ToString() : "0" + input.Day.ToString();
            string month = input.Month > 10 ? input.Month.ToString() : "0" + input.Month.ToString();
            return  day + "/" + month+ "/" + input.Year.ToString();
        }
        static private string GetCode()
        {
            DateTime input = DateTime.Now;
            string day = input.Day > 10 ? input.Day.ToString() : "0" + input.Day.ToString();
            string month = input.Month > 10 ? input.Month.ToString() : "0" + input.Month.ToString();
            return  day + "" + month+ "/" + input.Year.ToString();
        }
        
        static public int CountPages(WordprocessingDocument doc)
        {
            int pageCount = 1; // Assume at least one page

            // Get the body of the document
            Body body = doc.MainDocumentPart.Document.Body;

            // Iterate through paragraphs to count page breaks
            foreach (Paragraph paragraph in body.Elements<Paragraph>())
            {
                if (paragraph.Elements<Run>().Any(run => run.Elements<Break>().Any(b => b.Type == BreakValues.Page)))
                {
                    pageCount++;
                }
            }

            return pageCount;
        }
        static public void FindAndReplaceText(WordprocessingDocument doc, string search, string replace)
        {

            MainDocumentPart mainPart = doc.MainDocumentPart;

            // Access the body of the document
            Body body = mainPart.Document.Body;

            // Find and replace the text in runs and paragraphs
            foreach (var run in body.Descendants<Run>())
            {
                foreach (var textElement in run.Descendants<Text>())
                {
                    if (textElement.Text.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        textElement.Text = textElement.Text.Replace(search, replace);
                    }
                }
            }

            foreach (var paragraph in body.Descendants<Paragraph>())
            {
                foreach (var textElement in paragraph.Descendants<Text>())
                {
                    if (textElement.Text.Contains(search, StringComparison.OrdinalIgnoreCase))
                    {
                        textElement.Text = textElement.Text.Replace(search, replace);
                    }
                }
            }

            // Save the changes to the document
            mainPart.Document.Save();
        }
    }
}
