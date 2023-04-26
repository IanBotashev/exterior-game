using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GovernmentStatsUpdateScript : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText; 
    [SerializeField] private TMP_Text popText;
    private GovernmentScript player;

    private void Start()
    {
        player = GameManager.Instance.PlayerGovernment;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePopulationText();
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "$" + player.money;
    }
    
    private void UpdatePopulationText()
    {
        popText.text = player.population.ToString(CultureInfo.CurrentCulture);
    }
}
