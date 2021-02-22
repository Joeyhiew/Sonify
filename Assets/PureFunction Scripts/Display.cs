using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public Dropdown dropdown;
    public int indexDisplay;
    void Start()
    {
        List<string> modes = new List<string>();
        modes.Add("Display");
        modes.Add("No display");

        foreach (var mode in modes)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mode });
        }
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        indexDisplay = dropdown.value;

        //TextBox.text = dropdown.options[index].text;
    }

}
