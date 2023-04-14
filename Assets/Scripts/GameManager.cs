using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Constants
    [SerializeField] private const int NUMBER_OF_YEARS_TO_WIN = 20;

    //a public integer that holds the turn count
    public static int turnCount;
    public static int currentFund;
    public static int currentLost;
    public static int loanChoice;
    public static int investChoice;

    public static bool playerChooseOption; //has player chosen anything
    public static bool playerChoosenLoans; 
    public static bool playerChoosenInvest;

    public int baseValue; //used for calculating math values

    //Handles UI
    public TextMeshProUGUI smallLoanMoneyToReceiveDisplay;
    public TextMeshProUGUI smallLoanQuarterlyCostToIncreaseDisplay;
    public TextMeshProUGUI mediumLoanMoneyToReceiveDisplay;
    public TextMeshProUGUI mediumLoanQuarterlyCostToIncreaseDisplay;
    public TextMeshProUGUI largeLoanMoneyToReceiveDisplay;
    public TextMeshProUGUI largeLoanQuarterlyCostToIncreaseDisplay;

    public TextMeshProUGUI smallInvestMoneyToInvestDisplay;
    public TextMeshProUGUI smallInvestMoneyToReceiveDisplay;
    public TextMeshProUGUI mediumInvestMoneyToInvestDisplay;
    public TextMeshProUGUI mediumInvestMoneyToReceiveDisplay;
    public TextMeshProUGUI largeInvestMoneyToInvestDisplay;
    public TextMeshProUGUI largeInvestMoneyToReceiveDisplay;

    public TextMeshProUGUI choiceDisplay;
    public TextMeshProUGUI moneyMadeDisplay;
    public TextMeshProUGUI moneyLostDisplay;
    public TextMeshProUGUI nextChoiceDisplay;

    //Handles storing money to invest
    public int smallInvestMoneyToInvest;
    public int mediumInvestMoneyToInvest;
    public int largeInvestMoneyToInvest;

    //handles preventing player from taking infinite loans;
    int loanCooldown = 3;
    int lCTimer = 0; //loan cooldown timer
    bool canChooseLoan;

    //stores the pop ups
    public GameObject loanPopUp;
    public GameObject investmentPopUp;
    public GameObject warnPopup;
    public GameObject winScreen;
    public GameObject loseScreen;


    void Start()
    {
        turnCount = 1;

        currentFund = 1000;
        currentLost = 100;

        playerChoosenInvest = false;
        playerChoosenLoans = false;
        playerChooseOption = false;
        canChooseLoan = true;

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        warnPopup.SetActive(false);
        loanPopUp.SetActive(false);
        investmentPopUp.SetActive(false);
        loanChoice = 0;
        investChoice = 0;

        choiceDisplay.gameObject.SetActive(false);
        moneyMadeDisplay.gameObject.SetActive(false);
        moneyLostDisplay.gameObject.SetActive(false);
        nextChoiceDisplay.gameObject.SetActive(false);

        CalculateLoanChances();
        CalculateInvestmentChances();
    }

    public void ChoosenLoans() 
    {
        if (canChooseLoan == true)
        {
        playerChooseOption = true;
        playerChoosenLoans = true;
        playerChoosenInvest = false;
        investChoice = 0;
        }
        else
        {
            StartCoroutine(Popup(3.0f, "Can't Choose loan yet, please make an Investment."));
            Debug.Log("Error: Can't take another loan");
            loanChoice = 0;
        }
    }

    public void ChoosenInvest() 
    {
        playerChooseOption = true;
        playerChoosenLoans = false;
        playerChoosenInvest = true;
        loanChoice = 0;        
    }

    public void CalculateLoanChances()
    {
        int minSmallLoanGain = (currentFund/2) + ( (currentFund/100) * -10);
        int maxSmallLoanGain = (currentFund/2) + ( (currentFund/100) * 10);
        int minSmallQuarterlyCostToIncrease = minSmallLoanGain/20;
        int maxSmallQuarterlyCostToIncrease = maxSmallLoanGain/20;

        int minMediumLoanGain = (currentFund/2) + ( (currentFund/25) * -10);
        int maxMediumLoanGain = (currentFund/2) + ( (currentFund/25) * 10);
        int minMediumQuarterlyCostToIncrease = minMediumLoanGain/10;
        int maxMediumQuarterlyCostToIncrease = maxMediumLoanGain/10;
    
        int minLargeLoanGain = (currentFund/2) + ( (currentFund/5) * -5);
        int maxLargeLoanGain = (currentFund/2) + ( (currentFund/5) * 5);
        int minLargeQuarterlyCostToIncrease = minLargeLoanGain/4;
        int maxLargeQuarterlyCostToIncrease = maxLargeLoanGain/4;

        smallLoanMoneyToReceiveDisplay.text = "Money to Receive: $" + minSmallLoanGain.ToString() + " to $" + maxSmallLoanGain.ToString();
        mediumLoanMoneyToReceiveDisplay.text = "Money to Receive: $" + minMediumLoanGain.ToString() + " to $" + maxMediumLoanGain.ToString();
        largeLoanMoneyToReceiveDisplay.text = "Money to Receive: $" + minLargeLoanGain.ToString() + " to $" + maxLargeLoanGain.ToString();

        smallLoanQuarterlyCostToIncreaseDisplay.text = "Quarterly Cost to Increase: $" + minSmallQuarterlyCostToIncrease.ToString() + " to $" + maxSmallQuarterlyCostToIncrease.ToString();
        mediumLoanQuarterlyCostToIncreaseDisplay.text = "Quarterly Cost to Increase: $" + minMediumQuarterlyCostToIncrease.ToString() + " to $" + maxMediumQuarterlyCostToIncrease.ToString();
        largeLoanQuarterlyCostToIncreaseDisplay.text = "Quarterly Cost to Increase: $" + minLargeQuarterlyCostToIncrease.ToString() + " to $" + maxLargeQuarterlyCostToIncrease.ToString();
    }

    //Handles the math for the amount of money gained/lossed.
    public void CalculatingLoan()
    {
        int randFactor = 0; //random factor
        int moneyToRecieve = 0;
        int quarterlyCostToIncrease = 0;

        //gives player money, but raises Quarterly costs from then on.

        //choice 1 -low money
        if (loanChoice == 1)
        {
            randFactor = Random.Range(-10,11); //picks random int from between -10 and 10
            moneyToRecieve = (currentFund/2) + ( (currentFund/100) * randFactor);
            quarterlyCostToIncrease = moneyToRecieve/20;
        }
        //choice 2 -medium money
        if (loanChoice == 2)
        {
            randFactor = Random.Range(-10,11); //picks random int from between -10 and 10
            moneyToRecieve = (currentFund/2) + ( (currentFund/25) * randFactor);
            quarterlyCostToIncrease = moneyToRecieve/10;
        }
        //choice 3 -high money
        if (loanChoice == 3)
        {
            randFactor = Random.Range(-5,6); //picks random int from between -5 and 5
            moneyToRecieve = (currentFund/2) + ( (currentFund/5) * randFactor);
            quarterlyCostToIncrease = moneyToRecieve/4;
        }
        currentFund += moneyToRecieve;
        currentLost += quarterlyCostToIncrease;

        moneyMadeDisplay.text = "Made: $"+moneyToRecieve.ToString();
        moneyLostDisplay.text = "Quarter Increase: $"+quarterlyCostToIncrease.ToString();

        //turns on loan cooldown
        canChooseLoan = false;

    }

    public void CalculateInvestmentChances() {
        int baseValue = 50 + turnCount * (Random.Range(1, 7)) + (currentLost/150); //value is modified by turnCount * + or - 5%

        smallInvestMoneyToInvest = baseValue/2;
        mediumInvestMoneyToInvest = baseValue * 15;
        largeInvestMoneyToInvest = baseValue * 50;

        int maxSmallInvestMoneyToReceive = baseValue * 6;
        int maxMediumInvestMoneyToReceive = baseValue * (19 + 12);
        int maxLargeInvestMoneyToReceive = baseValue * (65 + 16);

        smallInvestMoneyToInvestDisplay.text = "Investment Cost: $" + smallInvestMoneyToInvest.ToString();
        mediumInvestMoneyToInvestDisplay.text = "Investment Cost: $" + mediumInvestMoneyToInvest.ToString();
        largeInvestMoneyToInvestDisplay.text = "Investment Cost: $" + largeInvestMoneyToInvest.ToString();

        smallInvestMoneyToReceiveDisplay.text = "Highest Returns: $" + maxSmallInvestMoneyToReceive.ToString();
        mediumInvestMoneyToReceiveDisplay.text = "Highest Returns: $" + maxMediumInvestMoneyToReceive.ToString();
        largeInvestMoneyToReceiveDisplay.text = "Highest Returns: $" + maxLargeInvestMoneyToReceive.ToString();
    }

    //Handles the math for the amount of money gained/lossed.
    public void CalculatingInvestment()
    {
        int randFactor = 0; //random factor
        int moneyToRecieve = 0;
        int moneyToInvest = 0;
        int randChoice = 0;
        int baseValue = 50; //value that all investments use and scales overtime with turn count.

        baseValue = 50 + turnCount * (Random.Range(1, 7)) + (currentLost/150); //value is modified by turnCount * + or - 5%

        //spends players money for that month, but has change to make a return on invetsment (ROI)
        if (investChoice == 1)
        {
            randChoice = Random.Range(1,101); //determines at random which of the choices appear
            if (randChoice <= 90) //90% of the time
            {
                //choice 1.1 - low money invested, low returns, completely safe (100% returns)
                moneyToInvest = baseValue / 2;
                moneyToRecieve = baseValue * Random.Range(1,4);

            }
            else //10% of the time
            {
                //choice 1.2 - low money invested, modest returns, mostly safe (90% returns)
                moneyToInvest = baseValue / 2;
                randFactor = Random.Range(1,101);
                    if (randFactor <= 90) {
                        moneyToRecieve = baseValue * Random.Range(3,6);
                    }
                    else {
                        moneyToRecieve = 0;
                    }
            }
        }

        //choice 2
        if (investChoice == 2)
        {
            moneyToInvest = baseValue * 15;
            randFactor = Random.Range(1,101);
            if (randFactor <= 30) // 30% of the time, 
            {
                moneyToRecieve = 0;
            }
            else if (31 <= randFactor && randFactor <= 80) // 50% of the time, 
            {
                moneyToRecieve = baseValue * (15 + Random.Range(-5,6));
            }
            else // 20% of the time
            {
                moneyToRecieve = baseValue * (19 + Random.Range(1,12));
            }
            
        }
        //choice 3
        if (investChoice == 3)
        {
            moneyToInvest = baseValue * 50;
            randFactor = Random.Range(1,101);
            if (randFactor <= 60) // 60% of the time, 
            {
                moneyToRecieve = 0;
            }
            else if (61 <= randFactor && randFactor <= 85) // 25% of the time, 
            {
                moneyToRecieve = baseValue * (50 + Random.Range(-10,11));
            }
            else // 15% of the time
            {
                 moneyToRecieve = baseValue * (65 + Random.Range(1,16));
            }

        }

        currentFund -= moneyToInvest;
        currentFund += moneyToRecieve;

        moneyMadeDisplay.text = "Spent: $"+moneyToInvest.ToString();
        moneyLostDisplay.text = "Received: $"+moneyToRecieve.ToString();
    }

    //methods that reveals the pop up menus
    public void revealLoanMenu()
    {
        if (canChooseLoan == false)
        {
            StartCoroutine(Popup(3.0f, "Can't Choose loan yet, please make an Investment."));
            Debug.Log("Error: Can't take another loan");
            loanChoice = 0;
        }
        else if (investmentPopUp.activeSelf == true)
        {
            StartCoroutine(Popup(2.0f, "Please close the Investment menu first"));
        }  
        else{
        loanPopUp.SetActive(true);
        }
    }

    public void closeLoanMenu()
    {
        if (investmentPopUp.activeSelf == true)
        {
            StartCoroutine(Popup(2.0f, "Please close the Investment menu first"));
        }
        else{
        loanPopUp.SetActive(false);
        }
    }

     public void revealInvestmentMenu()
    {  
        if (loanPopUp.activeSelf == true)
        {
            StartCoroutine(Popup(2.0f, "Please close the Loan menu first"));
        }  
        else{
        investmentPopUp.SetActive(true);
        }
    }

    public void closeInvestmentMenu()
    {
        if (loanPopUp.activeSelf == true)
        {
            StartCoroutine(Popup(2.0f, "Please close the Loan menu first"));
        }  
        else{
        investmentPopUp.SetActive(false);
        }
    }

    /*
    Methods for determining the user's choice.
    */
    public void ChooseOptionOne()
    {
        nextChoiceDisplay.gameObject.SetActive(true);
        if (loanPopUp.activeSelf)
        {
            loanChoice = 1;
            nextChoiceDisplay.text = "You plan to take out a small loan.";
            ChoosenLoans();
        }

        else
        {
            investChoice = 1;
            if (currentFund < smallInvestMoneyToInvest) //if we lack the funds to make the payment...
            {
                StartCoroutine(Popup(2.0f, "You lack the funds to make this investment!"));
            }
            else //Otherwise, its a valid selection
            {
            nextChoiceDisplay.text = "You plan to make a small investment.";
            ChoosenInvest();
            }
        }
    }
    
    public void ChooseOptionTwo()
    {
        nextChoiceDisplay.gameObject.SetActive(true);
        if (loanPopUp.activeSelf)
        {
            loanChoice = 2;
            nextChoiceDisplay.text = "You plan to take out a medium sized loan.";
            ChoosenLoans();
        }

        else
        {
            investChoice = 2;
            if (currentFund < mediumInvestMoneyToInvest)
            {
                StartCoroutine(Popup(2.0f, "You lack the funds to make this investment!"));
            }
            else
            {
            nextChoiceDisplay.text = "You plan to make a medium sized investment.";
            ChoosenInvest();
            }
        }
    }
    public void ChooseOptionThree()
    {
        nextChoiceDisplay.gameObject.SetActive(true);
        if (loanPopUp.activeSelf)
        {
        loanChoice = 3;
        nextChoiceDisplay.text = "You plan to take out a large loan.";
        ChoosenLoans();
        }

        else
        {
            investChoice = 3;
            if (currentFund < largeInvestMoneyToInvest)
            {
                StartCoroutine(Popup(2.0f, "You lack the funds to make this investment!"));
            }
            else
            {
                nextChoiceDisplay.text = "You plan to make a large investment.";
                ChoosenInvest();
            }
        }

    }    

    //a method advances the turn when called
    public void AdvanceTurn()
    {
        if (playerChooseOption == true) //if player chose something...
        {
            choiceDisplay.gameObject.SetActive(true);
            moneyMadeDisplay.gameObject.SetActive(true);
            moneyLostDisplay.gameObject.SetActive(true);
            nextChoiceDisplay.gameObject.SetActive(false);

            Debug.Log("Player made a choice.");
            if (playerChoosenLoans)
            {
                choiceDisplay.text = "You just took out a loan.";                
                CalculatingLoan();
            }
            if (playerChoosenInvest)
            {
                choiceDisplay.text = "You just made an investment.";
                CalculatingInvestment();
            }

            playerChoosenInvest = false;
            playerChoosenLoans = false;
            playerChooseOption = false;

            if (canChooseLoan == false)
            {
                lCTimer++;
                if (lCTimer >= loanCooldown)
                {
                    canChooseLoan = true;
                    lCTimer = 0;
                }
            }


            currentFund = currentFund - currentLost;
            loanChoice = 0;
            turnCount++;

            //if we have made it to turn 80... pop up win screen!
            if (turnCount%(NUMBER_OF_YEARS_TO_WIN*4)==0)
            {
                winScreen.SetActive(true);
            }

            //if our money is reduced to 0, we lose.
            if (currentFund <= 0) 
            {
                loseScreen.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Plyer did not make a choice.");
            StartCoroutine(Popup(2.0f,"Please Select an Option First."));
        }
    }

    //static method to display the warning popup, with a text of our desire, for a specified number of seconds.
    public IEnumerator Popup(float secsToShow, string textToDisplay)
    {
        warnPopup.SetActive(true);
        warnPopup.GetComponent<TextMeshProUGUI>().text = string.Format(textToDisplay);
        yield return new WaitForSeconds(secsToShow);
        warnPopup.SetActive(false);
    }
}