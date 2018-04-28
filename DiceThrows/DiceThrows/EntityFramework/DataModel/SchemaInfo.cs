using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.EntityFramework.DataModel
{
    [Table("tbl_schemaInfo")]
    public class SchemaInfo
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("version")]
        public int Version { get; set; }
    }
}
