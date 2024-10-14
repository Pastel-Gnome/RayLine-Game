using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	protected Rigidbody2D rb;
	protected Animator animator;

	protected Vector2 movement;
	protected float walkAcceleration = 50f;
	public float DRAG_CONSTANT = 0.01f;
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
			rb.AddForce(new Vector2(movement.x * WALK_SPEED, movement.y * WALK_SPEED) + new Vector2(Mathf.Pow(rb.velocity.x, 2) * -Mathf.Sign(rb.velocity.x) * DRAG_CONSTANT, Mathf.Pow(rb.velocity.y, 2) * -Mathf.Sign(rb.velocity.y) * DRAG_CONSTANT), ForceMode2D.Impulse);
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
