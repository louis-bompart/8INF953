using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndLevel : MonoBehaviour {

		public int level;
		public GameObject gameState;

		public void Awake(){
			gameState = GameObject.Find("GameState");
		}

	public void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GameState gs = gameState.GetComponent<GameState>();
			gs.levelSucceed [level] = true;
			SceneManager.LoadScene ("MainMenu");
		}
	}

}
