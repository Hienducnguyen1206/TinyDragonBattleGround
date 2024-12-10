using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

public class MinimapCameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    private Camera cam;
    [SerializeField] private Slider ZoomSlider;
    public bool FollowPlayer = false;
    public static MinimapCameraFollow Instance { get; private set; }

    private bool wasFollowingPlayer = false; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        
        cam.orthographicSize = ZoomSlider.value;
    }

    private void LateUpdate()
    {
        if (FollowPlayer && playerTransform != null)
        {
            if (!wasFollowingPlayer)
            {
                SmoothFollowPlayer();
                SmoothResetZoom();
                wasFollowingPlayer = true;
            }
            else
            {
               
                Vector3 newPosition = playerTransform.position;
                newPosition.y = transform.position.y; 
                transform.position = newPosition;
            }
        }
        else
        {
            wasFollowingPlayer = false;
        }
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void SetZoomSlider(Slider slider)
    {
        ZoomSlider = slider;
    }

    private void SmoothFollowPlayer()
    {
        if (!DOTween.IsTweening(transform))
        {
            Vector3 targetPosition = playerTransform.position;
            targetPosition.y = transform.position.y; 

            transform.DOMove(targetPosition, 1f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    FollowPlayer = true; 
                });
        }
    }

    private void SmoothResetZoom()
    {
        if (!DOTween.IsTweening(ZoomSlider))
        {
            float minZoomValue = ZoomSlider.minValue;

            ZoomSlider.DOValue(minZoomValue, 1f)
                .SetEase(Ease.OutCubic)
                .OnUpdate(() =>
                {
                    cam.orthographicSize = ZoomSlider.value; 
                })
                .OnComplete(() =>
                {
                    FollowPlayer = true; 
                });
        }
    }



}
