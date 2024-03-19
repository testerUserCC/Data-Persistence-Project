using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameManager gameManager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        gameManager.GameOver();
    }
}
