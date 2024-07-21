using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://wonsang98.tistory.com/entry/Unity-C-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%EC%9D%B4%EB%8F%991-TransformTranslate
public class mobmove : MonoBehaviour
{
    public GameObject player;
    public float delta;
    int term;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.term = Random.Range(1, 3);
        this.speed = 1f;
        term = 0;
        this.delta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > this.term)
        {
            this.speed = -this.speed;
            this.delta = 0;
            term = Random.Range(1, 3);
        }

        if (transform.localPosition.x - player.transform.localPosition.x > -3 && transform.localPosition.x - player.transform.localPosition.x < 3)
        {
            if(transform.localPosition.x - player.transform.localPosition.x < 0) //�÷��̾ ������ �����ʿ� ���� ��
            {
                this.speed = 1;
            }
            else if(transform.localPosition.x - player.transform.localPosition.x > 0)
            {
                this.speed = -1;
            }
        }

        transform.Translate(new Vector2( speed* Time.deltaTime, 0)); //�� �����̱� 
    }

    public void SetSpeed()
    {
        this.speed = -this.speed;
    }
}