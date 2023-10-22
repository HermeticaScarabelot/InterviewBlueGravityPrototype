using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool interactedRecently;

    public virtual void Interact()
    {
        Debug.Log("Interacted");   
    }
    
}
