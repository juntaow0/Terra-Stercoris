using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollision : MonoBehaviour
{
   [SerializeField]
   GameObject create;

   [SerializeField]
   GameObject create1;

   [SerializeField]
   string strCollidedWithTag;

   private void spawn(Collision collision)
   {
      if (collision.collider.tag == strCollidedWithTag)
         Instantiate(create, transform.position, Quaternion.identity);
         Instantiate(create1, transform.position, Quaternion.identity);
   }
}
