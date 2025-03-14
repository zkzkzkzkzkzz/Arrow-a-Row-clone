using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BossChest : MonoBehaviour
{
    private ChestPool chestPool;
    private Animator animator;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        chestPool = FindObjectOfType<ChestPool>();
        animator = GetComponent<Animator>();

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
        }
    }

    private void OpenChest()
    {
        animator.SetBool("isTrigger", true);

        RewardManager rewardMgr = FindAnyObjectByType<RewardManager>();
        if (rewardMgr != null)
            rewardMgr.ShowRewardSelection();

        StartCoroutine(ReturnChestAfterDelay(1f));
    }

    public void IdleBossChest()
    {
        animator.SetBool("isTrigger", false);
    }

    /// <summary>
    /// 애니메이션 재생 이후 풀로 자동 반환
    /// </summary>
    private IEnumerator ReturnChestAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        chestPool.ReturnBossChest(gameObject);
    }
}
