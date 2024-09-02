using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    [SerializeField] private Transform forcePivot;
    [SerializeField] private SpriteRenderer forceUpSprite;

    private float _startForce = 10;
    private float _force = 10;

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void ChangeForce(float force, float duration)
    {
        forceUpSprite.enabled = true;
        _force = force;
        StartCoroutine(BoostForce(duration));
    }

    private IEnumerator BoostForce(float duration)
    {
        yield return new WaitForSeconds(duration);
        _force = _startForce;
        forceUpSprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Ball")
        {
            var rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce((obj.transform.position - forcePivot.position).normalized * _force, ForceMode2D.Impulse);
        }
        else if (obj.tag == "Bonus")
        {
            var bonus = obj.GetComponent<Bonus>();
            bonus.controller.ApplyBonus(bonus.BonusType);
            Destroy(obj);
        }
    }
}
