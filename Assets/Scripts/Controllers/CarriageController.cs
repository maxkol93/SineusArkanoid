using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour
{
    [SerializeField] private Transform forcePivot;
    [SerializeField] private Transform ballPivot;
    [SerializeField] private GameObject magnetBonusGraphic;

    private bool _magnetEnable;

    private void Start()
    {
        GlobalEvents.UpdateNormalTime += GlobalEvents_UpdateNormalTime;
    }

    private void OnEnable()
    {
        _magnetEnable = false;
        magnetBonusGraphic.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "Ball")
        {
            var ball = obj.GetComponent<Ball>();
            if (_magnetEnable)
            {
                ball.ForcePivot = forcePivot;
                ball.PlaceOnCarriage(transform, ballPivot.position.y);
            }
            else
            {
                ball.ForcePivot = forcePivot;
                ball.Bounce();
            }
        }
        else if (obj.tag == "Bonus")
        {
            var bonus = obj.GetComponent<Bonus>();
            bonus.Controller.ApplyBonus(bonus.BonusType);
            Destroy(obj);
        }
    }

    private void OnDestroy()
    {
        GlobalEvents.UpdateNormalTime -= GlobalEvents_UpdateNormalTime;
    }

    private void GlobalEvents_UpdateNormalTime(object sender, System.EventArgs e)
    {
        var pos = Camera.main.ScreenToWorldPoint(GameInputController.ScreenMousePos);
        pos.y = transform.position.y;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void MagnetBonusApply(float duration = 10)
    {
        if (gameObject.activeInHierarchy)
        {
            _magnetEnable = true;
            magnetBonusGraphic.SetActive(true);
            StartCoroutine(MagnetTimer(duration));
        }
    }

    private IEnumerator MagnetTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        _magnetEnable = false;
        magnetBonusGraphic.SetActive(false);
    }
}
