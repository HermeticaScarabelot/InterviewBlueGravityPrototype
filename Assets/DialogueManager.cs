using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager DialogueManagerInstance;
    
    [SerializeField] private GameObject dialoguePanel;
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
        if (!dialoguePanel.activeSelf)
        {
            dialoguePanel.SetActive(true);
        }
        isTyping = true;
        
        char[] allChars = line.ToCharArray();
        
        foreach (var c in allChars)//Slowly iterate through all Chars to display the Text char by char
        {
            textUGUI.text += c;
            yield return new WaitForSeconds(delayBetweenChar);
        }
        isTyping = false;
    }

    public void CloseDialogue()
    {
        ResetText();
        dialoguePanel.SetActive(false);
    }
}
