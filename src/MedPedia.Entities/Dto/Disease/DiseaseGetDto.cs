using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Entities.Dto.Disease;

public class DiseaseGetDto : IDto
{
    public int Id { get; set; }
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string DefinitionOfDisease { get; set; }
    public string Signs { get; set; }
    public string Cause { get; set; }
    public string RiskFactors { get; set; }
    public string TransmissionWays { get; set; }
    public string Complications { get; set; }
    public string PreparingForVisit { get; set; }
    public string WhenVisit { get; set; }
    public string Recovery { get; set; }
    public string DiagnosticTests { get; set; }
    public string DrugTreatment { get; set; }
    public string Treatment { get; set; }
    public string ChangeOfLifestyle { get; set; }
    public string ImportantPoints { get; set; }
    public string BasicPointsOfTreatment { get; set; }
    public string Prevention { get; set; }    public string? ImageId { get; set; }
}