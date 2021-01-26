using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveModes : MonoBehaviour
{
    public Dropdown dropdown;
    public int indexWaveMode;

    // Start is called before the first frame update
    void Start()
    {
        List<string> modes = new List<string>();
        modes.Add("Sine");
        modes.Add("Square");

        foreach (var mode in modes)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mode });
        }
        DropdownItemSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        indexWaveMode = dropdown.value;

        //TextBox.text = dropdown.options[index].text;
    }
}
