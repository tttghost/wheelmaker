using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image img;
    public Renderer rd;
    
    // Start is called before the first frame update
    void Start()
    {
        AA().Forget();

    }
    
    
    async UniTask AA()
    {
        for (int i = 0; i < 10; i++)
        {
            img.color = Color.white * i * 0.1f;
            rd.material.color = Color.white * i * 0.1f;
            await Task.Delay(100);
        }
    }
}
