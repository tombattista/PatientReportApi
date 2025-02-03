using PatientReportApi.Models;

namespace PatientReportApi.Services.Interfaces;

public interface IReportService
{
    /// <summary>
    /// Get patient report with the specified id, if it exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    PatientReport? GetReport(Guid id);

    /// <summary>
    /// Get reports with the specified patient name
    /// </summary>
    /// <param name="patientName"></param>
    /// <returns></returns>
    IEnumerable<PatientReport> GetReports(string patientName = "");

    /// <summary>
    /// Updates a report with specified data, or adds a new report if a report with the corresponding
    /// id does not already exist
    /// </summary>
    /// <param name="report"></param>
    /// <returns></returns>
    bool AddOrUpdateReport(PatientReport report);

    /// <summary>
    /// Deletes the report with the specified id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool DeleteReport(Guid id);
}
