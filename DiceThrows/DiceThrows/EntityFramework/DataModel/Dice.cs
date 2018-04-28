using DiceThrows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DiceThrows.EntityFramework.DataModel
{
    [Table("tbl_dice")]
    [Serializable]
    public class Dice
    {
        [Key]
        [Column("dice_id")]
        public long DiceId { get; set; }
        [Column("color")]
        public DiceColor Color { get; set; }

        public Dice() { }

        public override string ToString()
        {
            return "dice_id = " + DiceId + "; Color = " + (Color == DiceColor.Black ? "Black" : "White");
        }
    }
}
