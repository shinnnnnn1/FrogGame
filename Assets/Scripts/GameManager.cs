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

    public bool isPlaying = true;
    public PlayerCtrl player;

    public float senX = 1f;
    public float senY = 1f;

    public bool[] acquiredBonus = new bool[10];

    [Header("UI")]
    [SerializeField] Image fadeImage;
    [SerializeField] Image optionUI;
    [SerializeField] Slider[] options;
    [SerializeField] Image dialogueImage;
    [SerializeField] TMP_Text dialogueText;
    

    [Header("Audio")]
    public AudioMixer audioMixer;
    public AudioSource[] sources;

    TriggerEvent currentTrigger;
    public int triggerIndex;

    [SerializeField] Camera mainCam;
    bool isSetted;

    public bool endingbool;

    private void OnLevelWasLoaded(int level)
    {
        if (!isSetted)
        {
            Setup();
            Fade(true);
            isSetted = true;
        }
    }

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
        mainCam = FindObjectOfType<Camera>();
        //sources = FindObjectsOfType<AudioSource>();
        sources = mainCam.GetComponentsInChildren<AudioSource>();

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
            fadeImage.DOFade(1f, 5f).SetEase(Ease.OutCubic).SetDelay(2f);
        }
    }
    public void EndingFade()
    {
        fadeImage.color = new Color(1, 1, 1, 0);
        fadeImage.DOPause();
        fadeImage.DOFade(1f, 5f);
    }

    #region Dialogue and Event
    public void StartDialogue(Dialogue dialogue, TriggerEvent trigger)
    {
        dialogueImage.DOFade(0.5f, 0.2f).SetDelay(2.0f);
        
        if(trigger != null)
        {
            currentTrigger = trigger;
            triggerIndex = trigger.on.Length;
        }
        else
        {
            currentTrigger = null;
            triggerIndex = 0;
        }

        StartCoroutine(DialogueEvent(dialogue));
    }
    IEnumerator DialogueEvent(Dialogue dialogue)
    {
        yield return new WaitForSeconds(2.3f);
        for (int i = 0; i < dialogue.dialogue.Length; i++)
        {
            DialogueEvent(i);
            StartCoroutine(TextAnim(dialogue, i));

            yield return new WaitForSeconds(dialogue.delay[i]);
            //yield return new WaitForSeconds(3f);
        }
        dialogueText.text = "";
        dialogueImage.DOFade(0, 0.2f);
        triggerIndex = 0;
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
    void DialogueEvent(int index)
    {
        currentTrigger?.CustomTrigger(index);
    }
    #endregion

    #region Options
    public void Option()
    {
        if(endingbool) { return;}
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
    public void ChangeSensitivityHor(Slider slider)
    {
        player.ChangeSensitivity(true, slider.value);
    }
    public void ChangeSensitivityVer(Slider slider)
    {
        player.ChangeSensitivity(false, slider.value);
    }
    public void BackToTitle()
    {
        Option();
        string scene = "Title";
        fadeImage.DOFade(1f, 2f).SetEase(Ease.OutCubic).OnKill(()=> SceneManager.LoadScene(scene));
        Destroy(this);
    }
    public void Restart()
    {
        Option();
        string scene = PlayerPrefs.GetString("Scene");
        fadeImage.DOFade(1f, 2f).SetEase(Ease.OutCubic).OnKill(() => SceneManager.LoadScene(scene));
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

        fadeImage.DOKill();
        isSetted = false;

        string nextScene = "Stage" + level + stage;
        Debug.Log("Next Scene = " + nextScene);

        SceneManager.LoadScene(nextScene);
    }

    public void PlaySE(AudioClip clip)
    {
        sources[1].PlayOneShot(clip);
    }

    public void Bonus(int index)
    {
        acquiredBonus[index] = true;
    }
}
