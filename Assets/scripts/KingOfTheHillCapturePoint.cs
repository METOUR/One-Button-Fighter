﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillCapturePoint : MonoBehaviour
{
    public KingOfTheHillGameMode activeGameMode;

    private void OnEnable()
    {
        activeGameMode = GetComponentInParent<KingOfTheHillGameMode>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            activeGameMode.PlayerInCaptureZone(collision.gameObject);
        }
    }
}
