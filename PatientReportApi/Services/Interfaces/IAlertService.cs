namespace PatientReportApi.Services.Interfaces;

public interface IAlertService
{
    /// <summary>
    /// Retrieves all alert terms
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetAlertTerms();
}
