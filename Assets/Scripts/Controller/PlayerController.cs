using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour

{
    public static PlayerController Instance { get; set; }
    public List<GameObject> characters;

    private float lastSwitch = -9999f;

    public float switchCooldown;


    public PlayerActions player;

    // Start is called before the first frame update
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
        InitChars();
        DontDestroyOnLoad(this.gameObject);
    }

    void InitChars()
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }
        characters[0].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch") && lastSwitch + switchCooldown < Time.time)
        {
            SwitchCharacter();
            lastSwitch = Time.time;
        }
    }

    public void DamagePlayer(GameObject target, int damage)
    {
        int index = getActiveCharacterIndex();
        player = characters[index].GetComponent<PlayerActions>();
        player.TakeDamage(damage);
        if (!characters[index].GetComponent<PlayerActions>().isAlive)
        {
            if (!SwitchCharacter())
            {
                GameController.Instance.GameOver();
            }
        }

    }

    public bool SwitchCharacter()
    {
        int currentChar = getActiveCharacterIndex();
        int nextChar = GetNextChar(currentChar);
        if (characters[nextChar].GetComponent<PlayerActions>().isAlive)
        {
            int currenthp = getCharacterHP(nextChar);
            Debug.Log(currenthp);
            characters[currentChar].SetActive(false);
            characters[nextChar].SetActive(true);
            characters[nextChar].GetComponent<PlayerActions>().currentHp = currenthp;
            return true;
        }
        else
        {
            return false;
        }
    }

    private int GetNextChar(int currentChar)
    {
        int nextChar = currentChar;
        int tries = 0;
        while (tries < 2)
        {
            if (nextChar == 2)
            {
                nextChar = 0;
            }
            else
            {
                nextChar++;
            }

            if (characters[nextChar].GetComponent<PlayerActions>().isAlive)
            {
                break;
            }
            tries++;
        }

        return nextChar;

    }
    public void Debuff(){
        gameObject.GetComponent<Rigidbody2D>().drag = 20f;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 6f;
        StartCoroutine(Buff());
    }
    private IEnumerator Buff(){
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Rigidbody2D>().drag = 0f;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 4f;
    }
    public int getActiveCharacterIndex()
    {
        foreach (GameObject character in characters)
        {
            if (character.activeSelf)
            {
                return characters.IndexOf(character);
            }
        }
        return -1;
    }

    public int getCharacterHP(int index)
    {
        return characters[index].GetComponent<PlayerActions>().currentHp;
    }

    public void RevivePlayer()
    {
        foreach (GameObject character in characters)
        {
            int maxHp = character.GetComponent<PlayerActions>().character.HP;
            character.GetComponent<PlayerActions>().isAlive = true;
            character.GetComponent<PlayerActions>().currentHp = maxHp;
        }
    }

    public void healPlayer()
    {
        SoundManager.instance.Play("Potion");
        int index = getActiveCharacterIndex();
        int currentHp = characters[index].GetComponent<PlayerActions>().currentHp;
        int maxHp = characters[index].GetComponent<PlayerActions>().character.HP;
        if (currentHp > maxHp - 20)
        {
            characters[index].GetComponent<PlayerActions>().currentHp = maxHp;
        }else
        {
            characters[index].GetComponent<PlayerActions>().currentHp += 20;
        }
    }
}
