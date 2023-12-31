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
    [SerializeField] private TextMeshProUGUI textUGUI; //Dialogue Text
    [SerializeField] private float delayBetweenChar;


    public Button dialogueButton;
    public bool isTyping; // Indicates whether the text is currently being typed

    [SerializeField] public GameObject playerGo;
    [SerializeField] public ShopManager shopManager;

    private void Awake()
    {
        if (!DialogueManagerInstance)
        {
            DialogueManagerInstance = this;
        }

        if (!playerGo)
        {
            playerGo = GameObject.FindGameObjectWithTag("Player");
            
        }
    }

    // Check if the dialogue panel is active, indicating if there's an active dialogue
    public bool IsInDialogue()
    {
        if (dialoguePanel.activeSelf)
        {
            return true;
        }

        return false;
    }
    
    private void Start()
    {
        shopManager = ShopManager.ShopManagerInstance;
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
