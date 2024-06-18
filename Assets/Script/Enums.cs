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
public enum bait
{
    fakeBait,
    woom,
    rice,
    meet,
    fish,
}
public enum GizmosTyle
{
    drawDir,drawRec, drawLine,
}
public enum TyleItem
{
    fish,bait,ship,
}
public enum Status
{
    equiped,Lock,unlock,normal,
}