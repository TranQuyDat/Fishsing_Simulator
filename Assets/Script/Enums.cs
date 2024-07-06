using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum area
{
    home,
    ship,
    port,
    city

}
public enum tag
{
    player,
    ship
}
public enum Action
{
    //action of player
    idle,running,sitting,fishing_idle,fishing_cast,fishing_reel,sleeping,
    
    //action of fish
    checkBait,eatBait,ateBait,
}
public enum Bait
{
    worm,
    rice,
    meat,
    fish,
}
public enum GizmosTyle
{
    drawDir,drawRec, drawLine,
}
public enum TyleItem
{
    fish,bait,ship,fish_Or_bait,
}
public enum Status
{
    equiped,Lock,unlock,normal,
}

public enum Scenes
{
    port,city,sea1,sea2, loading, menu,
}