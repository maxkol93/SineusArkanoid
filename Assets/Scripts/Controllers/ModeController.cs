using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeController : MonoBehaviour
{
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject secondCarriage;
    [SerializeField] private Transform bricksParent;

    private bool _doublesideEnable = false;

    private void Start()
    {
        GameInputController.Mode2Perfomed += GameInputController_Mode2Perfomed;
    }

    private void OnDestroy()
    {
        GameInputController.Mode2Perfomed -= GameInputController_Mode2Perfomed;
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
}
