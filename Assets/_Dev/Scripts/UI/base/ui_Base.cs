using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Linq;
using UnityEngine;

public interface IUI
{
    void Open();
    void Close();
}

public class ui_Base : MonoBehaviour, IUI
{
    private bool isInit = false;
    public GameObject go_Root { get; private set; }
    public RectTransform rectTransform { get; private set; }
    public CanvasGroup canvasGroup { get; private set; }

    public virtual void Awake()
    {
        if (isInit)
        {
            return;
        }
        isInit = true;

        CacheComponent();
    }

    protected virtual void CacheComponent()
    {
        go_Root = gameObject.SearchGameObject(nameof(go_Root));

        if(go_Root == null)
        {
            go_Root = new GameObject(nameof(go_Root), typeof(RectTransform));
            go_Root.transform.SetParent(transform, false);
            Util.SetAnchor(go_Root.GetComponent<RectTransform>(), Util.AnchorPreset.StretchAll);
            gameObject.Children()
                .Where(x => x.name != nameof(go_Root)).ToList()
                .ForEach(x => x.transform.SetParent(go_Root.transform));
        }

        rectTransform = go_Root.GetComponent<RectTransform>();

        canvasGroup = go_Root.GetOrAddComponent<CanvasGroup>();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }


}
