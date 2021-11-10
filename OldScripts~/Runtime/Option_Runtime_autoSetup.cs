using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Runtime_autoSetup : MonoBehaviour
{
    [SerializeField] float timeForSetup = 10;
    float timer;
    List<float> fpsHistory = new List<float>();

    float[] fpsBuffer;
    int id_buffer;

    void Start() => StartSetup();

    void StartSetup()
    {
        fpsBuffer = new float[60];
        timer = timeForSetup;
        enabled = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            enabled = false;
            return;
        }

        fpsBuffer[id_buffer] = GetFPS();
        id_buffer++;
        if(id_buffer >= fpsBuffer.Length)
        {
            float fps = 0;
            for (int i = 0; i < fpsBuffer.Length; i++)
                fps += fpsBuffer[i];
            fps /= fpsBuffer.Length;

            fpsHistory.Add(fps);
            id_buffer = 0;
        }
    }

    float deltaTime = 0.0f;
    float GetFPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //float msec = deltaTime * 1000.0f;
        //msec = (Mathf.RoundToInt(msec * 100)) / 100f;
        return 1.0f / deltaTime;
    }
}
