using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
