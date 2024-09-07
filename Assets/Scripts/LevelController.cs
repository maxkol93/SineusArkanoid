using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int BricksCount {  get; private set; }

    [SerializeField] private Transform bricksParent;
    [SerializeField] private List<GameObject> Levels;

    private BonusController _bonusController;
    private bool _gameOverEnable = true;
    private int _currentLevelIndex = 0;

    private void Start()
    {
        GlobalEvents.BrickDestroy += GlobalEvents_ScoreAdded;
        GlobalEvents.RestartLevel += GlobalEvents_RestartLevel;

        _bonusController = FindAnyObjectByType<BonusController>();
        BricksCount = bricksParent.childCount;
    }

    private void GlobalEvents_RestartLevel(object sender, ResartLevelEventArgs e)
    {
        Destroy(bricksParent.gameObject);

        if (e.NextLevel && _currentLevelIndex < Levels.Count - 1)
        {
            _currentLevelIndex++;
        }

        var bricks = Instantiate(Levels[_currentLevelIndex]);
        bricksParent = bricks.transform;
        _bonusController.RestartLevel(bricksParent);
        BricksCount = bricksParent.childCount;
    }

    private void GlobalEvents_ScoreAdded(object sender, BrickDestroyEventArgs e)
    {
        BricksCount = bricksParent.childCount;
        if (BricksCount == 1 && _gameOverEnable)
        {
            if (_currentLevelIndex == Levels.Count - 1)
                GlobalEvents.OnGameOver(true, true);
            else
                GlobalEvents.OnGameOver(true);
            _gameOverEnable = false;
            StartCoroutine(GameOverDelay());
        }
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _gameOverEnable = true;
    }
}
