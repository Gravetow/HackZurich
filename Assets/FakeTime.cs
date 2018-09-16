using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeTime : MonoBehaviour
{

    public TextMeshProUGUI text;
    int day = 16;


    private void Awake()
    {
        text.SetText(day + ". Sept");
    }

    public void AddDay()
    {
        day++;
        text.SetText(day + ". Sept");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
