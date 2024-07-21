using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : PlayerController
{
    float X, Y, front, present;
    // Start is called before the first frame update
    void Start()
    {
        X = player_X;
        Y = player_Y;
        front = player_Front;
        
        this.transform.position = new Vector2(X + front*0.2f, Y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(front * 0.1f, 0, 0);
        present = this.transform.position.x;

        if(Mathf.Abs(X - present) > 9.0f)
        {
            Destroy(gameObject);
        }
    }
}
