using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private int maxHeartAmount = 10;
    public int startHeart = 3;
    private int currentHealth;
    private int maxHealth;
    private int healthPerHeart = 2;
    
    public Image[] heartImages;
    public Sprite[] heartSprites;
    
    void Start()
    {
        currentHealth = startHeart * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        checkHealthAmount();
    }
    
    void checkHealthAmount()
    {
        for (int i=0; i < maxHeartAmount; i++)
        {
            if (startHeart <= i)
            {
                heartImages[i].enabled = false;
            }
            else
            {
                heartImages[i].enabled = true;
            }
        }
        
        UpdateHearts();
    }

    void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in heartImages)
        {
            if (empty)
            {
                image.sprite = heartSprites[0];
            }
            else
            {
                i++;
                if (currentHealth >= i * healthPerHeart)
                {
                    image.sprite = heartSprites[^1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - currentHealth));
                    int healthPerImage = healthPerHeart / (heartSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = heartSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    public void AddHeartContainer()
    {
        startHeart++;
        startHeart = Mathf.Clamp(startHeart, 0, maxHeartAmount);
        
        currentHealth = startHeart * healthPerHeart;
        maxHealth = maxHeartAmount * healthPerHeart;
        
        checkHealthAmount();
    }
}
