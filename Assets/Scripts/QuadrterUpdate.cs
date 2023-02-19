using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuadrterUpdate : MonoBehaviour
{
    public TextMeshProUGUI quaterText;
    int yearNumber = 1;
    int quarterNumber = 1;
    
    // Update is called once per frame
    void Update()
    {
        quaterText.text = string.Format("Year " + yearNumber + "\nQ" + quarterNumber);
    }

    public void NextYear()
    {
        if(GameManager.playerChooseOption == true)
        {
            if ((quarterNumber % 4) == 0)
            {
                yearNumber++;
                quarterNumber = 1;
            }
            else 
            {
                quarterNumber++;
            }
        }
    }
}
