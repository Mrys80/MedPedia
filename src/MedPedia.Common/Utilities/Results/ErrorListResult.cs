using MedPedia.Common.Entities.Abstracts;
using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class ErrorListResult<T> : ListResult<T> where T : class, IEnumerable<IDto>
    {
        public ErrorListResult(T data, Messages messageId) : base(data, false, messageId)
        {

        }

        public ErrorListResult(T data) : base(data, false)
        {

        }

        public ErrorListResult(Messages messageId) : base(default, false, messageId)
        {

        }

        public ErrorListResult() : base(default, false)
        {

        }
    }
}
