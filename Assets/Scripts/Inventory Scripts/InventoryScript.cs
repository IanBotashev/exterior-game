using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    public int capacity;

    /// <summary>
    /// Adds an item to the list.
    /// Checks if it would go over capacity, if so, returns an error.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(Item item)
    {
        if (CanAddNewItem())
        {
            items.Add(item);
        }
        else
        {
            throw new Exception("Cannot add another item as this inventory is at capacity.");
        }
    }
    
    /// <summary>
    /// Removes an item from this list.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    
    /// <summary>
    /// Checks if this inventory contains an item.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>If this inventory contains the item</returns>
    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
    
    /// <summary>
    /// Returns whether another item can be added.
    /// </summary>
    /// <returns></returns>
    public bool CanAddNewItem()
    {
        return items.Count + 1 <= capacity;
    }

    /// <summary>
    /// Moves an item from this inventory to another.
    /// Removes it from here, adds it there.
    /// Checks if the item exists in here.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="otherInventory"></param>
    public void MoveToAnotherInventory(Item item, InventoryScript otherInventory)
    {
        // Check if it exists here.
        if (!ContainsItem(item))
        {
            throw new Exception("Cannot move item, as this inventory doesn't have item.");
        }
        
        // Check if the other inventory can have another item.
        if (otherInventory.CanAddNewItem())
        {
            throw new Exception("Cannot move item, as the other inventory doesn't have space.");
        }
        
        otherInventory.AddItem(item);
        RemoveItem(item);
    }
}
