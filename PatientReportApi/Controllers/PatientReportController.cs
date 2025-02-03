using Microsoft.AspNetCore.Mvc;
using PatientReportApi.Models;
using PatientReportApi.Services.Interfaces;

namespace PatientReportApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientReportController(ILogger<PatientReportController> logger, IReportService reportService) : ControllerBase
{
    private readonly ILogger<PatientReportController> _logger = logger;
    private readonly IReportService _reportService = reportService;

    [HttpGet]
    public ActionResult<IEnumerable<PatientReport>> Get()
    {
        _logger.LogInformation("Retrieving all patient reports.");
        return Ok(_reportService.GetReports());
    }

    [HttpGet("{id}")]
    public ActionResult<PatientReport> Get(Guid id)
    {
        _logger.LogInformation("Retrieving all patient reports.", [id]);
        var item = _reportService.GetReport(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet("patient/{name}")]
    public ActionResult<PatientReport> GetPatientReports(string name)
    {
        _logger.LogInformation("Retrieving all patient reports.", [name]);
        return Ok(_reportService.GetReports(name));
    }
}
