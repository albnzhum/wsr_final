using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class materialScript : MonoBehaviour
{
    public SpriteAtlas spriteAtlas; // ������ �� ������-�����
    public string spriteName = "���_0"; // ��� ������� � ������

    void Start()
    {
        // �������� ������ �� ������ �� ������
        Sprite sprite = spriteAtlas.GetSprite(spriteName);

        // ������� ����� ��������
        Material material = new Material(Shader.Find("Standard"));

        // ���������� �������� ������� � ��������
        material.mainTexture = sprite.texture;

        // ��������� �������� � ������ �������
        GetComponent<Renderer>().material = material;
    }
}
