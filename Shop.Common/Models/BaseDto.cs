using Shop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shop.Common.Models
{
    public class BaseDto
    {
        public BaseDto()
        {
            Errors = new List<ErrorDto>();
        }

        [JsonIgnore]
        public bool HasError { get; set; }
        [JsonIgnore]
        public ErrorType? ErrorType { get; set; }
        [JsonIgnore]
        public List<ErrorDto> Errors { get; set; }
    }
}
