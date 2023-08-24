using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconmyManager : Singletoon<EconmyManager>
{
    private TMP_Text goldText;
    private int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "GoldAmountText";

    public void UpdateCurrentGold()
    {
        currentGold += 1;
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        FindObjectOfType<AudioManager>().Play("GoldPickup");
        goldText.text = currentGold.ToString("D3");
    }
}
