using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling_platform : MonoBehaviour, IReSetGame
{

    [SerializeField] float delay = 0.8f;
    private Vector3 originalPoint;

    Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPoint = transform.position;
        GameManager.Instance.addResetGame(this);
    }



    IEnumerator fall()
    {
        yield return new WaitForSeconds(delay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(fall());
    }

    public void ResetNotify()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = originalPoint;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(true);
    }
}
