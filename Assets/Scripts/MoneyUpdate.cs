using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUpdate : MonoBehaviour
{
    //The text that is displayed
    public TextMeshProUGUI moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text =string.Format("Current Funds : $" + GameManager.currentFund);
    }
}
