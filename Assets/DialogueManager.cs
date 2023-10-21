using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DialogueManagerInstance;
    
    [SerializeField] private GameObject textPanelGo;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private float delayBetweenChar;


    public Button dialogueButton;
    public bool isTyping;

    private void Awake()
    {
        if (!DialogueManagerInstance)
        {
            DialogueManagerInstance = this;
        }
    }

    public void ResetText()
    {
        textUGUI.text = String.Empty;
    }
    
    public IEnumerator TypeString(string line)
    {

        Debug.Log("Started Typing");
        isTyping = true;
        char[] allChars = line.ToCharArray();
        foreach (var c in allChars)
        {
            textUGUI.text += c;
            yield return new WaitForSeconds(delayBetweenChar);
        }

        isTyping = false;


    }
}
