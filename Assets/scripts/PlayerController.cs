using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterSignals
{
    [Header("References")]
    [SerializeField] private PlayerControllerInput playerController;
    //[SerializeField] ChangeColor changeColorScript;
    private CharacterController _characterController;
    private Camera _camera;
    
    [Header("Player Properties")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private float smoothLooking = 100f;
    private Vector2 _smoothLook = Vector2.zero;
    [SerializeField] private float rotationSpeed = 20f;
    
    [SerializeField] private float height = 4f;
    private float _cameraPosRatio;

    [Header("Audio Control")]
     [SerializeField] private AudioSource[] audioSources;
     [SerializeField] private float volumeControlDistance = 5f;
     [SerializeField] private float maxVolumeDistance = 30f;
     private AudioSource currentNearbyAudioSource;

     public GameObject[] relative;
    //bearbeiten ICharacterSignals
    private Subject<Vector3> _moved = new();
    private ReactiveProperty<bool> _isRunning;
    [SerializeField] private float strideLength = 2.5f;

    public float StrideLength => strideLength;
    public ReactiveProperty<bool> IsRunning => _isRunning;
    public IObservable<Vector3> Moved => _moved;
    public IObservable<Unit> Landed { get; }
    public IObservable<Unit> Jumped { get; }
    public IObservable<Unit> Stepped { get; }

    private void Update()
    {
        FindNearestAudioSource();
        Debug.Log(currentNearbyAudioSource);
        //AdjustVolumeBasedOnProximity();
    }
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();

        LockCursor();

        _cameraPosRatio = _camera.transform.localPosition.y / height;

        _moved.AddTo(this);
        _isRunning = new ReactiveProperty<bool>(false);
    }

    public void LockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        playerController.move.Subscribe(movDir =>
        {

            float moveSpeed;
            moveSpeed = playerController.run.Value ? runSpeed : walkSpeed;

            movDir *= moveSpeed;
            var localMove = new Vector3(movDir.x, 0, movDir.y);
            var worldMove = transform.TransformVector(localMove);
            var motion = worldMove * Time.deltaTime;
            _characterController.Move(motion);
        }).AddTo(this);


        _camera.transform.localRotation = Quaternion.identity;
        playerController.look.Where(vector => vector != Vector2.zero)
            .Subscribe(rawLook =>
            {

                var interpolatation = smoothLooking * Time.deltaTime;
                _smoothLook = new Vector2(Mathf.Lerp(_smoothLook.x, rawLook.x, interpolatation),
                    Mathf.Lerp(_smoothLook.y, rawLook.y, interpolatation));


                var horizontalLook = rotationSpeed * _smoothLook.x * Vector3.up * Time.deltaTime;
                transform.localRotation *= Quaternion.Euler(horizontalLook);


                var verticalLook = rotationSpeed * _smoothLook.y * Vector3.left * Time.deltaTime;

                var newQ = _camera.transform.localRotation * Quaternion.Euler(verticalLook);
                var euler = newQ.eulerAngles;
                _camera.transform.localRotation = Quaternion.Euler(euler);
            }).AddTo(this);
        playerController.VolumeUp.Subscribe(_ => AdjustVolume(true)).AddTo(this);
        playerController.VolumeDown.Subscribe(_ => AdjustVolume(false)).AddTo(this);
    }
    
    private void FindNearestAudioSource()
    {
        currentNearbyAudioSource = null;
        float nearestDistance = float.MaxValue;

        int i = 0;
        foreach (var audioSource in audioSources)
        {
            
            float distance = Vector3.Distance(_camera.transform.position, relative[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                currentNearbyAudioSource = audioSource;
               
            }

            i++;
        }
    }
    
    
    private void AdjustVolumeBasedOnProximity()
    {
       /* for (int i = 0; i < audioSources.Length; i++)
        {
            if (i < relative.Length) 
            {
                AudioSource audioSource = audioSources[i];
                GameObject relativeObject = relative[i];

                float distance =
                    Vector3.Distance(playerController.transform.position, relativeObject.transform.position);
                float volume = Mathf.Clamp01(1 - (distance / maxVolumeDistance));
                audioSource.volume = volume;
                Debug.Log("relative is " + relativeObject);
            }
        }*/
    }
    
    private void AdjustVolume(bool increase)
    {
        if (currentNearbyAudioSource != null)
        {
            float adjustment = increase ? 0.1f : -0.1f;
            currentNearbyAudioSource.volume = Mathf.Clamp(currentNearbyAudioSource.volume + adjustment, 0f, 1f);
        }
    }
    
}
