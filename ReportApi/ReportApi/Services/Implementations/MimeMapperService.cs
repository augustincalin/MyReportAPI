using DocumentFormat.OpenXml.Drawing;
using ReportApi.Services.Interfaces;

namespace ReportApi.Services.Implementations
{
    public class MimeMapperService : IMimeMapperService
    {
        public (string Format, string MimeType) GetFormat(string acceptHeader)
        {
            var result = (Format: "PDF", MimeType: "application/pdf");
            if (acceptHeader != null)
            {
                var values = acceptHeader.Split(',');
                foreach (var value in values)
                {
                    result = value switch
                    {
                        "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ("DOCX", value),
                        "application/pdf" => ("PDF", value),
                        "text/csv" => ("CSV", value),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" => ("XLSX", value),
                        "application/vnd.openxmlformats-officedocument.presentationml.presentation" => ("PPTX", value),
                        "application/rtf" => ("RTF", value),
                        "image/tiff" => ("IMAGE", value),
                        _ => (string.Empty, string.Empty)
                    };
                    if (string.Empty != result.Format)
                    {
                        break;
                    }
                }
            }

            return result.Format == string.Empty ? ("PDF", "application/pdf") : result;
        }
    }
}
