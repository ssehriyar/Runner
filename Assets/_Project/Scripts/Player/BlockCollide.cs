using UnityEngine;

public class BlockCollide : MonoBehaviour
{
	[SerializeField] private StackManager _stackManager;

	private void OnTriggerEnter(Collider other)
	{
		var block = other.GetComponent<Block>();
		if (block == null) return;

		_stackManager.PushBlock(block);
	}
}