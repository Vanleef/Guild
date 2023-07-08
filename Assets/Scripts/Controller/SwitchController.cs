using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    int characterOn = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SwitchCharacter(){
        int nextChar = GetNextChar();
        PlayerController.Instance.characters[characterOn].SetActive(false);
        PlayerController.Instance.characters[nextChar].SetActive(true);
        characterOn = nextChar;
    }

    private int GetNextChar(){
        int nextChar = characterOn;
        bool foundNext = false;
        while(!foundNext){
            if(nextChar == 2)
                nextChar = 0;
            else
                nextChar++;

            if(PlayerController.Instance.getCharacterHP(nextChar) > 0)
                foundNext = true;
        }

        return nextChar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            SwitchCharacter();
        }
    }
}
