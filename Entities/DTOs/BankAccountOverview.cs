﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class BankAccountOverview 
    {
        [Key]
        public string AccountID { get; set; }
        public string Name { get; set; }
        public string BankGroupID { get; set; }
        public string RegistrationNum { get; set; }
        public string AccountNum { get; set; }
        public string LedgerAccount { get; set; }
        public string CurrencyCode { get; set; }
    }
}
