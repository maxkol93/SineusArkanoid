using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public BonusController Controller { get; set; }
    public BonusType BonusType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.tag == "OutTrigger")
        {
            Destroy(gameObject);
        }
    }
}
