using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyScript2 : MonoBehaviour
{
    public GameObject target_object;
    public InputField chat_input_field;
    public Text chat_response_field;


    bool ani_key_pressed = false;


    void Start()
    {
        //아래의 SetAPIKey 괄호안에 생성한 키를 붙여 넣습니다.
        LeastSquares.OpenAIAccessManager.SetAPIKey("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");
        LeastSquares.OpenAIAccessManager.Temperature = 0.7;


        //시작시 입력창에 포커스 보냄
        chat_input_field.ActivateInputField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            target_object.transform.Rotate(0, -100 * Time.deltaTime, 0);
            DoMotion("TurnLeft");

            ani_key_pressed = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            target_object.transform.Rotate(0, 100 * Time.deltaTime, 0);
            DoMotion("TurnRight");

            ani_key_pressed = true;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                DoMotion("Running");
            else
                DoMotion("Walking");

            ani_key_pressed = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            DoMotion("Backwards");

            ani_key_pressed = true;
        }
        else
        {
            if (ani_key_pressed)
            {
                if (DoMotion("Waving"))
                    ani_key_pressed = false;
            }
        }
    }

    //종료버튼 기능
    public void OnQuitClick()
    {
        //유니티 종료 명령어
        Application.Quit();
    }

    public bool DoMotion(string motion_name)
    {
        Animator animator = target_object.GetComponent<Animator>();
        string layer_name = animator.GetLayerName(0);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(motion_name))
        {
            if (!animator.IsInTransition(0))
            {
                animator.CrossFade(layer_name + "." + motion_name, 0.3f, 0);
                return true;
            }
        }

        return false;
    }

    public void DoMotion1()
    {
        DoMotion("Death");
    }

    public void DoMotion2()
    {
        DoMotion("Zombie_Run");
    }

    public void DoMotion3()
    {
        DoMotion("Dance17_A");
    }

    public void DoMotion_Multi()
    {
        DoMotion("Dance18");
    }

    public void DoMotion4()
    {
        DoMotion("Mma_Kick");
    }

    public void DoMotion5_Walking()
    {
        DoMotion("Walking");
    }

    public void DoMotion6_Running()
    {
        DoMotion("Running");
    }

    public void DoInputPrompt()
    {
        string input_txt = chat_input_field.text;
        chat_response_field.text = "GPT에 요청중입니다...";


        // Debug.Log(input_txt);

        GPT_Talk(input_txt);

    }

    private List<LeastSquares.ChatCompletionMessage> _messages = new List<LeastSquares.ChatCompletionMessage>();

    public async void GPT_Talk(string prompt)
    {
        _messages.Clear();

        _messages.Add(new LeastSquares.ChatCompletionMessage
        {
            role = "user",
            content = prompt
        });

        string result = await LeastSquares.OpenAIAccessManager.RequestChatCompletion(_messages.ToArray());

        //Debug.Log(result);

        chat_response_field.text = result;

        //아래 코드 추가하기
        // DoMotion("Talk");
    }

    GameObject ttsAudioSource;

    public IEnumerator StartTTS(string content)
    {
        //string speech_lan = "en-US";
        string speech_lan = "ko-KR";

        string text = content;

        if (text.Length > 100)
            text = text.Substring(0, 100);

        string text_str = System.Uri.EscapeUriString(text);

        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&client=tw-ob&q=" + text_str + "&tl=" + speech_lan;

        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("ConnectionError");
            }
            else
            {
                if (ttsAudioSource == null)
                {
                    ttsAudioSource = new GameObject();
                    AudioSource a_source = ttsAudioSource.AddComponent<AudioSource>();
                    a_source.volume = 1f;
                }

                ttsAudioSource.transform.position = this.transform.position;

                AudioSource audioSource = ttsAudioSource.GetComponent<AudioSource>();
                audioSource.volume = 1f;

                audioSource.clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(www);
                audioSource.Play();
            }
        }

        yield break;
    }


    public void DoTTS()
    {
        StartCoroutine(StartTTS(chat_response_field.text));
    }

}


