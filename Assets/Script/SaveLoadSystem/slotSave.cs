using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class slotSave : MonoBehaviour
{
    public TextMeshProUGUI nameFile;
    public TextMeshProUGUI dateTime;
    public void btnSelect()
    {
        gamePausePanel gamePause = GameObject.FindObjectOfType<gamePausePanel>();
        gamePause.selectBoxSave(this);
    }

    public void delete()
    {
        GameManager gameMngr = FindObjectOfType<GameManager>();
        gameMngr.saveLoadGame.deleteFileSave(this);
    }
}
