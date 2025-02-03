namespace PatientReportApi.Models;

public class PatientReport
{
    public Guid Id { get; set; }

    public string PatientName { get; set; } = "";

    public DateTime ReportDate { get; set; }

    public string Summary { get; set; } = "";
}
