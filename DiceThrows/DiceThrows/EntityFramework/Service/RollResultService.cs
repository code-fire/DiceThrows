using DiceThrows.EntityFramework.Database;
using DiceThrows.EntityFramework.DataModel;
using DiceThrows.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.EntityFramework.Service
{
    public class RollResultService
    {
        private DB _context;

        public RollResultService(DB context)
        {
            _context = context;
        }
        public int GetNumberOfAces(DiceColor color)
        {
            return _context.RollResults.Where(rollResult => rollResult.Dice.Color == color && rollResult.RollValue == 1).Count();
        }
        public int GetNumberOfThrows(DiceColor color)
        {
            return _context.RollResults.Where(rollResult => rollResult.Dice.Color == color).Count();
        }
    }
}
