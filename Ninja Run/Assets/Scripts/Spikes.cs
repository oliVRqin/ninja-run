using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            SoundManager.S.MakePlayerDeathSound();
            //Destroy(col.gameObject);
            GameManager.S.PlayerDestroyed();
        }
    }
}
