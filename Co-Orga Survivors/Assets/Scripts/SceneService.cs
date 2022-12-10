using UnityEngine;

public class SceneService : MonoBehaviour
{
    private void Start() => GameObject.FindGameObjectWithTag("Music").GetComponent<MenüTrack>().PlayMusic();
}