using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ItemType {Test, Coal, Test2};

[CreateAssetMenu(menuName = "Inventory Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
}
