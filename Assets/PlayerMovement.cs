using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10;

    void FixedUpdate()
    {
        transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime);
    }
}
