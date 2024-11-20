using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastBasic : MonoBehaviour, IToast
{
    public bool IsOpen { get; private set; }

    public void Close()
    {

    }

    public void Open(string message)
    {
        Debug.Log("열림");
    }
}
