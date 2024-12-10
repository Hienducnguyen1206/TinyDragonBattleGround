using UnityEngine;
using UnityEngine.EventSystems;

public class MapDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private Camera minimapCamera;  
    [SerializeField] private RectTransform mapUI;  
    private Vector2 lastPosition;  
    public static MapDrag Instance { get; private set; }
    [SerializeField] float DragSpeed = 0.5f;
    private float orthoSize;
    float Msize;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        minimapCamera = MinimapCameraFollow.Instance.GetComponent<Camera>();
       
    }

    public void SetRectMapUI(RectTransform mapUI)
    {
        this.mapUI = mapUI;
    }

    private void Update()
    {
       
        orthoSize = minimapCamera.orthographicSize;
        Msize = -0.9f * orthoSize + 382.5f;

        Vector3 position = minimapCamera.transform.position;

        float xMin = -Msize, xMax = Msize;
        float zMin = -Msize, zMax = Msize;

        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.z = Mathf.Clamp(position.z, zMin, zMax);

        minimapCamera.transform.position = position;
    }

    public void OnDrag(PointerEventData eventData)
    {
       
      
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapUI, eventData.position, eventData.pressEventCamera, out localPoint);

  
        Vector2 delta = -localPoint + lastPosition;

      
        MoveMinimapCamera(delta);

  
        lastPosition = localPoint;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MinimapCameraFollow.Instance.FollowPlayer == true)
        {
            MinimapCameraFollow.Instance.FollowPlayer = false;
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapUI, eventData.position, eventData.pressEventCamera, out lastPosition);
    }

    private void MoveMinimapCamera(Vector2 delta)
    {
        
        
        
        Vector3 newPosition = minimapCamera.transform.position + new Vector3(delta.x  *DragSpeed* orthoSize/250f, 0, delta.y *DragSpeed* orthoSize/250f);

        float xMin = -Msize, xMax = Msize;  
        float zMin = -Msize, zMax = Msize;  


      
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);

      
        minimapCamera.transform.position = newPosition;

    }
}
