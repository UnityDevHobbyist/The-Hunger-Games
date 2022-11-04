using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    float myMaxScale = 5;
    float myCurrentScale = 1;
    float enemyScale;

    void CheckScale()
    {
        if (transform.localScale.x > myMaxScale)
            transform.localScale = new Vector3(myMaxScale, transform.localScale.y, transform.localScale.z);

        if (transform.localScale.y > myMaxScale)
            transform.localScale = new Vector3(transform.localScale.x, myMaxScale, transform.localScale.z);

        if (transform.localScale.z > myMaxScale)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, myMaxScale);
    }

    void IncreaseScale(GameObject other)
    {
        transform.localScale += other.gameObject.transform.localScale;

        CheckScale();

        myCurrentScale += (transform.localScale.x + transform.localScale.y + transform.localScale.z / 3);

        other.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            IncreaseScale(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Npc")
        {
            enemyScale = other.gameObject.transform.localScale.x + other.gameObject.transform.localScale.y + other.gameObject.transform.localScale.z;

            if (myCurrentScale > enemyScale)
            {
                IncreaseScale(other.gameObject);
            }
        }
    }
}
