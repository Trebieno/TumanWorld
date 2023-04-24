using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
       
    private static int _lastAudioIndex = 0;

    private Coroutine _audioPlayCoroutine;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }

        // _audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        if (_audioPlayCoroutine != null)
        {
            StopCoroutine(_audioPlayCoroutine);

            _lastAudioIndex++;
        }

        _lastAudioIndex = GetClipIndex();
        _audioPlayCoroutine = StartCoroutine(GetAudioPlay());
    }

    private IEnumerator GetAudioPlay()
    {
        while (true)
        {
            var clip = _audioClips[_lastAudioIndex];

            _audioSource.clip = clip;
            _audioSource.Play();

            yield return new WaitForSeconds(clip.length + Time.deltaTime);

            _audioSource.Stop();

            _lastAudioIndex++;
            _lastAudioIndex = GetClipIndex();
        }
    }

    private int GetClipIndex()
    {
        return _lastAudioIndex % _audioClips.Length;
    }
}
