using Sirenix.OdinInspector;
using UnityEngine;
using Pebble;

//----------------------------------------------
//----------------------------------------------
// CardStaticData
//----------------------------------------------
//----------------------------------------------
public class CardStaticData : Singleton<CardStaticData>
{
    public CardSpriteRef[] CardSprites;

    [AssetsOnly]
    public Sprite BackSprite;

    [AssetsOnly]
    public GameObject Prefab;
}