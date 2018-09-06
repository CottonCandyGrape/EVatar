using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace EyeHelpers
{
    public class ChatSend : MonoBehaviour
    {
        [SerializeField] private TransportTCP m_transport;

        private ChatState m_state = ChatState.HOST_TYPE_SELECT;

        private string m_hostAddress = "";

        private const int m_port = 50765;

        private string m_sendComment = "";
        private string m_prevComment = "";
        private string m_chatMessage = "";

        private List<string>[] m_message;

        private bool m_isServer = false;

        private static int MESSAGE_LINE = 18;
        private static int CHAT_MEMBER_NUM = 2;

        enum ChatState
        {
            HOST_TYPE_SELECT = 0,   // 방 선택.
            CHATTING,               // 채팅 중.
        };

        //Button button;

        // Use this for initialization
        void Start()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            System.Net.IPAddress hostAddress = hostEntry.AddressList[0];
            Debug.Log(hostEntry.HostName);
            m_hostAddress = hostAddress.ToString();

            //GameObject go = new GameObject("Network");
            //m_transport = go.AddComponent<TransportTCP>();

            m_transport.RegisterEventHandler(OnEventHandling);

            m_message = new List<string>[CHAT_MEMBER_NUM];
            for (int i = 0; i < CHAT_MEMBER_NUM; ++i)
            {
                m_message[i] = new List<string>();
            }

            SelectHostTypeGUI();

            //SetStartState();
        }

        // Update is called once per frame
        void Update()
        {
            switch (m_state)
            {
                case ChatState.HOST_TYPE_SELECT:
                    for (int i = 0; i < CHAT_MEMBER_NUM; ++i)
                    {
                        m_message[i].Clear();
                    }
                    break;

                case ChatState.CHATTING:
                    UpdateChatting();
                    break;
            }
        }

        void UpdateChatting()
        {
            byte[] buffer = new byte[10000];

            int recvSize = m_transport.Receive(ref buffer, buffer.Length);
            if (recvSize > 0)
            {
                //수신하는 소스코드
                string message = System.Text.Encoding.UTF8.GetString(buffer);
                Debug.Log("Recv data:" + message);
                m_chatMessage += message + "   ";// + "\n";

                int id = (m_isServer == true) ? 1 : 0;
            }
        }

        //void OnGUI()
        //{
        //    SetStartState();
        //}

        private void SetStartState()
        {
            switch (m_state)
            {
                case ChatState.HOST_TYPE_SELECT:
                    SelectHostTypeGUI();
                    break;

                case ChatState.CHATTING:
                    SendCommandText();
                    break;
            }
        }

        void SelectHostTypeGUI()
        {
            //서버IP지정
            m_hostAddress = "223.194.158.134";

            //채팅방무조건 들어가기 
            if (true)
            {
                bool ret = m_transport.Connect(m_hostAddress, m_port);
                if (ret)
                {
                    m_state = ChatState.CHATTING;
                }
            }

        }

        public void SendCommandText(string message = "")
        {
            // <tts=> 태그 제외하고 입력된 단어만 뽑기.
            string word = message.Split(new char[] { '=' }, System.StringSplitOptions.RemoveEmptyEntries)[1];
            word = word.Remove(word.Length - 1);

            // 입력된 단어가 있는 경우에만 TTS 전송 (안드로이드 장치로 전송).
            if (!string.IsNullOrEmpty(word))
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
                if (m_transport)
                {
                    m_transport.Send(buffer, buffer.Length);
                }
            }
        }

        void OnApplicationQuit()
        {
            if (m_transport != null)
            {
                m_transport.StopServer();
            }
        }

        public void OnEventHandling(NetEventState state)
        {
            switch (state.type)
            {
                case NetEventType.Connect:
                    if (m_transport.IsServer())
                    { }
                    else
                    { }
                    break;

                case NetEventType.Disconnect:
                    if (m_transport.IsServer())
                    { }
                    else
                    { }
                    break;
            }
        }
    }
}