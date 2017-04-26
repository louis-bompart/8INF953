using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckPointData {
	int id;
	public CheckPointData(CheckPoint checkpoint){
		this.id = checkpoint.id;
	}

	public CheckPoint GetCheckPoint() {
		return CheckPoint.mappedCP [id];
	}
}

public class CheckPoint : MonoBehaviour
{
	public bool isInit;
	public CheckPointData data;
	public static Dictionary<int, CheckPoint> mappedCP;
	private static IDManager idManager;
	internal int id;


	public void Awake(){
		if (idManager == null)
			idManager = new IDManager();
		if (mappedCP == null)
			mappedCP = new Dictionary<int, CheckPoint> ();
		this.id = idManager.GetNewID ();
		if (isInit) {
			MapSaveState.current.serializable.initCheck = data;
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			MapSaveState.current.serializable.finalCheck = data;
		}
	}
}


