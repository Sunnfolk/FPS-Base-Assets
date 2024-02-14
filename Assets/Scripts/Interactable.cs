using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    public void RunInteractEvent() => Event.Invoke();
}