using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}