using MedPedia.Common.Utilities.Enums;

namespace MedPedia.Common.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(Messages messageId) : base(true, messageId)
        {

        }

        public SuccessResult() : base(true)
        {

        }
    }
}
