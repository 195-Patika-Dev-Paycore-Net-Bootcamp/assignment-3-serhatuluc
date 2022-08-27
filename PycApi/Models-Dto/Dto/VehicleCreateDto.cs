using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Models_Dto.Dto
{
    //This dto is aimed to use in Create Http method of Vehicle. 
    //Since id is auto incremented
    public class VehicleCreateDto
    {
        public virtual string name { get; set; }
        public virtual string plate { get; set; }
    }
}
