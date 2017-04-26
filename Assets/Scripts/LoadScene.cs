using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public int level;
	public GameObject gameState;
	public void LoadOnClick (string scene){
		SceneManager.LoadScene (scene);
	}

	public void Quit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif

	}

	public void Awake(){
		gameState = GameObject.Find("GameState");
		GameState gs = gameState.GetComponent<GameState>();
		if (gs.levelSucceed[level]){
			this.gameObject.GetComponent<Image> ().color = Color.green;
		}
	}
}