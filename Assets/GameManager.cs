using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum STATE { LOOKFOR, GOTO, ATTACK, DEAD };
    public STATE currentState = STATE.LOOKFOR;
    public float enemySpeed;
    public float attackDistance;
    public float gotoDistance;
    public Transform target;
    public string playerTag;
    public float attackTime;
    public float currentTime;
    Animator animator;
   void start()
    {
        animator = GetComponent<Animator>();
    }
    IEnumerator Start()
    {
        currentTime = attackTime;
        
        while (true)
        {
            switch (currentState)
            {
                case STATE.LOOKFOR:
                    animator.SetBool("isIdle", true);
                    LookFor();

                    break;
                case STATE.GOTO:
                    animator.SetBool("isRunning", true);
                    Goto();
                    break;
                case STATE.ATTACK:
                    animator.SetBool("isShooting", true);
                    Attack();
                    break;
                case STATE.DEAD:
                    animator.SetBool("isSleeping",true);
                    Dead();
                    break;
                default:
                    break;
            }
            yield return null;
        }

    }

    public void LookFor()
    {
     
        if (Vector3.Distance(target.transform.position, this.transform.position) < gotoDistance)
        {
            currentState = STATE.GOTO;
        }
        print("This is LookForState");
    }
    public void Goto()
    {
        if (Vector3.Distance(target.transform.position, this.transform.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            currentState = STATE.ATTACK;
        }
        print("This is GotoState");
    }
    public void Attack()
    {
        
        
        if (Vector3.Distance(target.transform.position, this.transform.position) > attackDistance)
        {
            currentState = STATE.ATTACK;
        }
        print("This is AttackState");
    }
    public void Dead()
    {
        print("Game Over!!");
    }
}


