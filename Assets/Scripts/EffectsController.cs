using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private GameObject addScoreEddectPrefab;
    [SerializeField] private GameObject destroyEffectPrefab;

    private void Start()
    {
        GlobalEvents.ScoreAdded += GlobalEvents_ScoreAdded;
    }

    private void GlobalEvents_ScoreAdded(object sender, ScoreAddedEventArgs e)
    {
        var scoreEffect = Instantiate(addScoreEddectPrefab, e.Position, Quaternion.identity);
        scoreEffect.GetComponentInChildren<TMP_Text>().text = $"+{e.Value}";
        Instantiate(destroyEffectPrefab, e.Position, Quaternion.identity);
    }
}
