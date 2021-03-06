﻿using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SoundMode : MonoBehaviour
{
    public Dropdown dropdown;
    public int indexSoundMode;

    // Start is called before the first frame update
    void Start()
    {
        List<string> modes = new List<string>();
        modes.Add("Continuous");
        modes.Add("Discrete");

        foreach (var mode in modes)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mode });
        }
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        indexSoundMode = dropdown.value;

        //TextBox.text = dropdown.options[index].text;
    }
}
