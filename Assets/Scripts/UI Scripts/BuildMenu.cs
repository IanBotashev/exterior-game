using System;
using Controllers;
using Controllers.Building_Scripts;
using UnityEngine;

/// <summary>
/// A script that handles switching modes whenever the build button is pressed.
/// </summary>
public class BuildMenu : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = GameManager.PlayerController;
    }

    public void Clicked()
    {
        if (playerController.mode == PlayerControllerMode.Building)
        {
            playerController.ChangeMode(playerController.defaultMode);
        }
        else
        {
            playerController.ChangeMode(PlayerControllerMode.Building);
        }
    }
}