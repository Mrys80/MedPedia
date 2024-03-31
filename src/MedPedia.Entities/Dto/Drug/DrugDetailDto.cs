using MedPedia.Common.Entities.Abstracts;
using MedPedia.Entities.Dto.PharmaceuticalForms;
using MedPedia.Entities.Dto.SideEffect;

namespace MedPedia.Entities.Dto.Drug;

public class DrugDetailDto : IDto
{
    public string FaTitle { get; set; }
    public string EnTitle { get; set; }
    public string Brand { get; set; }
    public string? ImageId { get; set; }
    public string CategoryFaTitle { get; set; }
    public string CategoryEnTitle { get; set; }
    public string PharmacologicalClassification { get; set; }
    public string TherapeuticClassification { get; set; }
    public string MartindaleClassification { get; set; }
    public string PregnancyClassification { get; set; }
    public string MethodOfUse { get; set; }
    public string Indication { get; set; }
    public string MechanismOfEffect { get; set; }
    public string BeginningOfEffect { get; set; }
    public string PeakOfEffect { get; set; }
    public string Pharmacokinetics { get; set; }
    public string PrescriptionIndications { get; set; }
    public string Contraindicated { get; set; }
    public string IncreaseEffect { get; set; }
    public string ReduceEffect { get; set; }
    public string AvoidInteractions { get; set; }
    public string FoodInteractions { get; set; }
    public string DrugInteractions { get; set; }
    public string PoisoningAndTreatment { get; set; }
    public string Precautions { get; set; }
    public string SpecialAttention { get; set; }
    public string UseInBreastfeedingAndChildren { get; set; }
    public string Education { get; set; }
    public List<PharmaceuticalFormsGetDto> forms { get; set; }
    public string sideEffects { get; set; }
}