using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{

    public Modes modes;
    public int modeIndex;

    // Update is called once per frame
    void Update()
    {
        modeIndex = modes.indexMode;
        
    }
}
