using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMov : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        if (Input.GetKey(KeyCode.I))
            direction.z = 1;
        else if (Input.GetKey(KeyCode.K))
            direction.z = -1;

        transform.position += direction * Time.deltaTime * speed;
    }
}
