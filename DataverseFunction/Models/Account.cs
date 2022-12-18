using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataverseFunction
{
    public class Account
    {
        [JsonProperty("Name")]     
        public string Name { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

    }
}
