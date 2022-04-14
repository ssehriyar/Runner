using System.Collections;
using UnityEngine;

public class ObstacleCollide : MonoBehaviour
{
	[SerializeField] private GameObject _retry;
	[SerializeField] private GameObject _stackManager;
	[SerializeField] private Rigidbody _playerRigidbody;
	[SerializeField] private Collider _blockCollider;
	[SerializeField] private Animator _playerAnimator;
	[SerializeField] private PathFollower _pathFollower;
	[SerializeField] private RiseAndFall _riseAndFall;
	[SerializeField] private Ragdoll _ragdoll;

	private void OnCollisionEnter(Collision collision)
	{
		var obstacle = collision.gameObject.GetComponent<Obstacle>();
		if (obstacle == null) return;

		_stackManager.SetActive(false);

		_pathFollower.enabled = false;
		_playerRigidbody.isKinematic = true;
		_blockCollider.enabled = false;
		_playerAnimator.enabled = false;
		_riseAndFall.enabled = false;
		_ragdoll.OpenRagdoll();
		StartCoroutine(Retry());
	}

	private IEnumerator Retry()
	{
		yield return new WaitForSeconds(1.2f);
		_retry.SetActive(true);
	}
}
