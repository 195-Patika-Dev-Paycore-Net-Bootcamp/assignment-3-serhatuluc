
namespace PycApi.Models_Dto.Dto
{
    public class ContainerCreateDto
    {
        public virtual string containerName { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual int vehicle { get; set; }
    }
}
