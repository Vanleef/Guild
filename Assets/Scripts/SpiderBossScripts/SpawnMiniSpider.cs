using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnMiniSpider : MonoBehaviour
{
    [SerializeField]
    private Transform[] SpawnPoints;
    [SerializeField]
    private GameObject MiniSpider;
    [SerializeField]
    private int Waves = 2;
    [SerializeField]
    private int HowManyMiniSpiders = 5;
    [SerializeField]
    private float HowManySeconds = 10f;

    /* private void Start()
    {
        StartCoroutine(Spawn());
    }*/
    public IEnumerator Spawn()
    {
        for (int i = 0; i < Waves; i++)
        {
            SpawnMiniSpiders();
            yield return new WaitForSeconds(HowManySeconds);
        }
    }

    public void SpawnMiniSpiders()
    {
        for (int u = 0; u < HowManyMiniSpiders; u++)
        {
            Vector3 spawnpos = new Vector3(SpawnPoints[u].position.x, SpawnPoints[u].position.y, 0);
            Instantiate(MiniSpider, spawnpos, Quaternion.identity);
        }
    }
}
