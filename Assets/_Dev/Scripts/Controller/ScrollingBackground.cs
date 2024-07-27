using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.1f;
    public Vector2 direction = new Vector2(-1, 0);
    //public float repeatWidth = 10.0f;
    public float repeatSize = 5.0f;
    private RawImage rawImage;
    //private Vector2 textureSize;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        //textureSize = new Vector2(rawImage.texture.width, rawImage.texture.height);
    }

    void Update()
    {
        //float xOffset = Mathf.Repeat(Time.time * speed * direction.x, repeatWidth) / repeatWidth;
        float xOffset = Mathf.Repeat(Time.time * speed * direction.x, 1.0f);
        float yOffset = Mathf.Repeat(Time.time * speed * direction.y, 1.0f);

        rawImage.uvRect = new Rect(xOffset, yOffset, repeatSize, repeatSize);
    }

}
