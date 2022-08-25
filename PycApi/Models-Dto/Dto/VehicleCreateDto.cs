using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Models_Dto.Dto
{
    public class VehicleCreateDto
    {
        public virtual string name { get; set; }
        public virtual string plate { get; set; }
    }
}
