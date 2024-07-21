using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobBody : MonoBehaviour
{
    public GameObject Mob;
    public Rigidbody2D rigid2D;
    SpriteRenderer sp;
    public int RGB;
    int key;

    // Start is called before the first frame update
    void Start()
    {
        sp = Mob.GetComponent<SpriteRenderer>();
        this.RGB = Mob.GetComponent<Mobcontroller>().RGB;
    }

    void OffDamaged()
    {
        gameObject.layer = 9;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
    }

    void dotDamage()
    {
        Mob.GetComponent<Mobcontroller>().hp -= 5;
        Debug.Log("도트"+Mob.GetComponent<Mobcontroller>().hp);
        if (Mob.GetComponent<Mobcontroller>().hp <= 0) // 사망
        {
            Destroy(Mob);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(this.transform.position.x - col.transform.position.x > 0) key = 1;
        else key = -1;

        if(col.CompareTag("DefaultAttack") || col.CompareTag("CircleAttack") || col.CompareTag("ShootAttack"))
        {
            if (col.CompareTag("DefaultAttack"))
            {
                Mob.GetComponent<Mobcontroller>().hp -= 10;
            }
            else if (col.CompareTag("CircleAttack"))
            {
                Mob.GetComponent<Mobcontroller>().hp -= 10;
                Invoke("dotDamage", 1);
                Invoke("dotDamage", 2);
                Invoke("dotDamage", 3);
                Invoke("dotDamage", 4);
            }
            else if (col.CompareTag("ShootAttack"))
            {
                Mob.GetComponent<Mobcontroller>().hp -= 15;
            }
            Debug.Log(Mob.GetComponent<Mobcontroller>().hp);
            gameObject.layer = 7;
            sp.color = new Color(1, 1, 1, 0.4f);
            rigid2D.velocity = Vector3.zero; 
            rigid2D.AddForce(transform.right * key * 50.0f);
            rigid2D.AddForce(transform.right * key * 5, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1);
        }
        

        if (Mob.GetComponent<Mobcontroller>().hp <= 0) // 사망
        {
            Destroy(Mob);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
