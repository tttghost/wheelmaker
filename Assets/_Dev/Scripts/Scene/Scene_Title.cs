using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Title : MonoBehaviour
{
    private void Awake()
    {
        DBManager.instance.Init();
    }
}
