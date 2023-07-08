using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private Rigidbody2D rb;
    public int damage;
    [SerializeField]
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))
        {
            freezeAndDestroy(2);
        }

        NpcController npc = collision.GetComponent<NpcController>();
        Spider spider = collision.GetComponent<Spider>();
        if (npc != null)
        {
            particles.Play();
            stickAndDestroy(npc.gameObject, 0.3f);
            npc.Hit(damage);
        }
        if(spider != null){
            particles.Play();
            stickAndDestroy(spider.gameObject, 0.3f);
            spider.Hit(damage);
        }
    }


    private void freezeAndDestroy(float timeToDestroy)
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, timeToDestroy);
    }

    private void stickAndDestroy(GameObject obj, float timeToDestroy)
    {
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
