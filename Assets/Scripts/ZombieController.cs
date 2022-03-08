using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private Transform playerTransform;
    public float speed;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float step =  speed * Time.deltaTime;
        Vector3 target = playerTransform.position;
        target.y = 0;
        Vector3 moveTowards = Vector3.MoveTowards(transform.position, target, step);
        transform.position = moveTowards;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target-transform.position, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("Car")) {
            Destroy(gameObject);
        }
    }
}
