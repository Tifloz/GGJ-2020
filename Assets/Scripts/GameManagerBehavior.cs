﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    /*public Text goldLabel;
    public Text waveLabel;
    public Text healthLabel;
    public Text UpgradeLabel;
    public Text sellLabel;
    public GameObject CanvasTower;*/
    public bool gameOver = false;

    private int gold;

 /*   public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = gold.ToString();
        }
    }

    private int wave;

    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value;
            waveLabel.text = "Wave: " + (wave + 1);
        }
    }

    public int Upgrade
    {
        set
        {
            if (value == 0)
            {
                UpgradeLabel.text = "";
            }
            else if (value == -1)
            {
                UpgradeLabel.text = "Max upgraded";
            }
            else
            {
                UpgradeLabel.text = value.ToString() + " credits";
            }
        }
    }

    public int Sell
    {
        set
        {
            if (value == 0)
            {
                sellLabel.text = "";
            }
            else
            {
                sellLabel.text = value.ToString() + " credits";
            }
        }
    }

    private int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthLabel.text = health.ToString();
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
            }
        }
    }


// Use this for initialization
    void Start()
    {
        Gold = 20;
        Wave = 0;
        Health = 100;
    }

// Update is called once per frame
    void Update()
    {
    }
    */
}