using System.Collections;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using ReportApi.Services.Interfaces;

namespace ReportApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IDataService _dataService;
        private readonly IMimeMapperService _mimeMapperService;

        public ReportsController(ILogger<ReportsController> logger, IDataService dataService, IMimeMapperService mimeMapperService)
        {
            _logger = logger;
            _dataService = dataService;
            _mimeMapperService = mimeMapperService;
        }

        [HttpGet]
        public IActionResult ReportAsync([FromHeader] string accept)
        {
            var format = _mimeMapperService.GetFormat(accept);
            Hashtable info = new Hashtable();
            ////if (format == "IMAGE")
            ////{
            ////    info["OutputFormat"] = "PNG";
            ////}
            var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
            var reportSource = new Telerik.Reporting.UriReportSource();
            reportSource.Uri = "Reports/ImmoWertReport.trbp";

            reportSource.Parameters.Add("reports(0).InputData", _dataService["cover"]);

            var data = reportProcessor.RenderReport(format.Format, reportSource, info);
            return new FileContentResult(data.DocumentBytes, format.MimeType);
        }

        ////[HttpGet]
        ////public IEnumerable<WeatherForecast> Get()
        ////{
        ////    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        ////    {
        ////        Date = DateTime.Now.AddDays(index),
        ////        TemperatureC = Random.Shared.Next(-20, 55),
        ////        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        ////    })
        ////    .ToArray();
        ////}
    }
}