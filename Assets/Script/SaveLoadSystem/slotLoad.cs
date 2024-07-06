using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class slotLoad : MonoBehaviour
{
    public TextMeshProUGUI nameFile;
    public TextMeshProUGUI dateTime;
    public void btnLoad()
    {
        gamePausePanel gamePause = GameObject.FindObjectOfType<gamePausePanel>();
        gamePause.btnLoad(this);
    }

    public void delete()
    {
        GameManager gameMngr = FindObjectOfType<GameManager>();
        gameMngr.saveLoadGame.deleteFileSave(this);
    }
}
