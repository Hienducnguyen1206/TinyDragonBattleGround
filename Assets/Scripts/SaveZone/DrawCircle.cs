using UnityEngine;
using DG.Tweening;
using Photon.Pun; // Import thư viện DOTween

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawer : MonoBehaviour
{
    public float radius = 1000f; // Bán kính hình tròn
    public int segments = 100; // Số đoạn th
    public float width = 0.5f;
    private LineRenderer lineRenderer;
    public PhotonView photonView;
    public static CircleDrawer instance; // Singleton instance
    [SerializeField] Camera cam;
    private void Awake()
    {
        instance = this; // Assign singleton instance
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Configure LineRenderer
        lineRenderer.loop = false;
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = segments + 1; // Include an extra point to close the circle
        lineRenderer.widthMultiplier = width;

      //  DrawCircle(); // Draw the initial circle
    }

    private void Update()
    {
        lineRenderer.widthMultiplier =   0.5f + cam.orthographicSize/250f*0.5f ; // Update width dynamically

    }

    [PunRPC]
    public void DrawCircle()
    {
        float angle = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0, z)); // Set the position of each segment

            angle += 360f / segments;
        }
    }

    [PunRPC]
    public Tween MoveToNewPosition(Vector3 newPosition, float newRadius)
    {
       // Debug.Log("Draw Circle");

        // Tạo một Sequence để chứa cả 2 tween
        Sequence sequence = DOTween.Sequence();

        // Tween vị trí
        Tween positionTween = DOTween.To(() => transform.position, x => transform.position = x, newPosition, 1f).OnUpdate(() =>
        {
            DrawCircle(); // Cập nhật vòng tròn trong quá trình tween vị trí
        });

        // Thêm tween vị trí vào Sequence
        sequence.Append(positionTween);

        // Tween bán kính
        Tween radiusTween = DOTween.To(() => radius, x => radius = x, newRadius, 1f).OnUpdate(() =>
        {
            DrawCircle(); // Cập nhật vòng tròn trong quá trình tween bán kính
        });

        // Thêm tween bán kính vào Sequence
        sequence.Join(radiusTween);

        // Trả về sequence (Tween)
        return sequence;
    }

}
