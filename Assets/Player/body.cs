using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : PlayerController 
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Spine"))
        {
            Player.GetComponent<PlayerController>().hp -= 10;
            if(this.transform.position.x - col.transform.position.x > 0) Player.GetComponent<PlayerController>().key2 = 1;
            else Player.GetComponent<PlayerController>().key2 = -1;
        }
        else if(col.CompareTag("Mob"))
        {
            Debug.Log("it hurts");
            Player.GetComponent<PlayerController>().hp -= 5;
            if(this.transform.position.x - col.transform.position.x > 0) Player.GetComponent<PlayerController>().key2 = 1;
            else Player.GetComponent<PlayerController>().key2 = -1;
        }
        /*else if(col.CompareTag("Boss"))
        {
            Player.GetComponent<PlayerController>().hp -= 20;
            if(this.transform.position.x - col.transform.position.x > 0) Player.GetComponent<PlayerController>().key2 = 1;
            else Player.GetComponent<PlayerController>().key2 = -1;
        }
        else if(col.CompareTag("Cloud"))
        {
            Player.GetComponent<PlayerController>().hp -= 10;
            if(this.transform.position.x - col.transform.position.x > 0) Player.GetComponent<PlayerController>().key2 = 1;
            else Player.GetComponent<PlayerController>().key2 = -1;
        }
        else if(col.CompareTag("Rain"))
        {
            Player.GetComponent<PlayerController>().hp -= 5;
            if(this.transform.position.x - col.transform.position.x > 0) Player.GetComponent<PlayerController>().key2 = 1;
            else Player.GetComponent<PlayerController>().key2 = -1;
        }
        else if(col.CompareTag("rH"))
        {
            r = 100;
        }
        else if(col.CompareTag("gH"))
        {
            g = 100;
        }
        else if(col.CompareTag("bH"))
        {
            b = 100;
        }
        else if(col.CompareTag("H"))
        {
            Player.GetComponent<PlayerController>().hp += 20;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
