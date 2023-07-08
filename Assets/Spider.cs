using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    private NpcObject npcObject;
    [SerializeField]
    private Animator animatorNpc;
    [SerializeField]
    private int currentHp;
    private Transform target;
    [SerializeField]
    private float despawnDistance;
    private bool checkBossSound = false;

    void Start(){
        StartBossMusic();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        currentHp = npcObject.hp;
    }

    void Update()
    {
        if (currentHp > 0) {
            if (Vector2.Distance(transform.position, target.position) > despawnDistance) {
                Destroy(gameObject);
                return;
            }
        } else {
            Destroy(transform.gameObject.GetComponent<SpawnMiniSpider>());
            Destroy(transform.gameObject.GetComponent<ShootWeb>());
            Destroy(transform.gameObject.GetComponent<DistanceJoint2D>());
            Destroy(transform.gameObject.GetComponent<Collider2D>());
            Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
            
            //if(gameObject.GetComponent<UnlockController>() != null){
            //    gameObject.GetComponent<UnlockController>().unlockProgress(gameObject.GetComponent<UnlockController>().unlockOnDeath);
            //}
            return;
        }
    }

    public void Hit(int damage) {
        //animatorNpc.SetTrigger("Hit");
        currentHp -= damage;
        Debug.Log(currentHp);
        if (currentHp <= 0) {
            Die();
        }
    }

    private void Die() {
        CombatEvents.EnemyDied(npcObject.npcId);
        animatorNpc.SetTrigger("Death");
    }

    void StartBossMusic()
    {
        if (!checkBossSound)
        {
            SoundManager.instance.Play("BossFight2");
            checkBossSound = true;
        }
    }
}
