using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateDisplay : MonoBehaviour
{

    public int selectedState = 0;
    // Start is called before the first frame update
    void Start()
    {
        selectState();
    }

    // Update is called once per frame
    void Update()
    {

        int previousSelectedState = selectedState;
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedState = (selectedState + 2) % 3;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            selectedState = (selectedState + 1) % 3;
        }
        if (previousSelectedState != selectedState)
        {
            selectState();
        }
    }
    void selectState()
    {
        int i = 0;
        foreach (Transform state in transform)
        {
            if (i == selectedState)
            {
                state.gameObject.SetActive(true);
            }
            else
            {
                state.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
