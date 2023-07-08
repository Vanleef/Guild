using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    public int damage;
    [SerializeField]
    private Rigidbody2D rb;
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
            particles.Play();
            freezeAndDestroy();
        }


        if (collision.CompareTag("NPC"))
        {
            
            particles.Play();
            freezeAndDestroy(); 
            NpcController npc = collision.GetComponent<NpcController>();
            Spider spider = collision.GetComponent<Spider>();
            if(spider != null){
                spider.Hit(damage);
            }
            if(npc != null){
                npc.Hit(damage);
            }
                
            // particles.Play();
            // Destroy(gameObject.GetComponent<SpriteRenderer>());
            // Destroy(gameObject.GetComponent<Rigidbody2D>());
        }

        if (collision.CompareTag("MiniSpider"))
        {
            
            particles.Play();
            freezeAndDestroy(); 
            NpcController npc = collision.GetComponent<NpcController>();
            // particles.Play();
            // Destroy(gameObject.GetComponent<SpriteRenderer>());
            // Destroy(gameObject.GetComponent<Rigidbody2D>());
            npc.Hit(damage);
        }
    }

    private void freezeAndDestroy()
    {
        rb.velocity = Vector2.zero;
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        // Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, 0.7f);
    }

}
