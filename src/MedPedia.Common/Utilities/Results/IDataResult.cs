using MedPedia.Common.Entities.Abstracts;

namespace MedPedia.Common.Utilities.Results
{
    public interface IDataResult<out T> : IResult where T : class, IDto
    {
        T Data { get; }
    }
}
