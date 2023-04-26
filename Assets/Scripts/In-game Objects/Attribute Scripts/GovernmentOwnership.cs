using UnityEngine;
using UnityEngine.Events;

public class GovernmentOwnership : MonoBehaviour
{
    [SerializeField] private GovernmentScript owner;
    public UnityEvent onNewOwner;
    
    /// <summary>
    /// Get the current owner
    /// </summary>
    /// <returns></returns>
    public GovernmentScript GetOwner()
    {
        return owner;
    }
    
    /// <summary>
    /// Sets the new owner of this object and calls the OnNewOwner event
    /// </summary>
    /// <param name="newOwner"></param>
    public void SetOwner(GovernmentScript newOwner)
    {
        owner = newOwner;
        onNewOwner?.Invoke();
    }
}