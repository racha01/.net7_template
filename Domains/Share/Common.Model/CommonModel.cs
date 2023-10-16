
using System.Text.Json.Serialization;

namespace Common.Model
{
    public class LanguageCommonModel
    {
        public string en { get; set; } = string.Empty;
        public string th { get; set; } = string.Empty;
    }

    public class APIStandardErrorResponse<T> where T : class
    {
        public string trace_id { get; set; }// Unique identifier for the request
        public T data { get; set; }// Data response on success response  
        public object errors { get; set; }// errors list on bad reqest 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string error_code { get; set; }// Standardized error code
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public APIErrorStandardResponseErrorInfo error_info { get; set; }
    }

    public class APIStandardErrorResponse
    {
        public string trace_id { get; set; }// Unique identifier for the request
        public object errors { get; set; }// errors list on bad reqest 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string error_code { get; set; }// Standardized error code
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public APIErrorStandardResponseErrorInfo error_info { get; set; }
    }

    public class APIErrorStandardResponseErrorInfo
    {
        public string name { get; set; }// Standardized error name
        public string description { get; set; }// Standardized error description
        public string message_en { get; set; } // Human-readable error message in English
        public string message_th { get; set; } // Human-readable error message in Thai
        public string category { get; set; }// Error category ('custom' or 'system')  APIErrorStandardCategoryEnum
        public IDictionary<string, string> display { get; set; } // Display preferences for different devices <APIErrorStandardDeviceEnum, APIErrorStandardPresentationTypeEnum>

        public void SetDisplay(APIErrorStandardResponseDisplayOptionEnum aPIErrorStandardResponseOptionEnum)
        {
            switch (aPIErrorStandardResponseOptionEnum)
            {
                case APIErrorStandardResponseDisplayOptionEnum.AllDeviceAreText:
                    display = Enum.GetValues(typeof(APIErrorStandardDeviceEnum)).Cast<string>().ToDictionary(k => k, v => APIErrorStandardPresentationTypeEnum.text.ToString());
                    break;
                case APIErrorStandardResponseDisplayOptionEnum.AllDeviceAreModal:
                    display = Enum.GetValues(typeof(APIErrorStandardDeviceEnum)).Cast<string>().ToDictionary(k => k, v => APIErrorStandardPresentationTypeEnum.modal.ToString());
                    break;
                case APIErrorStandardResponseDisplayOptionEnum.AllDeviceAreToast:
                    display = Enum.GetValues(typeof(APIErrorStandardDeviceEnum)).Cast<string>().ToDictionary(k => k, v => APIErrorStandardPresentationTypeEnum.toast.ToString());
                    break;
            }
        }
    }

    public enum APIErrorStandardResponseDisplayOptionEnum
    {
        AllDeviceAreText,
        AllDeviceAreModal,
        AllDeviceAreToast
    }

    public enum APIErrorStandardCategoryEnum
    {
        Unknown = 0,
        System,
        Custom
    }

    public enum APIErrorStandardDeviceEnum
    {
        web,
        mobile,
    }

    public enum APIErrorStandardPresentationTypeEnum
    {
        text,
        modal,
        toast,
    }

    public class APIErrorGlobalResponseDevelop<T> : APIStandardErrorResponse<T> where T : class
    {
        public string error_message { get; set; }
        public string inner_error_message { get; set; }
        public string stack_trace { get; set; }
    }

    public class StandardContentErrorCodeModel
    {
        public string client_id { get; set; }
        public List<ErrorStandardContentModel> error_codes { get; set; }
    }

    public class ErrorStandardContentModel
    {
        public string error_code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string message_en { get; set; }
        public string message_th { get; set; }
        /// <summary>
        /// map with APIErrorStandardCategoryEnum
        /// </summary>
        public string category { get; set; }
        public string exception_namespace { get; set; }
        public int response_http_status { get; set; }
        public Dictionary<string, string> display { get; set; }
    }
}
