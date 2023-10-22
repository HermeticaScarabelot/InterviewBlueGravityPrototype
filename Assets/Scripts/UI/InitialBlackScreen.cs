using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InitialBlackScreen : MonoBehaviour
{
    [SerializeField] private float tweenDuration;
    [SerializeField] private Ease tweenEase;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartTween", 1f);
    }

    void StartTween()
    {
        image.DOColor(new Color(0, 0, 0, 0), tweenDuration).SetEase(tweenEase).OnComplete(() => gameObject.SetActive(false));
    }

}
