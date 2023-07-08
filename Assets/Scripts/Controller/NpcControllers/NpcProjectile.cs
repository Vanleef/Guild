using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcProjectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private Rigidbody2D rb;
    public int damage;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))
        {
            FreezeAndDestroy(0.3f);
        }

        Collider2D target = collision.GetComponent<Collider2D>();
        if (collision.CompareTag("Player"))
        {
            particles.Play();
            StickAndDestroy(target.gameObject, 0.3f);
            GameController.Instance.DamagePlayer(target.gameObject, damage);
        }
    }


    private void FreezeAndDestroy(float timeToDestroy)
    {
        if (animator != false) {
            animator.SetTrigger("Destroy");
        }
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, timeToDestroy);
    }

    private void StickAndDestroy(GameObject obj, float timeToDestroy)
    {
        if (animator != false) {
            animator.SetTrigger("Destroy");
        }
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        gameObject.transform.parent = obj.transform;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, timeToDestroy);
    }
}
