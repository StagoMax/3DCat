using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameEnding : MonoBehaviour
{

    Sprite spriteCaught;
    Sprite spriteWom;

    bool m_isPlayerAtExit;
    bool downDuration = false;
    public GameObject player;

    public float fadeDurtion = 1.0f;

    float m_Timer;

    public float displayImageDuration = 1.0f;

    public CanvasGroup exitBackgroundImageCanvasGrounp;


    bool m_isPlayerCaught;

    public Image image;
    private bool m_HasAudioPlayed;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        spriteCaught = Resources.Load<Sprite>("Caught");
        spriteWom = Resources.Load<Sprite>("Won");
    }

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
            EndLevel(spriteWom, false, exitAudio);
        }
        else if (m_isPlayerCaught)
        {
            EndLevel(spriteCaught, true, caughtAudio);
        }
    }

    void EndLevel(Sprite sprite, bool doRestart,AudioSource audioSource)
    {
        //结束当前程序,打包才能生效
        //Application.Quit();
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        image.sprite = sprite;
        if (m_Timer < fadeDurtion + displayImageDuration && !downDuration)
        {
            m_Timer += Time.deltaTime;
            exitBackgroundImageCanvasGrounp.alpha = m_Timer / fadeDurtion;
        }
        else
        {
            downDuration = true;
            m_Timer -= Time.deltaTime;
            exitBackgroundImageCanvasGrounp.alpha = m_Timer - 1.0f;
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
