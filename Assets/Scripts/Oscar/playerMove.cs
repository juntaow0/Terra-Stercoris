
using UnityEngine;

public class playerMove : MonoBehaviour
{
 private void OnMouseDrag()
 {
    Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.position = new Vector3(newPos.x, newPos.y);
}
}
