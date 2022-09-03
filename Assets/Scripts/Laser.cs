using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private static float _speed = 8.0f;

    void Update()
    {
        transform.Translate(new Vector3(0, Time.deltaTime * _speed, 0));
        if (transform.position.y >= 8.0f) Destroy(gameObject);
    }
}
