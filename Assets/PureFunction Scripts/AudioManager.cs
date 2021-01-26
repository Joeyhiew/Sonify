using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Text frequencyText;
    // frequency 2 is reference, frequency 1 is the variable
    int sampleRate;
    [SerializeField] [Range(0, 1)] public float Vol = 0.1f;
    float Frequency1;
    float refFrequency;
    float startFrequency = 233.082f;
    float minFrequency = 27.5f;
    float maxFrequency = 4186.01f;
    float phase1;
    float phase2;
    const double pi = Math.PI;

    AudioSource audioSource;
    void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        refFrequency = 440;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.volume = Vol;
    }

    public void updateFrequency(float freqRatio)
    {

        Frequency1 = Mathf.LerpUnclamped(refFrequency, startFrequency, freqRatio);
        print(startFrequency);
        frequencyText.text = ((float)Math.Round(Frequency1 * 100f) / 100f).ToString() + " HZ";
/*        if (Frequency1 >= maxFrequency)
        {
            //Frequency1 = maxFrequency;
        }
        else if (Frequency1 <= minFrequency)
        {
            //Frequency1 = minFrequency;
        }
        else
        {
            Frequency1 = Mathf.LerpUnclamped(refFrequency, startFrequency, freqRatio);
        }*/
    }

    public void IncreaseFrequencyRange()
    {
        if (startFrequency < 30)
        {
            return;
        }
        startFrequency = startFrequency / 2;
    }

    public void DecreaseFrequencyRange()
    {
        if (startFrequency >= 233)
        {
            return;
        }
        startFrequency = startFrequency * 2;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

        for (int i = 0; i < data.Length; i += channels)
        {
            phase1 += Vol * 2 * Mathf.PI * Frequency1 / sampleRate;
            phase2 += Vol * 2 * Mathf.PI * refFrequency / sampleRate;

            // Sine wave
            /*            if (optionManager.waveIndex == 0)
                        {*/
            data[i] = Mathf.Sin(phase1);

            if (channels == 2)
            {
                data[i + 1] = Mathf.Sin(phase2);
            }
            //}
            /*            // Square wave
                        else
                        {
                            data[i] = Vol * Mathf.Sign(Mathf.Sin((phase1)));

                            if (channels == 2)
                            {
                                data[i + 1] = Vol * Mathf.Sign(Mathf.Sin((phase2)));
                            }
                        }*/


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
}
