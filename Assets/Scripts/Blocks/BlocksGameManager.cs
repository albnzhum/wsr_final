using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlocksGameManager : MonoBehaviour
{
    public Transform[] blocks; // Массив блоков на поле
    public Transform block;
    public float blockAppearTime = 0.2f; // Время появления одного блока
    public float moveDuration = 1f; // Время перемещения блоков в центр

    private bool isLevelAppearing = false; // Флаг для контроля анимации появления уровня
    private bool isLevelAppeared = false;

    private void Start()
    {
        // Начать анимацию появления уровня
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
        // Обработка ввода от игрока
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

        // Появление уровня - поочередное появление блоков
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].gameObject.SetActive(true); // Включить блок
            yield return new WaitForSeconds(blockAppearTime);
        }

        isLevelAppearing = false;
        CreateBlocks();
    }

    private IEnumerator MoveBlocks(Vector3 direction)
    {// Вычисление конечной позиции блока в центре поля
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
