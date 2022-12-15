using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroText : MonoBehaviour
{
    private string[] introStoryArr;
    private int currentidx;
    public TMP_Text ScriptText;
    private const int lastPage = 5;

    void Start()
    {
        currentidx = 0;
        introStoryArr = new string[6];
        introStoryArr[0] = "";
        introStoryArr[1] = "옛날 옛적, 요정들이 사는 신비로운 곳인 디저트 카페 왕국에 뚱카롱과 도넛 형제가 아버지인 아메리카노 왕의 통치하에 평화롭게 살고 있었다. 마법의 힘이 깃든 이곳에는 디저트 요정들과 빵 요정들도 살고 있었다.\n";
        introStoryArr[2] = "첫째 왕자인 뚱카롱은 디저트 카페 왕국에서 디저트 요정들을 다스렸고, 둘째 왕자인 도넛은 빵 요정들을 다스렸다. 두 세력은 서로 사이좋게 공존하고 있었고, 아름다운 조화를 이루었다.\n";
        introStoryArr[3] = "어느 날 아메리카노 왕은 두 형제를 불러 모아 왕위 계승을 정하기 위한 시험을 내주었다. 시험은 삼 일 뒤에 디저트 카페에 방문한 손님 한 명을 두고, 두 왕자가 서로 힘을 겨뤄 손님의 살을 더 많이 찌우는 쪽이 왕위를 얻는 것이었다. 둘째 도넛 왕자는 잠시 곰곰이 생각하더니 조용히 어딘가로 사라졌다.\n";
        introStoryArr[4] = "시험 당일이 되어 모습을 감추었던 둘째 왕자와 도넛들이 나타났을 때, 뚱카롱 왕자는 무엇인가 대단히 잘못되었음을 직감했다. 하지만 이미 때는 늦어버렸다. 평화롭던 디저트 카페 왕국에 어두운 먹구름이 들이닥쳤다.\n";
        introStoryArr[5] = "";

        ScriptText.text = introStoryArr[0];
    }

    // Update is called once per frame
    public void NextStory()
    {
        if (currentidx >= lastPage) return;
        currentidx += 1;
        ScriptText.text = introStoryArr[currentidx];
    }

    public void PreviousStory()
    {
        if (currentidx <= 0) return;
        currentidx -= 1;
        ScriptText.text = introStoryArr[currentidx];
    }


}
/*
 * File : IntroText.cs
 * Desc
 *	: Book Scene에서 인트로 스토리 텍스트에서 페이지가 넘어가면 해당 페이지에 맞는 텍스트를 출력해줌
 *	
 * Functions
 *	NextStory() : Book 스크립트에서 참조해서 페이지가 뒤로 넘어갈 때 다음 텍스트 출력
 *	PrivateStory() : Book 스크립트에서 참조해서 페이지가 앞으로 넘어갈 때 이전 텍스트 출력
 */