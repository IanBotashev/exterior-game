using System;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// On start up, automatically makes the current object be a child to another one.
/// </summary>
public class AutomaticParentingScript : MonoBehaviour
{
    public string parentTag;

    private void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag(parentTag).transform;
        Destroy(this);
    }
}
