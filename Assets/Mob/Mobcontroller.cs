using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobcontroller : MonoBehaviour
{
    public int atkDmg, hp, key;
    public int RGB; //r이 1, g가 2, b가 3
    
    private void SetEnemyStatus(int atkDmg, int hp)
    {
        this.atkDmg = atkDmg;
        this.hp = hp;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
