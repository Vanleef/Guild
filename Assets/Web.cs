using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{

    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private Rigidbody2D rb;
    public int damage;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            freezeAndDestroy(0.3f);
        }

        player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            stickAndDestroy(player.gameObject, 2f);
        }
    }

    void freezeAndDestroy(float timeToDestroy)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, timeToDestroy);
    }
    
    void stickAndDestroy(GameObject obj, float timeToDestroy)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        gameObject.transform.parent = obj.transform;
        player.Debuff();
        Destroy(gameObject, timeToDestroy);
    }
}
