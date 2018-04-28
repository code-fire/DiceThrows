using DiceThrows.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DiceThrows.EntityFramework.DataModel
{
    [Table("tbl_roll_result")]
    [Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Dice))]
    public class RollResult
    {
        [Key]
        [Column("roll_result_id")]
        public long RollResultId { get; set; }
        [Column("dice_id")]
        public long DiceId { get; set; }
        [Column("roll_value")]
        public int RollValue { get; set; }

        [XmlIgnore()]
        public virtual Dice Dice { get; set; }

        public RollResult() { }

        public override string ToString()
        {
            return "roll #" + RollResultId + " with " + (Dice.Color == DiceColor.Black ? "Black" : "White") + " dice returned " + RollValue;
        }
    }
}