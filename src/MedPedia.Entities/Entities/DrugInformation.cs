using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class DrugInformation : BaseEntity
{
    public int DrugId { get; set; }
    public int PharmacologicalClassificationId { get; set; }
    public int TherapeuticClassificationId { get; set; }
    public int MartindaleClassificationId { get; set; }
    public int PregnancyClassificationId { get; set; }
    public int MethodOfUseId { get; set; }
    public int IndicationId { get; set; }
    public string MechanismOfEffect { get; set; }
    public string BeginningOfEffect { get; set; }
    public string PeakOfEffect { get; set; }
    public string Pharmacokinetics { get; set; }
    public string Pharmacodynamics { get; set; }
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
}