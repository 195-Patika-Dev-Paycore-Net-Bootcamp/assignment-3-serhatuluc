using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Models_Dto.Dto
{
    public class ContainerUpdateDto
    {
        public virtual int Id { get; set; }
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
    }
}
