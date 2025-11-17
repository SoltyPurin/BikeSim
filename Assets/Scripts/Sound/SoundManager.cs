using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Acceleration,
    Drift,
}
public class SoundManager : MonoBehaviour
{
    [SerializeField,Header("エンジン音格納リスト")]
    private List<AudioClip> _engineSoundList = new List<AudioClip>();
    [SerializeField, Header("ドリフト音")]
    private AudioClip _driftSound = default;
    [SerializeField, Header("アイドリング音を鳴らす速度")]
    private float _idlingSpeed = 4f;
    [SerializeField, Header("音のピッチの変わり方を明示するカーブ")]
    private AnimationCurve _pitchCurve = default;
    [SerializeField, Header("ステータス")]
    private BikeStatus _status = default;

    //エンジン音を変える時にピッチチェックに引っかからないようにするための最小値
    private float _changeMinPitch = 1.1f;
    private int _curEngineIndex = 0;
    private bool _isAccelerating = false;
    private bool _isIdlingPlaying = false;
    private bool _isDrifting = false;
    private AudioSource _audioSource = default;
    private Rigidbody _rigidBody = default;
    private PlayerState _state = PlayerState.Idle;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();
        PlayIdleSound();
    }


    private void Update()
    {

        if (_state == PlayerState.Idle)
        {
            return;
        }
        if(_state == PlayerState.Drift)
        {
            PlayDriftSound();
            return;
        }
        float speed = _rigidBody.velocity.magnitude;
        float t = Mathf.InverseLerp(0f, _status.GearMaxSpeeds[_curEngineIndex], speed);
        float pitch = Mathf.Lerp(1, 2, t);
        _audioSource.pitch = pitch;

        _isIdlingPlaying = false;
        _isDrifting = false;
        //ReadPitch(_audioSource.pitch);
}

    private void PlayDriftSound()
    {
        if (_isDrifting)
        {
            return;
        }
        _isDrifting = true;
        _audioSource.Stop();
        _audioSource.clip = _driftSound;
        _audioSource.Play();
    }
    private void PlayIdleSound()
    {

        if (_isIdlingPlaying)
        {
            return;
        }
        _audioSource.pitch = _changeMinPitch;
        _audioSource.clip = _engineSoundList[0];
        _audioSource.Play();
        _isIdlingPlaying = true;
    }

    public void Drift()
    {
        _state = PlayerState.Drift;
    }

    public void UnDrift()
    {
        _state = PlayerState.Acceleration;
        _audioSource.Stop();
        _audioSource.clip = _engineSoundList[_curEngineIndex];
        _audioSource.Play();
    }

    public void AxelAccelerating()
    {
        _state = PlayerState.Acceleration;
        _isAccelerating = true;
    }
    public void NotInputAxel()
    {
        _isAccelerating = false;
    }
    public void UpGear(int index)
    {
        _curEngineIndex = index;
        _curEngineIndex = Mathf.Clamp(_curEngineIndex,0,_engineSoundList.Count-1);
        _audioSource.Stop();
        _audioSource.clip = _engineSoundList[_curEngineIndex];
        _audioSource.Play();
        _audioSource.pitch = _changeMinPitch;
    }

    public void DownGear(int index)
    {
        _curEngineIndex = index;
        _curEngineIndex = Mathf.Clamp(_curEngineIndex, 0, _engineSoundList.Count - 1);
        _audioSource.Stop();
        _audioSource.clip = _engineSoundList[_curEngineIndex];
        _audioSource.Play();
        _audioSource.pitch = _changeMinPitch;

    }
}
