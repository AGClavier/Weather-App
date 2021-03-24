using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    public float speed = 1.0f;

    Rigidbody2D hit;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        hit = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Restart")
        {
            hit.transform.position = new Vector3(-15, -1, 10);
        }
    }
}
