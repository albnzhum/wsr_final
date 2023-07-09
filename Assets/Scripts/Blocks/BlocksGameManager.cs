using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlocksGameManager : MonoBehaviour
{
    public Transform[] blocks; // ������ ������ �� ����
    public Transform block;
    public float blockAppearTime = 0.2f; // ����� ��������� ������ �����
    public float moveDuration = 1f; // ����� ����������� ������ � �����

    private bool isLevelAppearing = false; // ���� ��� �������� �������� ��������� ������
    private bool isLevelAppeared = false;

    private void Start()
    {
        // ������ �������� ��������� ������
        StartCoroutine(AppearLevel());
    }

    private void CreateBlocks()
    {
        System.Random rand = new System.Random();
        int index = rand.Next(blocks.Length);
        if (index != 0 && index != 1 && index != 2 && index != 3 )
        {
            Instantiate(block, blocks[index].position, Quaternion.identity);
        }
        else
        {
            CreateBlocks();
        }
    }

    private void Update()
    {
        // ��������� ����� �� ������
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isLevelAppearing)
                StartCoroutine(MoveBlocks(Vector3.up));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isLevelAppearing)
                StartCoroutine(MoveBlocks(Vector3.down));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isLevelAppearing)
                StartCoroutine(MoveBlocks(Vector3.left));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isLevelAppearing)
                StartCoroutine(MoveBlocks(Vector3.right));
        }
    }

    private IEnumerator AppearLevel()
    {
        isLevelAppearing = true;

        // ��������� ������ - ����������� ��������� ������
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].gameObject.SetActive(true); // �������� ����
            yield return new WaitForSeconds(blockAppearTime);
        }

        isLevelAppearing = false;
        CreateBlocks();
    }

    private IEnumerator MoveBlocks(Vector3 direction)
    {// ���������� �������� ������� ����� � ������ ����
        Vector3 targetPosition = Vector3.zero;
        for (int i = 0; i < blocks.Length; i++)
        {
            targetPosition += blocks[i].position;
        }
        targetPosition /= blocks.Length;

        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;

            block.transform.position += direction * Time.deltaTime;

            yield return null;
        }


    }
}
