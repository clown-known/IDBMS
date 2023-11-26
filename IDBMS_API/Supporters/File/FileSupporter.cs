using BusinessObject.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using IDBMS_API.Constants;

namespace IDBMS_API.Supporters.File
{
    public class FileSupporter
    {
        public static byte[] GenContractForCompanyFileBytes(byte[] docxBytes,Project project,int? NumOfCopies,int? NumOfA,int? NumOfB)
        {
            if (project == null) return null;
            using (MemoryStream stream = new MemoryStream(docxBytes))
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    DateTime.UtcNow.AddHours(7).Month.ToString();
                    DateTime time = DateTime.UtcNow.AddHours(7);
                    User owner = project.Participations.Where(p => p.Role == BusinessObject.Enums.ParticipationRole.ProductOwner)
                        .FirstOrDefault().User;
                    FindAndReplaceText(doc, "[CreatedDate]", time.Day.ToString());
                    FindAndReplaceText(doc, "[CreatedMonth]", time.Month.ToString());
                    FindAndReplaceText(doc, "[CreatedYear]", time.Year.ToString());
                    FindAndReplaceText(doc, "[CompanyName]", project.CompanyName);
                    FindAndReplaceText(doc, "[CompanyAdress]", project.CompanyAdress);
                    FindAndReplaceText(doc, "[Representative]",owner.Name);
                    FindAndReplaceText(doc, "[Phone]", owner.Phone);
                    FindAndReplaceText(doc, "[Email]", owner.Email);
                    FindAndReplaceText(doc, "[BName]", ConstantValue.CompanyName);
                    FindAndReplaceText(doc, "[BAdress]", ConstantValue.CompanyAddress);
                    FindAndReplaceText(doc, "[BRepresentative]", ConstantValue.CompanyRep);
                    FindAndReplaceText(doc, "[BPhone]", ConstantValue.CompanyPhone);
                    FindAndReplaceText(doc, "[BPosition]", ConstantValue.CompanyPhone);
                    FindAndReplaceText(doc, "[BEmail]", ConstantValue.CompanyEmail);
                    FindAndReplaceText(doc, "[NumOfCopies]",""+ NumOfCopies?? ConstantValue.NumOfCopies.ToString());
                    FindAndReplaceText(doc, "[NumOfA]", ""+ NumOfA ?? ConstantValue.NumOfA.ToString());
                    FindAndReplaceText(doc, "[NumOfB]", ""+ NumOfB ?? ConstantValue.NumOfB.ToString());
                    FindAndReplaceText(doc, "[NumOfPages]", ""+ CountPages(doc));
                }
            }
            return docxBytes;
        }
        public static byte[] GenContractForCustomerFileBytes(byte[] docxBytes,Project project,int? NumOfCopies,int? NumOfA,int? NumOfB)
        {
            if (project == null) return null;
            using (MemoryStream stream = new MemoryStream(docxBytes))
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    DateTime.UtcNow.AddHours(7).Month.ToString();
                    DateTime time = DateTime.UtcNow.AddHours(7);
                    User owner = project.Participations.Where(p => p.Role == BusinessObject.Enums.ParticipationRole.ProductOwner)
                        .FirstOrDefault().User;
                    FindAndReplaceText(doc, "[CreatedDate]", time.Day.ToString());
                    FindAndReplaceText(doc, "[CreatedMonth]", time.Month.ToString());
                    FindAndReplaceText(doc, "[CreatedYear]", time.Year.ToString());
                    FindAndReplaceText(doc, "[CustomerName]", owner.Name);
                    FindAndReplaceText(doc, "[YearOfBirth]", owner.YearOfBirth.ToString());
                    FindAndReplaceText(doc, "[Adress]", owner.Address);
                    FindAndReplaceText(doc, "[Phone]", owner.Phone);
                    FindAndReplaceText(doc, "[Email]", owner.Email);
                    FindAndReplaceText(doc, "[BName]", ConstantValue.CompanyName);
                    FindAndReplaceText(doc, "[BAdress]", ConstantValue.CompanyAddress);
                    FindAndReplaceText(doc, "[BRepresentative]", ConstantValue.CompanyRep);
                    FindAndReplaceText(doc, "[BPhone]", ConstantValue.CompanyPhone);
                    FindAndReplaceText(doc, "[BPosition]", ConstantValue.CompanyPhone);
                    FindAndReplaceText(doc, "[BEmail]", ConstantValue.CompanyEmail);
                    FindAndReplaceText(doc, "[NumOfCopies]",""+ NumOfCopies?? ConstantValue.NumOfCopies.ToString());
                    FindAndReplaceText(doc, "[NumOfA]", ""+ NumOfA ?? ConstantValue.NumOfA.ToString());
                    FindAndReplaceText(doc, "[NumOfB]", ""+ NumOfB ?? ConstantValue.NumOfB.ToString());
                    FindAndReplaceText(doc, "[NumOfPages]", ""+ CountPages(doc));
                }
            }
            return docxBytes;
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
        static public void FindAndReplaceText(WordprocessingDocument doc, string findText, string replaceText)
        {
            // Get the main document part
            MainDocumentPart mainPart = doc.MainDocumentPart;

            // Create a list of paragraphs in the document
            var paragraphs = mainPart.Document.Body.Elements<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                // Create a list of runs in the paragraph
                var runs = paragraph.Elements<Run>();

                foreach (var run in runs)
                {
                    // Get the text within the run
                    var text = run.Elements<Text>().FirstOrDefault();

                    if (text != null && text.Text.Contains(findText))
                    {
                        // Replace the text
                        text.Text = text.Text.Replace(findText, replaceText);
                    }
                }
            }
        }
    }
}
