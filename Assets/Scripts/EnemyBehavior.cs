using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    float fireCooldown;
    public GameObject laser;
    GameObject player;
    Quaternion targetRotation;
    public float rotSpeed = 1;
    public float moveSpeed = 5;
    bool detectPlayer;
    public GameObject AIPath;
    GameObject currentPointobj;
    Vector3 currentPointVect;
    int checkpointCounter = 0;
	public GameObject myCarrier;

	private GameObject myFighterManager;
    private bool updatingCheckpoint = false;
    bool firing = false;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
		myFighterManager = GameObject.Find ("FighterManager");
        Path path = AIPath.GetComponent<Path>();
        if (path == null)
        {
            Debug.Log("no path has been set");
        }
        currentPointobj = path.childGameObjects[0];
        targetRotation = Quaternion.LookRotation(currentPointobj.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;

        /*if player is in range
            turn to player
            fire at interval
                fire
                start timer
                if timer is done reset
        */
        if (detectPlayer == true)
        {
            targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            if (firing == false)
            {
                StartCoroutine(fire());
            }
        }
        else
        {
            if ((Vector3.Distance(transform.position, currentPointobj.transform.position) < 10) && updatingCheckpoint == false)
            {
                updateCheckpoint();
            }
            else
            {
                targetRotation = Quaternion.LookRotation(currentPointobj.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            }
            
            /*
            if position is not less than x units away to currentPoint
                rotate towards the currentPoint
            
            
            */
        }

    }

    void updateCheckpoint()
    {
        updatingCheckpoint = true;
        Path path = AIPath.GetComponent<Path>();
        if (checkpointCounter < path.childGameObjects.Length -1)
        {
            checkpointCounter++;
        }
        else
        {
            checkpointCounter = 0;
        }
        currentPointobj = path.childGameObjects[checkpointCounter];

        updatingCheckpoint = false;
    }
		
    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag =="Player")
        {
            detectPlayer = true;

        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            detectPlayer = false;

        }
    }

    IEnumerator fire()
    {
        firing = true;
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y, transform.position.z), targetRotation);
        yield return new WaitForSeconds(1);
        firing = false;
    }
		
}
