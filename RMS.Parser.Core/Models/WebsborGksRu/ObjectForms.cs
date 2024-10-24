using Newtonsoft.Json;

namespace RMS.Parser.Core.Models.WebsborGksRu
{
    public class ObjectForms
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("okud")]
        public string Okud { get; set; }

        [JsonProperty("form_period")]
        public string FormPeriod { get; set; }

        [JsonProperty("formatted_period")]
        public string FormattedPeriod { get; set; }

        [JsonProperty("reported_period")]
        public string ReportedPeriod { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("period_comment")]
        public object PeriodComment { get; set; }

        [JsonProperty("dept_nsi_id")]
        public string DeptNsiId { get; set; }

        [JsonProperty("dept_nsi_code")]
        public string DeptNsiCode { get; set; }

        [JsonProperty("type_exam")]
        public object TypeExam { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("act_num")]
        public string ActNum { get; set; }

        [JsonProperty("act_date")]
        public string ActDate { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("updatingDate")]
        public object UpdatingDate { get; set; }

        [JsonProperty("isValid")]
        public bool IsValid { get; set; }

        [JsonProperty("periodicity")]
        public int Periodicity { get; set; }

        [JsonProperty("periodNum")]
        public int PeriodNum { get; set; }

        [JsonProperty("periodYear")]
        public int PeriodYear { get; set; }
    }
}
