public interface IInteractable
{
	public string interactionPrompt { get; }
	public bool isPromptable { get; set; }
	public InteractData Interact(Interactor interactor);
}
