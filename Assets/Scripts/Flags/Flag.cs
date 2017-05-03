using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flag : MonoBehaviour
{

    protected bool toUse;
    protected bool toDestroy;
	public bool isOnCooldown;
	public bool isEntered = false;
	public float triggeredTime;
	public float coolDownBegin;
	public float coolDownTime = 0.8f;
	public float nextTime = 0.1f;
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
		if (isOnCooldown && coolDownBegin + coolDownTime < Time.time) {
			isOnCooldown = false;
		}
		if (isEntered && triggeredTime + nextTime < Time.time) {
			isEntered = false;
		}
        if(toDestroy)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
		if (!isEntered) {
			source.Play ();
			ActivateFlag (other);
			isEntered = true;
			triggeredTime = Time.time;
		}
    }

    public void OnTriggerStay(Collider other)
    {
       // ActivateFlag(other);
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
        if (!isOnCooldown)
        {
			coolDownBegin = Time.time;
            tile.AddNextFlag();
            toUse = true;
            toDestroy = true;
        }
    }
}
