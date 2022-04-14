using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDone : MonoBehaviour
{
	private void Start()
	{
		gameObject.SetActive(false);
	}

	public void PopUpLevelDone()
	{
		gameObject.SetActive(true);
	}

	public void NextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}