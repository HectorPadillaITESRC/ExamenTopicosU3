using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Database First  - Code First

namespace RegistroPresion.Models
{
    [Table("registrospresiones")]
    public class Registro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public int Diastolica { get; set; }
        [NotNull]
        public int Sistolica { get; set; }
        [NotNull]
        public int Pulso { get; set; }
        [NotNull]
        public DateTime Fecha { get; set; }

        [Ignore]
        public string? Categoria { get; set; }
    }

}
