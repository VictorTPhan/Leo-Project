using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsSurvived : MonoBehaviour
{
    public TextMeshProUGUI survivedText;
    
    // Update is called once per frame
    void Update()
    {
        survivedText.text = string.Format(GameManager.turnCount + " Quarters");
    }   
    // Update is called once per frame
    void OnEnable()
    {
        
    }
}
