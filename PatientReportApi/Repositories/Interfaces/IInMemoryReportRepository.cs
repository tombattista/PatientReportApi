using PatientReportApi.Models;

namespace PatientReportApi.Repositories.Interfaces;

/// <summary>
/// In-memory data repository
/// </summary>
public interface IInMemoryReportRepository
{
    /// <summary>
    /// Retrieves all patient reports
    /// </summary>
    /// <returns></returns>
    IEnumerable<PatientReport> GetAll();

    /// <summary>
    /// Retrieves all patient reports with the specified patient name
    /// </summary>
    /// <param name="patientName"></param>
    /// <returns></returns>
    IEnumerable<PatientReport> GetByPatientName(string patientName);

    /// <summary>
    /// Returns the patient report with the specified id, if found
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    PatientReport? GetById(Guid id);

    /// <summary>
    /// Updates a PatientReport or, if an item with its id already exists, adds it to the repository
    /// </summary>
    /// <param name="item"></param>
    bool AddOrUpdate(PatientReport item);

    /// <summary>
    /// Deletes a PatientReport by id
    /// </summary>
    /// <param name="id"></param>
    bool Delete(Guid id);
}
