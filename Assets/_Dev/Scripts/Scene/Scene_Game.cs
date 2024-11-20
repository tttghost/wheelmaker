using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Game : MonoBehaviour
{
    private void Awake()
    {
        LevelUpController.instance.Init();
        GoldController.instance.Init();
    }
    private void Start()
    {
        UIManager.instance.OpenPanel<panel_Wheel>();
        UIManager.instance.OpenPanel<panel_Core>();
        UIManager.instance.OpenPanel<panel_LeftRight>();
        UIManager.instance.OpenPanel<panel_LevelUp>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(nameof(Scene_Title));
        }
    }
}
