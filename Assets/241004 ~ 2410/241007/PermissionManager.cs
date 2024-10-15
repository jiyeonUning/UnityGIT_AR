using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    [SerializeField] GPSManager GPSManager;

    private void Start()
    {
        Request(Permission.FineLocation);
    }


    // ������ ��ö�� �� �ִ� �Լ� �ڵ� ���� 
    public void Request(string targetPermission)
    {
        // �̹� ������ ������ �Ǿ��մ� ��Ȳ�̶��, ������ �ٽ� ���� �ʵ��� if���� ����
        if (Permission.HasUserAuthorizedPermission(targetPermission)) { OnSuccessed("Already Granted"); return; }


        // ���� ������ ���� ���� ��Ȳ�̶��, ������ �޵��� �ڵ带 ����

        // ���� ó�� ����� ���� (���� / ���� / ���̻� ����X) ��, ������ ���� ������ ��� ��ü�� ����
        PermissionCallbacks callbacks = new PermissionCallbacks();

        // �������� ����� ������ �̺�Ʈ�� �ٿ� �����Ѵ�.
        callbacks.PermissionGranted += OnSuccessed;
        // �ź����� ����� ������ �̺�Ʈ�� �ٿ� �����Ѵ�.
        callbacks.PermissionDenied += OnDenied;

        // ���� ��û �õ�
        Permission.RequestUserPermission(targetPermission, callbacks);
    }

    void OnSuccessed(string str)
    {
        Debug.Log("���� ����");
        GPSManager.GPSon();
    }

    void OnDenied(string str)
    {
        Debug.Log("���� �źε�");
        GPSManager.GPSoff();
    }
}
