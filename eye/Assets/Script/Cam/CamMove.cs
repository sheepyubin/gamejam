using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;

    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;


    private void Awake()
    {
        Cam1.depth= 0;
        Cam2.depth = -1;
        Cam1.enabled = true;
        Cam2.enabled = true;
    }

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>(); //플레이어 위치

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea() //카메라 범위
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos() //범위 그리기
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

    private void OnTriggerEnter2D(Collider2D collision) //카메라 전환 (스테이지 넘어 갈 때)
    {
        if (collision.CompareTag("Stage1"))
        {
            Debug.Log("1");
            ShowCam1View();
        }
        if (collision.CompareTag("Stage2"))
        {
            Debug.Log("2");
            ShowCam2View();
        }
    }

    public void ShowCam1View()
    {
        Cam1.enabled = true;
        Cam2.enabled = false;
    }

    public void ShowCam2View()
    {
        Cam1.enabled = false;
        Cam2.enabled = true;
    }
}