using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BowStringController : MonoBehaviour
{
    [SerializeField] private BowString _bowStringRenderer;
    private XRGrabInteractable _interactable;
    [SerializeField] private Transform _midPointGrabObject, _midPointVisualObject, _midPointParent;
    [SerializeField] private float _bowStringStretchLimit = 0.3f;
    private Transform _interactor;
    private float _strength, _previousStrength;
    [SerializeField] private float _stringSoundThreshold = 0.001f;
    [SerializeField] private AudioSource _audioSource;
    public UnityEvent OnBowPulled;
    public UnityEvent<float> OnBowReleased;

    private void Awake()
    {
        _interactable = _midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        _interactable.selectEntered.AddListener(PrepareBowString);
        _interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        OnBowReleased?.Invoke(_strength);
        _strength = 0;
        _previousStrength = 0;
        _audioSource.pitch = 1;
        _audioSource.Stop();

        _interactor = null;
        _midPointGrabObject.localPosition = Vector3.zero;
        _midPointVisualObject.localPosition = Vector3.zero;
        _bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        _interactor = arg0.interactorObject.transform;
        OnBowPulled?.Invoke();
    }

    private void Update()
    {
        if (_interactor != null)
        {
            Vector3 midPointLocalSpace =
                _midPointParent.InverseTransformPoint(_midPointGrabObject.position); // localPosition

            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);
            _previousStrength = _strength;
            HandleStringPushedBackToStart(midPointLocalSpace);
            HandleStringPulledBackTolimit(midPointLocalZAbs, midPointLocalSpace);
            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);
            _bowStringRenderer.CreateString(_midPointVisualObject.position);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs < _bowStringStretchLimit)
        {
            if (_audioSource.isPlaying == false && _strength <= 0.01f)
            {
                _audioSource.Play();
            }

            _strength = Remap(midPointLocalZAbs, 0, _bowStringStretchLimit, 0, 1);
            _midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
            PlayStringPullinSound();
        }
    }

    private void PlayStringPullinSound()
    {
        if (Mathf.Abs(_strength - _previousStrength) > _stringSoundThreshold)
        {
            if (_strength < _previousStrength)
            {
                _audioSource.pitch = -1;
            }
            else
            {
                _audioSource.pitch = 1;
            }
            _audioSource.UnPause();
        }
        else
        {
            _audioSource.Pause();
        }
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void HandleStringPulledBackTolimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= _bowStringStretchLimit)
        {
            _audioSource.Pause();
            _strength = 1;
            _midPointVisualObject.localPosition = new Vector3(0, 0, -_bowStringStretchLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z >= 0)
        {
            _audioSource.pitch = 1;
            _audioSource.Stop();
            _strength = 0;
            _midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
