using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    /// <summary>
    /// The type of item this resource gives out.
    /// </summary>
    public Item createdItem;
    /// <summary>
    /// How many resource items this resource deposit can give out.
    /// </summary>
    [SerializeField] private int capacity;

    
    /// <summary>
    /// Returns the item this resource object creates.
    /// </summary>
    /// <returns>Item</returns>
    public Item MineResource()
    {
        if (!HasCapacity())
        {
            Debug.LogError("Trying to mine from an empty resource deposit.");
            return null;
        }
        
        capacity--;
        return createdItem;
    }
    
    /// <summary>
    /// Returns a bool value depending on whether this resource can give out more items.
    /// </summary>
    /// <returns></returns>
    public bool HasCapacity()
    {
        return capacity > 0;
    }
}
