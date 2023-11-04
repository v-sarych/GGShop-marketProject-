﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.YourPayments.Entities
{
    public class SignatureParameters
    {
        public string Merchant {  get; set; }
        public string Date { get; set; }
        public string HttpMethod { get; set; }
        public string BaseUrl { get; set; }
        public string MD5BodySum {  get; set; }
    }
}
