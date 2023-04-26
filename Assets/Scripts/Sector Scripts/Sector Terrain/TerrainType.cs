using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Type", menuName = "Terrain Type", order = 0)]
public class TerrainType : ScriptableObject
{
    public float movementHamper;
    public Sprite spriteTerrain;
    public bool canBuildOn;
    public bool canHoldResources;
    public bool CanBeClaimed;
}
