using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Serialize
    [SerializeField] private float _requiredHoldTime;
    [SerializeField] private Image _fillImage;
    [SerializeField] private UnityEvent _onClick;
    [SerializeField] private UnityEvent _onRelease;
    [SerializeField] private UnityEvent _onLongClick;

    // Private
    private bool _pointerDown;
    private float _pointerDownTimer;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale > 0)
        {
            _pointerDown = true;
            _onClick?.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Time.timeScale > 0)
        {
            Reset();
            _onRelease?.Invoke();
        }
    }

    private void Update()
    {
        if (_pointerDown)
        {
            _pointerDownTimer += Time.deltaTime;

            if (_pointerDownTimer >= _requiredHoldTime)
            {
                _onLongClick?.Invoke();

                Reset();
            }

            _fillImage.fillAmount = _pointerDownTimer / _requiredHoldTime;
        }
    }

    private void Reset()
    {
        _pointerDown = false;
        _pointerDownTimer = 0;
        _fillImage.fillAmount = _pointerDownTimer / _requiredHoldTime;
    }
}
