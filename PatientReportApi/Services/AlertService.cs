
using PatientReportApi.Repositories.Interfaces;
using PatientReportApi.Services.Interfaces;

namespace PatientReportApi.Services;

public class AlertService(ILogger<AlertService> logger, IAlertRepository repository) : IAlertService
{
    private readonly ILogger<AlertService> _logger = logger;
    private readonly IAlertRepository _alertRepository = repository;

    /// <inheritdoc />
    public IEnumerable<string> GetAlertTerms()
    {
        try
        {
            return _alertRepository.GetAllAlertTerms();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving Alert Terms");
        }
        return [];
    }
}
