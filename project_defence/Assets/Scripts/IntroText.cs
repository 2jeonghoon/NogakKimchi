using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroText : MonoBehaviour
{
    private string[] introStoryArr;
    private int currentidx;
    public TMP_Text ScriptText;
<<<<<<< HEAD
    private const int lastPage = 5;
=======
    private const int lastPage = 7;
>>>>>>> origin/Jeonghoon

    void Start()
    {
        currentidx = 0;
<<<<<<< HEAD
        introStoryArr = new string[6];
        introStoryArr[0] = "";
        introStoryArr[1] = "옛날 옛적, 요정들이 살고 있는 신비로운 곳인 디저트 카페 왕국에 두 형제 뚱카롱(가명)과 도넛(가명)이, 아버지인 아메리카노(가명) 왕의 통치하에 평화롭게 살고 있었다.";
        introStoryArr[2] = "첫 째인 뚱카롱 왕자는 빵 요정들을 제외한 모든 디저트 요정들을 다스리고 있었고, 둘 째인 도넛 왕자는 빵 요정들을 다스리고 있었다";
        introStoryArr[3] = "어느 날 아메리카노 왕은 두 아들을 불러모아 왕위 계승을 정하기 위한 시험을 내주었다. 시험은 삼 일 뒤에 두 왕자가 서로 힘을 겨루어 디저트 카페에 방문한 손님 한 명을 두고 먼저 살을 찌우는 왕자에게 왕위를 물려주겠다는 내용이었다. 첫째 뚱카롱 왕자는 이미 결과는 정해진 수순이라고 생각하며 자신만만해 하는데, 둘째 도넛 왕자는 무언가를 골똘히 생각하더니 조용히 어딘가로 사라졌다.";
        introStoryArr[4] = "시험 당일이 되어 둘째 왕자가 다시 나타났을 때, 첫째 왕자는 뭔가 잘못 되었다는 것을 깨달았지만, 이미 때는 늦어버렸다. 둘째 도넛 왕자는 악마의 힘을 빌려 디저트 카페에서는 금지된 마약인 ‘슈가 파우더‘를 사용하여 빵 요정의 세력을 막강하게 키워서 돌아온 것이다. 악마의 힘에 눈이 먼 도넛 왕자와 빵 요정들은 왕국의 다른 요정들을 무시하고 괴롭혔고, 결국 두 세력은 서로 적대적으로 변하게 되었다. 첫째 왕자는 이대로 도넛 왕자가 왕위를 계승하게 되면 악마에 의해 디저트 카페 왕국이 몰락할 것을 직감했다. 왕위 쟁탈전에서 반드시 승리해서 동생을 악마로부터 구하고, 디저트 카페 왕국의 평화를 찾을 것을 다짐한다.";
        introStoryArr[5] = "";
=======
        introStoryArr = new string[8];
        introStoryArr[0] = "";
        introStoryArr[1] = "옛날 옛적, 요정들이 살고 있는 신비로운 곳인 디저트 카페 왕국에 두 형제 뚱카롱(가명)과 도넛(가명)이, 아버지인 아메리카노(가명) 왕의 통치하에 평화롭게 살고 있었다.";
        introStoryArr[2] = "첫 째인 뚱카롱 왕자는 빵 요정들을 제외한 모든 디저트 요정들을 다스리고 있었고, 둘 째인 도넛 왕자는 빵 요정들을 다스리고 있었다";
        introStoryArr[3] = "어느 날 아메리카노 왕은 두 형제를 불러 모아 왕위 계승을 정하기 위한 시험을 내주었다. 시험은 삼 일 뒤에 디저트 카페에 방문한 손님 한 명을 두고, 두 왕자가 서로 힘을 겨루어 손님의 살을 더 많이 찌우는 쪽이 왕위를 계승받는 것이었다.";
        introStoryArr[4] = "첫째 왕자 뚱카롱은 의욕이 넘쳤고, 시험에서 이길 것이라고 자신했다. 그러는 사이에 둘째 왕자 도넛은 잠시동안 무언가를 골똘히 생각하더니 조용히 어딘가로 사라졌고, 그 뒤로는 모습을 보이지 않았다.\n";
        introStoryArr[5] = "시험 당일이 되어 모습을 감추었던 도넛이 나타났을 때, 뚱카롱 왕자는 무엇인가 대단히 잘못되었음을 직감했다. 하지만 이미 때는 늦어버렸다. 도넛 왕자는 악마의 힘을 빌려 디저트 카페에서는 금지된 마약인 ‘슈가 파우더’를 사용하여 빵 요정의 세력을 막강하게 키워서 돌아온 것이었다.";
        introStoryArr[6] = "악마의 힘에 눈이 먼 빵 요정들은 다른 요정들을 괴롭혔고, 결국 디저트 카페 왕국의 두 세력은 적대적으로 변했다. 뚱카롱 왕자는 디저트 카페 왕국과 동생인 도넛 왕자를 구하기 위해 반드시 시험에서 승리할 것을 다짐한다.\n";
        introStoryArr[7] = "";
>>>>>>> origin/Jeonghoon

        ScriptText.text = introStoryArr[0];
    }

    // Update is called once per frame
    public void NextStory() {
        if(currentidx >= lastPage) return;
        currentidx += 1;
        ScriptText.text = introStoryArr[currentidx];
    }

    public void PreviousStory() {
        if(currentidx <= 0) return;
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