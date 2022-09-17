using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _EnemyPrefab;
    [SerializeField]
    GameObject _EnemyContainer;
    [SerializeField]
    float _waitTime;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine(_waitTime));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // spawn gameobject
    // Create a coroutine of type IEnumerator -- Vield Events
    // while loop

    IEnumerator SpawnRoutine(float waitTime)
    {
        // while loop (infinite loops)
            // Instantiate an object
            // yield wait for 5 seconds

        while(!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newEnemy = Instantiate(_EnemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
