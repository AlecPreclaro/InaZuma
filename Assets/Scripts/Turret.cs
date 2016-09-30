using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    bool detectPlayer = false;
    bool firing = false;
    Quaternion targetRotation;
    GameObject player;
    float rotSpeed = 1;
    public GameObject laser;
	
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update ()
    {
        if (detectPlayer == true)
        {
            targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            if (firing == false)
            {
                StartCoroutine(fire());
            }
        }
    }

    IEnumerator fire()
    {
        firing = true;
        Instantiate(laser, new Vector3(transform.position.x, transform.position.y, transform.position.z), targetRotation);
        yield return new WaitForSeconds(1);
        firing = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            detectPlayer = true;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            detectPlayer = false;

        }
    }
}
