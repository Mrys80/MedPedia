using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Dto.Drug;

namespace MedPedia.Entities.Dto.Experiment;

public class ExperimentInformationDto : IDto
{
    public int ExperimentId { get; set; }
    
    public KeyValueDto Abbreviation { get; set; }
    
    public KeyValueDto Usage { get; set; }
    
    public KeyValueDto InterferingFactors { get; set; }
    
    public KeyValueDto Description { get; set; }
    
    public KeyValueDto NormalSuffering { get; set; }
    
    public KeyValueDto ScopeOfCrisis { get; set; }
    
    public KeyValueDto CommonCausesOfDeficiencyOrExcess { get; set; }
    
    public KeyValueDto RelatedSymptomsOrDisease { get; set; }
    
    public KeyValueDto PreSamplingConditions { get; set; }
    
    public KeyValueDto RequiredSample { get; set; }
    
    public KeyValueDto VialType { get; set; }
    
    public KeyValueDto RelatedTests { get; set; }
    
    public KeyValueDto ClinicaNotes { get; set; }
    
    public KeyValueDto TestLimitations { get; set; }
    
    public KeyValueDto Title { get; set; }

    public KeyValueDto? ImageId { get; set; }

    public string FaTitle { get; set; }
    
    public string EnTitle { get; set; }
    
}