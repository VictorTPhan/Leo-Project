using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //a public integer that holds the turn count
    public static int turnCount;
    public static int currentFund;
    public static int currentLost;

    public static bool playerChooseOption; //has player chosen anything
    public static bool playerChoosenLoans; 
    public static bool playerChoosenInvest;

    //handles preventing player from taking infinite loans;
    int loanCooldown = 3;
    int lCTimer = 0; //loan cooldown timer
    bool canChooseLoan;

    //stores the pop ups
    public GameObject loanPopUp;

    void Start()
    {
        currentFund = 1000;
        turnCount = 1;
        currentLost = 100;
        playerChoosenInvest = false;
        playerChoosenLoans = false;
        playerChooseOption = false;
        canChooseLoan = true;
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
        }
        else
        {
            Debug.Log("Error: Can't take another loan");
        }
    }

    public void ChoosenInvest() 
    {
        playerChooseOption = true;
        playerChoosenLoans = false;
        playerChoosenInvest = true;        
    }

    //Handles the math for the amount of money gained/lossed.
    public void CalculatingLoan()
    {
        int randFactor; //random factor
        int moneyToRecieve;
        int quarterlyCostToIncrease;

        //gives player money, but raises Quarterly costs from then on.

        //choice 1 -low money
        randFactor = Random.Range(-10,11); //picks random int from between -10 and 10
        moneyToRecieve = (currentFund/2) + ( (currentFund/100) * randFactor);
        quarterlyCostToIncrease = moneyToRecieve/20;
        //choice 2 -medium money

        //choice 3 -high money

        currentFund += moneyToRecieve;
        currentLost += quarterlyCostToIncrease;

        //turns on loan cooldown
        canChooseLoan = false;
    }
    //Handles the math for the amount of money gained/lossed.
    public int CalculatingInvestment()
    {
        //spends players money for that month, but has change to make a return on invetsment (ROI)


        //choice 1

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
        turnCount++;
        }
        else
        {
            Debug.Log("Plyer did not make a choice.");
        }
    }
}