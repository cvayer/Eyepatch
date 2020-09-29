using Sirenix.OdinInspector;
using UnityEngine;
using System;
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