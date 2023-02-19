using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
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
    public static GameObject warnPopup;


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
        warnPopup.SetActive(false);
        loanPopUp.SetActive(false);
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

        }
        //choice 3 -high money
        if (loanChoice == 3)
        {

        }
        currentFund += moneyToRecieve;
        currentLost += quarterlyCostToIncrease;

        //turns on loan cooldown
        canChooseLoan = false;
    }
    //Handles the math for the amount of money gained/lossed.
    public int CalculatingInvestment()
    {
        int randFactor = 0; //random factor
        int moneyToRecieve = 0;
        int moneyToInvest = 0;
        int randChoice = 0;

        //spends players money for that month, but has change to make a return on invetsment (ROI)
        if (investChoice == 1)
        {
            randChoice = Random.Range(1,101); //determines at random which of the choices appear
            if (randChoice <= 90) //%90 of the time
            {
            //choice 1.1 - low money invested, low returns, completely safe

            //MATH GOES HERE    

            }
            else
            {
            //choice 1.2 - low money invested, modest returns, mostly safe

            }
        }

        //choice 2

        //choice 3
        return 0;
    }

    //method that reveals the loan pop up menu
    public void revealLoanMenu()
    {  
        loanPopUp.SetActive(true);
    }

    public void closeLoanMenu()
    {
        loanPopUp.SetActive(false);
    }

    /*
    Methods for determining the user's choice.
    */
    public void ChooseOptionOne()
    {
        loanChoice = 1;
        ChoosenLoans();
    }
    public void ChooseOptionTwo()
    {
        loanChoice = 2;
        ChoosenLoans();
    }
    public void ChooseOptionThree()
    {
        loanChoice = 3;
        ChoosenLoans();
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