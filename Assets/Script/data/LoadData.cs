using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "loadData", menuName = "data/loadData")]
public class LoadData : ScriptableObject
{
    public Scenes oldScene;
    public Scenes nextScene;
    public bool isloadFrSave;
    public DataSave dataSave;
    public float time;

    public string newNameSave;
    public void setLoadData(Scenes oldScene , Scenes scene, float time , bool isloadFrSave, DataSave dataSave)
    {
        this.oldScene = oldScene;
        this.nextScene = scene;
        this.time = time;
        this.isloadFrSave = isloadFrSave;
        this.dataSave = dataSave;

    }
}
