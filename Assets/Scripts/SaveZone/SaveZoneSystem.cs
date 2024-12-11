using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Photon.Pun;

public class SafeZoneSystem : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    class SaveZone
    {
        public Vector3 scale;
        public int duration; // Thời gian co lại
        public int delay;    // Thời gian chờ trước khi co lại
        public float damagePerSecond;
    }

    public static SafeZoneSystem Instance;
    [SerializeField] List<SaveZone> SaveZoneList = new List<SaveZone>();
    [SerializeField] CircleDrawer circleDrawer;
 

    private double startTime;
    private double elapsedTime;
    public string timeFormatted ;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            double masterStartTime = PhotonNetwork.Time;
            photonView.RPC("StartSafeZoneSystem", RpcTarget.All, masterStartTime);
        }
    }

  

    [PunRPC]
    private void StartSafeZoneSystem(double masterStartTime)
    {
        startTime = masterStartTime; // Đồng bộ thời gian bắt đầu
        Sequence sequence = DOTween.Sequence();

        Vector3 firstSafeZoneCenter = GetRandomCenterPosition(100f, transform.position);
        sequence.AppendCallback(() =>
        {
            circleDrawer.photonView.RPC("MoveToNewPosition", RpcTarget.All, firstSafeZoneCenter, 250f);
        });

        sequence.AppendCallback(() => CreateCountdown(70));
        sequence.AppendInterval(70f);
        sequence.AppendCallback(() => CreateCountdown(70));
        sequence.Append(transform.DOMove(firstSafeZoneCenter, 70f).SetEase(Ease.Linear));
        sequence.Join(transform.DOScale(500f, 70f).SetEase(Ease.Linear));

        Vector3 previousCenter = firstSafeZoneCenter;
        Vector3 currentScale = new Vector3(250f, 250f, 1f);

        for (int i = 0; i < SaveZoneList.Count - 1; i++)
        {
            SaveZone currentZone = SaveZoneList[i];
            Vector3 nextCenter = GetNextCenterPosition(currentZone.scale.x, previousCenter, currentScale.x);

            sequence.AppendCallback(() =>
             circleDrawer.photonView.RPC("MoveToNewPosition", RpcTarget.All, nextCenter, currentZone.scale.x / 2)
            );

            sequence.AppendCallback(() => CreateCountdown(currentZone.delay));
            sequence.AppendInterval(currentZone.delay);
            sequence.AppendCallback(() => CreateCountdown(currentZone.duration));

            sequence.Append(transform.DOMove(nextCenter, currentZone.duration).SetEase(Ease.Linear));
            sequence.Join(transform.DOScale(currentZone.scale, currentZone.duration).SetEase(Ease.Linear));

            previousCenter = nextCenter;
            currentScale = currentZone.scale;
        }

        sequence.AppendCallback(() =>
         circleDrawer.photonView.RPC("MoveToNewPosition", RpcTarget.All,previousCenter, 0.001f)
        );

        sequence.AppendCallback(() => CreateCountdown(SaveZoneList[^1].delay));
        sequence.AppendInterval(SaveZoneList[^1].delay);
        sequence.AppendCallback(() => CreateCountdown(SaveZoneList[^1].duration));
        sequence.Join(transform.DOScale(0.001f, SaveZoneList[^1].duration).SetEase(Ease.Linear));

        sequence.AppendCallback(() =>
        {
            circleDrawer.gameObject.SetActive(false);
        });
    }

    private Vector3 GetRandomCenterPosition(float maxOffset, Vector3 currentCenterPosition)
    {
        Vector2 randomPoint = Random.insideUnitCircle * maxOffset;
        return new Vector3(
            currentCenterPosition.x + randomPoint.x,
            currentCenterPosition.y,
            currentCenterPosition.z + randomPoint.y
        );
    }

    private Vector3 GetNextCenterPosition(float newScale, Vector3 currentCenterPosition, float currentScale)
    {
        float maxOffset = (currentScale - newScale) / 2;
        return GetRandomCenterPosition(maxOffset, currentCenterPosition);
    }

    private void CreateCountdown(int seconds)
    {
        double countdownStartTime = PhotonNetwork.Time;
        double countdownEndTime = countdownStartTime + seconds;

        DOTween.To(
            () => (float)(countdownEndTime - PhotonNetwork.Time),
            value =>
            {
                int remainingSeconds = Mathf.CeilToInt(value);
                if (remainingSeconds >= 0)
                {
                    int minutes = remainingSeconds / 60;
                    int secs = remainingSeconds % 60;
                     timeFormatted = $"{minutes}:{secs:D2}";
                    photonView.RPC("UpdateCountdownText", RpcTarget.All, timeFormatted);
                }
            },
            0f,
            seconds
        ).SetEase(Ease.Linear);
    }


    [PunRPC]
    private void UpdateCountdownText(string time)
    {
        timeFormatted = time;
       
    }
}
