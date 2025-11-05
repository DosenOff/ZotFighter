using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject PlayerHealthBar;
    public GameObject EnemyHealthBar;

    RectTransform playerHealthBarRect;
    RectTransform enemyHealthBarRect;

    float startingWidth;
    float startingOffset;

    // updates player health bar with new health
    public void UpdatePlayerHealth(int newHealth)
    {
        float healthFraction = newHealth / 100f;

        Vector2 pos = playerHealthBarRect.anchoredPosition;
        Vector2 size = playerHealthBarRect.sizeDelta;

        float newWidth = startingWidth * healthFraction;
        float offset = (startingWidth - newWidth) / 2;

        playerHealthBarRect.anchoredPosition = new Vector2(startingOffset - offset, pos.y);
        playerHealthBarRect.sizeDelta = new Vector2(newWidth, size.y);
    }

    // updates enemy health bar with new health
    public void UpdateEnemyHealth(int newHealth)
    {
        float healthFraction = newHealth / 100f;

        Vector2 pos = enemyHealthBarRect.anchoredPosition;
        Vector2 size = enemyHealthBarRect.sizeDelta;

        float newWidth = startingWidth * healthFraction;
        float offset = (startingWidth - newWidth) / 2;

        Debug.Log(newWidth);
        Debug.Log(offset);
        Debug.Log(startingOffset);
        enemyHealthBarRect.anchoredPosition = new Vector2(offset - startingOffset, pos.y);
        enemyHealthBarRect.sizeDelta = new Vector2(newWidth, size.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealthBarRect = PlayerHealthBar.GetComponent<RectTransform>();
        enemyHealthBarRect = EnemyHealthBar.GetComponent<RectTransform>();

        startingWidth = playerHealthBarRect.sizeDelta.x;
        startingOffset = playerHealthBarRect.anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
