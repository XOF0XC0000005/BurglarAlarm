using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _triggered;
    [SerializeField] private float _volumeSpeed = 0.0001f;

    private AudioSource _audioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private Coroutine lastIncreasedRoutine = null;
    private Coroutine lastDecreasedRoutine = null;

    public void IncreaseVolumeToMax()
    {
        _triggered?.Invoke();

        if (lastDecreasedRoutine != null)
        {
            StopCoroutine(lastDecreasedRoutine);
            lastDecreasedRoutine = null;
        }

        if (lastIncreasedRoutine == null)
        {
            lastIncreasedRoutine = StartCoroutine(ManipulateVolume(_audioSource.volume, _maxVolume, _volumeSpeed));
        }
    }

    public void DeacreaseVolumeToMin()
    {
        if (lastIncreasedRoutine != null)
        {
            StopCoroutine(lastIncreasedRoutine);
            lastIncreasedRoutine = null;
        }

        if (lastDecreasedRoutine == null)
        {
            lastDecreasedRoutine = StartCoroutine(ManipulateVolume(_audioSource.volume, _minVolume, _volumeSpeed));
        }
    }

    private IEnumerator ManipulateVolume(float currentVolume, float targetVolume, float volumeSpeed)
    {
        while(_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, targetVolume, volumeSpeed);
            volumeSpeed += volumeSpeed * Time.deltaTime;
            
            if (_audioSource.volume == _minVolume) 
            {
                _audioSource.Stop();
            }
            
            yield return null;
        }
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }
}
