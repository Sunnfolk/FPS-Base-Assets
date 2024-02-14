using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("Interacted");    
    }
}