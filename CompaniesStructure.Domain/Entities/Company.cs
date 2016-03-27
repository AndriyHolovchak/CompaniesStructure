using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompaniesStructure.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AnnualEarnings { get; set; }
        public int? ParentCompanyId { get; set; }
        public decimal TotalCompanyEarnings { get; set; }
    }
}
