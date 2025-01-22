using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour
{
    [SerializeField] bool isTrigger;
    [SerializeField] Image[] fadeImage;
    [SerializeField] TMP_Text bonusText;
    AudioSource source;
    
    void OnTriggerEnter(Collider other)
    {
        if(!GameManager.Instance.endingbool)
        {
            GameManager.Instance.EndingFade();

            GameManager.Instance.isPlaying = false;
            GameManager.Instance.endingbool = true;

            Invoke("Credit", 7f);
        }
    }

    void Credit()
    {
        SceneManager.LoadScene("EndingCredit");
    }

    void Start()
    {
        if (isTrigger) { return; }
        source = GetComponent<AudioSource>();
        fadeImage[0].DOFade(0, 3f).SetDelay(2f);
        fadeImage[1].DOFade(0, 3f).SetDelay(6f);
        StartCoroutine(Music());

        int bonus = 0;
        for (int i = 0; i < GameManager.Instance.acquiredBonus.Length; i++)
        {
            if (GameManager.Instance.acquiredBonus[i])
            {
                bonus++;
            }
            bonusText.text = bonus.ToString() + "/10";
        }

    }
    IEnumerator Music()
    {
        while (source.volume <= 1)
        {
            source.volume += Time.deltaTime / 5;
            yield return null;
        }
    }
}
