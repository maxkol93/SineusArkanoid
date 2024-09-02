using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform carriage;
    [SerializeField] private Transform ballPivot;
    [SerializeField] private int ballsLeft = 3;

    private Rigidbody2D _rb;
    private bool isFlight = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.SetParent(carriage);
    }

    void Update()
    {
        if (!isFlight && Input.GetKey(KeyCode.Mouse0))
        {
            isFlight = true;
            transform.SetParent(null);
            _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject.GetComponent<IDamageble>();
        if (obj != null) obj.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "OutTrigger")
        {
            ballsLeft--;
            if (ballsLeft == 0)
            {
                GlobalEvents.OnGameOver();
            }

            isFlight = false;
            GlobalEvents.BallsLeft(ballsLeft);
            _rb.velocity = Vector2.zero;
            transform.position = ballPivot.position;
            transform.SetParent(carriage);
        }
    }
}
