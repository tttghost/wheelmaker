using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdButton : MonoBehaviour
{
    public void Awake()
    {
        
    }

    public void Init(IAdManager adManager)
    {
        this.adManager = adManager;
    }
    IAdManager adManager;

    public void OnClickAd()
    {
        adManager.PlayAd();
    }
}
