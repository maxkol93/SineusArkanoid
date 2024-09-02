using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour, IDamageble
{
    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private bool givesPoints;

    private int _startHealth;

    private void Start()
    {
        healthLabel.text = health.ToString();
        sprite.color = _gradient.Evaluate(health / 10f);
        _startHealth = health;
    }

    public void TakeDamage()
    {
        if (health > 1)
        {
            health--;
            healthLabel.text = health.ToString();
            sprite.color = _gradient.Evaluate(health / 10f);
        }
        else
        {
            if (givesPoints) GlobalEvents.AddScore(transform.position, _startHealth);
            Destroy(gameObject);
        }
    }
}
