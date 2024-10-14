using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[SerializeField] Transform targetGO;
	[SerializeField] Vector2 offset = new Vector2(0, 0);

	private void Start()
	{
		if (targetGO != null)
		{
			this.transform.position = new Vector3(targetGO.position.x + offset.x, targetGO.position.y + offset.y, -10);
		}
	}

	private void LateUpdate()
	{
		if (targetGO != null)
		{
			transform.position = new Vector3(targetGO.position.x + offset.x, targetGO.position.y + offset.y, -10);
		}
	}

	public void SetCameraTarget(Transform tempTarget)
	{
		targetGO = tempTarget;
	}
}
