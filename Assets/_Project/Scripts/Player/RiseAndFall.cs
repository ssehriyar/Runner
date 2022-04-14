using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class RiseAndFall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Rigidbody _playerRigidbody;
	[SerializeField] private StackManager _stackManager;
	[SerializeField] private Animator _animator;
	[SerializeField] private GroundCheck _groundCheck;

	private readonly int Falling = Animator.StringToHash("Falling");
	private readonly int Exit = Animator.StringToHash("Exit");

	private bool _pointerDown = false;

	private void Start()
	{
		PathFollower.PathEnded += PathEnded;
	}

	private void PathEnded()
	{
		// To disable pointer events disable image
		GetComponent<Image>().enabled = false;
		_pointerDown = false;
		StopCoroutine(Up());
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pointerDown = true;
		StartCoroutine(Up());
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pointerDown = false;
	}

	private IEnumerator Up()
	{
		while (_pointerDown && !_stackManager.IsStackEmpty)
		{
			_playerRigidbody.velocity = Vector3.up * 5;
			yield return new WaitForSeconds(0.1f);
			_stackManager.PopBlock();
		}
		_pointerDown = false;
		StartCoroutine(Fall());
	}

	public IEnumerator Fall()
	{
		while (!_pointerDown && !_groundCheck.IsGrounded)
		{
			_animator.SetTrigger(Falling);
			yield return new WaitForEndOfFrame();
		}
		_animator.SetTrigger(Exit);
	}

	private void OnDestroy()
	{
		PathFollower.PathEnded -= PathEnded;
	}
}