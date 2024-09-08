using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] private GameObject addScoreEddectPrefab;
    [SerializeField] private GameObject destroyEffectPrefab;

    private ObjectPool _addScoreEffectPool;
    private ObjectPool _destroyEffectPool;

    private void Start()
    {
        GlobalEvents.BrickDestroy += GlobalEvents_BrickDestroy;

        _addScoreEffectPool = new ObjectPool(addScoreEddectPrefab);
        _destroyEffectPool = new ObjectPool(destroyEffectPrefab);
    }

    private void OnDestroy()
    {
        GlobalEvents.BrickDestroy -= GlobalEvents_BrickDestroy;
    }

    private void GlobalEvents_BrickDestroy(object sender, BrickDestroyEventArgs e)
    {
        if (e.IsLastBrick) return;

        var scoreEffect = _addScoreEffectPool.GetObject();
        scoreEffect.transform.position = e.Position;
        scoreEffect.GetComponentInChildren<TMP_Text>().text = $"+{e.ScoreValue}";
        var dur = scoreEffect.GetComponentInChildren<Animation>().clip.averageDuration;
        StartCoroutine(AddToPoolWithDelay(_addScoreEffectPool, scoreEffect, dur));

        var destroyEffect = _destroyEffectPool.GetObject();
        destroyEffect.transform.position = e.Position;
        var dur2 = destroyEffect.GetComponentInChildren<ParticleSystem>().main.duration + 0.2f;
        StartCoroutine(AddToPoolWithDelay(_destroyEffectPool, destroyEffect, dur2));
    }

    private IEnumerator AddToPoolWithDelay(ObjectPool pool, GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj) pool.AddObject(obj);
    }
}
