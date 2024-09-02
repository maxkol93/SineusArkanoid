using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Gradient _gradient = new Gradient { colorKeys = new[] { new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.yellow, 1f) } };

    private int _startHealth;

    private void Start()
    {
        healthLabel.text = health.ToString();
        //_gradient = new Gradient { colorKeys = new[] { new GradientColorKey(Color.red, 0f), new GradientColorKey(Color.yellow, 1f) } };
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
            GlobalEvents.AddScore(transform.position, _startHealth);
            Destroy(gameObject);
        }
    }
}
