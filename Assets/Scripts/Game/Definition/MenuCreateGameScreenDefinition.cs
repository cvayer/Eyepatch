using UnityEngine;
using UnityEditor;
using System.Collections;
using Pebble;

class MenuCreateGameDefinition : MenuCreateBase
{
    [MenuItem("Assets/Create/Eyepatch/Definition/Game")]
    public static void Create()
    {
        Create<GameDefinition>("GameDefinition");
    }
}
