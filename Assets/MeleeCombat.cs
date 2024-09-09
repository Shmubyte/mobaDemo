using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement)), RequireComponent(typeof(stats))]


public class MeleeCombat : MonoBehaviour
{

    private Movement moveScript;
    private stats Stats;
    private Animator anim;

    [Header("Target")]
    public GameObject targetEnemy;

    [Header("Melee Attack Variables")]
    public bool perfromMeleeAttack = true;
    private float attackInterval;
    private float nextAttackTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
        Stats = GetComponent<stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Calculates the attack speef and interval between each attack
        attackInterval = Stats.attackSpeed / ((500 + Stats.attackSpeed) + 0.01f);

        targetEnemy = moveScript.targetEnemy;

        if (targetEnemy != null && perfromMeleeAttack && Time.time > nextAttackTime) 
        {
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <+ moveScript.stoppingDistance) 
            {
                StartCoroutine(MeleeAttackInterval());
            }
        }
    }

    private IEnumerator MeleeAttackInterval() 
    {
        perfromMeleeAttack = true;

        //Trigger the animation for the attack
        anim.SetBool("isAttacking", true);

       // Wait based on the attack speed / attackInterval value
        yield return new WaitForSeconds(attackInterval);

        //CHecking if the Enemy is still alive
        if (targetEnemy == null) 
        {
            //Stopping the animation bool and letting it go back to being able to attack
            anim.SetBool("isAttacking", false);
            perfromMeleeAttack = true;
        }
    }

    private void meleeAttack() 
    {
        Stats.TakeDamage(targetEnemy, Stats.damage);

        //Set the next attack time
        nextAttackTime = Time.time + attackInterval;
        perfromMeleeAttack = true;

        //Stop calling the attack anim
        anim.SetBool("isAttacking", false);
    }
}
