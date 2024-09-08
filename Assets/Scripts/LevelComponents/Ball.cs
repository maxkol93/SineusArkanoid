using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Ball : MonoBehaviour
{
    public Transform ForcePivot { get; set; }
    public BallsControler BallsControler {  get; set; }

    [SerializeField] private SpriteRenderer forceUpSprite;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject trailBoost;
    [SerializeField] private AudioSource bounceSound;
    [SerializeField] private AudioSource outSound;

    private Rigidbody2D _rb;
    private bool _isFlight;
    private float _ballPivotY;
    private float _startForce = 8;
    private float _force = 8;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ballPivotY = transform.position.y;
        trailBoost.SetActive(false);

        GlobalEvents.UpdateNormalTime += GlobalEvents_UpdateNormalTime;
        GameInputController.MouseLeftClick += GameInputController_MouseLeftClick;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounceSound.Play();
        var obj = collision.gameObject.GetComponent<IDamageble>();
        if (obj != null) obj.TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "OutTrigger")
        {
            outSound.Play();
            BallsControler.BallOutside(this);
        }
    }

    private void OnDestroy()
    {
        GlobalEvents.UpdateNormalTime -= GlobalEvents_UpdateNormalTime;
        GameInputController.MouseLeftClick -= GameInputController_MouseLeftClick;
    }

    private void GameInputController_MouseLeftClick(object sender, System.EventArgs e)
    {
        if (!_isFlight)
        {
            _isFlight = true;
            transform.SetParent(null);
            Bounce();
        }
    }

    private void GlobalEvents_UpdateNormalTime(object sender, System.EventArgs e)
    {
        if (_isFlight)
        {
            var velocity = _rb.velocity;
            if (Vector2.Angle(velocity, Vector2.left) < 1)
            {
                _rb.velocity += Vector2.down;
            }
            if (velocity.sqrMagnitude < _force * _force - 20)
            {
                _rb.velocity = _rb.velocity.normalized * _force;
            }
        }
    }


    public void PlaceOnCarriage(Transform carriage, float pivotY = 0)
    {
        _rb.velocity = Vector2.zero;
        _isFlight = false;
        transform.position = new Vector3(transform.position.x, pivotY == 0 ? _ballPivotY : pivotY, transform.position.z);
        transform.SetParent(carriage);
    }

    public void Bounce()
    {
        bounceSound.Play();
        if (!_isFlight) return;
        _rb.velocity = Vector2.zero;
        _rb.AddForce((transform.position - ForcePivot.position).normalized * _force, ForceMode2D.Impulse);
    }

    public void ChangeForce(float force, float duration)
    {
        forceUpSprite.enabled = true;
        trail.SetActive(false);
        trailBoost.SetActive(true);
        _force = force;
        StartCoroutine(BoostForceTimer(duration));
    }

    private IEnumerator BoostForceTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _force = _startForce;
        trail.SetActive(true);
        trailBoost.SetActive(false);
        forceUpSprite.enabled = false;
    }
}
