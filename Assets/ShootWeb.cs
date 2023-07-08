using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeb : MonoBehaviour
{
    [SerializeField]
    private GameObject Web;
    [SerializeField]
    private GameObject FirePoint;
    public void Shoot(){
        StartCoroutine(CallShootWithInterval());
    }
    public IEnumerator CallShootWithInterval()
    {
        List<Vector3> rotations = new List<Vector3>();
        rotations.Add(new Vector3(0.0f, 0.0f, 180.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 190.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 200.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 210.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 220.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 230.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 240.0f));
        rotations.Add(new Vector3(0.0f, 0.0f, 250.0f));
        for(int i = 0; i < 10; i++){
            int teste = Random.Range(0, 8);
            FirePoint.transform.eulerAngles = rotations[teste];
            Instantiate(Web, FirePoint.transform.position, FirePoint.transform.rotation);
            yield return new WaitForSeconds(2f);
        }
            
    }
}
