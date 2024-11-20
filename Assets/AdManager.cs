//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AdManager : MonoBehaviour
//{
//    public static AdManager instance;
//    private void Awake()
//    {
//        instance = this;
//    }

//    public void PlayAd()
//    {
//        Debug.Log("Play Google Ad");
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdManager
{
    public void PlayAd();
}

public class AdManager : MonoBehaviour, IAdManager
{

    public void PlayAd()
    {
        Debug.Log("Play Google Ad");
    }
}
