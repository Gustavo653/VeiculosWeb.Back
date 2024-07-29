using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Image : BaseEntity
    {
        public required string Url { get; set; }
    }
}
