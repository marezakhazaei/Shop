using Shop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Models
{
    public class ErrorDto
    {
        public ErrorDto()
        { }

        public ErrorDto(ErrorCode exceptionCode, string propertyName, string message = null)
        {
            Code = exceptionCode;
            Message = message ?? exceptionCode.ToString();
            Target = propertyName;
        }

        public ErrorCode Code { get; set; }
        public string Target { get; set; }
        public string Message { get; set; }
    }
}
