
namespace PycApi.Models_Dto.Dto
{
    public class ContainerCreateDto
    {
        //This dto is aimed to use in Create Http method of Container. 
        //Since id is auto incremented
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual int vehicle { get; set; }
    }
}
