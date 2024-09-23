using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;

namespace DealOrNoDeal.Model
{
    /// <summary>
    /// The class round manager used to manage all aspects of the game that is associated with the round
    /// </summary>
    public class RoundManager
    {
        /// <summary>
        /// Gets or sets the curr round.
        /// </summary>
        /// <value>
        /// The curr round.
        /// </value>
        public int CurrRound
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the final round.
        /// </summary>
        /// <value>
        /// The final round.
        /// </value>
        public int FinalRound { get; set; }
        /// <summary>
        /// Gets or sets the type of the round.
        /// </summary>
        /// <value>
        /// The type of the round.
        /// </value>
        public RoundType RoundType { get; set; }
        /// <summary>
        /// Gets or sets the cases remaining in round.
        /// </summary>
        /// <value>
        /// The cases remaining in round.
        /// </value>
        public int CasesRemainingInRound { get; set; }
        /// <summary>
        /// Gets or sets the total number of cases in round.
        /// </summary>
        /// <value>
        /// The total number of cases in round.
        /// </value>
        public int TotalNumOfCasesInRound { get; set; }
        private readonly Dictionary<RoundType, int[]> numOfCasesToOpenPerRoundType;
        /// <summary>
        /// Gets the number of cases to open per round.
        /// </summary>
        /// <value>
        /// The number of cases to open per round.
        /// </value>
        public int[] NumOfCasesToOpenPerRound { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoundManager"/> class.
        /// </summary>
        public RoundManager()
        {
            this.RoundType = RoundType.ShortGame;
            this.FinalRound = (int)this.RoundType;
            this.CurrRound = 1;
            

            this.numOfCasesToOpenPerRoundType = new Dictionary<RoundType, int[]>()
            {
                { RoundType.ShortGame, new int[] { 8, 6, 4, 3, 2, 1, 1 } },
                { RoundType.LongGame, new int[] { 6, 5, 4, 3, 2, 1, 1, 1, 1, 1 } }
            };

            this.NumOfCasesToOpenPerRound = this.numOfCasesToOpenPerRoundType[this.RoundType];
        }
        /// <summary>
        /// Determines whether [is last round].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is last round]; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsLastRound()
        {
            return this.CurrRound == this.FinalRound;
        }
        /// <summary>
        /// Increments the round.
        /// </summary>
        public void IncrementRound()
        {
            this.CurrRound++;
        }
        /// <summary>
        /// Updates the round settings.
        /// </summary>
        public void UpdateRoundSettings()
        {
            this.FinalRound = (int)this.RoundType;
            this.NumOfCasesToOpenPerRound = this.numOfCasesToOpenPerRoundType[this.RoundType];
        }

    }
}
