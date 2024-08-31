using System;
using System.Collections;
using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference
        :BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public Guid? PromoCodeId { get; set; }
    }
}