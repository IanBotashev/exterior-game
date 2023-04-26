using UnityEngine;


public class PlayerControlModule : MonoBehaviour
{
    /// <summary>
    /// Check if we can respond to any events right now.
    /// Basically just returns the current status of the object.
    /// if disabled, we should not be allowed to respond.
    /// Also, if we're over ui, we're not allowed to run.
    /// </summary>
    /// <returns></returns>
    protected bool CanRun()
    {
        return this.enabled && !GridSystem.Instance.MouseOverUI();
    }
}