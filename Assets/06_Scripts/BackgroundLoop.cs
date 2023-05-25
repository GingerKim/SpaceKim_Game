﻿using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이

    private void Awake()
    {
        // 가로 길이를 측정하는 처리
        // boxCollider2D 컴포넌트의 size 필드의 x 값을 가로 길이로 사용
        // 오브젝트의 가로 길이와 박스컬라이더의 사이즈 x 값이 맞지 않으면 배경이 끊어지거나 겹침
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }

    private void Update()
    {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
        if (transform.position.x <= -width)
        {
            Reposition();
        }
    }

    // 위치를 리셋하는 메서드
    private void Reposition()
    {
        //현재 위치에서 오른쪽으로 가로길이 *2 만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2) transform.position + offset;
        
    }
}