using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Transform myTransform;
    Animator myAnimator;

    [SerializeField] TextMeshProUGUI topCoin;
    [SerializeField] float speed = 7, jumpSpeed=700f,groundPointRange=0.1f, attackRange=0.5f,attackRate=0.5f,jumpFrequency=1f,nextJumpTime;
    [SerializeField] Transform attackPoint,groundCheckPoint;
    [SerializeField] LayerMask enemyLayer,groundCheckLayer;
    [SerializeField] int hitAmount = 25;
    [SerializeField] AudioSource coinSound,doorSoud;
    float yatay, nextAttack=0f;
    string yon;
    int coinAmount;
    bool isGrounded;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
    }


    void Start()
    {
        topCoin.text = coinAmount.ToString();
        yon = "right";
    }

    
    void Update()
    {
        yatay = Input.GetAxis("Horizontal");
        PlayerMovement(yatay);
        InputManager();
        OnGroundCheck();
    }

    void InputManager()
    {
        
        if (Time.time >= nextAttack)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttack = Time.time+1f/attackRate;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded&&(nextJumpTime<=Time.time))
        {
            nextJumpTime= Time.time+jumpFrequency;
           Jump();
        }
    }

    void PlayerMovement(float yatay)
    {

            myRigidBody.velocity = new Vector2(yatay * speed * Time.deltaTime, 0);

        #region yön belirleme
        if (yatay > 0 && yon == "left")
        {
            yon = "right";
            myTransform.localScale = new Vector2(myTransform.localScale.x * -1, myTransform.localScale.y);
        }
        else if (yatay < 0 && yon == "right")
        {
            yon = "left";
            myTransform.localScale = new Vector2(myTransform.localScale.x * -1, myTransform.localScale.y);

        }
        myAnimator.SetFloat("Speed", Mathf.Abs(yatay));
        #endregion

        
    }

    void Attack()
    {
        myAnimator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange,enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("i hit to"+enemy.name);
            enemy.GetComponent<EnemyController>().TakeDamage(hitAmount);
        }
    }

    void Jump()
    {
        myRigidBody.AddForce(new Vector2(0,jumpSpeed));
        
    }
    

    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundPointRange,groundCheckLayer);
        myAnimator.SetBool("isGround", isGrounded);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Coin")
        {
            coinAmount++;
            coinSound.Play();
            topCoin.text=coinAmount.ToString();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag=="Door")
        {
            doorSoud.Play();
            if (SceneManager.GetActiveScene().buildIndex + 1<=2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }

        if (collision.gameObject.tag=="Reloader")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
