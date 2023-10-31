using System.ComponentModel.DataAnnotations;

namespace TechChallenge2.Domain.Entities
{
    public abstract class Base
    {
        [Key]
        public int Id { get; set; }
    }
}
