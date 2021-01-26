using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public SoundMode soundMode;
    public WaveModes waveModes;
    public int waveIndex;
    public int modeIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        modeIndex = soundMode.indexSoundMode;
        waveIndex = waveModes.indexWaveMode;
    }
}
