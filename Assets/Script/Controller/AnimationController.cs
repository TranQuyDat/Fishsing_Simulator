using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator ani;
    PlayerController playerCtrl;
    Action actIsRunning;
    Dictionary<Action, AnimationClip> animationDic;
    #region defalult method
    private void Awake()
    {
        playerCtrl = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateActAnim(playerCtrl.cur_action);
    }
    #endregion

    public void updateActAnim(Action ac)
    {
        if (ac == actIsRunning) return;
        
        actIsRunning = ac;

        ani.SetBool("isrun", ac == Action.running);
        ani.SetBool("isSleeping", ac == Action.sleeping);
        ani.SetBool("isSitting", ac == Action.sitting);
        ani.SetBool("isFishing_cast", ac == Action.fishing_cast);
        ani.SetBool("isFishing_idle", ac == Action.fishing_idle);
    }

    public void nextAction(Action ac)
    {

        playerCtrl.cur_action = ac;
    }
}
