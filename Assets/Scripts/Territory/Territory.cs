using System.Collections;
using UnityEngine;
using Pebble;
//----------------------------------------------
//----------------------------------------------
// EyepatchCard
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
        if (EyepatchCardStaticData.Instance.Prefab != null)
        {
            GameObject cardObj = Object.Instantiate(EyepatchCardStaticData.Instance.Prefab) as GameObject;
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
