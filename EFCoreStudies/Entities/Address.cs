﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressDescription { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
