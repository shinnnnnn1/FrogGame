using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider[] options;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartDialogue(Dialogue dialogue)
    {

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
