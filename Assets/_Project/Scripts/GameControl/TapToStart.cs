using UnityEngine;

public class TapToStart : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	[SerializeField] private GroundCheck _groundCheck;
	[SerializeField] private PathFollower _pathFollower;

	private readonly int Run = Animator.StringToHash("Run");

	public void StartGame()
	{
		_animator.SetTrigger(Run);
		_pathFollower.enabled = true;
		_groundCheck.enabled = true;
	}
}