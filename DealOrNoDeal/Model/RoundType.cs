using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Model
{
    /// <summary>
    /// The enum round type used to ensure type safety when switching between round types each enum value has an associated integer of the number of rounds
    /// </summary>
    public enum RoundType
    {
        LongGame = 10, ShortGame = 7

    }
}
