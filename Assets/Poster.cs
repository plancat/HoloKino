using UnityEngine;
using System.Collections;

public class Poster : MonoBehaviour
{

    SpriteRenderer spriterenderer;

    [HideInInspector]
    public Vector3 movetopos = Vector3.zero;
    [HideInInspector]
    public float movetospeed = 0;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void OnSelect()
    {

    }

    void Update()
    {
        if(movetospeed > 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, movetopos, 0.1f * movetospeed);
            if(Vector3.Distance(transform.localPosition, movetopos) < 0.05f)
            {
                transform.localPosition = movetopos;
                movetospeed = 0;
                movetopos = Vector3.zero;
            }
        }

        Color c = spriterenderer.color;
        Vector3 v = transform.localPosition;

        if (v.x < -4)
        {
            spriterenderer.color = new Color(c.r, c.g, c.b, (8 - Mathf.Abs(v.x)) / 4);
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
        }
        else if (v.x >= -4 && v.x < 0)
        {
            float t = 0;

            t = 0.25f - Mathf.Abs(v.x / 16);

            transform.localScale = new Vector3(0.75f + t, 0.75f + t, 1);
        }
        else if (v.x >= 0 && v.x < 4)
        {
            float t = 0;

            t = (v.x / 16);

            transform.localScale = new Vector3(1 - t, 1 - t, 1);
        }
        else if (v.x > 4)
        {
            spriterenderer.color = new Color(c.r, c.g, c.b, (8 - v.x) / 4);
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
        }
    }
}
