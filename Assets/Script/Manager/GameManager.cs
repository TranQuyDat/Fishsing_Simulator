using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameUiManager uiMngr;
    public fishManager fishMngr;
    public ShipManager shipMngr;
    public PlayerController playerCtrl;
    public fishingRodController fishingRodCtrl;
    private void Update()
    {
    }



    public void changeScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
