using System;
using Controllers.Building_Scripts;
using UnityEngine;

public class BuildMenuButton : MonoBehaviour
{
    private PlayerBuildingScript buildingScript;
    public BuildMenuItem menuItem;
    
    private void Awake()
    {
        buildingScript = GameManager.PlayerController.playerBuildingScript;
    }

    public void Clicked()
    {
        buildingScript.ChangeBuilding(menuItem);
    }
}