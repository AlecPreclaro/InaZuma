using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    public float speed = 50f;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		transform.LookAt (player.transform.position);
        StartCoroutine(destroy());
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);

    }
}
