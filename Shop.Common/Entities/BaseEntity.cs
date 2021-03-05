using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Common.Entities
{
    public abstract class BaseEntity<T>
        where T : IEquatable<T>
    {
        public T Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
