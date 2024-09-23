using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace DealOrNoDeal.Model
{
    /// <summary>Handles the management of the actual game play.</summary>
    public class GameManager
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            this.briefcases = new List<Briefcase>();
            this.banker = new Banker();
            this.roundManager = new RoundManager();
            this.gameSettings = new GameSettings();
            this.populateBriefcaseList();
            this.setNumberOfCasesToOpenForNextRound();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes the briefcase from play.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the dollar amount associated with the briefcase ID number or -1 if it cannot be found</returns>
        /// <exception cref="System.ArgumentException">id cannot be negative and must be less than 26</exception>
        public int RemoveBriefcaseFromPlay(int id)
        {
            if (id > GameManager.LargestId || id < GameManager.SmallestId)
            {
                throw new ArgumentException($"id cannot be negative and must be less than 26");
            }
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    this.briefcases.Remove(briefcase);
                    return briefcase.DollarAmount;
                }
            }

            return -1;
        }

        /// <summary>
        /// Updates all offer statistics in the Banker class
        /// </summary>
        public void UpdateOffers()
        {
            this.banker.GetOffer(this.briefcases, this.roundManager.NumOfCasesToOpenPerRound[this.roundManager.CurrRound - 1]);
        }

        /// <summary>
        /// Moves to next round using the round manager and sets the number of cases to open in the next round
        /// </summary>
        public void MoveToNextRound()
        {
            this.roundManager.IncrementRound();
            this.setNumberOfCasesToOpenForNextRound();
        }

        /// <summary>
        ///     Gets the dollar amount given the briefcase id.
        /// </summary>
        /// <param name="id">The briefcase identifier.</param>
        /// <returns>The selected briefcases amount in dollars</returns>
        /// <exception cref="System.ArgumentException">id cannot be negative and must be less than 26</exception>
        public int GetDollarAmount(int id)
        {
            if (id > GameManager.LargestId || id < GameManager.SmallestId)
            {
                throw new ArgumentException($"id cannot be negative and must be less than 26");
            }
            foreach (var briefcase in this.briefcases)
            {
                if (briefcase.Id == id)
                {
                    return briefcase.DollarAmount;
                }
            }

            return -1;
        }

        private void populateBriefcaseList()
        {
            var dollarAmounts = this.gameSettings.CaseValues[this.gameSettings.GameType].ToList();

            for (var idForBriefcase = 0; idForBriefcase < GameManager.NumOfBriefcases; idForBriefcase++)
            {
                var randomDollarAmount = this.getRandomDollarAmount(dollarAmounts);
                var briefcase = new Briefcase(idForBriefcase, randomDollarAmount);
                this.briefcases.Add(briefcase);
            }
        }

        private int getRandomDollarAmount(List<int> dollarAmounts)
        {
            var random = new Random();

            var randomIndex = random.Next(dollarAmounts.Count);
            var randomDollarAmount = dollarAmounts[randomIndex];

            dollarAmounts.RemoveAt(randomIndex);

            return randomDollarAmount;
        }

        private void setNumberOfCasesToOpenForNextRound()
        {
            this.roundManager.TotalNumOfCasesInRound = this.roundManager.NumOfCasesToOpenPerRound[this.roundManager.CurrRound - 1];
            this.roundManager.CasesRemainingInRound = this.TotalNumOfCasesInRound;
        }

        /// <summary>
        /// Gets the last case on board identifier.
        /// </summary>
        /// <returns>The id of the last case on the board</returns>
        public int GetLastCaseOnBoardId()
        {
            return this.briefcases.First(briefcase => briefcase.Id != this.FirstCaseSelected.Id).Id;
        }
        /// <summary>
        /// Determines whether [is last round] using the round manager.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is last round]; otherwise, <c>false</c>.
        /// </returns>
        public Boolean IsLastRound()
        {
            return this.roundManager.IsLastRound();
        }
        /// <summary>
        /// Sets the game settings and updates the corresponding behaviors to that specified game type
        /// </summary>
        /// <param name="gameType">Type of the game.</param>
        /// <param name="roundType">Type of the round.</param>
        public void SetGameSettings(GameType gameType, RoundType roundType)
        {

            this.gameSettings.GameType = gameType;
            this.gameSettings.UpdateMonetaryValuesForCases();

            this.roundManager.RoundType = roundType;
            this.roundManager.UpdateRoundSettings();
        }

        #endregion

        #region Data Members

        private readonly IList<Briefcase> briefcases;
        private const int SmallestId = 0;
        private const int LargestId = NumOfBriefcases;
        private const int NumOfBriefcases = 26;

        private readonly RoundManager roundManager;
        private readonly Banker banker;
        private readonly GameSettings gameSettings;


        /// <summary>
        /// Collaborates with the roundManager class to access the current round
        /// </summary>
        /// <value>
        /// The current round.
        /// </value>
        public int CurrRound => this.roundManager.CurrRound;

        /// <summary>
        /// Collaborates with the round manager to access the cases remaining in the round
        /// </summary>
        /// <value>
        /// The cases remaining in round.
        /// </value>
        public int CasesRemainingInRound {
            get
            {
                return this.roundManager.CasesRemainingInRound;
            }
            set
            {
                this.roundManager.CasesRemainingInRound = value;
            }
        }
        

        /// <summary>
        /// Gets or sets the first case selected.
        /// </summary>
        /// <value>
        /// The first case selected.
        /// </value>
        public Briefcase FirstCaseSelected { get; set; } = null;

        /// <summary>
        /// Collaborates with the banker class to access the last offer
        /// </summary>
        /// <value>
        /// The current offer.
        /// </value>
        public int LastOffer => this.banker.LastOffer;
        /// <summary>
        /// Collaborates with the banker class to access of the lowest offer
        /// </summary>
        /// <value>
        /// The minimum offer.
        /// </value>
        public int MinOffer => this.banker.MinOffer;

        /// <summary>
        /// Collaborates with the banker class to access the highest offer
        /// </summary>
        /// <value>
        /// The maximum offer.
        /// </value>
        public int MaxOffer => this.banker.MaxOffer;

        /// <summary>
        /// Collaborates with the round manager to access the total number of cases in the round
        /// </summary>
        /// <value>
        /// The total number of cases in round.
        /// </value>
        public int TotalNumOfCasesInRound => this.roundManager.TotalNumOfCasesInRound;


        /// <summary>
        /// Collaborates with the banker class to access the average offer
        /// </summary>
        /// <value>
        /// The average offer.
        /// </value>
        public int AvgOffer => this.banker.AvgOffer;


        /// <summary>
        /// Collaborates with the game settings class to access the dollar amounts for each case
        /// </summary>
        /// <value>
        /// The monetary values for cases.
        /// </value>
        public Collection<int> MonetaryValuesForCases => this.gameSettings.MonetaryValueForCases;
        /// <summary>
        /// Collaborates with the game settings class in order to access the game type to use in the code behind
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public GameType GameType => this.gameSettings.GameType;
        #endregion
    }



}
