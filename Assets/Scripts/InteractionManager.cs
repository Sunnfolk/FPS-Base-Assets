using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private new Camera camera;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Transform _interactObject;
    private Renderer _interactRenderer;
    
    public void UpdateInteraction(bool interacted)
    {
        SetDefaultMaterial();

        var rayDirection = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(rayDirection, out var hit, interactRange))
        {
            _interactObject = hit.transform;
            
            if (!_interactObject.TryGetComponent(out IInteractable interactInterface)) return;
       
            SetHighlightMaterial();
            
            if (!interacted) return;
            interactInterface.Interact();
        }
    }

    private void SetHighlightMaterial()
    {
        
        _interactRenderer = _interactObject.GetComponent<Renderer>();
        defaultMaterial = _interactRenderer.material;

        if (_interactRenderer != null)
        {
            _interactRenderer.material = highlightMaterial;
        }
    }

    private void SetDefaultMaterial()
    {
        if (_interactObject == null) return;
        _interactRenderer = _interactObject.GetComponent<Renderer>();
        _interactRenderer.material = defaultMaterial;
        _interactObject = null;
        _interactRenderer = null;
    }
}