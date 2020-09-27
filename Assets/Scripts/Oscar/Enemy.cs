using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   int health = 100;

   private void OnCollisionEnter2D(Collision2D collision)
   {
      bool hitPlayer = collision.gameObject.tag == "Player";

      if (hitPlayer)
      {
         health = health - 10;
      }

      if (health <= 0)
      {
         Destroy(gameObject);
      }
   }

}
