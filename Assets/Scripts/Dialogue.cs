using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string[] dialogue;
    public float[] delay;
}
