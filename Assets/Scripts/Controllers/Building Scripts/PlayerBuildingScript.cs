using System;
using Controllers.Building_Scripts;
using UnityEngine;

public class PlayerBuildingScript : PlayerControlModule
{
    /// <summary>
    /// The Building that's currently being built.
    /// </summary>
    public BuildMenuItem currentBuilding;

    public GameObject ghostBuilding;
    private GovernmentScript player;

    private void Update()
    {
        player = GameManager.Instance.PlayerGovernment;
        ghostBuilding.transform.position =
            GridSystem.Instance.CenterPosition(GridSystem.Instance.GetMouseWorldPosition());
    }

    private void OnEnable()
    {
        CreateGhostBuild();
    }

    private void OnDisable()
    {
        Destroy(ghostBuilding);  // We're being disabled, remove the ghost build.
    }

    /// <summary>
    /// Creates a "ghost" of a building prefab to showcase to the player where it's gonna be built.
    ///
    /// First we create a brand new game object, titled "Ghost Building"
    /// Then we copy the sprite and color from the building prefab
    /// And also it's scale.
    /// </summary>
    private void CreateGhostBuild()
    {
        ghostBuilding = new GameObject("Ghost Building");  // Create ghost
        var sprite = ghostBuilding.AddComponent<SpriteRenderer>();  // Add a sprite renderer component.
        var currentBuildingSpriteRenderer = currentBuilding.buildingPrefab.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 4;
        sprite.sprite = currentBuildingSpriteRenderer.sprite;  // Set it's sprite
        var color = currentBuildingSpriteRenderer.color;
        color.a = 0.8f;  // Set it to be transparent
        Debug.Log(color);
        sprite.color = color;

        ghostBuilding.transform.localScale = currentBuilding.buildingPrefab.transform.localScale;  // Copy scale over.
    }
    
    /// <summary>
    /// If we have to change building while turned on, we set the currentBuilding variable,
    /// then destroy old ghost and create a new one.
    /// </summary>
    /// <param name="newBuild"></param>
    public void ChangeBuilding(BuildMenuItem newBuild)
    {
        currentBuilding = newBuild;
        Destroy(ghostBuilding);
        CreateGhostBuild();
    }
    
    /// <summary>
    /// When the user presses the left click button, actually place the building.
    /// Subtract the cost from the government
    /// Checks if it's within bounds.
    /// </summary>
    public void Build()
    {
        if (!CanRun()) return;
        // Check if we're out of bounds
        var mousePos = GridSystem.Instance.GetMouseWorldPosition();
        if (!GridSystem.Instance.WithinBounds(mousePos)) Debug.LogWarning("Tried to build outside of bounds!");
        if (!GridSystem.Instance.CanBuildOnSector(mousePos, player)) return;  // Check if we can build there

        
        
        var newBuild = Instantiate(currentBuilding.buildingPrefab);
        newBuild.transform.position = mousePos;
        
        player.AddMoney(-currentBuilding.cost);
    }
}