using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Image logo;
    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private GameObject storyTell;
    [SerializeField]
    [Range(0, 100)]
    private float moveUpTime;
    [SerializeField]
    [Range(0, 100)]
    private float fadeOutTime;
    

    // Start is called before the first frame update
    void Start()
    {
        storyTell.SetActive(false);
        settings.SetActive(false);
        SoundManager.instance.Play("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenSettings(!settings.activeSelf);
        }
    }

    public void OpenSettings(bool show)
    {
        settings.SetActive(show);
    }

    public void OpenStoryTell()
    {
        SoundManager.instance.Play("Story");
        storyTell.SetActive(true);
        storyTell.transform.Find("Story").DOMoveY(transform.position.y, moveUpTime).OnComplete(
            () => Loader.Load(Loader.Scene.Game));
            
    }

    public void CloseGame()
    {
        Application.Quit();
    }


}
