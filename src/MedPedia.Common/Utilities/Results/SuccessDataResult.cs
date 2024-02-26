using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T> where T : class, IDto
    {
        public SuccessDataResult(T data, Messages messageId) : base(data, true, messageId)
        {

        }

        public SuccessDataResult(T data) : base(data, true)
        {

        }

        public SuccessDataResult(Messages messageId) : base(default, true, messageId)
        {

        }

        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
