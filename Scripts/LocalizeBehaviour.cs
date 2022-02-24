using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Data;
using TMPro;

namespace DKCommon
{
    ///<summary>
    ///현지화 해야할 텍스트에 붙어 텍스트를 현지화 해줌
    ///</summary>
    ///
    public class LocalizeBehaviour : MonoBehaviour
    {
        // 키값이다. 외부 스크립트 혹은 인스펙터에서 사람이 입력한다.
        public string key;
        // 현지화 할 텍스트 컴포넌트
        private TextMeshProUGUI textUI;
        // 현지화된 문자열
        private string localizedString;
        // 중괄호 기준으로 왼쪽, 오른쪽 상수
        private const int LEFT = 0;
        private const int RIGHT = 1;

        // 시작 시 한번 업데이트 한다.
        private void Start()
        {
            // 텍스트 컴포넌트를 받아온다.
            textUI = GetComponent<TextMeshProUGUI>();
            Refresh();
        }

        // 텍스트 컴포넌트를 현지화하는 함수 
        public void Refresh()
        {
            if (!textUI)
            {
                textUI = GetComponent<TextMeshProUGUI>();
            }
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            // 지정된 키를 변수가 처리되지 않은 문자열로 변환한다.
            localizedString = Localizer.KeyToTerm(key);
            // 키값이 존재하여 문자열이 제대로 생성 되었으면
            if (localizedString != null)
            {
                // 중괄호 안의 변수들을 처리한다.
                Parse();
                // 현지화 및 중괄호 처리된 문자열을 텍스트 컴포넌트에 입력한다.
                textUI.text = localizedString;
            }
            else
            {
                textUI.text = key;
            }
        }

        public void SetKey(string newKey)
        {
            key = newKey;
        }

        private void Parse()
        {
            string result = "";

            // 중괄호가 있으면
            if (localizedString.Contains("{"))
            {
                // 중괄호 시작마다 나눔
                string[] leftBraceChunk = localizedString.Split('{');
                for (int i = 0; i < leftBraceChunk.Length; i++)
                {
                    // 중괄호 끝을 포함하고 있으면 중괄호 기준으로 왼쪽은 변수, 오른쪽은 그냥 문자열이다.
                    if (leftBraceChunk[i].Contains("}"))
                    {
                        string[] rightBraceChunk = leftBraceChunk[i].Split('}');
                        // 변수 항들을 숫자로 대체하고 계산한다.
                        //result += new DataTable().Compute(ReplaceVariables(rightBraceChunk[LEFT]), null).ToString();
                        result += Localizer.GetVar(rightBraceChunk[LEFT]);
                        // 괄호 밖 문자열을 더한다.
                        result += rightBraceChunk[RIGHT];
                    }
                    // 중괄호 끝을 포함하고 있지 않으면 그냥 문자열이다.
                    else
                    {
                        result += leftBraceChunk[i];
                    }
                }
            }
            // 대괄호, 중괄호가 없으면 그냥 문자열이다.
            else
            {
                result = localizedString;
            }
            localizedString = result;
        }
    }
}