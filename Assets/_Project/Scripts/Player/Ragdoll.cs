using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Collider[] _ragdollColliders;
	private Rigidbody[] _ragdollRigidbodys;

	private void Awake()
	{
		_ragdollColliders = GetComponentsInChildren<Collider>();
		_ragdollRigidbodys = GetComponentsInChildren<Rigidbody>();

		foreach (var collider in _ragdollColliders)
		{
			collider.enabled = false;
		}

		foreach (var rb in _ragdollRigidbodys)
		{
			rb.isKinematic = true;
		}
	}

	public void OpenRagdoll()
	{
		foreach (var collider in _ragdollColliders)
		{
			collider.enabled = true;
		}

		foreach (var rb in _ragdollRigidbodys)
		{
			rb.isKinematic = false;
		}
	}
}