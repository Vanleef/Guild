using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNpcText : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    private Transform player;
    [SerializeField]
    private float talkDistance;
    private bool checkPuzzleSong = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (canvas.enabled == true) {
            canvas.gameObject.SetActive(false);
        }
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) < talkDistance) {
            canvas.gameObject.SetActive(true);
            StartPuzzleMusic();
        } else {
            canvas.gameObject.SetActive(false);
        }
    }

    private void StartPuzzleMusic() {
        if (!checkPuzzleSong)
            {
                SoundManager.instance.Play("Puzzle");
                checkPuzzleSong = true;
            }
    }
}
