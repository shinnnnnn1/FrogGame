using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    bool isPlaying = true;
    PlayerCtrl player;

    [Header("UI")]
    [SerializeField] Image fadeImage;
    [SerializeField] Image optionUI;
    [SerializeField] Slider[] options;
    [SerializeField] Image dialogueImage;
    [SerializeField] TMP_Text dialogueText;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource[] sources;

    void Start()
    {
        Setup();
        Fade(true);
    }
    void Setup()
    {
        Cursor.visible = false;
        Cursor.lockState = isPlaying ? CursorLockMode.Locked : CursorLockMode.None;

        player = FindObjectOfType<PlayerCtrl>();
        sources = FindObjectsOfType<AudioSource>();


        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
    }
    public void Fade(bool fadeIn)
    {
        if(fadeIn)
        {
            fadeImage.DOFade(0f, 5f).SetEase(Ease.InCubic).SetDelay(1f);
        }
        else
        {
            fadeImage.DOFade(1f, 10f).SetEase(Ease.OutCubic).SetDelay(2f);
        }
    }

    #region Dialogue and Event
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueImage.DOFade(0.1f, 0.2f).SetDelay(2.0f);
        StartCoroutine(DialogueEvent(dialogue));
    }
    IEnumerator DialogueEvent(Dialogue dialogue)
    {
        yield return new WaitForSeconds(2.3f);
        for (int i = 0; i < dialogue.dialogue.Length; i++)
        {
            DialogueEvent(dialogue, i);
            StartCoroutine(TextAnim(dialogue, i));
            sources[0].PlayOneShot(dialogue.narration[i]);
            //yield return new WaitForSeconds(dialogue.narration[i].length + 0.5f);
            yield return new WaitForSeconds(3f);
        }
        dialogueText.text = "";
        dialogueImage.DOFade(0, 0.2f);
    }
    IEnumerator TextAnim(Dialogue dialogue, int index)
    {
        dialogueText.text = dialogue.dialogue[index];
        for (int i = 0; i < dialogue.dialogue[index].Length; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }
        dialogueText.maxVisibleCharacters = dialogue.dialogue[index].Length;
    }
    void DialogueEvent(Dialogue dialogue, int index)
    {
        switch (dialogue.name)
        {
            case "A1_1":

                break;
        }
    }
    public void CustomEvent(int i)
    {
        switch (i)
        {
            case 0:
                break;
        }
    }
    #endregion

    #region Options
    public void Option()
    {
        isPlaying = !isPlaying;
        optionUI.gameObject.SetActive(!optionUI.gameObject.activeSelf);
        player.canMove = isPlaying;
        player.canLook = isPlaying;
        Time.timeScale = isPlaying ? 1 : 0;
        Cursor.visible = !isPlaying;
        Cursor.lockState = isPlaying ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public void ChangeAudioValue(Slider slider)
    {
        float value = slider.value * 10 -80;
        audioMixer.SetFloat(slider.name, value);
    }
    public void ChangeSensitivity()
    {

    }
    public void Title()
    {

    }
    #endregion

    public void ChangeScene()
    {
        string[] a = { "A", "B", "C", "D", "E", "F", };

        string level = PlayerPrefs.GetString("Scene").Substring(5, 1);
        string stage = (int.Parse(PlayerPrefs.GetString("Scene").Substring(6, 1)) + 1).ToString();

        if ((level == "A" && stage == "4") || (level != "A" && stage == "2"))
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (level == a[i])
                {
                    level = a[i + 1];
                    stage = "1";
                    break;
                }
            }
        }

        string nextScene = "Stage" + level + stage;
        SceneManager.LoadScene(nextScene);

        //Debug.Log("Next Scene = " + nextScene);
    }

    public void PlaySE(AudioClip clip)
    {

    }
}
