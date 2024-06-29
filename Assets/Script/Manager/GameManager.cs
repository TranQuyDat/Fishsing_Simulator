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
    public inventory iv;
    public GameObject instructionBTN;
    public cameraFollow mainCamera;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        mainCamera = FindObjectOfType<cameraFollow>();
    }
    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.resetInput();

            Debug.Log("Update input");
        }
    }

    private void Update()
    {
    }


    public void changeScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}
