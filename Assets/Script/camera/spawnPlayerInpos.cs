using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayerInpos : MonoBehaviour
{
    PlayerController playerCTRL;
    public GameManager gameMngr;
    public bool isFaceRight = false;
    public Scenes newScene;
    public List<Scenes> oldScene;
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        if (gameMngr.playerCtrl != null && playerCTRL == null)
        {
            
            playerCTRL = gameMngr.playerCtrl;
            if (playerCTRL.transform.parent == null)
                spawnPlayer();
            else
            {
                spawnParentOfPlayer();
                bool b = playerCTRL == null || !oldScene.Contains(playerCTRL.scenes) || !playerCTRL.ischangeScene;

                
            }

        }
    }

    public void spawnPlayer()
    {
        bool b = playerCTRL == null || !oldScene.Contains(gameMngr.loadData.oldScene) || !playerCTRL.ischangeScene;
        if (b) return;
        float euler = (isFaceRight) ? playerCTRL.faceRight : playerCTRL.faceLeft;
        playerCTRL.transform.position = transform.position;
        playerCTRL.setFaceDir(euler);
        playerCTRL.ischangeScene = false;
        playerCTRL.scenes = newScene;
    }
    public void spawnParentOfPlayer()
    {
        bool b = playerCTRL == null || !oldScene.Contains(gameMngr.loadData.oldScene) || !playerCTRL.ischangeScene;
        if (b) return;
        playerCTRL.transform.parent.position = transform.position;

        playerCTRL.ischangeScene = false;
        playerCTRL.scenes = newScene;
        print("okok");
    }
}
