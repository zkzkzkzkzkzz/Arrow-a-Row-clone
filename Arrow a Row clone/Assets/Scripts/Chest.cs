using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private ChestPool chestPool;
    private Animator animator;
    private StatType stat;
    private float value;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        chestPool = FindObjectOfType<ChestPool>();
        animator = GetComponent<Animator>();

        stat = GetRandomChestStat();
        value = CheckStatValue(stat);

        DisplayText();

        if (audioSource != null)
            audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
                audioSource.Play();
            OpenChest();
            other.GetComponent<Player>().IncreaseStat(stat, value);
        }
    }

    private StatType GetRandomChestStat()
    {
        StatType[] validStats = System.Enum.GetValues(typeof(StatType)).Cast<StatType>()
                        .Where(stat => stat.CanBeAppliedInChest())
                        .ToArray();

        return validStats[Random.Range(0, validStats.Length)];
    }

    private float CheckStatValue(StatType type)
    {
        float res = 0;

        switch (type)
        {
            case StatType.ARROWATK:
            case StatType.ARROWRATE:
            case StatType.ARROWSPEED:
            case StatType.ARROWRANGE:
            case StatType.SWORDSPEED:
            case StatType.SWORDRANGE:
            case StatType.SWORDCNT:
                res = (float)WeightedRandom(1, 2, 0.9f);
                break;
            case StatType.SWORDATK:
                res = (float)WeightedRandom(1, 2, 0.9f) / 2f;
                break;
            case StatType.SWORDRATE:
                res = (float)WeightedRandom(1, 2, 0.9f) * 5f;
                break;
            default:
                break;
        }

        return res;
    }

    private int WeightedRandom(int v1, int v2, float fvalue)
    {
        return Random.value < fvalue ? v1 : v2;
    }

    private void OpenChest()
    {
        animator.SetBool("isTrigger", true);
        StartCoroutine(ReturnChestAfterDelay(1f));
    }

    public void IdleChest()
    {
        animator.SetBool("isTrigger", false);
    }

    /// <summary>
    /// 애니메이션 재생 이후 풀로 자동 반환
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator ReturnChestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        chestPool.ReturnChest(gameObject);
    }

    private void DisplayText()
    {
        if (stat == StatType.SWORDATK)
            GetComponentInChildren<TextMeshPro>().text = stat.GetStatName() + "\n+" + value.ToString("F1");
        else if (stat == StatType.SWORDRATE)
            GetComponentInChildren<TextMeshPro>().text = stat.GetStatName() + "\n-" + ((int)value).ToString() + "%";
        else
            GetComponentInChildren<TextMeshPro>().text = stat.GetStatName() + "\n+" + ((int)value).ToString();
    }
}
