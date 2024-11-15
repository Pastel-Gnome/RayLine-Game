using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageClue : SaveableObj, IInteractable
{
	protected bool opened = false;

	public int anomalyID;
	[SerializeField] char cluePiece;

	[TextArea(minLines: 1, maxLines: 4)]
	[SerializeField] string clueContents = "Example t<color=\"red\"><b>e</b></color>xt";

	public bool isPromptable { get; set; } = true;

	private void Start()
	{
		uniqueId = UniqueID.CreateID(anomalyID.ToString() + cluePiece, transform);
	}

	public virtual void Interact(Interactor interactor)
	{
		if (!opened)
		{
			OpenClue();
		} else
		{
			ObtainClue();
		}
	}

	protected virtual void OpenClue()
	{
		HiddenMessageManager.instance.OpenClueWindow(clueContents);
		opened = true;
	}

	protected virtual void ObtainClue()
    {
		HiddenMessageManager.instance.CloseClueWindow();
		HiddenMessageManager.instance.AddClueLetter(cluePiece);
		SelfDestruct();
    }

	public virtual void SelfDestruct()
	{
		GameEventsManager.instance.AddDeleteable(uniqueId);
		Destroy(gameObject);
		//CallAfterFrames.Create(2, () => { Destroy(gameObject); });
	}
}
