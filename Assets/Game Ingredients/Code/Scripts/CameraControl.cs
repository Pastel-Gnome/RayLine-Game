using Cinemachine.Utility;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[SerializeField] Transform targetGO;
	private float speed = 0.05f;
	[SerializeField] Vector2 allowance = new Vector2(0, 0);

	private void Start()
	{
		if (targetGO != null)
		{
			this.transform.position = new Vector3(targetGO.position.x, targetGO.position.y, -10);
		}
	}

	private void LateUpdate()
	{
		if (targetGO != null)
		{
			if (Mathf.Abs(transform.position.x - targetGO.position.x) > allowance.x && allowance.x >= 0)
			{
				transform.position += new Vector3(targetGO.position.x - transform.position.x, 0, 0).normalized * speed;
			}
			if (Mathf.Abs(transform.position.y - targetGO.position.y) > allowance.y && allowance.y >= 0)
			{
				transform.position += new Vector3(0, targetGO.position.y - transform.position.y, 0).normalized * speed;
			}
		}
	}

	public void SetCameraTarget(Transform tempTarget)
	{
		targetGO = tempTarget;
	}
}
