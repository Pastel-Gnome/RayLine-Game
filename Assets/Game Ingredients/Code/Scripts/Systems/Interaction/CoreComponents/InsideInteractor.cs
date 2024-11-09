using UnityEngine;
using UnityEngine.InputSystem;

public class InsideInteractor : MonoBehaviour
{
	private IInteractable interactable;

	private void Start()
	{
		interactable = GetComponent<IInteractable>();
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (interactable != null)
		{
			if (context.started) 
			{
				interactable.Interact(null);
			}
		}
	}
}
