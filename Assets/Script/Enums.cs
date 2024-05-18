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
    idle,run,sitting,fishing_idle,fishing_cast,sleeping,
    
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