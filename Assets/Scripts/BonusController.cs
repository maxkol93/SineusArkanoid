using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusPrefabs;
    [SerializeField] private CarriageController carriage;
    [SerializeField] private BallController mainBall;
    [SerializeField] private GameObject bottomWall;

    private void Start()
    {
        GlobalEvents.ScoreAdded += GlobalEvents_ScoreAdded;
    }

    public void ApplyBonus(BonusType bonusType)
    {
        if (bonusType == BonusType.BottomWall)
        {
            bottomWall.SetActive(true);
        }
        else if (bonusType == BonusType.InceraseBallSpeed)
        {
            carriage.ChangeForce(20, 10);
        }
    }

    private void GlobalEvents_ScoreAdded(object sender, ScoreAddedEventArgs e)
    {
        if (Random.Range(0, 5) == 0)
        {
            var bonus = Instantiate(bonusPrefabs[Random.Range(0, bonusPrefabs.Count)], e.Position, Quaternion.identity);
            bonus.GetComponent<Bonus>().controller = this;
        }
    }

    private void OnDestroy()
    {
        GlobalEvents.ScoreAdded -= GlobalEvents_ScoreAdded;
    }
}
