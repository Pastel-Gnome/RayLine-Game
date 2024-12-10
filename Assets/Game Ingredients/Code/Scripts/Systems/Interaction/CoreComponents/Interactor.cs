using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
	[Header("Interaction Info")]
	[SerializeField] Transform interactionPoint;
	[SerializeField] float interactionRadius = 0.5f;
	[SerializeField] LayerMask interactableLayer;
	[SerializeField] InteractionPromptUI interactionPromptUI;

	[Header("Current Available Objects")]
	[SerializeField] int numCollidersFound;
	private IInteractable interactable;
	private readonly Collider2D[] colliders = new Collider2D[3];
	

	private void Update()
	{
		numCollidersFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableLayer);
		if (numCollidersFound > 0)
		{
			for (int i = 0; i < numCollidersFound; i++)
			{
				interactable = colliders[i].GetComponent<IInteractable>();
				if (interactable != null)
				{
					if (interactable.isPromptable)
					{
						if (!interactionPromptUI.isShowing)
						{
							interactionPromptUI.ShowPrompt();
						}
						break;
					} else if (i == numCollidersFound - 1)
					{
						interactionPromptUI.HidePrompt();
					}
				}
			}
		}
		else
		{
			if (interactable != null) interactable = null;
			if (interactionPromptUI.isShowing) interactionPromptUI.HidePrompt();
		}
	}

	public void OnInteract(InputAction.CallbackContext context)
	{
		if (numCollidersFound > 0)
		{
			if (interactable != null)
			{
				if (context.started && !DialogueManager.inProgress) 
				{
					interactable.Interact();
				}
				if (!interactable.isPromptable) interactionPromptUI.HidePrompt();
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(interactionPoint.position, interactionRadius);
	}
}
