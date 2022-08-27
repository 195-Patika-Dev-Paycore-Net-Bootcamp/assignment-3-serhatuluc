using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Models_Dto.Dto
{
    public class ContainerUpdateDto
    {
        //This dto is aimed to use in Update Http method of Container. 
        //Since we do not want vehicle id to be modified this way
        public virtual int Id { get; set; }
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
    }
}
