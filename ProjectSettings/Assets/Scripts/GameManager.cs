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

    void Start()
    {
        currentFund = 1000;
        turnCount = 1;
        currentLost = 100;
        playerChoosenInvest = false;
        playerChoosenLoans = false;
        playerChooseOption = false;
    }

   
    void Update()
    {
        
    }

    public void ChoosenLoans() 
    {
        playerChooseOption = true;
        playerChoosenLoans = true;
        playerChoosenInvest = false;
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
        //gives player money, but raises Quarterly costs from then on.
    }
    //Handles the math for the amount of money gained/lossed.
    public void CalculatingInvestment()
    {
        //spends players money for that month, but has change to make a return on invetsment (ROI)
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
            }
            if (playerChoosenInvest)
            {
                Debug.Log("Player choose to make an investment.");
            }

        playerChoosenInvest = false;
        playerChoosenLoans = false;
        playerChooseOption = false;


        currentFund = currentFund - currentLost;
        turnCount++;
        }
        else
        {
            Debug.Log("Plyer did not make a choice.");
        }
    }
}