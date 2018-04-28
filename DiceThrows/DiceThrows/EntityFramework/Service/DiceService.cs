using DiceThrows.EntityFramework.Database;
using DiceThrows.EntityFramework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.EntityFramework.Service
{
    public class DiceService
    {
        private DB _context;

        public DiceService(DB context)
        {
            _context = context;
        }
        public RollResult RollDice(Random rnd, Dice dice)
        {
            return new RollResult { Dice = dice, RollValue = rnd.Next(1, 7) };
        }
    }
}
