using Sirenix.OdinInspector;
using UnityEngine;
using Pebble;

//----------------------------------------------
//----------------------------------------------
// EyepatchCardStaticData
//----------------------------------------------
//----------------------------------------------
public class EyepatchCardStaticData : Singleton<EyepatchCardStaticData>
{
    public EyepatchCardSpriteRef[] CardSprites;

    [AssetsOnly]
    public Sprite BackSprite;

    [AssetsOnly]
    public GameObject Prefab;


    public Sprite GetSprite(Card32Value Value, Card32Family Family)
    {
        foreach(EyepatchCardSpriteRef cardRef in CardSprites)
        {
            if(cardRef.Value == Value && cardRef.Family == Family)
            {
                return cardRef.Prefab;
            }
        }
        return null;
    }
}