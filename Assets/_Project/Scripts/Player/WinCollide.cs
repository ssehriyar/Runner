using UnityEngine;

public class WinCollide : MonoBehaviour
{
	public static bool WinTriggered { get; private set; } = false;

	private void OnTriggerEnter(Collider other)
	{
		var win = other.GetComponent<WinTrigger>();
		if (win == null) return;

		WinTriggered = true;
	}
}