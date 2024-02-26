using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(Messages messageId) : base(false, messageId)
        {

        }

        public ErrorResult() : base(false)
        {

        }
    }
}
