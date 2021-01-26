using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modes : MonoBehaviour
{
    public Dropdown dropdown;
    public int indexMode;
    void Start()
    {
        List<string> modes = new List<string>();
        modes.Add("Current");
        modes.Add("Fixed octave, vary distance");

        foreach (var mode in modes)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mode });
        }
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        indexMode = dropdown.value;

        //TextBox.text = dropdown.options[index].text;
    }

}
