using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall : MonoBehaviour, IDamageble
{
    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}
