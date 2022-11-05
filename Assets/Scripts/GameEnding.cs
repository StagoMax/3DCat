using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    bool m_isPlayerAtExit;
    bool downDuration = false;
    public GameObject player;

    public float fadeDurtion = 1.0f;

    float m_Timer;

    public float displayImageDuration = 1.0f;

    public CanvasGroup exitBackgroundImageCanvasGrounp;

    public CanvasGroup CaughtBackgroundImageCanvasGrounp;

    bool m_isPlayerCaught;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    bool m_HasAudioPlayed;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_isPlayerAtExit = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGrounp, false, exitAudio);
        }
        else if (m_isPlayerCaught)
        {
            EndLevel(CaughtBackgroundImageCanvasGrounp, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imagecanvasGroup, bool doRestart,AudioSource audioSource)
    {
        //结束当前程序,打包才能生效
        //Application.Quit();
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        if (m_Timer < fadeDurtion + displayImageDuration && !downDuration)
        {
            m_Timer += Time.deltaTime;
            imagecanvasGroup.alpha = m_Timer / fadeDurtion;
        }
        else
        {
            downDuration = true;
            m_Timer -= Time.deltaTime;
            imagecanvasGroup.alpha = m_Timer - 1.0f;
            Debug.Log($"m_timer={m_Timer}");
            if (m_Timer < 1.0f)
            {
                if (doRestart)
                {
                    SceneManager.LoadScene(0);
                }
                else { UnityEditor.EditorApplication.isPlaying = false; }

            }

        }
    }

    internal void CaughtPlayer()
    {
        m_isPlayerCaught = true;
    }
}
