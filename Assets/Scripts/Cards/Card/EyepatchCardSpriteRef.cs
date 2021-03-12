using Sirenix.OdinInspector;
using UnityEngine;
using System;
using Pebble;

//-------------------------------------------------------
//-------------------------------------------------------
// EyepatchCardSpriteRef
//-------------------------------------------------------
//-------------------------------------------------------
[Serializable]
public class EyepatchCardSpriteRef
{
    public Card32Value Value;
    public Card32Family Family;

    [AssetsOnly]
    public Sprite Prefab;
}