using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentScript : MonoBehaviour
{
    public int population;
    public float money;
    public Color countryColor;
    public List<GovernmentScript> activeEnemies;

    /// <summary>
    /// Function to add money. Use negatives to take away.
    /// </summary>
    /// <param name="amount">Amount to add</param>
    public void AddMoney(int amount)
    {
        money += amount;
        activeEnemies = new List<GovernmentScript>();
    }
    
    /// <summary>
    /// Function to add population. Use negatives to take away.
    /// </summary>
    /// <param name="amount">Amount to add</param>
    public void AddPopulation(int amount)
    {
        population += amount;
    }
}
 