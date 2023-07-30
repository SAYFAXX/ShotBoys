using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform pathChart;
    [SerializeField] private PathType pathType;
    [SerializeField] private float hareketSuresi = 5f; // Karakterin hareket etmesi i�in belirlenen s�re

    private Vector3[] pathArray;
    private bool isReversed = false;
    public Animator animator;
    bool isStarted = false;
    Sequence mySequence, mySequence2, mySequence3;
    private void Update()
    {
        GameState();
    }
    public void GameState()
    {
        if (GameManager.instance.gameSit == GameManager.GameSit.Started && !isStarted)
        {
            isStarted = true;
            animator = GetComponent<Animator>();
            DOVirtual.DelayedCall(2, PathNodes);
        }
        //else if (GameManager.instance.gameSit == GameManager.GameSit.GameOver && isStarted)
        //{
        //    animator.SetBool("dance", true);
        //    isStarted
        //        = false;
        //}
    }
    public void PathNodes()
    {
        pathArray = new Vector3[pathChart.childCount];
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathChart.GetChild(i).position;
        }

        FollowPath();
    }

    void FollowPath()
    {
        animator.SetBool("run", true);

        if (!isReversed) // Follow the path in normal order
        {
            mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOPath(pathArray, hareketSuresi, pathType).SetLookAt(0.001F).OnComplete(ContinueCharacter).SetEase(Ease.Linear));

        }
        else // Follow the path in reverse order
        {
            Vector3[] reversedPath = new Vector3[pathArray.Length];
            for (int i = 0; i < pathArray.Length; i++)
            {
                reversedPath[i] = pathArray[pathArray.Length - 1 - i];
            }

            mySequence2 = DOTween.Sequence();
            mySequence2.Append(transform.DOPath(reversedPath, hareketSuresi, pathType).SetLookAt(0.001F).OnComplete(ContinueCharacter).SetEase(Ease.Linear));

        }
    }

    void ContinueCharacter()
    {
        animator.SetBool("run", false);

        isReversed = !isReversed;


        mySequence3 = DOTween.Sequence();
        mySequence3.Append(DOVirtual.DelayedCall(2, FollowPath));
    }

    public void DotweenKill()
    {
        mySequence.Kill();
        mySequence2.Kill();
        mySequence3.Kill();
    }
}

