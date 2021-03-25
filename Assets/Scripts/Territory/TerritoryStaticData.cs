using Sirenix.OdinInspector;
using UnityEngine;
using Pebble;

//----------------------------------------------
//----------------------------------------------
// CardStaticData
//----------------------------------------------
//----------------------------------------------
public class TerritoryStaticData : Singleton<TerritoryStaticData>
{
    public TerritoryDefinition[] Territories;

    [AssetsOnly]
    public GameObject Prefab;
}