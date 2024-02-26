using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class SuccessListResult<T> : ListResult<T> where T : class, IEnumerable<IDto>
    {
        public SuccessListResult(T data, Messages messageId) : base(data, true, messageId)
        {

        }

        public SuccessListResult(T data) : base(data, true)
        {

        }

        public SuccessListResult(Messages messageId) : base(default, true, messageId)
        {

        }

        public SuccessListResult() : base(default, true)
        {

        }
    }
}
