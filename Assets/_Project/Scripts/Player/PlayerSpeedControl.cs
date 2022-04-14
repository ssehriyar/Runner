using UnityEngine;

public class PlayerSpeedControl : MonoBehaviour
{
	[SerializeField] private int _maxFallSpeed = 10;

	private Rigidbody _rb;
	private Vector3 _velocity;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_maxFallSpeed *= -1;
		_velocity = _maxFallSpeed * Vector3.up;
	}

	private void FixedUpdate()
	{
		if (_rb.velocity.y < _maxFallSpeed)
			_rb.velocity = _velocity;
	}
}