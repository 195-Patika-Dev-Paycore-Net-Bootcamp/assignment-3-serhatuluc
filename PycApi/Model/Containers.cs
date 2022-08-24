using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Model
{
    public class Containers
    {
        public virtual int Id { get; set; }
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual int vehicle { get; set; }

    }
}
