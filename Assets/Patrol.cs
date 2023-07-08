using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;

    public Animator animator;
    public LayerMask whatIsEnemies;


    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
        if (groundInfo.collider.CompareTag("Ground"))
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        else if (groundInfo.collider.CompareTag("Player"))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(groundDetection.position, 10, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                /*Vector3 heading = enemiesToDamage[i].GetComponent<Transform>().position - transform.position;
                transform.position = heading;*/
                animator.SetTrigger("Attack");
                enemiesToDamage[i].GetComponent<PlayerActions>().TakeDamage(5);
            }

        }
    }
    /* private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ground"))
        {
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0,-180, 0);
                movingRight = false;
            }else{
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
            }
        }
    }*/
}
