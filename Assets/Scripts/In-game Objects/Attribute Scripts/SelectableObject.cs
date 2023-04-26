using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{
    public UnityEvent onRightClick;
    public UnityEvent onSelected;
    public UnityEvent onUnselected;
    public bool canMultiSelect;
    [SerializeField] private GameObject selectionHighlight;
    
    /// <summary>
    /// Activates a highlight object.
    /// </summary>
    private void ActivateHighlight()
    {
        selectionHighlight.SetActive(true);
    }
    
    /// <summary>
    /// Deactivates the highlight object.
    /// </summary>
    private void DeactivateHighlight()
    {
        selectionHighlight.SetActive(false);
    }
    
    /// <summary>
    /// Method to be used by other scripts to denote when this object is selected by a government.
    /// </summary>
    public void Select()
    {
        ActivateHighlight();
        onSelected?.Invoke();
    }
    
    /// <summary>
    /// Method to be used by other scripts to denote when this object is unselected by a government.
    /// </summary>
    public void Unselect()
    {
        DeactivateHighlight();
        onUnselected?.Invoke();
    }
    
    /// <summary>
    /// While selected, if a government right clicks, this event is called.
    /// </summary>
    public void RightClicked()
    {
        onRightClick?.Invoke();
    }
}