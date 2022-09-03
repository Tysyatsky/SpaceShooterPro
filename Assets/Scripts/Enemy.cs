using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    private float _minXPos = -6.0f;
    private float _maxXPos = 6.0f;

    void Start()
    {
        transform.position = new Vector3(0, 10, 0);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -Time.deltaTime * _speed, 0));
        if (transform.position.y < -10f)
        {
            float randomX = Random.Range(_minXPos, _maxXPos);
            transform.position = new Vector3(randomX ,10, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage(); 
        }
        else if (other.tag == "Laser") Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
