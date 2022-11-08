using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour,IReSetGame
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    private Vector3 originalPoint;

    private void Awake()
    {
        GameManager.Instance.addResetGame(this);
        originalPoint = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed);
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }

    public void ResetNotify()
    {
        transform.position = originalPoint;
    }
}
