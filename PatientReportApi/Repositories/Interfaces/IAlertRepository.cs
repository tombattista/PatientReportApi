namespace PatientReportApi.Repositories.Interfaces;

public interface IAlertRepository
{
    /// <summary>
    /// Retrieves all patient reports
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetAllAlertTerms();
}
