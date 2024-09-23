using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using DealOrNoDeal.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DealOrNoDeal.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DealOrNoDealPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const int ApplicationHeight = 500;

        /// <summary>
        ///     The application width
        /// </summary>
        public const int ApplicationWidth = 500;

        private IList<Button> briefcaseButtons;
        private IList<Border> dollarAmountLabels;
        private readonly GameManager gameManager;
        
        
        #endregion

        #region Constructors

        public DealOrNoDealPage()
        {
            this.gameManager = new GameManager();
            this.InitializeComponent();
            this.initializeUiDataAndControls();
            
        }

        #endregion

        #region Methods

        private void initializeUiDataAndControls()
        {
            this.promptUserForGameSettings();
            this.setPageSize();
            this.briefcaseButtons = new List<Button>();
            this.dollarAmountLabels = new List<Border>();
            this.buildBriefcaseButtonCollection();
            this.buildDollarAmountLabelCollection();
            this.buildDollarAmountLabelsText();
            this.dealButton.Visibility = Visibility.Collapsed;
            this.noDealButton.Visibility = Visibility.Collapsed;
            this.initializeWelcomeLabel();
        }

        private void setPageSize()
        {
            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));
        }

        private void buildDollarAmountLabelCollection()
        {
            this.dollarAmountLabels.Clear();

            this.dollarAmountLabels.Add(this.label0Border);
            this.dollarAmountLabels.Add(this.label1Border);
            this.dollarAmountLabels.Add(this.label2Border);
            this.dollarAmountLabels.Add(this.label3Border);
            this.dollarAmountLabels.Add(this.label4Border);
            this.dollarAmountLabels.Add(this.label5Border);
            this.dollarAmountLabels.Add(this.label6Border);
            this.dollarAmountLabels.Add(this.label7Border);
            this.dollarAmountLabels.Add(this.label8Border);
            this.dollarAmountLabels.Add(this.label9Border);
            this.dollarAmountLabels.Add(this.label10Border);
            this.dollarAmountLabels.Add(this.label11Border);
            this.dollarAmountLabels.Add(this.label12Border);
            this.dollarAmountLabels.Add(this.label13Border);
            this.dollarAmountLabels.Add(this.label14Border);
            this.dollarAmountLabels.Add(this.label15Border);
            this.dollarAmountLabels.Add(this.label16Border);
            this.dollarAmountLabels.Add(this.label17Border);
            this.dollarAmountLabels.Add(this.label18Border);
            this.dollarAmountLabels.Add(this.label19Border);
            this.dollarAmountLabels.Add(this.label20Border);
            this.dollarAmountLabels.Add(this.label21Border);
            this.dollarAmountLabels.Add(this.label22Border);
            this.dollarAmountLabels.Add(this.label23Border);
            this.dollarAmountLabels.Add(this.label24Border);
            this.dollarAmountLabels.Add(this.label25Border);
        }

        private void buildBriefcaseButtonCollection()
        {
            this.briefcaseButtons.Clear();

            this.briefcaseButtons.Add(this.case0);
            this.briefcaseButtons.Add(this.case1);
            this.briefcaseButtons.Add(this.case2);
            this.briefcaseButtons.Add(this.case3);
            this.briefcaseButtons.Add(this.case4);
            this.briefcaseButtons.Add(this.case5);
            this.briefcaseButtons.Add(this.case6);
            this.briefcaseButtons.Add(this.case7);
            this.briefcaseButtons.Add(this.case8);
            this.briefcaseButtons.Add(this.case9);
            this.briefcaseButtons.Add(this.case10);
            this.briefcaseButtons.Add(this.case11);
            this.briefcaseButtons.Add(this.case12);
            this.briefcaseButtons.Add(this.case13);
            this.briefcaseButtons.Add(this.case14);
            this.briefcaseButtons.Add(this.case15);
            this.briefcaseButtons.Add(this.case16);
            this.briefcaseButtons.Add(this.case17);
            this.briefcaseButtons.Add(this.case18);
            this.briefcaseButtons.Add(this.case19);
            this.briefcaseButtons.Add(this.case20);
            this.briefcaseButtons.Add(this.case21);
            this.briefcaseButtons.Add(this.case22);
            this.briefcaseButtons.Add(this.case23);
            this.briefcaseButtons.Add(this.case24);
            this.briefcaseButtons.Add(this.case25);

            this.storeBriefCaseIndexInControlsTagProperty();
        }

        private void storeBriefCaseIndexInControlsTagProperty()
        {
            for (var i = 0; i < this.briefcaseButtons.Count; i++)
            {
                this.briefcaseButtons[i].Tag = i;
            }
        }

        private void buildDollarAmountLabelsText()
        {
            for (var i = 0; i < this.dollarAmountLabels.Count; i++)
            {
                var dollarAmountBorder = this.dollarAmountLabels[i];
                var dollarAmountTextBlock = (TextBlock)dollarAmountBorder.Child;
                dollarAmountTextBlock.Text = this.gameManager.MonetaryValuesForCases[i].ToString("C0");
            }
        }

        private void initializeWelcomeLabel()
        {
            GameType gameType = this.gameManager.GameType;
            if (gameType == GameType.Syndicated)
            {
                this.roundLabel.Text = $"Welcome to Synd. Deal or No Deal!";
            }
            else
            {
                this.roundLabel.Text = $"Welcome to {gameType} Deal or No Deal!";
            }
        }

        private void promptUserForGameSettings()
        {
            this.
        }

        private void briefcase_Click(object sender, RoutedEventArgs e)
        {
            var selectedBriefCase = (Button)sender;

            selectedBriefCase.IsEnabled = false;
            selectedBriefCase.Visibility = Visibility.Collapsed;

            var selectedBriefCaseId = this.getBriefcaseId(selectedBriefCase);
            var selectedBriefCaseAmount = this.gameManager.GetDollarAmount(selectedBriefCaseId);

            this.handleCaseSelection(selectedBriefCaseId, selectedBriefCaseAmount);

            this.gameManager.CasesRemainingInRound -= 1;
            this.updateCurrentRoundInformation();

            this.handleFinalBriefCaseClick(selectedBriefCaseId);


        }

        private void handleFinalBriefCaseClick(int selectedBriefCaseId)
        {
            if (this.gameManager.IsLastRound() && this.gameManager.CasesRemainingInRound == 0)
            {
                string formattedDollarAmount = this.gameManager.GetDollarAmount(selectedBriefCaseId).ToString("C");
                this.summaryOutput.Text = $"Congratulations you win {formattedDollarAmount} \nGAME OVER";
                this.setBriefcaseButtonsAreEnabled(false);
            }
        }

        private void handleCaseSelection(int selectedBriefCaseId, int selectedBriefCaseAmount)
        {
            
            if (this.gameManager.FirstCaseSelected == null)
            {
                Briefcase firstBriefcase = new Briefcase(selectedBriefCaseId, selectedBriefCaseAmount);
                this.gameManager.FirstCaseSelected = firstBriefcase;
                this.gameManager.CasesRemainingInRound++;
            }
            else if (!this.gameManager.IsLastRound())
            {
                this.findAndGrayOutGameDollarLabel(selectedBriefCaseAmount);
                this.gameManager.RemoveBriefcaseFromPlay(selectedBriefCaseId);
            }
        }

        private void findAndGrayOutGameDollarLabel(int amount)
        {
            foreach (var currDollarAmountLabel in this.dollarAmountLabels)
            {
                if (grayOutLabelIfMatchesDollarAmount(amount, currDollarAmountLabel))
                {
                    break;
                }
            }
        }

        private static bool grayOutLabelIfMatchesDollarAmount(int amount, Border currDollarAmountLabel)
        {
            var matched = false;

            if (currDollarAmountLabel.Child is TextBlock dollarTextBlock)
            {
                var labelAmount = int.Parse(dollarTextBlock.Text, NumberStyles.Currency);
                if (labelAmount == amount)
                {
                    currDollarAmountLabel.Background = new SolidColorBrush(Colors.Gray);
                    matched = true;
                }
            }
            

            return matched;
        }

        private int getBriefcaseId(Button selectedBriefCase)
        {
            var briefcaseId = (int)selectedBriefCase.Tag;
            return briefcaseId;
        }

        private void updateCurrentRoundInformation()
        {
           
            int totalCasesToOpenInRound = this.gameManager.CasesRemainingInRound;
            this.casesToOpenLabel.Text = $"{totalCasesToOpenInRound} more cases to open";

            
            int totalCasesToOpen = this.gameManager.TotalNumOfCasesInRound;
            var currRound = this.gameManager.CurrRound;
            this.roundLabel.Text = $"Round {currRound} : {totalCasesToOpen} cases to open";


            this.handleEndOfRound(totalCasesToOpenInRound);
            this.handleLastRound();

            this.setSummaryOutput();

            

        }

        private void setSummaryOutput()
        {
            var minOffer = this.gameManager.MinOffer.ToString("C");
            var maxOffer = this.gameManager.MaxOffer.ToString("C");
            var avgOffer = this.gameManager.AvgOffer.ToString("C");
            var lastOffer = this.gameManager.LastOffer.ToString("C");
            if (this.gameManager.CasesRemainingInRound != 0 && this.gameManager.CurrRound != 1 &&
                !this.gameManager.IsLastRound())
            {
                this.summaryOutput.Text =
                    $"Offers: Min: {minOffer} Max: {maxOffer}{Environment.NewLine}            Average: {avgOffer}";
            }
            else if (this.gameManager.CurrRound == 1 && this.gameManager.CasesRemainingInRound != 0)
            {
                int originalCaseId = this.gameManager.FirstCaseSelected.Id;
                this.summaryOutput.Text = $"Your case is {originalCaseId + 1}";
            }
            else if (this.gameManager.IsLastRound())
            {
                this.summaryOutput.Text =
                    $"Offers: Min: {minOffer} Max: {maxOffer}{Environment.NewLine}            Average: {avgOffer}";
            }
            else
            {
                this.summaryOutput.Text =
                    $"Offers: Min: {minOffer} Max: {maxOffer}{Environment.NewLine}            Current: {lastOffer}";
            } 
        }

        private void handleEndOfRound(int totalCasesToOpenInRound)
        {

            
            if (totalCasesToOpenInRound == 0 && !this.gameManager.IsLastRound())
            {
                this.setBriefcaseButtonsAreEnabled(false);

                this.gameManager.UpdateOffers();


                this.setSummaryOutput();

                this.dealButton.Visibility = Visibility.Visible;
                this.noDealButton.Visibility = Visibility.Visible;
            }
            
        }

        private void handleLastRound()
        {
            
            
            if (this.gameManager.IsLastRound())
            {
                this.roundLabel.Text = "This is the final round";
                this.casesToOpenLabel.Text = "Select a case below";

                Button firstCaseSelectedButton =
                    this.briefcaseButtons.First(button => (int)button.Tag == this.gameManager.FirstCaseSelected.Id);
                firstCaseSelectedButton.Visibility = Visibility.Visible;
                firstCaseSelectedButton.IsEnabled = true;

                StackPanel firstCaseSelectedStackPanel = (StackPanel)firstCaseSelectedButton.Parent;
                firstCaseSelectedStackPanel.Children.Remove(firstCaseSelectedButton);


                int lastCaseOnBoardId = this.gameManager.GetLastCaseOnBoardId();
                Button lastCaseOnBoardButton = this.briefcaseButtons.First(button => (int)button.Tag == lastCaseOnBoardId);
                StackPanel lastCaseOnBoardStackPanel = (StackPanel)lastCaseOnBoardButton.Parent;
                lastCaseOnBoardStackPanel.Children.Remove(lastCaseOnBoardButton);

                if (this.gameManager.FirstCaseSelected.Id < lastCaseOnBoardId)
                {
                    this.middleRowStackPanel.Children.Add(firstCaseSelectedButton);
                    this.middleRowStackPanel.Children.Add(lastCaseOnBoardButton);
                }
                else
                {
                    this.middleRowStackPanel.Children.Add(lastCaseOnBoardButton);
                    this.middleRowStackPanel.Children.Add(firstCaseSelectedButton);
                }
            }

            this.handlePlayAgain();
            
        }

        private async void handlePlayAgain()
        {
            if (this.gameManager.IsLastRound() && this.gameManager.CasesRemainingInRound == 0)
            {
                var continueDialog = new ContentDialog()
                {
                    Title = "Thanks for playing!",
                    Content = "Do you want to play again?",
                    PrimaryButtonText = "Yes",
                    SecondaryButtonText = "No"
                };

                var result = await continueDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    // Restart the application
                    Frame.Navigate(typeof(DealOrNoDealPage));
                }
                else if (result == ContentDialogResult.Secondary)
                {
                    Application.Current.Exit();
                }
            }
        }

        private void dealButton_Click(object sender, RoutedEventArgs e)
        {
            var valueContainedInOriginalCase = this.gameManager.GetDollarAmount(this.gameManager.FirstCaseSelected.Id);
            var formattedValue = valueContainedInOriginalCase.ToString("C");

            var acceptedOffer = this.gameManager.LastOffer;
            var formattedOffer = acceptedOffer.ToString("C");

            this.summaryOutput.Text =
                $"Your case contained: {formattedValue} \nAccepted offer: {formattedOffer} \nGAME OVER";

            this.dealButton.Visibility = Visibility.Collapsed;
            this.noDealButton.Visibility = Visibility.Collapsed;

            
        }


        private void noDealButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.gameManager.IsLastRound()) 
            {
                var lastBriefcaseOnBoardId = this.gameManager.GetLastCaseOnBoardId();
                var lastBriefcaseOnBoardDollarAmount = this.gameManager.GetDollarAmount(lastBriefcaseOnBoardId);
                var formattedValue = lastBriefcaseOnBoardDollarAmount.ToString("C");

                this.summaryOutput.Text = $"Congratulations you win {formattedValue} \nGAME OVER";

                this.findAndGrayOutGameDollarLabel(this.gameManager.GetDollarAmount(this.gameManager.FirstCaseSelected.Id));
                this.gameManager.RemoveBriefcaseFromPlay(this.gameManager.FirstCaseSelected.Id);

                this.dealButton.Visibility = Visibility.Collapsed;
                this.noDealButton.Visibility = Visibility.Collapsed;
            }
            else
            {

                this.setBriefcaseButtonsAreEnabled(true);
                
                this.dealButton.Visibility = Visibility.Collapsed;
                this.noDealButton.Visibility = Visibility.Collapsed;

                this.gameManager.MoveToNextRound();
                this.updateCurrentRoundInformation();
            }
        }

        private void setBriefcaseButtonsAreEnabled(bool enabled)
        {
            if (enabled)
            {
                foreach (var button in this.briefcaseButtons)
                {
                    button.IsEnabled = true;
                }
            }
            else
            {
                foreach (var button in this.briefcaseButtons)
                {
                    button.IsEnabled = false;
                }
            }
        }

        #endregion
    }
}