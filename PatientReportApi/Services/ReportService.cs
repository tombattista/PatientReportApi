using PatientReportApi.Models;
using PatientReportApi.Repositories.Interfaces;
using PatientReportApi.Services.Interfaces;

namespace PatientReportApi.Services;

public class ReportService(IInMemoryReportRepository repository) : IReportService
{
    private readonly IInMemoryReportRepository _repository = repository;

    /// <inheritdoc />
    public bool AddOrUpdateReport(PatientReport report)
    {
        return _repository.AddOrUpdate(report);
    }

    /// <inheritdoc />
    public bool DeleteReport(Guid id)
    {
        return _repository.Delete(id);
    }

    /// <inheritdoc />
    public PatientReport? GetReport(Guid id)
    {
        return _repository.GetById(id);
    }

    /// <inheritdoc />
    public IEnumerable<PatientReport> GetReports(string patientName = "")
    {
        return string.IsNullOrWhiteSpace(patientName)
            ? _repository.GetAll()
            : _repository.GetByPatientName(patientName);
    }
}
