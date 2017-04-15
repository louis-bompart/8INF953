using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actionner : MonoBehaviour {

	//isActive correspond à l'activation ou non de l'actionneur
	public bool isActive;

	// Animator de l'objet
	Animator anim;

	// Variables utilisees pour savoir si une plaque est en contact avec un cadavre
	// ou un joueur
	private bool hasPlayer;


	// pour savoir si une plaque est en contact avec un cadavre
	// ou un joueur
	public bool isTriggered{
		get { return (hasPlayer); }
	}

	void Start () {
		anim = GetComponent<Animator>();
	}


	void Update () {

		// Active la plaque si isTriggered est vrai
		if (gameObject.CompareTag ("Plaque")) {
			if (isTriggered) {
				isActive = true;
			} else {
				isActive = false;
			}
			anim.SetBool ("Activated", isActive);
		}

	}

	// Active / Desactive le levier selon si il est Desactive/Active
	public void activate(){

		if (isActive) {
			isActive = false;
		} else {
			isActive = true;

		}
		anim.SetBool("Activated", isActive);
	}

	// On regarde les collisions entre plaque et joueur/cadavre
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag ("Plaque")) {
			hasPlayer = true;
		}
	}

	// On regarde les collisions entre plaque et joueur/cadavre
	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag("Plaque"))
		{
			hasPlayer = true;
		}
	}

	// On regarde si le joueur sort de la plaque
	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag ("Plaque")) {
			hasPlayer = false;
		}

	}

}
