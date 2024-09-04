using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Ball : MonoBehaviour
{
    //[SerializeField] private Transform carriage;
    public Transform forcePivot;
    public BallsControler ballsControler;
    //[SerializeField] private Transform ballPivot;
    //[SerializeField] private int ballsLeft = 3;

    private Rigidbody2D _rb;
    private bool _isFlight;
    private float _ballPivotY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ballPivotY = transform.position.y;
        //PlaceOnCarriage();
    }

    void Update()
    {
        if (!_isFlight && Input.GetKey(KeyCode.Mouse0))
        {
            _isFlight = true;
            transform.SetParent(null);
            Bounce(10);
        }
    }

    public void PlaceOnCarriage(Transform carriage)
    {
        _rb.velocity = Vector2.zero;
        _isFlight = false;
        transform.position = new Vector3(transform.position.x, _ballPivotY, transform.position.z);
        transform.SetParent(carriage);
    }

    public void Bounce(float force)
    {
        if (!_isFlight) return;
        _rb.velocity = Vector2.zero;
        _rb.AddForce((transform.position - forcePivot.position).normalized * force, ForceMode2D.Impulse);
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
            ballsControler.BallOutside(this);
        }
    }
}
