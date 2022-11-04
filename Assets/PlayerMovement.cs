using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10;
    bool bothKeysDown = false;

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
        {
            speed /= 2;
            bothKeysDown = true;
        }

        transform.position += transform.forward * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position += transform.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        if (bothKeysDown)
        {
            speed *= 2;
            bothKeysDown = false;
        }
    }
}
