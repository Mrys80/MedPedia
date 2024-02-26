using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T> where T : class, IDto
    {
        public T Data { get; }

        public DataResult(T data, bool isSuccess, Messages messageId) : base(isSuccess, messageId)
        {
            Data = data;
        }

        public DataResult(T data, bool isSuccess) : base(isSuccess)
        {
            Data = data;
        }
    }
}
