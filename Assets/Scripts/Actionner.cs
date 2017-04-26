using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Actionner : MonoBehaviour
{

    //isActive correspond à l'activation ou non de l'actionneur
    public bool isActive;
	private Tile tile;

    // Animator de l'objet
    protected Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	private void GetTitleAtMyPos() {
		int x = (int)transform.parent.position.z;
		int y = (int)transform.parent.position.x;
		Debug.Log ("My pos is: " + x + ";" + y);
		Tile tile = MapSaveState.current.tiles [x, y].GetTile();
		if (this.tile != null)
		if (this.tile.id != tile.id)
			this.tile.data.hasActionner=false;
		this.tile = tile;
		this.tile.data.hasActionner = true;
	}

	public virtual void OnDisable() {
		if (this.tile != null)
			this.tile.data.actionnerValue = isActive;
	}

    public virtual void Update()
    {
	//	Debug.Log (isActive);
		GetTitleAtMyPos();
		if (tile != null) {
			tile.data.ActionnerValue = isActive;
		}
        //anim.SetBool("Activated", isActive);
    }

}