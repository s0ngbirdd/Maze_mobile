using UnityEngine;

public class PopupDisabler : MonoBehaviour
{
    // Private
    private UIController _UIController;

    private void Start()
    {
        _UIController = FindObjectOfType<UIController>();
    }

    public void DisablePopup()
    {
        _UIController.DisablePopup();
    }
}