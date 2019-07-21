using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;
    const string display = "{0} FPS";
    public Text m_Text;

    Color red = new Color(255, 0, 0);
    Color orange = new Color(255, 128, 0);
    Color green = new Color(0, 255, 0);
    Color light_blue = new Color(128, 128, 255);


    private void Start()
    {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        m_Text = GetComponent<Text>();
    }


    private void Update()
    {
        // measure average frames per second
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
            m_Text.text = string.Format(display, m_CurrentFps);

            if(m_CurrentFps<24)
            {
                m_Text.color = red;
            }
            if (m_CurrentFps<30 && m_CurrentFps>=24)
            {
                m_Text.color = orange;
            }
            if(m_CurrentFps<45 && m_CurrentFps >= 30)
            {
                m_Text.color = green;
            }
            if (m_CurrentFps>=45)
            {
                m_Text.color = light_blue;
            }            
        }
    }

}
