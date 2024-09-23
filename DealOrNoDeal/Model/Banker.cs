using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DealOrNoDeal.Model
{
    /// <summary>
    /// The banker class used to facilitate and model all actions taken by the banker in deal or no deal
    /// </summary>
    public class Banker
    {
        /// <summary>
        /// Gets or sets the last offer.
        /// </summary>
        /// <value>
        /// The last offer.
        /// </value>
        public int LastOffer { get; set; } = 0;
        /// <summary>
        /// Gets or sets the maximum offer.
        /// </summary>
        /// <value>
        /// The maximum offer.
        /// </value>
        public int MaxOffer { get; set;  } = int.MinValue;
        /// <summary>
        /// Gets or sets the minimum offer.
        /// </summary>
        /// <value>
        /// The minimum offer.
        /// </value>
        public int MinOffer { get; set; } = int.MaxValue;

        /// <summary>
        /// Gets or sets the average offer.
        /// </summary>
        /// <value>
        /// The average offer.
        /// </value>
        public int AvgOffer { get; set; } = 0;

        private int numOfOffers = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="Banker"/> class.
        /// </summary>
        public Banker()
        {

        }
        #region Methods        


        /// <summary>
        /// Updates the offer statistics.
        /// Post-conditions: - the last offer will be updated with this method call
        /// - the last offer will be checked against the min and max offer to set them appropriately 
        /// </summary>
        /// <param name="briefcasesStillInPlay">The briefcases still in play.</param>
        /// <param name="numOfCasesToBeOpenedNextRound">The number of cases to be opened next round.</param>
        /// <exception cref="System.ArgumentException">
        /// briefcases cannot be null
        /// or
        /// there cannot be zero cases to open in the next round
        /// </exception>
        
        public void GetOffer(IList<Briefcase> briefcasesStillInPlay, int numOfCasesToBeOpenedNextRound)
        {
            if (briefcasesStillInPlay == null)
            {
                throw new ArgumentException("briefcases cannot be null");
            }

            if (numOfCasesToBeOpenedNextRound == 0)
            {
                throw new ArgumentException("there cannot be zero cases to open in the next round");
            }

            double sum = briefcasesStillInPlay.Sum(briefcase => briefcase.DollarAmount);
            
            var offer = sum / numOfCasesToBeOpenedNextRound / briefcasesStillInPlay.Count;

            
            int indexOfTenthsPlace = ((int)offer / 10).ToString().Length - 1;
            char tenthsPlaceDigit = (offer / 10).ToString()[indexOfTenthsPlace];
            if (int.Parse(tenthsPlaceDigit.ToString()) >= 5)
            {
                offer = offer + 100;
            }

            int roundedOffer
                = (int)offer / 100 * 100;
            
            this.LastOffer = roundedOffer;

            this.numOfOffers++;
            this.updateOfferStatistics();
            
        }

        private void updateOfferStatistics()
        {
            if (this.LastOffer < this.MinOffer)
            {
                this.MinOffer = this.LastOffer;
            }

            if (this.LastOffer > this.MaxOffer)
            {
                this.MaxOffer = this.LastOffer;
            }

            this.AvgOffer = (this.AvgOffer + this.LastOffer) / this.numOfOffers;
        }

        #endregion
    }
}