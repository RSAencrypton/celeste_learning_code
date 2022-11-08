using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    private float nextRunTime;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            nextRunTime = Time.time + waitTime;

            switchToPoint();
        }
    }

    private void FixedUpdate()
    {
        if (Time.time < nextRunTime)
            return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed);


    }


    void switchToPoint()
    {
        target = Vector2.Distance(transform.position, pointA.position) >=
            Vector2.Distance(transform.position, pointB.position) ? pointA : pointB;
    }

}
