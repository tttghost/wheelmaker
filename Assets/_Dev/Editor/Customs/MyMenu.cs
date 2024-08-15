using UnityEditor;
using UnityEngine;

public class MyMenu : MonoBehaviour
{
    [MenuItem("MyMenu/" + nameof(RemoveMyStatus))]
    private static void RemoveMyStatus()
    {
        if(PlayerPrefs.HasKey(nameof(MyStatus)))
        {
            PlayerPrefs.DeleteKey(nameof(MyStatus));
            Debug.Log("Remove MyStatus Success");
        }
        else
        {
            Debug.Log("None Data");
        }
    }
}
