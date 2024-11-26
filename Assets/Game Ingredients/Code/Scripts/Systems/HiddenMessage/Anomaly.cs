using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Anomaly : SaveableObj, IInteractable
{
    public int anomalyID;
    public string answer;
	[SerializeField] UnityEvent fixedEvent = new UnityEngine.Events.UnityEvent();
	private Animator parentAnimator;
    private DialogueActivator parentDialogue;

	public bool isPromptable { get; set; } = true;

	protected override void Start()
	{
		uniqueId = UniqueID.CreateID(anomalyID.ToString() + "Parent", transform);
		CallAfterDelay.Create(0.1f, () => {
            parentAnimator = transform.parent.GetComponent<Animator>();
            parentDialogue = transform.parent.GetComponent<DialogueActivator>();
            parentAnimator.SetTrigger("Flip");
			parentDialogue.interactable = false;
		}).transform.SetParent(transform);
	}

	public virtual void Interact(Interactor interactor)
    {
        OpenAnomaly();
	}

	public void FixAnomaly()
    {
		parentAnimator.SetTrigger("Flip");
		parentDialogue.interactable = true;

        fixedEvent.Invoke();
        GameEventsManager.instance.AddFixedAnomalies(anomalyID);
        Destroy(gameObject);
    }

    public void OpenAnomaly()
    {
        HiddenMessageManager.instance.UpdateCurrentAnomaly(this);
        HiddenMessageManager.instance.OpenInputWindow();
    }
}
