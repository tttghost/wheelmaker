using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

#region core
[Serializable]
public class Wrapper<T>
{
    public T[] array;
}
#endregion

public static partial class Util
{
    #region Json - json기본기능


    /// <summary>
    /// json Beautify(줄맞춤)
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static string Beautify(string jsonString)
    {
        try
        {
            string beautifiedJson = JValue.Parse(jsonString).ToString(Newtonsoft.Json.Formatting.Indented);
            return beautifiedJson;
        }
        catch
        {
            return jsonString;
        }
    }

    /// <summary>
    /// JSON 배열을 파싱하는 헬퍼 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T[] FromJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    /// <summary>
    /// JSON 배열을 파싱하는 헬퍼 메서드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static List<T> FromJsonList<T>(string json)
    {
        return new List<T>(FromJsonArray<T>(json));
    }

    #endregion



    #region Dictionary - 딕셔너리 하위 STL 추가,제거 기능

    /// <summary>
    /// 딕셔너리의 값인 리스트에 데이터 추가
    /// </summary>
    /// <typeparam name="T1">타입1</typeparam>
    /// <typeparam name="T2">타입2</typeparam>
    /// <param name="dic">딕셔너리</param>
    /// <param name="data">추가할 데이터</param>
    /// <param name="key">키값</param>
    public static void DicList<T1, T2>(ref Dictionary<T1, List<T2>> dic, T1 key, T2 data)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new List<T2>());
        }
        dic[key].Add(data);
    }

    /// <summary>
    /// 딕셔너리 값인 큐에 데이터 추가
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dic"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    public static void DicQueue<T1, T2>(ref Dictionary<T1, Queue<T2>> dic, T1 key, T2 data)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new Queue<T2>());
        }
        dic[key].Enqueue(data);
    }

    #endregion



    #region Search - 이름으로 GameObject or Transform 하위의 GameObject or Component 찾기

    /// <summary>
    /// 해당 오브젝트 하위에서 특정 이름의 컴포넌트 서치
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_gameObject"></param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static T Search<T>(this GameObject _gameObject, string _name) where T : UnityEngine.Object
    {
        return _gameObject.transform.Search<T>(_name);
    }

    /// <summary>
    /// 해당 오브젝트 하위에서 특정 이름의 컴포넌트 서치
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_obj"></param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static T Search<T>(this Transform _transform, string _name) where T : UnityEngine.Object
    {
        var go = Search(_transform, _name);

        if (go == null)
        {
            return null;
        }

        if (typeof(T) == typeof(GameObject))
        {
            return go as T;
        }
        else
        {
            return go.GetComponent<T>();
        }
    }

    /// <summary>
    /// 해당 오브젝트 하위에서 특정 이름의 트랜스폼 서치
    /// </summary>
    /// <param name="_obj"></param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static GameObject SearchGameObject(this GameObject _obj, string _name)
    {
        Transform trnasform = Search(_obj.transform, _name);
        return trnasform != null ? trnasform.gameObject : null;

    }

    /// <summary>
    /// 자식 트랜스폼 찾기
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static Transform Search(this Transform _target, string _name)
    {
        if (_target.name == _name) return _target;

        for (int i = 0; i < _target.childCount; ++i)
        {
            var result = Search(_target.GetChild(i), _name);

            if (result != null) return result;
        }

        return null;
    }

    /// <summary>
    /// 부모 트랜스폼 찾기
    /// </summary>
    /// <param name="_target"></param>
    /// <param name="_name"></param>
    /// <returns></returns>
    public static Transform SearchParent(this Transform _target, string _name)
    {
        if (_target.name == _name) return _target;

        if (_target.parent == null) return null;

        if (_target.parent.name == _name)
        {
            return _target.parent;
        }
        else
        {
            return SearchParent(_target.parent, _name);
        }
    }

    #endregion



    #region setdata - 컴포넌트에 부가기능 추가
    /// <summary>
    /// 로컬라이징
    /// </summary>
    /// <param name="tMP_Text"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static TMP_Text SetData(this TMP_Text tMP_Text, LocalizionData data)
    {
        LocalizationEvent localizationEvent = tMP_Text.GetOrAddComponent<LocalizationEvent>();
        localizationEvent.SetLocalizing(data);
        return tMP_Text;
    }

    //public static Button SetData(this Button button, Action action, eAudioClips eAudioClips = eAudioClips.effect_click, float volumeScale = 1f)
    //{
    //    button.onClick.AddListener(() =>
    //    {
    //        action?.Invoke();
    //        SoundManager.instance.PlayEffect(eAudioClips, volumeScale);
    //    });

    //    return button;
    //}

    //public static Toggle SetData(this Toggle tog, Action action = null, Action<bool> actionBool = null, eAudioClips eAudioClips = eAudioClips.effect_click, float volumeScale = 1f)
    //{
    //    tog.onValueChanged.AddListener((b) =>
    //    {
    //        action?.Invoke();
    //        actionBool?.Invoke(b);
    //        SoundManager.instance.PlayEffect(eAudioClips, volumeScale);
    //    });

    //    return tog;
    //}

    #endregion



    #region Enum - 이넘 의 길이, string to enum 등의 기능

    /// <summary>
    /// enum을 stringArray로 변환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string[] Enum2StringArray<T>()
    {
        return Enum.GetNames(typeof(T));
    }

    /// <summary>
    /// enum을 enumArray로 변환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] Enum2EnumArray<T>()
    {
        return Enum.GetValues(typeof(T)) as T[];
    }

    /// <summary>
    /// enum원형의 길이 구하기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int EnumLength<T>()
    {
        return Enum.GetNames(typeof(T)).Length;
    }

    /// <summary>
    /// string을 enum으로 변환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_str"></param>
    /// <returns></returns>
    public static T String2Enum<T>(string _str)
    {
        try { return (T)Enum.Parse(typeof(T), _str); }
        catch
        {
            try { return (T)Enum.Parse(typeof(T), "none"); }
            catch
            {
                Debug.LogError(_str + " enum 없음");
                return default;
            }
        }
    }

    /// <summary>
    /// enum을 string으로 변환
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_enum"></param>
    /// <returns></returns>
    public static string Enum2String<T>(T _enum) where T : Enum
    {
        try { return Enum.GetName(typeof(T), _enum); }
        catch { return String.Empty; }
    }

    /// <summary>
    /// enum의 숫자를 string으로 변환
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string EnumInt2String(object obj)
    {
        return ((int)obj).ToString();
    }

    #endregion



    #region etc - 분류하기엔 크기가 작은 기능들

    public static void DestroyChildrenGameObject(Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 고정스케일로 변경
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="parent"></param>
    /// <param name="scale"></param>
    public static void lossyscale(Transform tr, Transform parent, float scale)
    {
        tr.SetParent(null);
        tr.localScale = Vector3.one * scale;
        tr.SetParent(parent);
        tr.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 텍스쳐를 스프라이트로 변경
    /// </summary>
    /// <param name="_tex"></param>
    /// <returns></returns>
    public static Sprite Tex2Sprite(Texture2D _tex)
    {
        return Sprite.Create(_tex, new Rect(0, 0, _tex.width, _tex.height), new Vector2(0.5f, 0.5f));
    }

    /// <summary>
    /// 컨텐츠사이즈피터 버그가 있어 레이아웃 갱신하는 함수
    /// </summary>
    /// <param name="go"></param>
    /// <param name="objName"></param>
    public static void RefreshLayout(GameObject go, string objName)
    {
        RectTransform rectTr = Search<RectTransform>(go, objName);
        if (!rectTr)
        {
            Debug.Log("리프레쉬할게없음");
            return;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTr);
    }

    /// <summary>
    /// 렌더모드 변경(Opaque,Transparent)
    /// </summary>
    /// <param name="standardShaderMaterial"></param>
    /// <param name="blendMode"></param>
    public static void ChangeRenderMode(Material standardShaderMaterial, BlendMode blendMode)
    {
        //switch (blendMode)
        //{
        //    case BlendMode.Opaque:
        //        standardShaderMaterial.SetFloat("_Surface", 0); // 0 = Opaque, 1 = Transparent
        //        standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        //        standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        //        standardShaderMaterial.SetInt("_ZWrite", 1);
        //        standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //        standardShaderMaterial.renderQueue = -1;
        //        // URP 키워드 설정
        //        standardShaderMaterial.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
        //        standardShaderMaterial.EnableKeyword("_SURFACE_TYPE_OPAQUE");
        //        break;
        //    case BlendMode.Cutout:
        //        standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        //        standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        //        standardShaderMaterial.SetInt("_ZWrite", 1);
        //        standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //        standardShaderMaterial.renderQueue = 2450;
        //        break;
        //    case BlendMode.Fade:
        //        standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //        standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //        standardShaderMaterial.SetInt("_ZWrite", 0);
        //        standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
        //        standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //        standardShaderMaterial.renderQueue = 3000;
        //        break;
        //    case BlendMode.Transparent:
        //        standardShaderMaterial.SetFloat("_Surface", 1); // 0 = Opaque, 1 = Transparent
        //        standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //        standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //        standardShaderMaterial.SetInt("_ZWrite", 0);
        //        standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
        //        standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
        //        standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //        standardShaderMaterial.renderQueue = 3001;
        //        standardShaderMaterial.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        //        standardShaderMaterial.DisableKeyword("_SURFACE_TYPE_OPAQUE");
        //        break;
        //}
    }

    /// <summary>
    /// GetComponentsInChildren 강화버전 (비활성화 상태도 호출가능)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_target"></param>
    /// <returns></returns>
    public static List<T> GetComponentsInChildrenPlus<T>(this Transform _target) where T : Component
    {
        List<T> list = new List<T>();
        GetComponentsInChildrenPlus(_target, ref list);
        return list;
    }

    private static void GetComponentsInChildrenPlus<T>(this Transform _target, ref List<T> list) where T : Component
    {
        for (int i = 0; i < _target.childCount; ++i)
        {
            Transform childTr = _target.GetChild(i);
            if (childTr.TryGetComponent(out T t))
            {
                list.Add(t);
            }
            GetComponentsInChildrenPlus(childTr, ref list);
        }
    }

    /// <summary>
    /// 컴포넌트가 없으면 추가하고 있으면 가져오고
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        if (!go.TryGetComponent(out T t))
        {
            t = go.AddComponent<T>();
        }
        return t;
    }


    #endregion



    #region renderer, material, shader
    //독립적으로 코루틴 돌아가게 하기 위한 dictionary
    private static Dictionary<int, Coroutine> coroutines = new Dictionary<int, Coroutine>();

    /// <summary>
    /// shader의 특정 float 값 fade 처리
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <param name="name"></param>
    /// <param name="st"></param>
    /// <param name="en"></param>
    public static void ShaderFade_Float(MeshRenderer meshRenderer, string name, float st, float en, float duration = 0.5f)
    {
        int id = meshRenderer.GetInstanceID();
        Coroutine currentCoroutine = null;

        if (coroutines.ContainsKey(id))
        {
            currentCoroutine = coroutines[id];
        }

        if (currentCoroutine != null)
        {
            //Space_3.instance.StopCoroutine(currentCoroutine);
            coroutines.Remove(id);
        }

        //currentCoroutine = Space_3.instance.StartCoroutine(Co_ShaderFade_Float(meshRenderer, name, st, en, duration));
        coroutines.Add(id, currentCoroutine);
    }

    /// <summary>
    /// shader의 특정 color 값 fade 처리
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <param name="name"></param>
    /// <param name="st"></param>
    /// <param name="en"></param>
    /// <param name="duration"></param>
    public static void ShaderFade_Color(MeshRenderer meshRenderer, string name, Color st, Color en, float duration = 0.5f)
    {
        int id = meshRenderer.GetInstanceID();
        Coroutine currentCoroutine = null;

        if (coroutines.ContainsKey(id))
        {
            currentCoroutine = coroutines[id];
        }

        if (currentCoroutine != null)
        {
            //Space_3.instance.StopCoroutine(currentCoroutine);
            coroutines.Remove(id);
        }

        //currentCoroutine = Space_3.instance.StartCoroutine(Co_ShaderFade_Color(meshRenderer, name, st, en, duration));
        //coroutines.Add(id, currentCoroutine);
    }

    /// <summary>
    /// 메쉬 페이드
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <param name="name"></param>
    /// <param name="st"></param>
    /// <param name="en"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static async UniTask Co_ShaderFade_Float(MeshRenderer meshRenderer, string name, float st, float en, float duration)
    {
        float curTime = 0;
        while (curTime < 1f)
        {
            meshRenderer.material.SetFloat(name, Mathf.Lerp(st, en, curTime += Time.deltaTime / duration));
            await UniTask.Yield();
        }
        meshRenderer.material.SetFloat(name, en);
    }

    /// <summary>
    /// 메쉬-컬러 페이드
    /// </summary>
    /// <param name="meshRenderer"></param>
    /// <param name="name"></param>
    /// <param name="st"></param>
    /// <param name="en"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    public static async UniTask Co_ShaderFade_Color(MeshRenderer meshRenderer, string name, Color st, Color en, float duration)
    {
        float curTime = 0;
        while (curTime <= 1f)
        {
            meshRenderer.material.SetColor(name, Color.Lerp(st, en, curTime += Time.deltaTime / duration));
            await UniTask.Yield();
        }
        meshRenderer.material.SetColor(name, en);
    }

    /// <summary>
    /// 카메라 바라보기
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target"></param>
    public static void LookAtCamera(Transform transform, Vector3 target)
    {
        transform.LookAt(2 * transform.position - target);
        transform.eulerAngles = transform.eulerAngles.y * Vector3.up;
    }

    /// <summary>
    /// return : -180 ~ 180 degree (for unity)
    /// 두 벡터 사이의 각도
    /// </summary>
    /// <param name="vStart"></param>
    /// <param name="vEnd"></param>
    /// <returns></returns>
    public static float GetBetweenAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public static float GetViewingAngle(Vector3 cameraPosition, Vector3 cameraForward, Vector3 objectPosition)
    {
        // 카메라에서 물체를 바라보는 방향 벡터
        Vector3 directionToObject = objectPosition - cameraPosition;

        // 카메라의 forward 방향과 물체 방향 사이의 각도를 구합니다.
        float angle = Vector3.SignedAngle(cameraForward, directionToObject, Vector3.up);

        return angle;
    }

    #endregion



    #region lerp - uniTask로 lerp 처리

    ///// <summary>
    ///// rectTransform의 피봇
    ///// </summary>
    ///// <param name="rectTransform"></param>
    ///// <param name="start"></param>
    ///// <param name="end"></param>
    ///// <param name="ease"></param>
    ///// <returns></returns>
    //public static async UniTask Lerp_RectTransform_Pivot(RectTransform rectTransform, Vector3 start, Vector3 end, EasingFunction.Ease ease = EasingFunction.Ease.Linear)
    //{
    //    float curTime = 0f;
    //    float durTime = 0.5f;
    //    EasingFunction.Function function = EasingFunction.GetEasingFunction(ease);

    //    while (curTime <= 1f)
    //    {
    //        rectTransform.pivot = Vector3.Lerp(start, end, function(0f, 1f, curTime += Time.deltaTime / durTime));
    //        await UniTask.Yield();
    //    }

    //    rectTransform.pivot = end;
    //}

    ///// <summary>
    ///// rectTransform의 엥커드포지션
    ///// </summary>
    ///// <param name="rectTransform"></param>
    ///// <param name="start"></param>
    ///// <param name="end"></param>
    ///// <param name="ease"></param>
    ///// <returns></returns>
    //public static async UniTask Lerp_RectTransform_AnchoredPosition(RectTransform rectTransform, Vector3 start, Vector3 end, EasingFunction.Ease ease = EasingFunction.Ease.Linear, CancellationToken token = default)
    //{
    //    float curTime = 0f;
    //    float durTime = 0.2f;
    //    EasingFunction.Function function = EasingFunction.GetEasingFunction(ease);

    //    while (curTime <= 1f)
    //    {
    //        rectTransform.anchoredPosition = Vector3.Lerp(start, end, function(0f, 1f, curTime += Time.deltaTime / durTime));
    //        await UniTask.Yield(cancellationToken: token);
    //    }
    //    rectTransform.anchoredPosition = end;
    //}


    ///// <summary>
    ///// canvasGroup의 알파
    ///// </summary>
    ///// <param name="canvasGroup"></param>
    ///// <param name="start"></param>
    ///// <param name="end"></param>
    ///// <param name="ease"></param>
    ///// <returns></returns>
    //public static async UniTask Lerp_CanvasGroup_Alpha(CanvasGroup canvasGroup, float start, float end, EasingFunction.Ease ease = EasingFunction.Ease.Linear, CancellationToken token = default)
    //{
    //    float curTime = 0f;
    //    float durTime = 0.2f;
    //    EasingFunction.Function function = EasingFunction.GetEasingFunction(ease);

    //    while (curTime <= 1f)
    //    {
    //        canvasGroup.alpha = Mathf.Lerp(start, end, function(0f, 1f, curTime += Time.deltaTime / durTime));
    //        await UniTask.Yield(cancellationToken: token);
    //    }

    //    canvasGroup.alpha = end;
    //}

    #endregion



    #region localizingEvent
    /// <summary>
    /// 로컬라이제이션(다국어) 가져오기
    /// </summary>
    /// <param name="id"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string GetLocalization(LocalizionData masterLocalData)
    {
        return GetLocalization(masterLocalData.id, masterLocalData.args);
    }
    public static string GetLocalization(string id, params object[] args)
    {
        if (string.IsNullOrEmpty(id)) return "";
        var localizeData = DBManager.instance.GetLocalizeData(id);

        string language;
        string result;
        LANGUAGE lANGUAGE = LANGUAGE.Korean;
        try
        {
            switch (lANGUAGE)
            {
                case LANGUAGE.Korean:
                    language = localizeData.ko; break;
                case LANGUAGE.English:
                default:
                    language = localizeData.en; break;
            };
            result = args == null || args.Length == 0 ? language : string.Format(language, args);
        }
        catch
        {
            //result = $"로컬라이징필요 - {id}";
            result = id;
        }
        return result;
    }

    #endregion



    #region Layer - 레이어 부가기능

    /// <summary>
    /// 특정레이어 인덱스 가져오기
    /// </summary>
    /// <param name="layerName"></param>
    /// <returns></returns>
    public static int GetLayerIndex(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }

    /// <summary>
    /// 특정레이어 레이어마스크
    /// </summary>
    /// <param name="layerName"></param>
    /// <returns></returns>
    public static int GetLayerMask(string layerName)
    {
        return 1 << LayerMask.NameToLayer(layerName);
    }

    /// <summary>
    /// 특정레이어만 제외 레이어마스크
    /// </summary>
    /// <param name="layerName"></param>
    /// <returns></returns>
    public static int ExceptLayerMask(string layerName)
    {
        return -1 - GetLayerMask(layerName);
    }
    #endregion


    #region delegate - 이벤트 형태 모아놓음
    public delegate void Handler_Void();
    public delegate void Handler_Bool(bool b);
    public delegate void Handler_Int(int i);
    public delegate void Handler_FloatInt(float f, int i);
    public delegate void Handler_Float(float f);
    public delegate void Handler_String(string s);
    public delegate void Handler_RaycastHit(RaycastHit hit);
    #endregion

    /// <summary>
    /// 러프 인벌스러프
    /// </summary>
    /// <param name="fromA"></param>
    /// <param name="fromB"></param>
    /// <param name="toA"></param>
    /// <param name="toB"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static float LerpInverseLerp(float fromA, float fromB, float toA, float toB, float val)
    {
        return Mathf.Lerp(toA, toB, Mathf.InverseLerp(fromA, fromB, val));
    }

    #region setdata - 컴포넌트에 부가기능 추가

    public static Button SetData(this Button btn, Action action, /*eAudioClips eAudioClips = eAudioClips.effect_click,*/ float volumeScale = 1f)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
            action?.Invoke();
            //SoundManager.instance.PlayEffect(eAudioClips, volumeScale);
        });

        return btn;
    }

    public static Toggle SetData(this Toggle tog, Action action = null, Action<bool> actionBool = null, /*eAudioClips eAudioClips = eAudioClips.effect_click,*/ float volumeScale = 1f)
    {
        tog.onValueChanged.RemoveAllListeners();
        tog.onValueChanged.AddListener((b) =>
        {
            action?.Invoke();
            actionBool?.Invoke(b);
            //SoundManager.instance.PlayEffect(eAudioClips, volumeScale);
        });

        return tog;
    }
    #endregion


    public enum AnchorPreset
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        StretchTop,
        StretchMiddle,
        StretchBottom,
        StretchLeft,
        StretchCenter,
        StretchRight,
        StretchAll
    }

    public static void SetAnchor(RectTransform rectTransform, AnchorPreset preset)
    {
        switch (preset)
        {
            // 일반적인 앵커 프리셋
            case AnchorPreset.TopLeft:
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 1);
                break;
            case AnchorPreset.TopCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 1);
                rectTransform.anchorMax = new Vector2(0.5f, 1);
                rectTransform.pivot = new Vector2(0.5f, 1);
                break;
            case AnchorPreset.TopRight:
                rectTransform.anchorMin = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(1, 1);
                break;
            case AnchorPreset.MiddleLeft:
                rectTransform.anchorMin = new Vector2(0, 0.5f);
                rectTransform.anchorMax = new Vector2(0, 0.5f);
                rectTransform.pivot = new Vector2(0, 0.5f);
                break;
            case AnchorPreset.MiddleCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                break;
            case AnchorPreset.MiddleRight:
                rectTransform.anchorMin = new Vector2(1, 0.5f);
                rectTransform.anchorMax = new Vector2(1, 0.5f);
                rectTransform.pivot = new Vector2(1, 0.5f);
                break;
            case AnchorPreset.BottomLeft:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                rectTransform.pivot = new Vector2(0, 0);
                break;
            case AnchorPreset.BottomCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 0);
                rectTransform.anchorMax = new Vector2(0.5f, 0);
                rectTransform.pivot = new Vector2(0.5f, 0);
                break;
            case AnchorPreset.BottomRight:
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.pivot = new Vector2(1, 0);
                break;

            // Stretch 모드 프리셋
            case AnchorPreset.StretchTop:
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(0.5f, 1);
                rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y); // left와 right는 0으로
                rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y); // 상단 스트레치이므로 y 값은 유지
                break;
            case AnchorPreset.StretchMiddle:
                rectTransform.anchorMin = new Vector2(0, 0.5f);
                rectTransform.anchorMax = new Vector2(1, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y); // left와 right는 0으로
                rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y); // middle 스트레치이므로 y 값은 유지
                break;
            case AnchorPreset.StretchBottom:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 0);
                rectTransform.pivot = new Vector2(0.5f, 0);
                rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y); // left와 right는 0으로
                rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y); // 하단 스트레치이므로 y 값은 유지
                break;
            case AnchorPreset.StretchLeft:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 0.5f);
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0); // top과 bottom은 0으로
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0); // 좌측 스트레치이므로 x 값은 유지
                break;
            case AnchorPreset.StretchCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 0);
                rectTransform.anchorMax = new Vector2(0.5f, 1);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0); // top과 bottom은 0으로
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0); // 가운데 스트레치이므로 x 값은 유지
                break;
            case AnchorPreset.StretchRight:
                rectTransform.anchorMin = new Vector2(1, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(1, 0.5f);
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0); // top과 bottom은 0으로
                rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0); // 우측 스트레치이므로 x 값은 유지
                break;
            case AnchorPreset.StretchAll:
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(1, 1);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.offsetMin = Vector2.zero; // 모든 방향에 대해 오프셋 0
                rectTransform.offsetMax = Vector2.zero;
                break;
        }
    }
}

