using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    private Tile tile;

    private void GetTitleAtMyPos()
    {
        int x = (int)transform.parent.position.z;
        int y = (int)transform.parent.position.y;
        //Debug.Log("My pos is: " + x + ";" + y);
        Tile tile = MapSaveState.current.tiles[x, y].GetTile();
        if (this.tile != null)
            if (this.tile.id != tile.id)
                this.tile.data.hasCorpse = false;
        this.tile = tile;
        this.tile.data.hasCorpse = true;
    }

    public virtual void OnDisable()
    {
        if (this.tile != null)
            this.tile.data.hasCorpse = false;
    }

    public virtual void Update()
    {
        //	Debug.Log (isActive);
        GetTitleAtMyPos();
        //anim.SetBool("Activated", isActive);
    }

}
