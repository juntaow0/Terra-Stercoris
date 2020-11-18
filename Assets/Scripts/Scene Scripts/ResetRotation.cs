using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    public void StandUp() {
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }
}
