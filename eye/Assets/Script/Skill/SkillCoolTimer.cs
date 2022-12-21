using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTimer : MonoBehaviour
{
    public Image skillFilter;

    public float coolTime;

    private float currentCoolTime;

    private bool canUseSkill = true;
    [Header("UI")]
    [SerializeField]
    private Button[] actionButtons;
    public KeyCode[] action;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(action[0]))
        {
            ActionButtonOnClick(0);
        }
    }
    private void ActionButtonOnClick(int btnIndex)
    {
        actionButtons[btnIndex].onClick.Invoke();
    }
    public void UseSkill()
    {
        if (canUseSkill)
        {
            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1;
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false;
        }
        else
        {
            Debug.Log("���� ��ų�� ����� �� �����ϴ�.");
        }
    }
    IEnumerator Cooltime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }
        canUseSkill = true;

        yield break;
    }
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1f);

            currentCoolTime -= 1f;
        }
        yield break;
    }
}
