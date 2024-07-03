using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class GameSaveUI
{
    public GameObject ui;
    public InputField NameField;
    public void setNameField(string text)
    {
        NameField.text = text;
    }
}
[System.Serializable]
public class GameLoadUI
{
    public GameObject ui;
}
[System.Serializable]
public class GameSettingUI
{
    public GameObject ui;
}
public class gamePausePanel : MonoBehaviour
{
    public GameManager gameMngr;
    public GameSaveUI gameSaveUI;
    public GameLoadUI gameLoadUI;
    public GameSettingUI gameSettingUI;
    public int countClick = 0;
    public bool isdoubleClick = false;
    // Start is called before the first frame update
    private void Awake()
    {
        gameMngr = GameObject.FindObjectOfType<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectBoxSave(slotSave slot)
    {
        gameSaveUI.setNameField(slot.nameFile.text);
    }
    public void btnSave(InputField inputfield)
    {
        gameMngr.saveLoadGame.save(inputfield.text,gameMngr.playerCtrl.scenes);
    }
    public void btnLoad(slotLoad slot)
    {
        StartCoroutine(checDoubleClick());
        if (!isdoubleClick)
        {
            return;
        }
            print("click"+slot.name);
        gameMngr.saveLoadGame.load(slot.nameFile.text);
    }
   
    
    IEnumerator checDoubleClick()
    {
        countClick+=1;
        if (countClick > 1)
        {
            isdoubleClick = true;
            countClick = 0;
            StopCoroutine(checDoubleClick());
        }
        yield return new WaitForSeconds(0.5f);
        if (countClick <= 1)
        {
            isdoubleClick = false;
            countClick = 0;
        }
        
    }
}
