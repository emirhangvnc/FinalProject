using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    public class OperationClaim:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
