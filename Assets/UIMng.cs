using UnityEngine;
using System.Collections;

public class UIMng : MonoBehaviour
{
    public Poster[] Posters;
    public SpriteRenderer[] PostersRenderer;

    private int FocusNum;

    SelectPoster FocusSel;

    void Start()
    {
        FocusNum = 2;
        FocusSel = transform.FindChild("FocusSel").gameObject.GetComponent<SelectPoster>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Left();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Right();


        for(int i = 0; i < PostersRenderer.Length;i++)
        {
            PostersRenderer[i].sortingOrder = i + 1;
            if (i == FocusNum)
                PostersRenderer[i].sortingOrder = 100;
        }

        Quaternion toQuat = Camera.main.transform.localRotation;
        toQuat.x = 0;
        toQuat.z = 0;
        this.transform.rotation = toQuat;

        FocusSel.myCol = Posters[FocusNum].gameObject;
    }

    public void Left()
    {
        for(int i = 0; i < Posters.Length; i++)
        {
            if (Posters[i].movetospeed > 0)
                return;
        }

        if (FocusNum < 1)
            return;

        FocusNum--;

        foreach (var it in Posters)
        {
           it.movetopos = new Vector3(it.transform.localPosition.x + 4, it.transform.localPosition.y, it.transform.localPosition.z);
           it.movetospeed = 1;
        }
    }

   public void Right()
    {
        for (int i = 0; i < Posters.Length; i++)
        {
            if (Posters[i].movetospeed > 0)
                return;
        }

        if (FocusNum >= Posters.Length - 1)
            return;

        FocusNum++;

        foreach (var it in Posters)
        {
            it.movetopos = new Vector3(it.transform.localPosition.x - 4, it.transform.localPosition.y, it.transform.localPosition.z);
            it.movetospeed = 1;
        }
    }

}
