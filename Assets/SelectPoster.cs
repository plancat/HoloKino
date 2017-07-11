using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using HoloToolkit.Unity;

public class SelectPoster : MonoBehaviour
{
    public bool isLeft;
    public bool isFocus;
    public GameObject myCol;
    float defScalex;
    float defScaley;
    // Use this for initialization
    void Start()
    {
        defScalex = transform.localScale.x;
        defScaley = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (myCol == null)
            return;

        if (this.name.CompareTo("LeftSel") == 0)
            myCol.transform.localScale = new Vector3(-1.0f, 1.0f, transform.localScale.z);
        else
            myCol.transform.localScale = new Vector3(1.0f, 1.0f, transform.localScale.z);

        if (GazeManager.Instance.Hit)
        {

            if (this.name.CompareTo(GazeManager.Instance.HitInfo.collider.gameObject.name) == 0)
            {
                Debug.Log("2");
                if (this.name.CompareTo("LeftSel") == 0)
                    myCol.transform.localScale = new Vector3(-1.3f, 1.3f, transform.localScale.z);
                else
                    myCol.transform.localScale = new Vector3(1.3f, 1.3f, transform.localScale.z);

            }
            else
            {
                Debug.Log("3");
                if (this.name.CompareTo("LeftSel") == 0)
                    myCol.transform.localScale = new Vector3(-1.0f, 1.0f, transform.localScale.z);
                else
                    myCol.transform.localScale = new Vector3(1.0f, 1.0f, transform.localScale.z);
            }
        }
    }

    void OnSelect()
    {
        if (isFocus)
        {
            SceneManager.LoadScene(1);
            return;
        }
        else
        {
            if (isLeft)
                transform.parent.GetComponent<UIMng>().Left();
            else
                transform.parent.GetComponent<UIMng>().Right();
        }
    }
}
