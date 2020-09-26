using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeath : MonoBehaviour
{
   int health = 500;

   private void OnCollisionEnter2D(Collision2D collision)
   {
      bool hitPlayer = collision.gameObject.tag == "Enemy";

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
