using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; set; }
    [SerializeField]
    private CharacterObject warrior;
    [SerializeField]
    private CharacterObject archer;
    [SerializeField]
    private CharacterObject mage;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject lifeBar;
    [SerializeField]
    private GameObject currentHp;
    [SerializeField]
    private GameObject maxHp;
    [SerializeField]
    private GameObject lifeBarArcher;
    [SerializeField]
    private GameObject currentHpArcher;
    [SerializeField]
    private GameObject maxHpArcher;
    [SerializeField]
    private GameObject lifeBarMage;
    [SerializeField]
    private GameObject currentHpMage;
    [SerializeField]
    private GameObject maxHpMage;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    [Range(0, 100)]
    private int dmg;

    private int actualLife;
    private int actualLifeArcher;
    private int actualLifeMage;

    [SerializeField]
    private GameObject soundPrefab;

    private SoundManager soundManager;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        if (SoundManager.instance == null) Instantiate(soundPrefab);

        soundManager = SoundManager.instance;

        soundManager.Play("Level 1");

        //Cursor.visible = false;
        gameOver.SetActive(false);
        pauseMenu.SetActive(false);
        actualLife = warrior.HP;
        actualLifeArcher = archer.HP;
        actualLifeMage = mage.HP;
        maxHp.GetComponent<TextMeshProUGUI>().text = actualLife.ToString();
        maxHpArcher.GetComponent<TextMeshProUGUI>().text = actualLifeArcher.ToString();
        maxHpMage.GetComponent<TextMeshProUGUI>().text = actualLifeMage.ToString();
        Physics2D.IgnoreLayerCollision(10,10);
        DontDestroyOnLoad(this);
    }

    void Awake()
    {

    }

    void Update()
    {
        SetLife();
        SetLifeArcher();
        SetLifeMage();

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Cursor.visible = true;
            Pause();
        }
    }

    public void SetLife()
    {
        PlayerController.Instance.getCharacterHP(0);
        Slider lifeSlider = lifeBar.GetComponent<Slider>();
        lifeSlider.SetValueWithoutNotify(PlayerController.Instance.getCharacterHP(0));
        currentHp.GetComponent<TextMeshProUGUI>().text = PlayerController.Instance.getCharacterHP(0).ToString();
    }

    public void SetLifeArcher( )
    {
        Slider lifeSlider = lifeBarArcher.GetComponent<Slider>();
        lifeSlider.SetValueWithoutNotify(PlayerController.Instance.getCharacterHP(1));
        currentHpArcher.GetComponent<TextMeshProUGUI>().text = PlayerController.Instance.getCharacterHP(1).ToString();
    }

    public void SetLifeMage( )
    {
        Slider lifeSlider = lifeBarMage.GetComponent<Slider>();
        lifeSlider.SetValueWithoutNotify(PlayerController.Instance.getCharacterHP(2));
        currentHpMage.GetComponent<TextMeshProUGUI>().text = PlayerController.Instance.getCharacterHP(2).ToString();
    }

    public void Resume()
    {
        //Cursor.visible = false;
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        Resume();
        //Cursor.visible = true;
        Loader.Load(Loader.Scene.StartMenu);

    }


    public void DamagePlayer(GameObject target, int damage)
    {
        PlayerController.Instance.DamagePlayer(target, damage);
        int life = PlayerController.Instance.player.currentHp;
        switch(PlayerController.Instance.getActiveCharacterIndex()){
            case 0: actualLife = life;
            break;
            case 1 : actualLifeArcher = life;
            break;
            case 2: actualLifeMage = life;
            break;
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);
        soundManager.Play("GameOver");
    }

    public void Retry()
    {
        soundManager.Play("Level 1");
        Vector3 checkpointPos = CheckpointController.Instance.lastCheckpointPos;
        GameObject.Find("Player").transform.position = checkpointPos;
        PlayerController.Instance.RevivePlayer();
        // Loader.Load(Loader.Scene.CheckPointTest);
        gameOver.SetActive(false);
        Resume();
        GameProgress.instance.resetProgress();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}