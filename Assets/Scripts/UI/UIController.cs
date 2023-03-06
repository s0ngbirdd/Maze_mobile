using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject _popupObject;
    [SerializeField] private Animator _popupAnimator;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _shieldButton;
    [SerializeField] private GameObject _startFadeObject;
    [SerializeField] private GameObject _endFadeObject;

    private void OnEnable()
    {
        Finish.OnFinish += EnableEndFade;
    }

    private void OnDisable()
    {
        Finish.OnFinish -= EnableEndFade;
    }

    private void Start()
    {
        EnableStartFade();
    }

    public void EnablePopup()
    {
        _popupObject.SetActive(true);
        Time.timeScale = 0;
        _pauseButton.interactable = false;
        _shieldButton.interactable = false;
    }

    public void DisablePopup()
    {
        _popupObject.SetActive(false);
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

    private void EnableEndFade()
    {
        _endFadeObject.SetActive(true);
    }

    public void EnableStartFade()
    {
        _startFadeObject.SetActive(true);
    }
}
