using BusinessObject.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
namespace IDBMS_API.Supporters.File
{
    public class FileSupporter
    {
        public static byte[] GenContractForCompanyFileBytes(byte[] docxBytes,Project project)
        {
            using (MemoryStream stream = new MemoryStream(docxBytes))
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, true))
                {
                    DateTime.UtcNow.AddHours(7).Month.ToString();
                    DateTime time = DateTime.UtcNow.AddHours(7);
                    FindAndReplaceText(doc, "[CreatedDate]", time.Day.ToString());
                    FindAndReplaceText(doc, "[CreatedMonth]", time.Month.ToString());
                    FindAndReplaceText(doc, "[CreatedYear]", time.Year.ToString());
                    FindAndReplaceText(doc, "[CompanyName]", project.CompanyName);
                    FindAndReplaceText(doc, "[CompanyAdress]", "");
                }
            }
            return docxBytes;
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
