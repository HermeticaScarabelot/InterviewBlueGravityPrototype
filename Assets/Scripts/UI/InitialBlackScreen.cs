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

    //Important for hiding the first ms that the Inventory and Shop take to initialize
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        Invoke("StartTween", 1f);
    }

    void StartTween()
    {
        image.DOColor(new Color(0, 0, 0, 0), tweenDuration).SetEase(tweenEase).OnComplete(() => gameObject.SetActive(false));
    }

}
