public interface IInteractable
{
	public bool isPromptable { get; }
	public void Interact(Interactor interactor);
}
