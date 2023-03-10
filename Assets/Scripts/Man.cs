
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class Man : MonoBehaviour, IControllable
{
    private NavMeshAgent _navMeshAgent;
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Outline _outline;
    private Marker _marker;
    
    private Animator _animator;
    private float _animationBlend;

    public Material controlMaterial;
    public Material releaseMaterial;

    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
    public bool InControl { get; set; }

    public float motionSpeed;
    
    private void Awake()
    {
        // Get the NavMeshAgent component
        _navMeshAgent = GetComponent<NavMeshAgent>();

        //find skinned mesh renderer in chilren
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _outline = GetComponent<Outline>();

        _marker = FindFirstObjectByType<Marker>();

        _animator = GetComponent<Animator>();
    }

    public void ToggleControl()
    {
        if (InControl)
        {
            InControl = false;
            ReleaseControl();
        }
        else
        {
            InControl = true;
            TakeControl();
        }
    }

    public void ReleaseControl()
    {
     
        _outline.enabled = false;

        Material[] mats = _skinnedMeshRenderer.materials;

        for (int i = 0; i < _skinnedMeshRenderer.materials.Length; i++)
        {
            mats[i] = releaseMaterial;
        }
        _skinnedMeshRenderer.materials = mats;

        _outline.enabled = true;
        _outline.OutlineColor = controlMaterial.color;
    }

    public void TakeControl()
    {
       
        _outline.enabled = false;

        Material[] mats = _skinnedMeshRenderer.materials;
        
        for (int i = 0; i < _skinnedMeshRenderer.materials.Length; i++)
        {
            mats[i] = controlMaterial;
        }
        _skinnedMeshRenderer.materials = mats;

        _outline.enabled = true;
        _outline.OutlineColor = releaseMaterial.color;
    }

    private void Update()
    {
        if (InControl)
        {
            if (_marker.InControl)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_marker.transform.position);
            }
            else
            {
                _navMeshAgent.isStopped = true;
            }
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, _navMeshAgent.velocity.magnitude, Time.deltaTime * 10f);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat("Speed", _animationBlend);
        _animator.SetFloat("MotionSpeed", motionSpeed);
    }

    [Button]
    public void TestTakeControl()
    {
        Debug.Log("Take Control Man");
        TakeControl();
    }

    [Button]
    public void TestReleaseControl()
    {
        Debug.Log("Release Control Man");
        ReleaseControl();
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(transform.position), FootstepAudioVolume);
            }
        }
    }
}
