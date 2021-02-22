using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{

    public Modes modes;
    public Display display;
    public int modeIndex;
    public int displayIndex;

    // Update is called once per frame
    void Update()
    {
        modeIndex = modes.indexMode;
        displayIndex = display.indexDisplay;
        
    }
}
