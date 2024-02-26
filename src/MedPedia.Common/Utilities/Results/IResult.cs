using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }

        //[JsonIgnore]
        Messages MessageId { get; }

        //[JsonIgnore]
        string MessageEn { get; }
        string Message { get; }
    }
}
