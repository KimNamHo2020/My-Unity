using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("계단")]
    [Space(10)]
    public GameObject[] stairs;

    private enum State { Start, Left, Right };
    private State state;
    private Vector3 oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        Init();
        InitStairs();
    }

    private void Init()
    {
        state = State.Start;
        oldPosition = new Vector3(0.75f, -0.1f, 0); // 시작 위치 설정
    }

    private void InitStairs()
    {
        float stairSpacing = 0.75f; // 계단 간격

        for (int i = 0; i < stairs.Length; i++)
        {
            // 무작위로 오른쪽 또는 왼쪽으로 이동할지 결정
            int rand = Random.Range(0, 2);
            if (rand == 0) // 오른쪽으로 이동
            {
                state = State.Right;
            }
            else // 왼쪽으로 이동
            {
                state = State.Left;
            }

            // 이동 방향에 따라 x값 결정
            float newX = oldPosition.x + (state == State.Right ? stairSpacing : -stairSpacing);

            // y값 증가는 항상 +0.5로 고정
            float newY = oldPosition.y + 0.5f;

            // 새로운 위치 설정
            Vector3 newPosition = new Vector3(newX, newY, 0);

            // 각 계단의 위치를 새로 계산된 위치로 설정합니다.
            stairs[i].transform.position = newPosition;

            // 다음 계단을 위한 이전 위치를 업데이트합니다.
            oldPosition = newPosition;
        }
    }
}
