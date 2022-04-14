using UnityEngine;

public class Block : MonoBehaviour
{
	private BoxCollider _boxCollider;

	private void Start() => _boxCollider = GetComponent<BoxCollider>();

	public void ChangeCollide(bool b) => _boxCollider.enabled = b;
}
