﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] allCharacterPrefabs;

    public Color[] playerColors;

    public static GameManager gameManagerInstance;

    public enum GameMode { freeForAll, oddBall};

    private void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }
    }
}