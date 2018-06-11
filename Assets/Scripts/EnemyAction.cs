using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {

    private Animator animator;
    private bool isDead = false;
    private bool canAttack = false;
    private int actionIndex = 0;

    private float moveSpeed = 0.02f;
    private bool canMove = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        actionIndex = Random.Range(1,3);
    }

    private void Update()
    {
        if (isDead == false)
        {
            animator.SetInteger("ActionIndex", actionIndex);
            if (canMove)
            {
                if (actionIndex == 1) transform.Translate(transform.forward * moveSpeed, Space.World);
                else
                    transform.Translate(transform.forward * moveSpeed * 2, Space.World);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Fence") {
            canMove = false;
            animator.SetBool("CanAttack",true);
        }
    }
    public void IdleEvent() {
        canMove = true;
    }
    public void BehitEvent() {
        canMove = false;
        canAttack = false;
        animator.SetBool("isDead", true);
        isDead = true;
    }

    public void DeadEvent() {
        GameManager.Instance.GetScore();
        Destroy(gameObject,2f);
    }
}
