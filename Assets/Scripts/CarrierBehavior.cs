using UnityEngine;
using System.Collections;

public class CarrierBehavior : MonoBehaviour {

    public GameObject ship;
    public GameObject[] pathsArray;
    public GameObject[] shipsArray;

	//public GameObject fighterManger;
    private bool canSpawn = true;
    public int maxShips = 5;
    public int shipCounter = 0;
    public float health = 100;
	public float spawnedShipMoveSpeed = 50;
	public float spawnedShipTurnSpeed = 3;
	public float spawnedShipHealthRewardAmount = 5;
    GameObject manager;

	// Use this for initialization
	void Start () {
		//fighterManger = GameObject.Find ("FighterManager");
        shipsArray = new GameObject[maxShips];
        StartCoroutine(spawnShip());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator spawnShip()
    {        

        if (shipCounter < maxShips)
        {
            shipsArray[shipCounter] = Instantiate(ship, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            EnemyBehavior enemyBehavior = shipsArray[shipCounter].GetComponent<EnemyBehavior>();
            int randomPath = Random.Range(0, (pathsArray.Length - 1));
            enemyBehavior.AIPath = pathsArray[randomPath];
			//Apply fighter move and turn speed
			shipsArray[shipCounter].GetComponent<EnemyBehavior>().moveSpeed = spawnedShipMoveSpeed;
			shipsArray [shipCounter].GetComponent<EnemyBehavior> ().rotSpeed = spawnedShipTurnSpeed;
			shipsArray [shipCounter].GetComponent<EnemyHealthBehavior> ().playerHealthRewardAmount = spawnedShipHealthRewardAmount;
            shipCounter++;

        }
        else 
        {
            for (int i = 0; i < maxShips; i++)
            {
                if (shipsArray[i] == null)
                {
                    shipsArray[i] = Instantiate(ship, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
                    EnemyBehavior enemyBehavior = shipsArray[i].GetComponent<EnemyBehavior>();
					shipsArray [i].GetComponent<EnemyBehavior> ().moveSpeed = spawnedShipMoveSpeed;
					shipsArray [i].GetComponent<EnemyBehavior> ().rotSpeed = spawnedShipTurnSpeed;
					shipsArray [i].GetComponent<EnemyHealthBehavior> ().playerHealthRewardAmount = spawnedShipHealthRewardAmount;
                    int randomPath = Random.Range(0, (pathsArray.Length - 1));
                    enemyBehavior.AIPath = pathsArray[randomPath];
                    break;
                }

            }
        }
        
        yield return new WaitForSeconds(20);
        StartCoroutine(spawnShip());
    }
}
