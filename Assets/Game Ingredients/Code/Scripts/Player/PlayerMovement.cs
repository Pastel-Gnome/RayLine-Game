using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	protected Rigidbody2D rb;
	protected Animator animator;

	protected Vector2 movement;
	public float WALK_SPEED = 5f;

	private static bool canMove = true;

	protected virtual void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
	}

	public static void DisableMovement()
	{
		canMove = false;
	}

	public static void EnableMovement()
	{
		canMove = true;
	}

	protected virtual void FixedUpdate()
	{
        if (canMove && movement != Vector2.zero)
        {
			SoundManager.instance.ToggleWalkSound(true);
			rb.velocity = new Vector2 (movement.x * WALK_SPEED, movement.y * WALK_SPEED);
			animator.SetFloat("Horizontal", movement.x);
			animator.SetFloat("Vertical", movement.y);
			animator.SetFloat("Speed", movement.sqrMagnitude);
		} else
		{
			SoundManager.instance.ToggleWalkSound(false);
			rb.velocity = Vector2.zero;
			animator.SetFloat("Speed", 0);
		}
		
	}
}
