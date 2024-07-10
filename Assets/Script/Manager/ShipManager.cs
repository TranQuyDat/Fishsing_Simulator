using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{

    public ItemData it_Equiped;
    public List<GameObject> ships;
    public GameObject curShip;
    public BoatController curShipCtrl;
    public storeShip st;
    public GameManager gameMngr;
    public TriggerChangeScene triggerChange;
    // Start is called before the first frame update
    private void Awake()
    {
        gameMngr = FindObjectOfType<GameManager>();
        foreach (slotItem_ship sl in st.slots)
        {
            bool b = sl.item == null || sl.item.status != Status.equiped;
            if (b) continue;
            it_Equiped = sl.item;
            break;
        }
        setActiveShips();
    }

    // Update is called once per frame
    void Update()
    {
        if( (it_Equiped !=null && curShip == null) || (it_Equiped == null && curShip != null))
        {
            setActiveShips();
        }
        updateShips();
    }

    public void updateShips()
    {
        if (gameMngr.playerCtrl == null || gameMngr.playerCtrl.transform.parent == null
            ||ships.Contains(gameMngr.playerCtrl.transform.parent.gameObject)) return;

        GameObject obj = gameMngr.playerCtrl.transform.parent.gameObject;
        ships.Add(obj);
        obj.transform.SetParent(gameMngr.shipMngr.transform);
    }
    public void setActiveShips()
    {
        for(int i = 0; i<ships.Count;i++)
        {
            if (ships[i] == null)
            {
                ships.Remove(ships[i]);
                continue;
            }
            ships[i].SetActive(it_Equiped != null  && ships[i].name == it_Equiped.nameItem);
            if (it_Equiped != null && ships[i].name == it_Equiped.nameItem)
            {
                curShip = ships[i];
                curShipCtrl = ships[i].GetComponent<BoatController>();

                if(triggerChange!=null) triggerChange.target = curShipCtrl.posSit.gameObject;
            }
        }
    }

    public void destroyShipslike(GameObject obj)
    {
        foreach(GameObject s in ships)
        {
            if(s.name == obj.name && s != obj)
            {
                ships.Remove(s);
                Destroy(s);
            }
        }
    }
}
