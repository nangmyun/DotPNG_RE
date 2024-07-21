using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer sp;
    public Image Hpbar, R, G, B;
    public GameObject defaultAttack, circleAttack, shootAttackPrefab, bodyCollider;
    public bool Attacked = false;
    Rigidbody2D rigid2D;
    Animator animator, defaultAttackAnimator, circleAttackAnimator;
    float jumpForce = 500.0f, walkForce = 100.0f, maxWalkSpeed = 4.0f;
    float jumpTime, jumpTime2;
    bool isJumping = false, dashPossible = true;
    public int key2;
    public static float player_X, player_Y, player_Front;
    public int key = 0, Max = 100, hp = 100;
    public static int r = 0, g = 0, b = 0, rg = 0, gb = 0, rb = 0;
    public int atkDmg = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.defaultAttackAnimator = defaultAttack.GetComponent<Animator>();
        this.circleAttackAnimator = circleAttack.GetComponent<Animator>();
        this.sp = GetComponent<SpriteRenderer>();
    }

    void dashChange()
    {
        dashPossible = true;
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        bodyCollider.layer = 10;
        sp.color = new Color(1, 1, 1, 1);
    }

    void dead()
    {
        transform.position = new Vector2(-0.47f, 0.85f);
        r = 0;
        g = 0;
        b = 0;
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -18)
        {
            dead();
        }
        //체력 바
        Hpbar.fillAmount = (float)this.hp / (float)this.Max;
        R.fillAmount = r / (float)this.Max;
        G.fillAmount = g / (float)this.Max;
        B.fillAmount = b / (float)this.Max;

        if(key2 != 0)
        {
            gameObject.layer = 8;
            bodyCollider.layer = 8;
            sp.color = new Color(1, 1, 1, 0.4f);
            this.rigid2D.velocity = Vector3.zero; 
            this.rigid2D.AddForce(transform.right * key2 * 100.0f);
            this.rigid2D.AddForce(transform.right * key2 * 7, ForceMode2D.Impulse);
            key2 = 0;
            if (hp <= 0) // 사망
            {
                dead();
            }
            Invoke("OffDamaged", 1);
        }

        /*
        //Jump
        if (Input.GetKeyDown(KeyCode.A) && !animator.GetBool("Jump"))
        {
            rigid2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);

        }

        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.normalized.x * 0.5f, rigid2D.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
            sp.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //Animation
        if (Mathf.Abs(rigid2D.velocity.normalized.x) < 0.3)
            animator.SetBool("Walk", false);
        else
            animator.SetBool("Walk", true);
        */

        //점프
        if (Input.GetKeyDown(KeyCode.A) && !isJumping && this.rigid2D.velocity.y == 0) 
        {
            animator.SetBool("Jump", true);
            jumpTime = Time.time;
            isJumping = true;
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        if(Input.GetKey(KeyCode.A) && isJumping) //누른 시간에 따라 점프 높이 조절
        {
            jumpTime2 = Time.time - jumpTime;
            this.rigid2D.AddForce(transform.up * this.jumpForce * jumpTime2 * 0.55f);
        }

        if((isJumping && jumpTime2 > 0.3f )|| Input.GetKeyUp(KeyCode.A))
        {
            this.isJumping = false;
            animator.SetBool("Jump", false);
        }

        //걷기
        
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            key = 1; 
            animator.SetBool("Walk", true);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            key = -1;
            animator.SetBool("Walk", true);
        }
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)){
            key = 0;
            animator.SetBool("Walk", false);
        }

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if(speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        if(key != 0 && !isJumping && (this.rigid2D.velocity.y == 0 || this.rigid2D.velocity.x == 0))
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //공격
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDefaultAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerCircleAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDash") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShootAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerHealMotion"))
        {
            if(Input.GetKeyDown(KeyCode.S)) //기본 공격
            {
                this.animator.SetTrigger("DefaultAttackTrigger");
                this.defaultAttackAnimator.SetTrigger("DefaultAttackTrigger");
            }
            else if(Input.GetKey(KeyCode.D) && dashPossible) //대시
            {
                this.rigid2D.velocity = Vector3.zero;
                this.animator.SetTrigger("PlayerDashTrigger");
                this.rigid2D.AddForce(transform.right * transform.localScale.x * 100.0f);
                this.rigid2D.AddForce(transform.right * transform.localScale.x * 10, ForceMode2D.Impulse);
                dashPossible = false;
                Invoke("dashChange", 1);
            }
            else if(Input.GetKeyDown(KeyCode.Q) && r >= 100) //r 스킬(슛 공격)
            {
                r -= 100;
                this.animator.SetTrigger("ShootAttackTrigger");
                player_X = this.transform.position.x;
                player_Y = this.transform.position.y;
                player_Front = transform.localScale.x;
                GameObject shoot = Instantiate(shootAttackPrefab);
            }
            else if(Input.GetKeyDown(KeyCode.W) && g >= 100) //g 스킬(원 공격)
            {
                g -= 100;
                this.animator.SetTrigger("CircleAttackTrigger");
                this.circleAttackAnimator.SetTrigger("CircleAttackTrigger");
            }
            else if(Input.GetKeyDown(KeyCode.E) && b >= 100) //b 스킬(힐 하기)
            {
                b -= 100;
                this.animator.SetTrigger("HealMotionTrigger");
                hp += 20;
                if(hp > 100) hp = 100;
            }
        }
    }
    /*
    void FixedUpdate()
    {
        //Move
        float h = Input.GetAxisRaw("Horizontal");

        rigid2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Right Max Speed
        if (rigid2D.velocity.x > maxWalkSpeed)
            rigid2D.velocity = new Vector2(maxWalkSpeed, rigid2D.velocity.y);
        else if (rigid2D.velocity.x < maxWalkSpeed * (-1)) //Left
            rigid2D.velocity = new Vector2(maxWalkSpeed * (-1), rigid2D.velocity.y);

        //Landing Platform
        if (rigid2D.velocity.y < 0)
        {
            Debug.DrawRay(rigid2D.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    Debug.Log(rayHit.collider.name);
                animator.SetBool("Jump", false);
            }
        }

    }*/
}
