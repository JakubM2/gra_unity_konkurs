using UnityEngine;
using System.Collections;
//using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
public class EnemyScript : MonoBehaviour {

    public GameMaster gm;
    public GameObject arrow;

    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    public float myWidth;
    public float myHeight;

    public Transform target; //player

    public Transform shootPoint;

    public float EnemyHealth = 100;
    public float FireballDamage = 40;

    //XXX
    private float arrowTimer;
    private float arrowCoolDown = 3;
    private bool canArrow;

    //shoot
    public bool awake = false;
    public float distance = 10f;
    public float wakeRange;
    public float arrowMax = 5;
    public float arrowOnScreen = 0;
    public bool facingRight = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //?????????????

        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = 1;
        myHeight = -1;
    }

    void FixedUpdate()
    {
        //Use this position to cast the isGrounded/isBlocked lines from
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        //Check to see if there's ground in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        //Check to see if there's a wall in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

        //If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            facingRight = !facingRight;
            myTrans.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;








        /*if (arrowOnScreen <= arrowMax)
        {
            //yield return new WaitForSeconds(2);
            if (target.position.x <= transform.position.x + distance && facingRight == false && target.position.y <= transform.position.y + 2 && target.position.y >= transform.position.y - 2)
            {
                Debug.Log("EnemyScript1"); //left shoot
                StartCoroutine(EnemyShoot(false));
            }
            else if (target.position.x >= transform.position.x + distance && facingRight == true && target.position.y <= transform.position.y + 2 && target.position.y >= transform.position.y - 2)
            {
                Debug.Log("EnemyScript2"); //right shoot
                StartCoroutine(EnemyShoot(true));
            }
        }*/
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fire"))
        {
            Destroy(col.gameObject);
            DamageEnemy(FireballDamage);
        }
    }

    public void DamageEnemy(float damage)
    {
        EnemyHealth -= damage;
        if (EnemyHealth <= 0)
        {
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        gm.enemyKill += 1;
        Destroy(gameObject); // add destroy only this gameObject
    }

    /*IEnumerator EnemyShoot(bool lookingRight)
    {
        if (lookingRight == true)
        {
            arrowOnScreen += 1;
            GameObject clone = Instantiate(arrow) as GameObject;
            clone.transform.position = transform.position;
            //gm.PlaySound("Fire"); //addArrow sounds
            yield return new WaitForSeconds(5);
        } else if(lookingRight == false)
        {
            arrowOnScreen += 1;
            GameObject clone = Instantiate(arrow) as GameObject;
            clone.transform.position = -transform.position;            
            //gm.PlaySound("Fire"); //addArrow sounds
            yield return new WaitForSeconds(5);
        }
    }

    public void Execute()
    {
        ThrowArrow();
    }

    private void ThrowArrow()
    {
        arrowTimer += Time.deltaTime;
        if(arrowTimer >= arrowCoolDown)
        {
            canArrow = true;
            arrowTimer = 0;
        }
        if(canArrow)
        {
            canArrow = false;
            //animator shoot arrow
        }
    }*/
}