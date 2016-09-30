using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    public GameObject[] childGameObjects;
	// Use this for initialization
	void Awake ()
    {
        childGameObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childGameObjects[i] = transform.GetChild(i).gameObject;

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
