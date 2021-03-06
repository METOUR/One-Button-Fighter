﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    public BaseCharacter myCharacter;
    public int myPlayerID;
    public GameMode activeGameMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("weapon"))
        {
            int otherPartyID = collision.gameObject.GetComponentInParent<BaseCharacter>().myPlayerInfo.playerID;
            if (otherPartyID != myPlayerID && myCharacter.isVulnerable == true)
            {
                activeGameMode.CharacterCollision(otherPartyID, myPlayerID);
            }
        }
    }
}
