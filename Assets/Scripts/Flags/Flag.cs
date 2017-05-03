using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flag : MonoBehaviour
{

    protected bool toUse;
    protected bool toDestroy;
    public Tile tile;

	private AudioSource source;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

    // Use this for initialization
    void Start()
    {
        toUse = false;
        toDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(toDestroy)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
		source.Play ();
        ActivateFlag(other);
    }

    public void OnTriggerStay(Collider other)
    {
        ActivateFlag(other);
    }

    private void OnTriggerExit(Collider other)
    {
        toUse = false;
    }

    private void OnDestroy()
    {
        toUse = false;
    }

    public override bool Equals(object obj)
    {
        //TODO DUH
        return this.GetType() == obj.GetType();
    }

    public void SetListener(Tile tile)
    {
        this.tile = tile;
    }

    public virtual void ActivateFlag(Collider other)
    {
        if (!other.GetComponent<PlayerControl>().isOnCooldown)
        {
            other.GetComponent<PlayerControl>().SetCooldown(true);
            tile.AddNextFlag();
            toUse = true;
            toDestroy = true;
        }
    }
}
