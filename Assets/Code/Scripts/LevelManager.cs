using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform endPoint; // Added endPoint variable
    public Transform[] path;

    public int lives;
    public int currency = 500;



    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 500;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You don't have enough currency to purchase this item.");
            return false;
        }
    }
    private void Update()
    {
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // Perform game over actions here, such as showing a game over screen, resetting the level, etc.
        if (lives > 0)
        {
            Debug.Log("Game Over");
            lives = 0;
        }
    }
}