using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPathfinding : MonoBehaviour
{
    public Camera cam;

    float myMaxScale = 5;
    float myCurrentScale = 1;
    float enemyScale;

    GameObject[] items;
    GameObject target;

    float speed = 5;

    int forward = 0;
    int right = 0;

    double range = .5;

    void Awake()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        target = items[Random.Range(0, items.Length - 1)].gameObject;
    }

    void FollowTarget()
    {
        if (target.transform.position.x - range > transform.position.x)
            right = 1;
        else if (target.transform.position.x + range < transform.position.x)
            right = -1;
        else
            right = 0;

        if (target.transform.position.z - range > transform.position.z)
            forward = 1;
        else if (target.transform.position.z + range < transform.position.z)
            forward = -1;
        else
            forward = 0;
    }

    void FixedUpdate()
    {
        FollowTarget();

        transform.Translate(transform.forward * forward * speed * Time.deltaTime);
        transform.Translate(transform.right * right * speed * Time.deltaTime);
    }

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

            items = GameObject.FindGameObjectsWithTag("Item");
            

            if (items.Length > 0)
                target = items[Random.Range(0, items.Length - 1)].gameObject;
            else
                target = GameObject.FindWithTag("Player");
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

        else if (other.gameObject.tag == "Player")
        {
            enemyScale = other.gameObject.transform.localScale.x + other.gameObject.transform.localScale.y + other.gameObject.transform.localScale.z;

            if (myCurrentScale > enemyScale)
            {
                IncreaseScale(other.gameObject);

                cam.gameObject.SetActive(true);
            }
        }
    }
}
