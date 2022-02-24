using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKCommon
{
    // 언어는 현재 한국어
    public enum Language { Korean, English, Japanese };

    ///<summary>번역 문서를 읽어들여 저장</summary>
    public static class Localizer
    {
        // 번역 문서
        private static TextAsset mainTextAsset;
        private static TextAsset dialogTextAsset;
        private static TextAsset questTextAsset;
        private static TextAsset serverErrorMessages;
        private static TextAsset fateLevelUpTextAsset;
        public static Language Language { get; private set; }
        private static readonly Dictionary<string, string> localizationItems = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> serverErrorMessageItems = new Dictionary<string, string>();
        private static string LANGUAGE_PLAYERPREFS_KEY;
        private static Dictionary<string, string> globalVariables = new Dictionary<string, string>();
        // 중괄호 기준으로 왼쪽, 오른쪽 상수
        private const int LEFT = 0;
        private const int RIGHT = 1;

        static Localizer()
        {
            SetLanguage();
            Parse();
        }

        public static void SetVar(string varName, string value)
        {
            if (globalVariables.ContainsKey(varName))
            {
                globalVariables[varName] = value;
            }
            else
            {
                globalVariables.Add(varName, value);
            }
        }

        public static void SetVar(string varName, float value)
        {
            SetVar(varName, value.ToString());
        }

        public static void SetVar(string varName, long value)
        {
            SetVar(varName, value.ToString());
        }

        public static string GetVar(string varName)
        {
            if (globalVariables.ContainsKey(varName))
            {
                return globalVariables[varName];
            }
            else
            {
                return varName;
            }
        }

        public static string KeyToTerm(string key)
        {
            // 해당하는 키가 없으면
            if (!localizationItems.TryGetValue(key, out string term))
            {
                Debug.LogError(key + "에 해당하는 키가 없습니다");
            }
            return term;
        }

        public static string ErrorKeyToTerm(string errorKey)
        {
            if (!serverErrorMessageItems.TryGetValue(errorKey, out string term))
            {
                Debug.LogError(errorKey + "에 해당하는 키가 없습니다.");
                return null;
            }
            return term;
        }

        public static bool HasKey(string key)
        {
            return localizationItems.ContainsKey(key);
        }

        public static void ChangeLanguage()
        {
            localizationItems.Clear();
            serverErrorMessageItems.Clear();
            SetLanguage();
            Parse();
        }

        private static void SetLanguage()
        {
#if UNITY_EDITOR
            LANGUAGE_PLAYERPREFS_KEY = "Editor-Language";
#else
        LANGUAGE_PLAYERPREFS_KEY = "Language";
#endif
            // 기존 언어 설정이 있으면 언어 설정을 가져온다.
            if (PlayerPrefs.HasKey(LANGUAGE_PLAYERPREFS_KEY))
            {
                Language = (Language)PlayerPrefs.GetInt(LANGUAGE_PLAYERPREFS_KEY);
            }
            // 기존 언어 설정이 없으면 기기 설정을 가져온다.
            else
            {
                // 기기 언어가 일본어면
                if (Application.systemLanguage == SystemLanguage.Japanese)
                {
                    Language = Language.Japanese;
                }
                else if (Application.systemLanguage == SystemLanguage.Korean)
                {
                    Language = Language.Korean;
                }
                else
                {
                    Language = Language.English;
                }
                // 언어 설정을 저장한다.
                PlayerPrefs.SetInt(LANGUAGE_PLAYERPREFS_KEY, (int)Language);
            }
        }

        private static void Parse()
        {
            mainTextAsset = Resources.Load<TextAsset>("TextAssets/SFTTranslation");
            string[] lines = mainTextAsset.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split('\t');
                for (int j = 0; j < cells.Length; j++)
                {
                    cells[j] = cells[j].Replace("\\n", "\n");
                }
                localizationItems.Add(cells[0], cells[(int)Language + 1]);
            }
        }
        public static string ReplaceVars(string localizedString)
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
            return result;
        }
    }
}