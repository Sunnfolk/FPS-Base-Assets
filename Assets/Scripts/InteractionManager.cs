using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private string interactTag = "Interactable";
    [SerializeField] private float interactRange;

    [SerializeField] private new Camera camera;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Transform _interactingWith;
    private Renderer _interactingRenderer;

    // ReSharper disable Unity.PerformanceAnalysis
    public void OnClick(bool interact)
    {
        if (_interactingWith == null) return;
        if (!interact) return;
        _interactingWith.GetComponent<Interactable>().RunInteractEvent();
    }

    #region INTERFACES
    
    public void InteractWithObject(GameObject objectToInteractWith, bool interact)
    {
        if (objectToInteractWith.TryGetComponent(out IInteractable interactableObject))
        {
            interactableObject.Interact();
        }
    }

    public void InteractWithObject(bool interact)
    {
        if (!interact) return;
        if (_interactingWith.TryGetComponent(out IInteractable interactableObject))
        {
            interactableObject.Interact();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateInteractionInterfaces(bool interacted)
    {
        if (_interactingWith != null)
        {
            // RESETS INTERACTABLE_OBJ MATERIAL
            var interactingRenderer = _interactingWith.GetComponent<Renderer>();
            interactingRenderer.material = defaultMaterial;
            _interactingWith = null;
        }
        
        // SET 3D / PERSPECTIVE CAMERA DIRECTION Based on mouse POS
        var rayDirection = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(rayDirection, out var hit, interactRange))
        {
            var selected = hit.transform;
            
            if (!selected.TryGetComponent(out IInteractable interactableObject)) return;
       
            var interactingRenderer = selected.GetComponent<Renderer>();
            defaultMaterial = interactingRenderer.material;

            if (interactingRenderer != null)
            {
                interactingRenderer.material = highlightMaterial;
            }

            _interactingWith = selected;
            
            if (!interacted) return;
            interactableObject.Interact();
            print("This obj was interacted with");
        }
    }
    
    #endregion
    

    public void UpdateInteraction()
    {
        if (_interactingWith != null)
        {
            // RESETS INTERACTABLE_OBJ MATERIAL
          var interactingRenderer = 
              _interactingWith.GetComponent<Renderer>();
          interactingRenderer.material = defaultMaterial;
          _interactingWith = null;
        }
        
        // SET 3D / PERSPECTIVE CAMERA DIRECTION Based on mouse POS
        var rayDirection = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(rayDirection, out var hit) && hit.distance < interactRange)
        {
            var selected = hit.transform;

            if (selected.CompareTag(interactTag))
            {
                var interactingRenderer = selected.GetComponent<Renderer>();
                defaultMaterial = interactingRenderer.material;

                if (interactingRenderer != null)
                {
                    interactingRenderer.material = highlightMaterial;
                }

                _interactingWith = selected;
            }
        }
    }
}