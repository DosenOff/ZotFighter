using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject PlayerHealthBar;
    public GameObject EnemyHealthBar;

    RectTransform playerHealthBarRect;
    RectTransform enemyHealthBarRect;

    // updates player health bar with new health
    public void UpdatePlayerHealth(int newHealth)
    {
        float healthFraction = newHealth / 100f;

        Vector2 pos = playerHealthBarRect.anchoredPosition;
        Vector2 size = playerHealthBarRect.sizeDelta;

        float newWidth = size.x * healthFraction;
        float offset = (size.x - newWidth) / 2;

        playerHealthBarRect.anchoredPosition = new Vector2(pos.x - offset, pos.y);
        playerHealthBarRect.sizeDelta = new Vector2(newWidth, size.y);
    }

    // updates enemy health bar with new health
    public void UpdateEnemyHealth(int newHealth)
    {
        float healthFraction = newHealth / 100f;

        Vector2 pos = enemyHealthBarRect.anchoredPosition;
        Vector2 size = enemyHealthBarRect.sizeDelta;

        float newWidth = size.x * healthFraction;
        float offset = (size.x - newWidth) / 2;
        
        enemyHealthBarRect.anchoredPosition = new Vector2(pos.x - offset, pos.y);
        enemyHealthBarRect.sizeDelta = new Vector2(newWidth, size.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBarRect = PlayerHealthBar.GetComponent<RectTransform>();
        enemyHealthBarRect = EnemyHealthBar.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
