using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject secondCarriage;
    [SerializeField] private Transform bricksParent;

    private bool _bricksKinematic = true;
    private bool _doublesideEnable = false;

    private void Start()
    {
        GameInputController.Mode2Perfomed += GameInputController_Mode2Perfomed;

        //GameInputController.Mode1Perfomed += GameInputController_Mode1or2Perfomed;
        //GameInputController.Mode2Perfomed += GameInputController_Mode1or2Perfomed;
        GameInputController.Mode3Perfomed += GameInputController_Mode3Perfomed;
        //GameInputController.Mode3Perfomed += GameInputController_Mode3Perfomed;

        //GlobalEvents.RestartLevel += GlobalEvents_RestartLevel;
    }

    private void GameInputController_Mode3Perfomed(object sender, System.EventArgs e)
    {
        _bricksKinematic = false;
        foreach (Transform brick in bricksParent)
        {
            var rb = brick.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            rb.isKinematic = false;
        }
        StartCoroutine(KinematicTimer());
    }

    private IEnumerator KinematicTimer()
    {
        yield return new WaitForSeconds(5);
        _bricksKinematic = true;
        foreach (Transform brick in bricksParent)
        {
            var rb = brick.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            if (rb == null) continue;
            rb.isKinematic = true;
        }

    }

    private void GameInputController_Mode2Perfomed(object sender, System.EventArgs e)
    {
        _doublesideEnable = !_doublesideEnable;
        if (_doublesideEnable)
        {
            topWall.SetActive(false);
            secondCarriage.SetActive(true);
        }
        else
        {
            topWall.SetActive(true);
            secondCarriage.SetActive(false);
        }
    }

    //private void GlobalEvents_RestartLevel(object sender, System.EventArgs e)
    //{
    //    bricksParent = FindObjectOfType<LevelController>().BricksParent;
    //}

    //private void SwitchBricksKinematic(object sender, System.EventArgs e)
    //{
    //    _bricksKinematic = !_bricksKinematic;
    //    foreach (Transform brick in bricksParent)
    //    {
    //        var rb = brick.GetComponent<Rigidbody2D>();
    //        rb.velocity = Vector3.zero;
    //        rb.angularVelocity = 0;
    //        if (rb == null) continue;
    //        if (_bricksKinematic) rb.isKinematic = true;
    //        else rb.isKinematic = false;
    //    }
    //}
}
