using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	public bool IsGrounded { get; private set; }
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private RiseAndFall _riseAndFall;

	private void Start()
	{
		enabled = false;
	}

	private void Update()
	{
		IsGrounded = Physics.CheckSphere(transform.position, 0.1f, _groundMask);
		if (!IsGrounded)
			StartCoroutine(_riseAndFall.Fall());
	}
}
