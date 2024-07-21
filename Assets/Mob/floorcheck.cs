using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorcheck : MonoBehaviour
{
    public GameObject mob;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("end of the floor");

        mob.GetComponent<mobmove>().speed = -mob.GetComponent<mobmove>().speed;
        Debug.Log(mob.GetComponent<mobmove>().speed);
    

    }
}
