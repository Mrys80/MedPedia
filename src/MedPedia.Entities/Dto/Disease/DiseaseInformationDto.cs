using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Dto.Drug;

namespace MedPedia.Entities.Dto.Disease;

public class DiseaseInformationDto : IDto
{
    public string FaTitle { get; set; }
    
    public string EnTitle { get; set; }

    public int DiseaseId { get; set; }
    
    public KeyValueDto? ImageId { get; set; }
    
    public KeyValueDto DefinitionOfDisease { get; set; }
    
    public KeyValueDto Signs { get; set; }
    
    public KeyValueDto Cause { get; set; }
    
    public KeyValueDto RiskFactors { get; set; }
    
    public KeyValueDto TransmissionWays { get; set; }
    
    public KeyValueDto Complications { get; set; }
    
    public KeyValueDto PreparingForVisit { get; set; }
    
    public KeyValueDto WhenVisit { get; set; }
    
    public KeyValueDto Recovery { get; set; }
    
    public KeyValueDto DiagnosticTests { get; set; }
    
    public KeyValueDto DrugTreatment { get; set; }
    
    public KeyValueDto Treatment { get; set; }
    
    public KeyValueDto ChangeOfLifestyle { get; set; }
    
    public KeyValueDto ImportantPoints { get; set; }
    
    public KeyValueDto BasicPointsOfTreatment { get; set; }
    
    public KeyValueDto Prevention { get; set; }
}