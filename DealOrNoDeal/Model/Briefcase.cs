using System;

namespace DealOrNoDeal.Model
{
    public class Briefcase
    {
        #region Properties        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the dollar amount.
        /// </summary>
        /// <value>
        /// The dollar amount.
        /// </value>
        public int DollarAmount { get; set; }

        #endregion

        #region Constructors

        public Briefcase(int id, int dollarAmount)
        {
            if (id > 26 || id < 0)
            {
                throw new ArgumentException("id cannot be negative and must less than 26");
            }

            if (dollarAmount < 0)
            {
                throw new ArgumentException("dollar amount cannot be negative");
            }
            
            this.Id = id;
            this.DollarAmount = dollarAmount;
        }

        #endregion
    }
}