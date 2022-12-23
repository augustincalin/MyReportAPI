using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace ReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController: ControllerBase
    {
        [HttpGet]
        public IActionResult ReportAsync() {
            var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            var reportSource = new Telerik.Reporting.UriReportSource
            {
                Uri = "Reports/Report1.trdp"
            };
            var data = reportProcessor.RenderReport("PDF", reportSource, null);
            return new FileContentResult(data.DocumentBytes, "application/pdf");
        }
    }
}
