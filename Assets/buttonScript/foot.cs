using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class foot : MonoBehaviour {

    public static int step;
    public Text stepText;

    // Use this for initialization
    void Start()
    {
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        stepText.text = " X " + step;
    }

}
