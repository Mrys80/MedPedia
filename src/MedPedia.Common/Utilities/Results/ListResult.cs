using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class ListResult<T> : Result, IListResult<T> where T : class, IEnumerable<IDto>
    {
        public T Data { get; }

        public ListResult(T data, bool isSuccess, Messages messageId) : base(isSuccess, messageId)
        {
            Data = data;
        }

        public ListResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }
    }
}
