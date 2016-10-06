using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class slider5 : MonoBehaviour
{
    public Slider sliders;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sliders.value = Mathf.MoveTowards(sliders.value, 9500.0f, -1.0f);
        if (sliders.value == 0.0f)
        {
            Application.LoadLevel("lose5");
        }
    }

}