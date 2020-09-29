using UnityEngine;
using UnityEditor;
using System.Collections;

class MenuCreateGameScreenDefinition : MenuCreateBase
{
    [MenuItem("Assets/Create/Eyepatch/Definition/GameScreen")]
    public static void Create()
    {
        Create<GameScreenDefinition>("GameScreenDefinition");
    }
}
