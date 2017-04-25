using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actionnable : MonoBehaviour {

	public Actionner actionneur;

	// Si la plateforme bouge alors que l'activateur est desactive
	public bool isActiveByDefault;

	// Si la plateforme bouge
	public bool isActive;

	// Si la plateforme est activable
	public bool isActivable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
		// Si la plateforme est activable :
		// Elle bouge ou non selon la position de l'activateur
		if (isActivable)
		{
			if (actionneur.isActive)
				isActive = !isActiveByDefault;
			else
				isActive = isActiveByDefault;
			//if (!isMovingAlone)
			////{
			//{
			//    isMoving = true;
			//}
			//if (!actionneur.isActive)
			//{
			//    isMoving = false;
			//}
			//}
			//}
		}
		else
		{
			isActive = isActiveByDefault;
			//if (actionneur.isActive && isMoving)
			//{
			//    isMoving = false;
			//}
			//if (!actionneur.isActive && !isMoving)
			//{
			//    isMoving = true;
			//}
		}

	}
}
