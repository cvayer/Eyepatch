using System.Collections;
using UnityEngine;
using Pebble;
//----------------------------------------------
//----------------------------------------------
// Card
//----------------------------------------------
//----------------------------------------------
public partial class Territory
{
    //----------------------------------------------
    // Variables

    //----------------------------------------------
    public Territory()
    {
    }

    //----------------------------------------------
    public TerritoryComponent Spawn()
    {
        if (CardStaticData.Instance.Prefab != null)
        {
            GameObject cardObj = Object.Instantiate(CardStaticData.Instance.Prefab) as GameObject;
            TerritoryComponent territoryComp = cardObj.GetComponent<TerritoryComponent>();
            if(territoryComp != null)
            {
                territoryComp.Init(this);
            }
            return territoryComp;
        }
        return null;
    }
}
