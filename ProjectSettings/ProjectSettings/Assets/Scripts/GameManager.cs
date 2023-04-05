using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Constants
    [SerializeField] private const int NUMBER_OF_YEARS_TO_wIN = 20;

    //a public integer that holds the turn count
    public static int turnCount;
    public static int currentFund;
    public static int currentLost;
    public static int loanChoice;
    public static int investChoice;

    public static bool playerChooseOption; //has player chosen anything
    public static bool playerChoosenLoans; 
    public static bool playerChoosenInvest;

    //handles preventing player from taking infinite loans;
    int loanCooldown = 3;
    int lCTimer = 0; //loan cooldown timer
    bool canChooseLoan;

    //stores the pop ups
    public GameObject loanPopUp;
    public GameObject investmentPopUp;
    public static GameObject warnPopup;
    public GameObject winScreen;


    void Start()
    {
        currentFund = 1000;
        turnCount = 1;
        currentLost = 100;
        playerChoosenInvest = false;
        playerChoosenLoans = false;
        playerChooseOption = false;
        canChooseLoan = true;
        warnPopup = GameObject.Find("WarnPopup");
        //winScreen = GameObject.Find("winScreen");
        //winScreen.SetActive(false);
        warnPopup.SetActive(false);
        loanPopUp.SetActive(false);
        investmentPopUp.SetActive(false);
        loanChoice = 0;
        investChoice = 0;
    }

   
    void Update()
    {
        
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
            randFactor = Random.Range(-5,6); //picks random int from between -10 and 10
            moneyToRecieve = (currentFund/2) + ( (currentFund/5) * randFactor);
            quarterlyCostToIncrease = moneyToRecieve/4;
        }
        currentFund += moneyToRecieve;
        currentLost += quarterlyCostToIncrease;

        Debug.Log("Player made: " + moneyToRecieve + "; Player quarter cost increase: " + quarterlyCostToIncrease);

        //turns on loan cooldown
        canChooseLoan = false;
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

        Debug.Log("Player spent: " + moneyToInvest + "; Player recieved: " + moneyToRecieve); 
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
        if (loanPopUp.activeSelf)
        {
        loanChoice = 1;
        ChoosenLoans();
        }

        else
        {
        investChoice = 1;
        ChoosenInvest();
        }
    }
    public void ChooseOptionTwo()
    {
        if (loanPopUp.activeSelf)
        {
        loanChoice = 2;
        ChoosenLoans();
        }

        else
        {
        investChoice = 2;
        ChoosenInvest();
        }
    }
    public void ChooseOptionThree()
    {
        if (loanPopUp.activeSelf)
        {
        loanChoice = 3;
        ChoosenLoans();
        }

        else
        {
        investChoice = 3;
        ChoosenInvest();
        }

    }    

    //a method advances the turn when called
    public void AdvanceTurn()
    {
        if (playerChooseOption == true) //if player chose something...
        {
            Debug.Log("Player made a choice.");
            if (playerChoosenLoans)
            {
                Debug.Log("Player choose to take a loan.");
                CalculatingLoan();
            }
            if (playerChoosenInvest)
            {
                Debug.Log("Player choose to make an investment.");
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
        if (turnCount/4 == NUMBER_OF_YEARS_TO_wIN)
        {
           winScreen.SetActive(true);
        }

        //if our money is reduced to 0, we lose.
        //TODO: add the lose condition and popup toggle.

        }
        else
        {
            Debug.Log("Plyer did not make a choice.");
            StartCoroutine(Popup(2.0f,"Please Select an Option First."));
        }
    }

    //static method to display the warning popup, with a text of our desire, for a specified number of seconds.
    public static IEnumerator Popup(float secsToShow, string textToDisplay)
    {
        warnPopup.SetActive(true);
        warnPopup.GetComponent<TextMeshProUGUI>().text = string.Format(textToDisplay);
        yield return new WaitForSeconds(secsToShow);
        warnPopup.SetActive(false);
    }
}