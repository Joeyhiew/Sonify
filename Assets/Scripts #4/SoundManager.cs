using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // frequency 2 is reference, frequency 1 is the variable
    int sampleRate;
    [SerializeField] [Range(0, 1)] float Vol = 0.1f;
    float Frequency1;
    float Frequency2;
    float freqRatio;
    float refFreqRatio;
    int minFrequency = 100;
    int maxFrequency = 1000;
    float phase1;
    float phase2;
    const double pi = Math.PI;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        sampleRate = AudioSettings.outputSampleRate;
        Frequency2 = 440;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
    }

    public void updateFrequency(float value, float maxValue)
    {
        freqRatio = 1-(value / maxValue);
        print("freqratio: " + freqRatio);
        Frequency1 = Mathf.LerpUnclamped(minFrequency, Frequency2, freqRatio);
        print("FReq1: " + Frequency1);
    }

    float Derivative(Func<float, float, float> MyFunction, Vector2 mouseWorld)
    {
        float h = 0.1f;
        return (MyFunction(mouseWorld.x + h, mouseWorld.y + h) - MyFunction(mouseWorld.x - h, mouseWorld.y - h)) / 2*h;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

        for (int i = 0; i < data.Length; i += channels)
        {
            phase1 += 2 * Mathf.PI * Frequency1 / sampleRate;
            phase2 += 2 * Mathf.PI * Frequency2 / sampleRate;

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
