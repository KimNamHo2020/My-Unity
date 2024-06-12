using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 oldPosition;
    private bool isTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 시작 위치 설정
        startPosition = new Vector3(0.75f, -0.1f, 0);
        transform.position = startPosition;
        oldPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 클릭하면
        {
            CharMove(); // 캐릭터 이동 함수 호출
        }
        else if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼을 클릭하면
        {
            CharTurn(); // 캐릭터 방향 전환 함수 호출
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면
        {
            ResetPosition(); // 캐릭터 위치 초기화 함수 호출
        }

        // 계단과 캐릭터의 위치를 확인하여 사망 처리
        CheckDeath();
    }

    private void CharTurn()
    {
        isTurn = !isTurn;
        spriteRenderer.flipX = isTurn;
    }

    private void CharMove()
    {
        // 캐릭터 이동 방향에 따라 x값과 y값 설정
        float newX = isTurn ? -0.75f : 0.75f;
        float newY = 0.5f;

        // 현재 위치에 이동량 추가
        oldPosition.x += newX;
        oldPosition.y += newY;

        // 캐릭터 이동
        transform.localPosition = oldPosition;

        // 애니메이션 실행
        anim.SetTrigger("Move");
    }

    private void ResetPosition()
    {
        // 처음 위치로 캐릭터를 이동
        transform.position = startPosition;
        oldPosition = startPosition;
    }

    private void CheckDeath()
    {
        // GameManager에서 계단 배열 가져오기
        GameObject[] stairs = GameManager.Instance.stairs;

        // 계단과 캐릭터의 위치 비교하여 겹치는지 확인
        bool isOnStair = false;
        foreach (GameObject stair in stairs)
        {
            if (stair.transform.position == transform.position)
            {
                isOnStair = true;
                break;
            }
        }

        // 계단과 겹치지 않으면 사망 처리
        if (!isOnStair)
        {
            Debug.Log("Die");
            ResetPosition(); // 캐릭터 위치 초기화
        }
    }
}
