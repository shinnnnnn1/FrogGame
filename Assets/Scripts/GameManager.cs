using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
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

    bool isPlaying= true;
    public PlayerCtrl player;

    [Header("UI")]
    public Image fadeImage;
    public Image optionUI;
    public Slider[] options;
    public Image dialogueImage;
    public TMP_Text dialogueText;

    public AudioSource[] sources;

    void Start()
    {
        Cursor.visible = !isPlaying;
        Cursor.lockState = isPlaying ? CursorLockMode.Locked : CursorLockMode.None;

        sources = FindObjectsOfType<AudioSource>();

        fadeImage.DOFade(0f, 5f).SetEase(Ease.InCubic).SetDelay(1f);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueImage.DOFade(0.1f, 0.2f).SetDelay(2.0f);
        StartCoroutine(DialogueEvent(dialogue));
    }

    IEnumerator DialogueEvent(Dialogue dialogue)
    {
        yield return new WaitForSeconds(2.3f);
        for (int i  = 0; i < dialogue.dialogue.Length; i ++)
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
        for(int i = 0; i < dialogue.dialogue[index].Length; i ++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }
        dialogueText.maxVisibleCharacters = dialogue.dialogue[index].Length;
    }

    void DialogueEvent(Dialogue dialogue, int index)
    {
        switch(dialogue.name)
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

    public void ChangeValue(Slider slider)
    {

        switch (slider.gameObject.name)
        {
            case "Master":
                
                break;
            case "BGM":

                break;
            case "SE":
                
                break;
            case "Voice":

                break;
            case "Horizontal":
                
                break;
            case "Vertical":
                break;
        }

    }
}
