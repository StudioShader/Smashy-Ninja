using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : Unit {
    private Animator anim;

    [SerializeField]
    private float atackTime, dAtackTime = 0.3f, rPTimer = 0.4f, dRPTimer, rushTimer = 1f, dRushTimer = 0, rushReload = 5, abilityTimer = 0;

    private bool Grounded = true, sitDown = false, walled = false, wasWalled = false, AfterGrabJump = false, AiredDeath, Arrow, onDesk, onDeskAnim, onDownButton1 = false, wasAtack = false;

    private GameObject text, Money, Shurikens, FinishM;

    public bool Down = false, abilityToUseAbility = true;

    [SerializeField]
    public bool Rush = true, up = true;

    [SerializeField]
    private float a;

    [SerializeField]
    private int shurikens;

    [SerializeField]
    public float speed = 5, JumpForce, Xatack = 5, OverJumpForce, OverUpJumpForce;

    public float dJtime, deltaJTime = 0.2f, dOtime, deltaOtime = 0.4f;

    private SpriteRenderer sprite;
    [SerializeField]
    private Arrow arrow;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Endless mode") {
            FinishM = GameObject.FindGameObjectWithTag("FinishMenu");
            FinishM.SetActive(false);
        }
        Money = GameObject.FindGameObjectWithTag("Money");
        Shurikens = GameObject.FindGameObjectWithTag("Shurikens");
        text = GameObject.FindGameObjectWithTag("debugtext");
        particle = Resources.Load<GameObject>("Particles/Particle2");
        arrow = Resources.Load<Arrow>("Particles/arrow");
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0, 0);
    }
    public override void Update()
    {
        if (Time.timeScale == 0) return;
        Shurikens.GetComponent<Text>().text = shurikens.ToString();
        Money.GetComponent<Text>().text = PlayerPrefs.GetInt("AllMoney").ToString();
        if (dead)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (!dead)
        {
            if (Rush && dRushTimer <= rushTimer)
            {
                Time.timeScale = 0.7f;
                dRushTimer += Time.deltaTime;
                GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject.SetActive(true);
                SpawnRushParticle();
            }
            else
            {
                Time.timeScale = 1f;
                dRushTimer = 0;
                RushOf();
               // GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).gameObject.SetActive(false);
            }
            if (!Application.isMobilePlatform )
            {
                if (Input.GetKey("s"))
                {
                    toDown();
                }
            }
            /*if (rb.velocity.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }*/
            if (Health <= 0)
            {
                if (!isGrounded())
                {
                    AiredDeath = true;
                }
                dead = true;
                Death();
            }
            if (rb.velocity.x < speed)
            {
                rb.velocity = new Vector3(rb.velocity.x + 0.5f * a * Time.deltaTime, rb.velocity.y, 0);
            }
        }
        if (isGrounded())
        {
            AfterGrabJump = false;
            Grounded = true;
            if (!dead)
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
            }
        }
        else
        {
            Grounded = false;
        }

        if (!dead)
        {
            if (dOtime < deltaOtime) dOtime += Time.deltaTime;
            if (abilityTimer < rushReload) abilityTimer += Time.deltaTime;
            if (isOnWall())
            {
                AfterGrabJump = false;
                walled = true;
            }
            else walled = false;

            if (dJtime < deltaJTime) { dJtime += Time.deltaTime; }
            onDeskAnim = false;
            if (Input.GetKey("w") && !Application.isMobilePlatform) {
                Jump();
            }
            if (Application.isMobilePlatform && onDownButton1)
            {
                Jump();
            }
            if (atackTime < dAtackTime)
            {
                atackTime += Time.deltaTime;
            }
            else
            {
                if (!wasAtack) {
                    //text.GetComponent<Text>().text = "atack false";
                    anim.SetBool("Atack", false);
                    Arrow = false;
                }
                wasAtack = false;
            }
            if (Input.GetKeyDown("d")) Atack();
            if (Input.GetKeyDown("a")) Shot();
            if (Input.GetKeyDown("space")) RushOn();
        }
        anim.SetBool("Grounded", Grounded);
        anim.SetBool("Walled", walled);
        anim.SetBool("AfterGrabJump", AfterGrabJump);
        anim.SetBool("Dead", dead);
        anim.SetBool("AiredDeath", AiredDeath);
        anim.SetBool("sitDown", sitDown);
        if (Arrow)
        {
            anim.SetBool("Arrow", Arrow);
           // if(anim.geta)
        }
        anim.SetBool("Arrow", Arrow);
        anim.SetBool("onDeskAnim", onDeskAnim);
    }
    public override void Death()
    {
        FindObjectOfType<AudioManager>().Play("Meat1");
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Arrow = false;
        dead = true;
        SpawnParticles(particle,18);
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //GetComponentInChildren<Collider2D>().enabled = false;
    }
    public void Jump()
    {
        if (sitDown)
        {
            Turnup();
        }
        if (dJtime >= deltaJTime && !dead && abilityToUseAbility)
        {
            
            if (isOnWall() && dOtime >= deltaOtime)
            {
                FindObjectOfType<AudioManager>().Play("Jump");
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(new Vector2(-50 * OverJumpForce, 50 * OverUpJumpForce));
                dOtime = 0;
                dJtime = 0;
                AfterGrabJump = true;
            }
            else
            {
                if (isGrounded())
                {
                    FindObjectOfType<AudioManager>().Play("Jump");
                    rb.velocity = new Vector3(10, 0, 0);
                    rb.AddForce(new Vector2(0, 250 * JumpForce));
                    dJtime = 0;
                }
                else
                {
                    if (onDesk)
                    {
                        onDeskAnim = true;
                        rb.velocity = new Vector2(speed, speed);
                    }
                }
            }
        }
    }
    public bool isGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].gameObject.GetComponent<Collider2D>().isTrigger && colliders[i].gameObject != gameObject && colliders[i].gameObject.layer != 12 )
            {
                return true;
            }
        }
        return false;
    }
    public bool isOnWall()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector3(transform.position.x + 0.25f, transform.position.y + 0.71f, 0), 0.35f);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.layer == 8)
            {
                return true;
            }
        }
        return false;
    }
    public override void Atack()
    {
        if (sitDown)
        {
            Turnup();
        }
        if (atackTime >= dAtackTime && abilityToUseAbility)
        {
            //text.GetComponent<Text>().text = "atack true";
            anim.SetInteger("Random", Mathf.RoundToInt(Random.Range(0, 2)));
            FindObjectOfType<AudioManager>().Play("SwordSwing1");
            anim.SetBool("Atack", true);
            atackTime = 0;
            Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - gameObject.GetComponent<BoxCollider2D>().size.x, transform.position.y), new Vector2(transform.position.x + Xatack, transform.position.y + 1.6f));
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].GetComponent<Player>())
                {
                    if (colliders[i].GetComponent<Arrow>() && colliders[i].GetComponent<Arrow>().gargoyle)
                    {
                        Destroy(colliders[i].gameObject);
                        atackTime = dAtackTime;
                        wasAtack = true;
                    }
                    if (colliders[i].GetComponent<Unit>())
                    {
                        if (!colliders[i].GetComponent<Mage>() && !colliders[i].GetComponent<Chest>())
                        {
                            FindObjectOfType<AudioManager>().Play("Meat1");
                        }

                        if (colliders[i].GetComponent<Mage>())
                        {
                            FindObjectOfType<AudioManager>().Play("MagicDeath");
                        }

                        colliders[i].GetComponent<Unit>().RecieveDamage(1);
                        atackTime = dAtackTime;
                        wasAtack = true;
                    }
                }
            }
        }
    }
    public void Shot()
    {
        if (sitDown)
        {
            Turnup();
        }
        if (atackTime >= dAtackTime && abilityToUseAbility && shurikens != 0)
        {
            FindObjectOfType<AudioManager>().Play("Shuriken");
            shurikens -= 1;
            Arrow = true;
            atackTime = 0;
            Vector3 position = transform.position; position.y += 0.9f;
            Arrow newarrow = Instantiate(arrow, position, arrow.transform.rotation) as Arrow;
            newarrow.parent = this.gameObject;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Arrow arrow = other.gameObject.GetComponent<Arrow>();
        if (arrow && arrow.parent != gameObject)
        {
            RecieveDamage(1);
        }
        if (other.gameObject.CompareTag("Desk"))
        {
            onDesk = true;
        }
        if (other.gameObject.CompareTag("Spikes"))
        {
            RecieveDamage(1);
        }
        if (other.gameObject.CompareTag("FireBall"))
        {
            RecieveDamage(1);
        }
        if (other.gameObject.CompareTag("Cask"))
        {
            shurikens += 3;
            other.gameObject.GetComponentInChildren<Animator>().SetBool("Taken", true);
            other.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (other.gameObject.GetComponent<Coin>())
        {
            //Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("CoinCollect");
            other.gameObject.GetComponent<Coin>().Del();
            PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") + 1);
            //Debug.Log(PlayerPrefs.GetInt("AllMoney"));
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            FinishLevel();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Desk"))
        {
            onDesk = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spartan"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            RecieveDamage(1);
        }
    }
    public void toDown()
    {
        Down = true;
        Invoke("DownFalse", 0.3f);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].gameObject.GetComponent<Collider2D>().isTrigger && colliders[i].gameObject != gameObject && colliders[i].gameObject.layer != 12 && colliders[i].gameObject.layer != 16 && colliders[i].gameObject.layer != 17 && abilityToUseAbility)
            {
                Sitdown();
                break;
            }
        }
    }
    public void Button1Down()
    {
        onDownButton1 = true;
    }
    public void Button1Up()
    {
        onDownButton1 = false;
    }
    public void SpawnRushParticle()
    {
        if (dRPTimer >= rPTimer)
        {
            GameObject r =  PoolScript.instance.GetObjectFromPool("RushParticle", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(Vector3.zero));
            dRPTimer = 0;
        }
        else
        {
            dRPTimer += Time.deltaTime;
        }
        
    }
    public void RushOn()
    {
        if (abilityTimer >= rushReload && abilityToUseAbility) {
            abilityTimer = 0;
            JumpForce *= 1.5f;
            OverUpJumpForce *= 1.5f;
            rb.gravityScale *= 2f;
            speed *= 2;
            Rush = true;
        }
    }
    public void RushOf()
    {
        if (Rush)
        {
            JumpForce /= 1.5f;
            OverUpJumpForce /= 1.5f;
            speed /= 2;
            rb.gravityScale /= 2;
        }
        Rush = false;
    }
    private void Sitdown()
    {
        sitDown = true;
        abilityToUseAbility = false;
        up = true;
        Invoke("Turnup",0.7f);
        gameObject.GetComponent<Collider2D>().offset = new Vector2(0, gameObject.GetComponent<BoxCollider2D>().offset.y/2);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y/2);
    }
    public void Turnup()
    {
        if (sitDown) {
            sitDown = false;
            abilityToUseAbility = true;
            up = false;
            gameObject.GetComponent<Collider2D>().offset = new Vector2(0, gameObject.GetComponent<BoxCollider2D>().offset.y * 2);
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y * 2);
        }
    }
    private void DownFalse()
    {
        Down = false;
    }
    public void FinishLevel()
    {
        Time.timeScale = 0;
        FinishM.SetActive(true);
    }
}
