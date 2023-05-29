using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    private const float FADE_OUT_SPEED = 0.03f;
    [SerializeField]
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        StartCoroutine(HideProcess());
    }

    private IEnumerator HideProcess()
    {
        yield return new WaitForSeconds(2);
        
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= FADE_OUT_SPEED;
            yield return new WaitForSeconds(FADE_OUT_SPEED);
        }
        
        gameObject.SetActive(false);
    }
}
