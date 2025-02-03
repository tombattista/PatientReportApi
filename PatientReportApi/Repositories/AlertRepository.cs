using PatientReportApi.Repositories.Interfaces;

namespace PatientReportApi.Repositories;

public class AlertRepository() : IAlertRepository
{
    /// <inheritdoc />
    public IEnumerable<string> GetAllAlertTerms() => [ "tachycardia", "arrhythmia" ];
}
