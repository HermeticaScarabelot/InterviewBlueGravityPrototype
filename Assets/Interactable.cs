using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType
    {
        Dialogue,
        Flower
    }
    
    public InteractableType type;

    public virtual void Interact()
    {
        Debug.Log("Interacted");   
    }
    
}
