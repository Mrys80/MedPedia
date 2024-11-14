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
    public KeyValueDto PharmacologicalClassification { get; set; }
    public KeyValueDto TherapeuticClassification { get; set; }
    public KeyValueDto MartindaleClassification { get; set; }
    public KeyValueDto PregnancyClassification { get; set; }
    public KeyValueDto MethodOfUse { get; set; }
    public KeyValueDto Indication { get; set; }
    public KeyValueDto MechanismOfEffect { get; set; }
    public KeyValueDto BeginningOfEffect { get; set; }
    public KeyValueDto PeakOfEffect { get; set; }
    public KeyValueDto Pharmacokinetics { get; set; }
    public KeyValueDto PrescriptionIndications { get; set; }
    public KeyValueDto Contraindicated { get; set; }
    public KeyValueDto IncreaseEffect { get; set; }
    public KeyValueDto ReduceEffect { get; set; }
    public KeyValueDto AvoidInteractions { get; set; }
    public KeyValueDto FoodInteractions { get; set; }
    public KeyValueDto DrugInteractions { get; set; }
    public KeyValueDto PoisoningAndTreatment { get; set; }
    public KeyValueDto Precautions { get; set; }
    public KeyValueDto SpecialAttention { get; set; }
    public KeyValueDto UseInBreastfeedingAndChildren { get; set; }
    public KeyValueDto Education { get; set; }
    
    public List<PharmaceuticalFormsGetDto> forms { get; set; }
    public KeyValueDto sideEffects { get; set; }
}