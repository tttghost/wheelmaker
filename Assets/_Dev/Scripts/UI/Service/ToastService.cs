using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastService : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            IToast toast = new ToastController(nameof(ToastBasic));
            toast.Open("hello!");
        }
    }
}
