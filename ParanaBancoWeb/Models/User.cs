using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParanaBancoWeb.Models
{
    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    public class User : Entity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}
