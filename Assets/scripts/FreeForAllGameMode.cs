﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAllGameMode : GameMode
{
    [Header("specific to this mode")]
    public int[] playerScores;

    public GameObject[] initialSpawnPoints;
    public GameObject[] spawnPoints;

    public ChangeColorToWinningPlayer changeColorToWinningPlayer;
    public GameObject explosionPrefab;

    public int pointsToWin = 20;

    public override void AtEndOfOnEnable()
    {
        playerScores = new int[players.Count];
    }

    public void RespawnPlayer(int PlayerToRespawnID)
    {
        Vector2 furthestAwaySpawnPoint = FindNodeFarthestFromAnyActivePlayer(spawnPoints).transform.position;
        SpawnPlayer(PlayerToRespawnID, furthestAwaySpawnPoint);
    }

    public override void CharacterCollision(int attackerPlayerID, int VictimPlayerID)
    {
        if (isGameOver == false)
        {
            playerScores[attackerPlayerID] += 1;
        }

        //HACK
        //sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/player killed");
        //explosion
        GameObject newExplosion;
        newExplosion = Instantiate(explosionPrefab, players[VictimPlayerID].activeCharacterInScene.transform.position, Quaternion.identity);
        ParticleSystem ps = newExplosion.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
        psmain.startColor = GameManager.gameManagerInstance.playerColors[VictimPlayerID];

        Destroy(players[VictimPlayerID].activeCharacterInScene);
        RespawnPlayer(VictimPlayerID);

        UpdateUI();

        //HACK
        camshake.Shake();
        //HACK
        ChangeBackgroundColor();

        if (isGameOver == false)
        {
            CheckWinCondition(attackerPlayerID);
        }
    }

    public override void UpdateUI()
    {
        for (int i = 0; i < playerScoresTextMesh.Length; i++)
        {
            playerScoresTextMesh[i].text = playerScores[i].ToString();
        }           
    }

    public override void CheckWinCondition(int scoringPlayer)
    {
        if (playerScores[scoringPlayer] >= pointsToWin)
        {
            GameManager.gameManagerInstance.winningPlayerID = scoringPlayer;
            StartCoroutine(GameWon());
        }
    }

    public void ChangeBackgroundColor()
    {
        //HACK
        //change bg color
        int highestScore = -1;
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] > highestScore)
            {
                highestScore = playerScores[i];
            }
        }
        int playersTiedForHighScore = 0;
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] == highestScore)
            {
                playersTiedForHighScore += 1;
            }
        }
        if (playersTiedForHighScore > 1)
        {
            changeColorToWinningPlayer.setColor(changeColorToWinningPlayer.defaultColor);
        }
        if (playersTiedForHighScore == 1)
        {
            Color winningPlayerColor;
            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] == highestScore)
                {
                    changeColorToWinningPlayer.setColor(GameManager.gameManagerInstance.playerColors[i]);
                }
            }
        }
    }
}
