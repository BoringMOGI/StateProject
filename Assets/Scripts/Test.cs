using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 from = Vector3.zero;
        Vector3 to = new Vector3(x, 0, y);
        Vector3 v = to - from;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
