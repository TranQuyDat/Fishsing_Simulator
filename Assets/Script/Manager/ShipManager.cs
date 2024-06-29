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

    public TriggerChangeScene triggerChange;
    // Start is called before the first frame update
    private void Awake()
    {
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
        if(curShip == null && it_Equiped != null)
        {
            setActiveShips();
        }
    }

    public void setActiveShips()
    {
        foreach (GameObject obj in ships)
        {
            if (obj == null)
            {
                ships.Remove(obj);
                continue;
            }
                obj.SetActive(it_Equiped != null  && obj.name == it_Equiped.nameItem);
            if (it_Equiped != null && obj.name == it_Equiped.nameItem)
            {
                curShip = obj;
                curShipCtrl = obj.GetComponent<BoatController>();

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
