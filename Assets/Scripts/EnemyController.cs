using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator enemyAnimator;

    [SerializeField] AudioSource deathSound;
    [SerializeField] int maxHealt = 100;
    int currentHealt;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealt = maxHealt;   
       
    }

   
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealt-=damageAmount;
        Die(currentHealt);
    }

    void Die(int healt)
    {
        if (healt<=0)
        {
            deathSound.Play();
            enemyAnimator.SetBool("Died", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
        else
        {
            enemyAnimator.SetTrigger("TakeDamage");
        }
    }
}
