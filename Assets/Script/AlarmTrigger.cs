using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _triggered;
    [SerializeField] private float _volumeSpeed = 0.3f;

    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine _lastRoutine = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private IEnumerator ManipulateVolume(float targetVolume, float volumeSpeed)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, volumeSpeed);

            if (_audioSource.volume == _minVolume)
            {
                _audioSource.Stop();
            }

            yield return null;
        }
    }

    public void IncreaseVolumeToMax()
    {
        _triggered?.Invoke();

        if (_lastRoutine != null)
        {
            StopCoroutine(_lastRoutine);
            _lastRoutine = null;
            _lastRoutine = StartCoroutine(ManipulateVolume(_maxVolume, _volumeSpeed));
        }
        else
        {
            _lastRoutine = StartCoroutine(ManipulateVolume(_maxVolume, _volumeSpeed));
        }
    }

    public void DeacreaseVolumeToMin()
    {
        if (_lastRoutine != null)
        {
            StopCoroutine(_lastRoutine);
            _lastRoutine = null;
            _lastRoutine = StartCoroutine(ManipulateVolume(_minVolume, _volumeSpeed));
        }
        else
        {
            _lastRoutine = StartCoroutine(ManipulateVolume(_minVolume, _volumeSpeed));
        }
    }
}
