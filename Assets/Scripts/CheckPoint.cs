using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckPointData {

}

public class CheckPoint : MonoBehaviour
{
	public bool isInit;
	public CheckPointData data;

	public void Start(){
		if (isInit) {
			MapSaveState.current.serializable.initCheck = data;
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.tag == "player") {
			MapSaveState.current.serializable.finalCheck = data;
		}
	}
}


