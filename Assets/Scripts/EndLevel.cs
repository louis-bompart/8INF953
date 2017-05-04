using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndLevel : MonoBehaviour {

		public int level;
		public GameObject gameState;
	private AudioSource source;
	public float timeDelay = 0.2f;
	private WaitForSeconds waitDelay;

		public void Awake(){
			gameState = GameObject.Find("GameState");
		waitDelay = new WaitForSeconds (timeDelay); 
		source = GetComponent<AudioSource> ();
		}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			source.Play ();
			StartCoroutine (win ()); 

		}
	}

	private IEnumerator win()
	{
		yield return waitDelay; 
		GameState gs = gameState.GetComponent<GameState>();
		gs.levelSucceed [level] = true;
		SceneManager.LoadScene ("MainMenu");
	}

}
