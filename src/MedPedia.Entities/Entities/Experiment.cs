using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class Experiment:BaseEntity
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string Abbreviation { get; set; }
    public string Usage { get; set; }
    public string InterferingFactors { get; set; }
    public string Description { get; set; }
    public string NormalSuffering { get; set; }
    public string ScopeOfCrisis { get; set; }
    public string CommonCausesOfDeficiencyOrExcess { get; set; }
    public string RelatedSymptomsOrDisease { get; set; }
    public string PreSamplingConditions { get; set; }
    public string RequiredSample { get; set; }
    public string VialType { get; set; }
    public string RelatedTests { get; set; }
    public string ClinicaNotes { get; set; }
    public string TestLimitations { get; set; }
    public string? ImageId { get; set; }
    public int ExperimentCategoryId { get; set; }
}