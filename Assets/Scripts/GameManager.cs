using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //a public integer that holds the turn count
    public int turnCount;
    public static int currentFund;

    void Start()
    {
        currentFund = 1000;
        turnCount = 1;
    }

   
    void Update()
    {
        
    }

    //a method advances the turn when called
    public void AdvanceTurn()
    {
        //turnCount = turnCount + 1
        turnCount++;
    }
}