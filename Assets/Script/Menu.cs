using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameManager gameMngr;

    public void btn_continue()
    {
        gameMngr.changeScene(Scenes.port);
    }
}
