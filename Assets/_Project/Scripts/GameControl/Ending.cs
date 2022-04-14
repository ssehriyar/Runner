using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Ending : MonoBehaviour
{
	[SerializeField] private GameObject _slider;
	[SerializeField] private Transform _player;
	[SerializeField] private Transform _block;
	[SerializeField] private Transform _bridge;
	[SerializeField] private Rigidbody _playerRigidbody;
	[SerializeField] private Animator _playerAnimator;
	[SerializeField] private Animator _botAnimator;
	[SerializeField] private StackManager _stackManager;
	[SerializeField] private GroundCheck _groundCheck;
	[SerializeField] private PathFollower _pathFollower;
	[SerializeField] private LevelDone _levelDone;

	private readonly int Dance = Animator.StringToHash("Dance");
	private readonly int SadIdle = Animator.StringToHash("SadIdle");
	private readonly int Idle = Animator.StringToHash("Idle");
	private readonly int Run = Animator.StringToHash("Run");

	private int _totalBlockCount = 0;

	private float _nextBridgeRow;
	private float _currentBridgeRow;
	private float _bridgeColumn1;
	private float _bridgeColumn2;

	private void Start()
	{
		_nextBridgeRow = _block.localScale.z + 0.02f;
		_currentBridgeRow = -_nextBridgeRow;

		_bridgeColumn1 = 0f;
		_bridgeColumn2 = -(_block.localScale.x) * 1.5f;

		PathFollower.PathEnded += StartEnding;
	}

	private void StartEnding()
	{
		StartCoroutine(GetReadyForEnding());
	}

	private IEnumerator GetReadyForEnding()
	{
		while (!_groundCheck.IsGrounded)
		{
			yield return new WaitForEndOfFrame();
		}

		_groundCheck.enabled = false;
		_playerRigidbody.isKinematic = true;
		_pathFollower.enabled = false;
		_playerAnimator.SetTrigger(Idle);
		_slider.SetActive(false);

		StartCoroutine(PushBlocksToBridge());
	}

	private IEnumerator PushBlocksToBridge()
	{
		while (_stackManager.Blocks.Count > 0)
		{
			var block = _stackManager.Blocks.Pop();
			block.transform.SetParent(_bridge);
			block.transform.DOLocalMove(EmptyBridgePosition(), 0f);
			block.transform.DOLocalRotate(Vector3.zero, 0f);
			_totalBlockCount++;
			yield return new WaitForSeconds(0.01f);
		}
		_bridge.SetParent(null);
		StartCoroutine(PlayerMoveOnBridge());
	}

	private Vector3 EmptyBridgePosition()
	{
		return (_totalBlockCount % 2) switch
		{
			0 => new Vector3(_bridgeColumn1, 0, _currentBridgeRow += _nextBridgeRow),
			1 => new Vector3(_bridgeColumn2, 0, _currentBridgeRow),
			_ => new Vector3(_bridgeColumn1, 0, _currentBridgeRow += _nextBridgeRow),
		};
	}

	private IEnumerator PlayerMoveOnBridge()
	{
		_playerAnimator.SetTrigger(Run);
		int counter = 0;
		while (counter < _totalBlockCount * 0.5f && !WinCollide.WinTriggered)
		{
			counter++;
			_player.position += _player.forward * _nextBridgeRow;
			yield return new WaitForSeconds(0.01f);
		}

		StartCoroutine(PlayAnimation());
	}

	private IEnumerator PlayAnimation()
	{
		if (WinCollide.WinTriggered)
		{
			_playerAnimator.SetTrigger(Dance);
			_botAnimator.SetTrigger(Dance);
		}
		else
		{
			_playerAnimator.SetTrigger(SadIdle);
			_botAnimator.SetTrigger(SadIdle);
		}

		yield return new WaitForSeconds(3f);

		_levelDone.PopUpLevelDone();
	}

	private void OnDestroy()
	{
		PathFollower.PathEnded -= StartEnding;
	}
}