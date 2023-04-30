using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefab;
    [SerializeField] GameObject moneyPrefab;

    private Vector3 spawnPosition = new Vector3(25, 0, 0);
    //private float startDelay = 2f;
    private float repeateRate = 2f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //InvokeRepeating(nameof(SpawnObstacle), startDelay, repeateRate / playerControllerScript.GetComponent<Animator>().speed);
        StartCoroutine(SpawnJumpingObstacles());
        StartCoroutine(SpawnMoney());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver == false)
        {
            int prefabSize = obstaclePrefab.Length;
            int i = (int)(Random.value * prefabSize);

            Instantiate(obstaclePrefab[i], spawnPosition, obstaclePrefab[i].transform.rotation);
            
            //Debug.Log("Obstacle to spawn: "+ i+" obstacle name: " + obstaclePrefab[i].name);
        }
    }

    IEnumerator SpawnJumpingObstacles() {
        while (!playerControllerScript.gameOver) {
            float test = repeateRate / playerControllerScript.GetComponent<Animator>().speed;
            if (playerControllerScript.gameOver == false) {
                int prefabSize = obstaclePrefab.Length;
                int i = (int)(Random.value * prefabSize);

                Instantiate(obstaclePrefab[i], spawnPosition, obstaclePrefab[i].transform.rotation);
            }

            yield return new WaitForSeconds(test);
        }
    }

    IEnumerator SpawnMoney() {
        while (!playerControllerScript.gameOver) {
            Instantiate(moneyPrefab, new Vector3(spawnPosition.x, Random.Range(4, 8), 0), moneyPrefab.transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
