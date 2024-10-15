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


    // 권한을 요철할 수 있는 함수 코드 구성 
    public void Request(string targetPermission)
    {
        // 이미 권한이 승인이 되어잇는 상황이라면, 승인을 다시 받지 않도록 if문을 구성
        if (Permission.HasUserAuthorizedPermission(targetPermission)) { OnSuccessed("Already Granted"); return; }


        // 아직 승인을 받지 않은 상황이라면, 승인을 받도록 코드를 구성

        // 권한 처리 결과에 대해 (승인 / 거절 / 더이상 요정X) 시, 반응에 대한 정보를 담는 객체를 선언
        PermissionCallbacks callbacks = new PermissionCallbacks();

        // 승인했을 경우의 반응을 이벤트로 붙여 구현한다.
        callbacks.PermissionGranted += OnSuccessed;
        // 거부했을 경우의 반응을 이벤트로 붙여 구현한다.
        callbacks.PermissionDenied += OnDenied;

        // 권한 요청 시도
        Permission.RequestUserPermission(targetPermission, callbacks);
    }

    void OnSuccessed(string str)
    {
        Debug.Log("권한 허용됨");
        GPSManager.GPSon();
    }

    void OnDenied(string str)
    {
        Debug.Log("권한 거부됨");
        GPSManager.GPSoff();
    }
}
