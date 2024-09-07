using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusPrefabs;
    [SerializeField] private List<CarriageController> carriages;
    [SerializeField] private BallsControler ballsControler;
    [SerializeField] private GameObject bottomWalls;
    [SerializeField] private Transform bricksParent;

    private bool _bricksKinematic = true;
    private List<GameObject> _instances = new List<GameObject>();

    private void Start()
    {
        GlobalEvents.BrickDestroy += GlobalEvents_BrickDestroy;
        GlobalEvents.GameOver += GlobalEvents_GameOver;
    }

    private void OnDestroy()
    {
        GlobalEvents.BrickDestroy -= GlobalEvents_BrickDestroy;
        GlobalEvents.GameOver -= GlobalEvents_GameOver;
        //GlobalEvents.RestartLevel -= GlobalEvents_RestartLevel;
    }

    private void GlobalEvents_GameOver(object sender, GameOverEventArgs e)
    {
        for (int i = _instances.Count - 1; i >= 0; i--)
        {
            Destroy(_instances[i]);
        }
        _instances.Clear();

        foreach (Transform wall in bottomWalls.transform) wall.gameObject.SetActive(false);
        ballsControler.ChangeForce(8, 1);
    }

    public void ApplyBonus(BonusType bonusType)
    {
        if (bonusType == BonusType.BottomWall)
        {
            foreach (Transform wall in bottomWalls.transform)
            {
                if (!wall.gameObject.activeInHierarchy)
                {
                    wall.gameObject.SetActive(true);
                    break;
                }
            }
        }
        else if (bonusType == BonusType.InceraseBallSpeed)
        {
            ballsControler.ChangeForce(12, 10);
        }
        else if (bonusType == BonusType.Magnet)
        {
            foreach (var carriage in carriages) carriage.MagnetBonusApply();
        }
        else if (bonusType == BonusType.AddBall)
        {
            ballsControler.AddBall();
        }
        else if (bonusType == BonusType.BricksKinematic)
        {
            BricksKinematicRun();
        }
    }

    private void GlobalEvents_BrickDestroy(object sender, BrickDestroyEventArgs e)
    {
        if (e.IsLastBrick) return;

        if (Random.Range(0, 3) == 0)
        {
            var bonus = Instantiate(bonusPrefabs[Random.Range(0, bonusPrefabs.Count)], e.Position, Quaternion.identity);
            bonus.GetComponent<Bonus>().controller = this;
            _instances.Add(bonus);
        }
    }

    private void BricksKinematicRun()
    {
        _bricksKinematic = false;
        foreach (Transform brick in bricksParent)
        {
            if (brick.GetComponent<Brick>().KinematicEnable)
            {
                var rb = brick.GetComponent<Rigidbody2D>();
                if (rb == null) continue;
                rb.isKinematic = false;
            }
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
            if (rb == null) continue;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = 0;
            //rb.isKinematic = true;
            rb.bodyType = RigidbodyType2D.Static;
        }

    }

    public void RestartLevel(Transform bricks)
    {
        bricksParent = bricks;
    }
}
