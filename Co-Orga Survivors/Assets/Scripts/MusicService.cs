using System.Linq;
using UnityEngine;

public class MusicService : MonoBehaviour
{
    public string     trackToStart;
    public string     trackToStop;
    public GameObject menüTrackPrefab;
    public GameObject battleTrackPrefab;

    private void Start()
    {
        // InstantiateAudioSources();
        // PlayMusic();
        // StopMusic();
    }

    private void PlayMusic()
    {
        if (FetchMenütrack() is { } menütrack && trackToStart == nameof(MenüTrack))
            menütrack.PlayMusic();

        if (FetchBattleTrack() is { } battleTrack && trackToStart == nameof(BattleTrack))
            battleTrack.PlayMusic();
    }

    private void StopMusic()
    {
        if (FetchMenütrack() is { } menüTrack && trackToStop == nameof(MenüTrack))
            menüTrack.StopMusic();

        if (FetchBattleTrack() is { } battleTrack && trackToStop == nameof(BattleTrack))
            battleTrack.StopMusic();
    }

    private void InstantiateAudioSources()
    {
        var menüTrackMissing   = FetchMenütrack() is null;
        var battleTrackMissing = FetchBattleTrack() is null;

        if (menüTrackMissing)
            Instantiate(menüTrackPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        if (battleTrackMissing)
            Instantiate(battleTrackPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private static BattleTrack FetchBattleTrack() => FetchTrack<BattleTrack>(nameof(BattleTrack));

    private static MenüTrack FetchMenütrack() => FetchTrack<MenüTrack>(nameof(MenüTrack));

    private static T FetchTrack<T>(string name)
            where T : MonoBehaviour => GameObject.FindGameObjectsWithTag("Music")
                                                ?.FirstOrDefault(go => go.name.Contains(name))
                                                ?.GetComponent<T>();
}