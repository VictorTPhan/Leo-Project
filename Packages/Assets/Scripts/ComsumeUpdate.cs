using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComsumeUpdate : MonoBehaviour
{
    public TextMeshProUGUI comsumeText;
   

    // Update is called once per frame
    void Update()
    {
        comsumeText.text = string.Format("Expected Losses : -$" + GameManager.currentLost);
    }
}
