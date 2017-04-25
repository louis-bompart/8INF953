using UnityEngine;
using System.Collections;

public class Plateform : Actionnable
{

    // Pointeur vers la case du tableau contenant la position suivante vers laquelle se diriger
    public int pointeurDest;

    // Vitesse de la plateforme
    public int moveSpeed;


    // Si la plateforme est mobile
    public bool isMobile;


    // Animator
    public Animator anim;

    // Tableau contenant les positions des differentes destination de la plateforme
    public Vector3[] tabDestination;
    // Si la plateforme bouge
    public Vector3 nextDestination;

    // RigidBody
    private Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
	public override void Update()
	{
		base.Update ();
		if (isActive)
		{
			Move();
		}

    }

    // Mouvement de la plateforme, si la plateforme a atteint la destination, le pointeur s'incremente
    // et la plateforme continue vers sa prochaine destination

    public void Move()
    {

        rb.MovePosition(Vector3.MoveTowards(transform.position, nextDestination, Time.deltaTime * moveSpeed));
        //rb.AddForce((nextDestination - transform.position) * moveSpeed);
        if (transform.position == nextDestination)
        {
            pointeurDest++;
            if (pointeurDest == tabDestination.Length)
                pointeurDest = 0;
            nextDestination = tabDestination[pointeurDest];
        }
    }

    //Detecte si la plateforme et le player sont en collisions
    // Si c'est le cas, le player et son groundChecker passent en fils de la plateforme
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Feet"))
        {
            //collider.transform.parent.parent = gameObject.transform;
            if (collider.transform.parent.GetComponent<PlayerControl>().isStopped)
            {
                GetComponent<FixedJoint>().connectedBody = collider.transform.parent.GetComponent<Rigidbody>();
            }
            //gameObject.layer = 8;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Feet"))
            if (other.transform.parent.GetComponent<PlayerControl>().isStopped)
                GetComponent<FixedJoint>().connectedBody = other.transform.parent.GetComponent<Rigidbody>();
            else
                GetComponent<FixedJoint>().connectedBody = null;
    }

    // Detecte si la plateforme et le player sortent de collisions
    // Si c'est le cas, player et groundChecker ne sont plus fils de la plateforme
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Feet"))
        {
            GetComponent<FixedJoint>().connectedBody = null;
            //collider.transform.parent.parent = transform.root;
            //gameObject.layer = 10;
        }
    }
}