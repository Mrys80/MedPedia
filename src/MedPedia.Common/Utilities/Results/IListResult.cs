using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Utilities.Results
{
    public interface IListResult<out T> : IResult where T : class?, IEnumerable<IDto>?
    {
        T Data { get; }
    }
}
