using MedPedia.Common.Entities.Entities;

namespace MedPedia.Entities.Entities;

public class DrugForms:BaseEntity
{
    public int DrugId { get; set; }
    public int PharmaceuticalFormId { get; set; }

}