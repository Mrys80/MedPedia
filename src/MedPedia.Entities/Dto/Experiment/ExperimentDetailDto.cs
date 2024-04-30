using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Experiment;

public class ExperimentDetailDto : IDto
{
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
}