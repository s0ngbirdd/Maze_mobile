using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _popup;
    [SerializeField] private Animator _popupAnimator;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _shieldButton;

    public void EnablePopup()
    {
        _popup.SetActive(true);
        Time.timeScale = 0;
        _pauseButton.interactable = false;
        _shieldButton.interactable = false;
    }

    public void DisablePopup()
    {
        _popup.SetActive(false);
        Time.timeScale = 1;
        _pauseButton.interactable = true;
        _shieldButton.interactable = true;
    }

    public void PlayDisablePopupAnimation()
    {
        _popupAnimator.SetTrigger("Disabled");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}