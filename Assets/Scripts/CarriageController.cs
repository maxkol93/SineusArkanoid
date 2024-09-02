using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    [SerializeField] private Transform forcePivot;

    void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ball = collision.gameObject;
        var rb = ball.GetComponent<Rigidbody2D>();
        //var ballPos2d = new Vector2(ball.transform.position.x, ball.transform.position.y);
        //var pivotPos2d = new Vector2(forcePivot.position.x, forcePivot.position.y);
        //var vector = (ballPos2d - pivotPos2d);
        //vector.Normalize();
        rb.velocity = Vector2.zero;
        rb.AddForce((ball.transform.position - forcePivot.position).normalized * 10, ForceMode2D.Impulse);
    }
}
