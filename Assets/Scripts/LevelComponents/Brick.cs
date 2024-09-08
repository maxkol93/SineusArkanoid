using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour, IDamageble
{
    public bool KinematicEnable { get; private set; } = true;

    [SerializeField] private int health;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private List<Sprite> textures;
    [SerializeField] private TMP_Text healthLabel;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private bool givesPoints;
    [SerializeField] private bool replaceSprite = true;
    [SerializeField] private AudioSource destroySound;

    private LevelController _levelController;
    private int _startHealth;

    private void Start()
    {
        healthLabel.text = health.ToString();
        if (replaceSprite) sprite.sprite = textures[Random.Range(0, textures.Count)];
        sprite.sortingOrder = Random.Range(-10, 0);
        sprite.color = _gradient.Evaluate(Mathf.Min(1f, health / 6f));
        _startHealth = health;
        _levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BricksBottomTrigger"))
        {
            KinematicEnable = false;
            var rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
        }
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
            destroySound.transform.SetParent(null);
            destroySound.Play();
            var isLast = _levelController.BricksCount == 1;
            if (givesPoints) GlobalEvents.OnBrickDestroy(transform.position, _startHealth, isLast);
            Destroy(gameObject);
        }
    }
}
