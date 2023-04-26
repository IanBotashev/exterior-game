using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    [SerializeField] private InventoryScript inventory;
    public void OnRightClick()
    {
        Debug.Log("Building Right Click");
    }
}
