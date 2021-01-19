using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace JdMvc.Models
{
    public class Province
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty(PropertyName = "code")]
        public int id { get; set; }
        public string name { get; set; }
        [NotMapped]
        public List<Province> children { get; set; }

        [JsonProperty(PropertyName = "codes")]
        public int parentid { get; set; }
        [JsonProperty(PropertyName = "names")]

        public string parentname { get; set; }

        public string areacode { get; set; }

        public string zipcode { get; set; }

        public string depth { get; set; }

    }
    public class ProvinceDet{
        public int code{get;set;}
        public string name{get;set;}
    }
    [NotMapped]
    public class ProvinceDetails
    {
        public int status { get; set; }
        public string msg { get; set; }
        public List<Province> result { get; set; }
    }

}