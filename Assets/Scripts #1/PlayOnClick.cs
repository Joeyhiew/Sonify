using UnityEngine;
using System;

public class PlayOnClick : MonoBehaviour
{
    public Boundaries bounds;
    public RefBoundaries refBounds;
    public OptionManager optionManager;

    int sampleRate;
    [SerializeField] [Range(0, 1)] float Vol = 0.1f;
    float Frequency1;
    float Frequency2;
    float freqRatio;
    float refFreqRatio;
    int minFrequency = 80;
    int maxFrequency = 1000;
    float phase1;
    float phase2;
    const double pi = Math.PI;

    AudioSource audioSource;

    void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        freqRatio = bounds.freqRatio;
        refFreqRatio = refBounds.freqRatio;
        Frequency1 = Mathf.Lerp(minFrequency, maxFrequency, freqRatio);
        Frequency2 = Mathf.Lerp(minFrequency, maxFrequency, refFreqRatio);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
    }

    // function for the curve
    void OnAudioFilterRead(float[] data, int channels)
    {

        for (int i = 0; i < data.Length; i += channels)
        {
            phase1 += 2 * Mathf.PI * Frequency1 / sampleRate;
            phase2 += 2 * Mathf.PI * Frequency2 / sampleRate;

            // Sine wave
            if (optionManager.waveIndex == 0)
            {
                data[i] = Mathf.Sin(phase1);

                if (channels == 2)
                {
                    data[i + 1] = Mathf.Sin(phase2);
                }
            }
            // Square wave
            else
            {
                data[i] = Vol * Mathf.Sign(Mathf.Sin((phase1)));

                if (channels == 2)
                {
                    data[i + 1] = Vol * Mathf.Sign(Mathf.Sin((phase2)));
                }
            }


            if (phase1 >= 2 * Mathf.PI)
            {
                phase1 -= 2 * Mathf.PI;
            }
            if (phase2 >= 2 * Mathf.PI)
            {
                phase2 -= 2 * Mathf.PI;
            }
        }

    }

    // Update frequency when mouse move
    void Update()
    {
        freqRatio = bounds.freqRatio;
        refFreqRatio = refBounds.freqRatio;

        // continuous
        if (optionManager.modeIndex == 0)
        {
            Frequency1 = Mathf.Lerp(minFrequency, maxFrequency, freqRatio);
            Frequency2 = Mathf.Lerp(minFrequency, maxFrequency, refFreqRatio);
        }
        // discrete by default
        else
        {
            Frequency1 = ((int)Mathf.Round((maxFrequency - minFrequency) * freqRatio + minFrequency) / 50) * 50;
            Frequency2 = ((int)Mathf.Round((maxFrequency - minFrequency) * refFreqRatio + minFrequency) / 50) * 50;
        }

        


    }
}
