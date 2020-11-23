using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Success : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            SoundManager.S.MakeGameWonSound();
            GameManager.S.PlayerWon();
        }
    }
}
