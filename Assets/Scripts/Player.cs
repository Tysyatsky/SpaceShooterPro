using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    GameObject _LaserPrefab;
    [SerializeField]
    GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _IsTripleActive = false;

    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            FireLaser();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * Time.deltaTime * _speed);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);
        }
      
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }
    void FireLaser()
    {   
        _canFire = Time.time + _fireRate;
        if(_IsTripleActive)
        {
            Instantiate(_TripleShotPrefab, this.transform.position + new Vector3(0, transform.lossyScale.y - 1,0), Quaternion.identity);
        }
        else
        {
            Instantiate(_LaserPrefab, transform.position + new Vector3(0, transform.lossyScale.y / 2 + _LaserPrefab.transform.lossyScale.y + 0.2f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        Debug.Log("Damage!");
        if (--_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            Debug.Log("You died!");
        }
    }

    public void TripleShotActive()
    {
        _IsTripleActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {   
        yield return new WaitForSeconds(5.0f);
        _IsTripleActive = false;
    }
}
