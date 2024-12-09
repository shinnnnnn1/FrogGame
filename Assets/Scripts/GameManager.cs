using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueImage.DOFade(0.1f, 0.2f);
        StartCoroutine(DialogueEvent(dialogue));
    }

    IEnumerator DialogueEvent(Dialogue dialogue)
    {
        yield return new WaitForSeconds(0.5f);

        for(int i  = 0; i < dialogue.dialogue.Length; i ++)
        {
            StartCoroutine(TextAnim(dialogue, i));

            sources[0].PlayOneShot(dialogue.narration[i]);
            //yield return new WaitForSeconds(dialogue.narration[i].length + 0.5f);

            yield return new WaitForSeconds(10f);

            Debug.Log(i);
        }
    }

    IEnumerator TextAnim(Dialogue dialogue, int index)
    {
        for(int i = 0; i < dialogue.dialogue[index].Length; i ++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
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
