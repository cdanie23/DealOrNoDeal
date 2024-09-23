using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.UI.Xaml.Controls.Maps;

namespace DealOrNoDeal.Model
{
    /// <summary>
    /// The game settings class used to maintain all the settings for the current game
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Gets the case values. Used to have a Collections of case values associated with each game type
        /// </summary>
        /// <value>
        /// The case values.
        /// </value>
        public Dictionary<GameType, Collection<int>> CaseValues { get; private set; }

        /// <summary>
        /// Gets or sets the type of the game.
        /// </summary>
        /// <value>
        /// The type of the game.
        /// </value>
        public GameType GameType { get; set; }
        /// <summary>
        /// Stores the current values of the cases within the game type
        /// Gets the monetary value for cases.
        /// </summary>
        /// <value>
        /// The monetary value for cases.
        /// </value>
        public Collection<int> MonetaryValueForCases { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSettings"/> class.
        /// </summary>
        public GameSettings()
        {
            this.GameType = GameType.Syndicated;

            this.setUpCaseValues();

            this.MonetaryValueForCases = new Collection<int>(this.CaseValues[this.GameType]);
            //TODO Set up a dialog at application launch that prompts the user to select the game settings and update them accordingly do this with a new backup to save files incase you mess it up
        }

        private void setUpCaseValues()
        {
            this.CaseValues = new Dictionary<GameType, Collection<int>>();


            this.CaseValues.Add(GameType.Regular, new Collection<int>
                {
                    0, 1, 5, 10, 25, 50, 75, 100, 200,
                    300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000,
                    100000, 200000, 300000, 400000, 500000, 750000, 1000000
                }
            );

            this.CaseValues.Add(GameType.Mega, new Collection<int>
            {
                0, 100, 500, 1000, 2500, 5000, 7500,
                10000, 20000, 30000, 40000, 50000, 75000, 100000,
                225000, 400000, 500000, 750000, 1000000, 2000000, 3000000,
                4000000, 5000000, 6000000, 8500000, 10000000
            });
            this.CaseValues.Add(GameType.Syndicated, new Collection<int>
            {
                0, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400,
                500, 750, 1000, 2500, 5000, 10000, 25000,
                50000, 75000, 100000, 150000, 200000, 250000,
                350000, 500000
            });
        }
        /// <summary>
        /// Updates the monetary values for cases.
        /// </summary>
        public void UpdateMonetaryValuesForCases()
        {
            this.MonetaryValueForCases = this.CaseValues[this.GameType];
        }
    }
}
