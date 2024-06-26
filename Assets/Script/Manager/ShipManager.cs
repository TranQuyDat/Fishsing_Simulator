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
        
    }

    public void setActiveShips()
    {
        foreach (GameObject obj in ships)
        {
            obj.SetActive(it_Equiped != null  && obj.name == it_Equiped.nameItem);
            if (it_Equiped != null && obj.name == it_Equiped.nameItem)
            {
                curShip = obj;
                curShipCtrl = obj.GetComponent<BoatController>();

                triggerChange.target = curShipCtrl.posSit.gameObject;
            }
        }
    }

    public void destroyShipslike(GameObject obj)
    {
        foreach(GameObject s in ships)
        {
            if(s.name == obj.name && s != obj)
            {
                Destroy(s);
            }
        }
    }
}
