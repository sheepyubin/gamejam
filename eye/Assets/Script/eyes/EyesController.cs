using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyesController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text NameText = null;
    [SerializeField] private Text ClassText = null;
    [SerializeField] private Image ItemImage = null;
    private EyesDB.EyesRow EyeInfo = null;

    public SpriteRenderer ItemRenderer;

    public Sprite[] Eyes;

    bool isEyes = false;
    GameObject player;

    private void Awake()
    {
        //player = FindObjectOfType<Player>();
        EyeInfo = new EyesDB.EyesRow();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isEyes == true)
        {
            //player.ItemNum++;
            //Debug.Log(player.ItemNum);
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.F) && isEyes == true)
        {
            Debug.Log("�ȵ�");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            isEyes = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isEyes = false;
        }
    }
    public static EyesController Create(int id)
    {
        GameObject IT = Instantiate(Resources.Load("Eyes/eye")) as GameObject;


        EyesController Eye = IT.GetComponent<EyesController>();
        Eye.Init(id);

        return Eye;
    }
    public void Init(int id)
    {
        EyesDB gameDB = EyesDB.GetSingleton();
        List<EyesDB.EyesRow> result = gameDB.GetEyes(id);

        if (result != null)
        {
            EyeInfo = result[0];
            ItemImage.sprite = Eyes[id];
            ItemRenderer.sprite = Eyes[id];
            RefreshItemInfo();
        }
    }
    private void RefreshItemInfo()
    {

        NameText.text = " �� " + EyeInfo.Name + "�� ��";
        ClassText.text = " ���� \n -" + EyeInfo.Class;
    }
}
