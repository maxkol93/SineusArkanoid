using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    [SerializeField] private Transform forcePivot;
    [SerializeField] private SpriteRenderer forceUpSprite;

    private float _startForce = 10;
    private float _force = 10;
    private bool _magnetEnable;

    private void Start()
    {
        GlobalEvents.UpdateNormalTime += GlobalEvents_UpdateNormalTime;
    }

    private void GlobalEvents_UpdateNormalTime(object sender, System.EventArgs e)
    {
        //var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = Camera.main.ScreenToWorldPoint(GameInputController.ScreenMousePos);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    private void Update()
    {
        //var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //pos.y = transform.position.y;
        //pos.z = transform.position.z;
        //transform.position = pos;
    }

    public void ChangeForce(float force, float duration)
    {
        forceUpSprite.enabled = true;
        _force = force;
        StartCoroutine(BoostForceTimer(duration));
    }

    private IEnumerator BoostForceTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _force = _startForce;
        forceUpSprite.enabled = false;
    }

    public void MagnetBonusApply(float duration = 10)
    {
        _magnetEnable = true;
        StartCoroutine(MagnetTimer(duration));
    }

    private IEnumerator MagnetTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _magnetEnable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Ball")
        {
            var ball = obj.GetComponent<Ball>();
            if (_magnetEnable)
            {
                ball.PlaceOnCarriage(transform);
            }
            else
            {
                ball.Bounce(_force);
            }
        }
        else if (obj.tag == "Bonus")
        {
            var bonus = obj.GetComponent<Bonus>();
            bonus.controller.ApplyBonus(bonus.BonusType);
            Destroy(obj);
        }
    }

    private void OnDestroy()
    {
        GlobalEvents.UpdateNormalTime -= GlobalEvents_UpdateNormalTime;
    }
}
