using UnityEngine;
using System;

public class PlayOnClick : MonoBehaviour
{
    public Boundaries bounds;
    public OptionManager optionManager;

    int sampleRate;
    [SerializeField] [Range(0, 1)] float Vol = 0.3f;
    float Frequency;
    float freqRatio;
    int minFrequency = 80;
    int maxFrequency = 1000;
    double increment, cachedFrequency;
    double[] phase;
    const double pi = Math.PI;

    void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        phase = new double[(int)AudioSettings.speakerMode];
        freqRatio = bounds.freqRatio;
        Frequency = Mathf.Lerp(minFrequency, maxFrequency, freqRatio);
    }

    // function for the curve
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (cachedFrequency != Frequency)
        {
            cachedFrequency = Frequency;
            increment = pi * 2 * Frequency / sampleRate;
        }

        // dataLength represents the number of samples per channel
        int dataLength = data.Length / channels;
        for (int n = 0; n < dataLength; n++)
        {
            for (int i = 0; i < channels; i++)
            {
                int sample = n * channels + i;

                // Sine wave
                if (optionManager.waveIndex == 0)
                {
                    data[sample] = Vol * (float)Math.Sin(phase[i]);
                }
                // Square wave
                else
                {
                    data[sample] = Vol * Mathf.Sign(Mathf.Sin((float)(phase[i])));
                }

            
                
                phase[i] += increment;


            }
        }
    }

    // Update frequency when mouse move
    void Update()
    {
        freqRatio = bounds.freqRatio;

        // continuous
        if (optionManager.modeIndex == 0)
        {
            Frequency = Mathf.Lerp(minFrequency, maxFrequency, freqRatio);
        }
        // discrete by default
        else
        {
            Frequency = ((int)Mathf.Round((maxFrequency - minFrequency) * freqRatio + minFrequency) / 50) * 50;
        }
        
    }
}
