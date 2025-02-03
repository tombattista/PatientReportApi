using PatientReportApi.CustomExceptions;
using PatientReportApi.Models;
using PatientReportApi.Repositories.Interfaces;
using System;
using System.Text.Json;

namespace PatientReportApi.Repositories;

/// <inheritdoc />
public class InMemoryReportRepository : IInMemoryReportRepository
{
    private readonly ILogger<InMemoryReportRepository> _logger;
    private readonly IConfiguration _configuration;

    private readonly List<PatientReport> _items = [
        new PatientReport()
        {
            Id = Guid.NewGuid(),
            PatientName = "Tom Battista",
            ReportDate = DateTime.Parse("12/26/2024"),
            Summary = "Summary text for 1..."
        },
        new PatientReport()
        {
            Id = Guid.NewGuid(),
            PatientName = "Tom Battista",
            ReportDate = DateTime.Parse("12/01/2024"),
            Summary = @"Tachycardia is a medical condition characterized by an abnormally fast heart rate.It can be caused by various factors,
                        including stress, anxiety, or an underlying health condition.Symptoms may include dizziness, shortness of breath, and
                        fatigue.Treatment typically involves lifestyle changes, medications, or medical interventions to correct the heart rate."
        },
        new PatientReport()
        {
            Id = Guid.NewGuid(),
            PatientName = "Tom Battista",
            ReportDate = DateTime.Parse("11/18/2024"),
            Summary = @"Arrhythmia refers to an irregular heartbeat, which can manifest as a variation in the rhythm or rate of the heart's contractions.",
        },
        new PatientReport()
        {
            Id = Guid.NewGuid(),
            PatientName = "Tom Battista",
            ReportDate = DateTime.Parse("10/18/2024"),
            Summary = @"Wherein the heart flutters eratically, therein shall arrhythmia be found."
        },
        new PatientReport()
        {
            Id = Guid.NewGuid(),
            PatientName = "Julie Battista",
            ReportDate = DateTime.Parse("12/18/2024"),
            Summary = @"Too much glue on a 3x5 index card leads to tachycardia."
        }
    ];

    /// <summary>
    /// Constructor - load data
    /// </summary>
    /// <param name="logger"></param>
    public InMemoryReportRepository(ILogger<InMemoryReportRepository> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        LoadData().GetAwaiter().GetResult();
    }

    /// <inheritdoc />
    public IEnumerable<PatientReport> GetAll() => _items;

    /// <inheritdoc />
    public IEnumerable<PatientReport> GetByPatientName(string patientName) => _items.Where(rpt => string.Compare(rpt.PatientName, patientName, StringComparison.OrdinalIgnoreCase) == 0);

    /// <inheritdoc />
    public PatientReport? GetById(Guid id) => _items.FirstOrDefault(item => item.Id == id);

    /// <inheritdoc />
    public bool AddOrUpdate(PatientReport item)
    {
        try
        {
            var index = _items.FindIndex(existingItem => existingItem.Id == item.Id);
            if (index != -1)
            {
                _items[index] = item;
            }
            else
            {
                _items.Add(item);
            }
            return true;
        }
        catch (ArgumentNullException anex)
        {
            _logger.LogError(new RepositoryException("Invalid argument passed to InMemoryReportRepository.AddOrUpdate", anex), anex.Message);
        }
        catch (OutOfMemoryException oomex)
        {
            _logger.LogError(new RepositoryException("Insufficient memory encountered during InMemoryReportRepository.AddOrUpdate", oomex), oomex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(new RepositoryException("Error encountered during InMemoryReportRepository.AddOrUpdate", ex), ex.Message);
        }
        return false;
    }

    /// <inheritdoc />
    public bool Delete(Guid id)
    {
        var item = _items.FirstOrDefault(existingItem => existingItem.Id == id);
        if (item != null)
        {
            try
            {
                return _items.Remove(item);
            }
            catch (ArgumentNullException anex)
            {
                _logger.LogError(new RepositoryException("Invalid argument passed to InMemoryReportRepository.Delete", anex), anex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(new RepositoryException("Error encountered during InMemoryReportRepository.Delete", ex), ex.Message);
            }
            return false;
        }

        _logger.LogWarning($"Unable to delete PatientReport. Item with id {id} does not exist.");
        return true;
    }

    /// <summary>
    /// Loads data into _items collection.
    /// </summary>
    private async Task LoadData()
    {
        string url = GetDataUrl();

        PatientReport[] data = await FetchData(url);

        if (data is not null)
        {
            _items.Clear();
            _items.AddRange(data);
        }
    }

    /// <summary>
    /// Fetches data from via Http call.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private async Task<PatientReport[]> FetchData(string url)
    {
        using HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string jsonResponse = (await response.Content.ReadAsStringAsync()).Trim('"');
        try
        {
            PatientReport[] data = JsonSerializer.Deserialize<PatientReport[]>(jsonResponse) ?? [];
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(new RepositoryException("Unable to parse report data.", ex), ex.Message);
        }
        return [];
    }

    /// <summary>
    /// Gets url from app settings.
    /// </summary>
    /// <returns></returns>
    private string GetDataUrl()
    {
        return _configuration["AppSettings:ReportFilePath"] ?? "";
    }
}
