using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MedPedia.Common.Utilities.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; }

        //[JsonIgnore]
        public Messages MessageId { get; }

        // [JsonIgnore]
        public string MessageEn { get; }
        public string Message { get; }

        public Result(bool isSuccess, Messages messageId) : this(isSuccess)
        {
            MessageId = messageId;
            MessageEn = messageId.ToDisplay();
            Message = messageId.ToDescription();
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
