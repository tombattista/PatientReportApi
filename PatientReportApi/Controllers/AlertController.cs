using Microsoft.AspNetCore.Mvc;
using PatientReportApi.Models;
using PatientReportApi.Services.Interfaces;

namespace PatientReportApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlertController(ILogger<AlertController> logger, IAlertService alertService) : ControllerBase
{
    private readonly ILogger<AlertController> _logger = logger;
    private readonly IAlertService _alertService = alertService;


    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        _logger.LogInformation("Retrieving all alert terms.");
        return Ok(_alertService.GetAlertTerms());
    }
}
