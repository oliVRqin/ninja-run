using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool faceLeft = true;

    private CharacterController2D controller;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMove = speed * Time.fixedDeltaTime;

        if (faceLeft) { horizontalMove *= -1.0f; }

        controller.Move(horizontalMove, false, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            Debug.Log("Hit the Collider");
            faceLeft = !faceLeft;
        } /* else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Goodbye Cruel World!!!");
            SoundManager.S.MakeEnemyDeathSound();
            Destroy(this.gameObject);
        } */
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.transform.tag == "Player")
        {
            var vectorToPlayer = col.transform.position - this.transform.position;
            if (Vector3.Angle(vectorToPlayer, this.transform.up) < 50) 
            {
                // Enemy dies
                SoundManager.S.MakeEnemyDeathSound();
                Destroy(this.gameObject);
            } else {
                // Player dies
                SoundManager.S.MakePlayerDeathSound();
                Destroy(col.gameObject);
            }
        }
    }

}
