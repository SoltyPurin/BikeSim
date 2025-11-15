using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Acceleration,
}
public class SoundManager : MonoBehaviour
{
    [SerializeField,Header("エンジン音格納リスト")]
    private List<AudioClip> _engineSoundList = new List<AudioClip>();
    [SerializeField, Header("アイドリング音を鳴らす速度")]
    private float _idlingSpeed = 4f;
    [SerializeField, Header("音のピッチの変わり方を明示するカーブ")]
    private AnimationCurve _pitchCurve = default;

    //エンジン音を変える時にピッチチェックに引っかからないようにするための最小値
    private float _changeMinPitch = 1.1f;
    private int _curEngineIndex = 0;
    private bool _isAccelerating = false;
    private bool _isIdlingPlaying = false;
    private AudioSource _audioSource = default;
    private Rigidbody _rigidBody = default;
    private PlayerState _state = PlayerState.Idle;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody>();
        PlayIdleSound();
    }

    private void PlayIdleSound()
    {

        if (_isIdlingPlaying)
        {
            return;
        }
        Debug.Log(this.gameObject.name + "がアイドリングしてる");
        _audioSource.pitch = _changeMinPitch;
        _audioSource.clip = _engineSoundList[0];
        _audioSource.Play();
        _isIdlingPlaying=true;
    }

    private void Update()
    {

        if (_state == PlayerState.Idle)
        {
            return;
        }
        if(_rigidBody.velocity.magnitude <= _idlingSpeed)
        {
            PlayIdleSound();
            return;
        }

        float pitchValue = _pitchCurve.Evaluate(_rigidBody.velocity.magnitude);
        if (_isAccelerating)
        {
            // ピッチを上げてエンジン音を高回転にする
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, 2.0f, Time.deltaTime * 2);
        }
        else
        {
            // ピッチを下げてエンジン音をアイドリングに戻す
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, 1.0f, Time.deltaTime * 2);
        }
        _isIdlingPlaying = false;
        //ReadPitch(_audioSource.pitch);
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
    public void UpGear()
    {
        _curEngineIndex++;
        _curEngineIndex = Mathf.Clamp(_curEngineIndex,0,_engineSoundList.Count-1);
        _audioSource.Stop();
        _audioSource.clip = _engineSoundList[_curEngineIndex];
        _audioSource.Play();
        _audioSource.pitch = _changeMinPitch;
    }

    public void DownGear()
    {
        _curEngineIndex--;
        _curEngineIndex = Mathf.Clamp(_curEngineIndex, 0, _engineSoundList.Count - 1);
        _audioSource.Stop();
        _audioSource.clip = _engineSoundList[_curEngineIndex];
        _audioSource.Play();
        _audioSource.pitch = _changeMinPitch;

    }
}
