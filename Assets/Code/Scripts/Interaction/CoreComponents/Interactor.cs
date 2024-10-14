using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
	[SerializeField] Transform interactionPoint;
	[SerializeField] float interactionRadius = 0.5f;
	[SerializeField] LayerMask interactableLayer;
	[SerializeField] InteractionPromptUI interactionPromptUI;

	private readonly Collider2D[] colliders = new Collider2D[3];
	[SerializeField] int numCollidersFound;

	private IInteractable interactable;

	private void Update()
	{
		numCollidersFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position, interactionRadius, colliders, interactableLayer);
		if (numCollidersFound > 0)
		{
			interactable = colliders[0].GetComponent<IInteractable>();
			if (interactable != null)
			{
				if (!interactionPromptUI.isShowing && interactable.isPromptable)
				{
					interactionPromptUI.ShowPrompt(interactable.interactionPrompt);
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
				if (context.started) 
				{
					int tempID;
					if ((tempID = interactable.Interact(this).questPieceID) > 0) 
					{
						GameInfo.instance.RegisterQuestInput(tempID);
					}
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
