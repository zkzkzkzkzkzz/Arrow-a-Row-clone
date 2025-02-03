using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float resetPos = -35f;
    [SerializeField] private float startPos = 35f;

    void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;

        if (transform.position.z <= resetPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPos);
        }
    }
}
