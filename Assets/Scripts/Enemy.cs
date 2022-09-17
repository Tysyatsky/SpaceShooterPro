using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject laserEnemyPrefab;

    private float _minXPos = -6.0f;
    private float _maxXPos = 6.0f;

    private float _fireRate = 3.0f;
    private float _canfire = -1.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(new Vector3(0, -Time.deltaTime * _speed, 0));
        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(_minXPos, _maxXPos);
            transform.position = new Vector3(randomX, 10, 0);
        }

        if (Time.time > _canfire)
        {
            _canfire = Time.time + _fireRate;
            Instantiate(laserEnemyPrefab, transform.position
                + new Vector3(0, laserEnemyPrefab.transform.lossyScale.y - transform.lossyScale.y / 2 - 1f, 0),
                Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        else if (other.tag == "Laser") Destroy(other.gameObject);
        if (other.tag != "Enemy")
            Destroy(this.gameObject);
    }
}
