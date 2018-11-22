using System;
using System.Collections.Generic;
using System.Text;
using ESBasic.Helpers;
using ESBasic;
using JustLib;
using JustLib.Controls;
namespace RemoteReading.Core
{
    [Serializable]
    public class LastWordsRecord
    {
        public LastWordsRecord() { }
        public LastWordsRecord(string _speakerID, string _speakerName, bool me, ChatBoxContent content)
        {
            this.speakerID = _speakerID;
            this.speakerName = _speakerName;
            this.isMe = me;
            this.chatBoxContent = content;
        }

        #region SpeakerID
        private string speakerID;
        public string SpeakerID
        {
            get { return speakerID; }
            set { speakerID = value; }
        }
        #endregion

        #region SpeakerName
        private string speakerName;
        public string SpeakerName
        {
            get { return speakerName; }
            set { speakerName = value; }
        }
        #endregion

        #region ChatBoxContent
        private ChatBoxContent chatBoxContent;
        public ChatBoxContent ChatBoxContent
        {
            get { return chatBoxContent; }
            set { chatBoxContent = value; }
        }
        #endregion

        #region IsMe
        private bool isMe;
        public bool IsMe
        {
            get { return isMe; }
            set { isMe = value; }
        }
        #endregion

        #region SpeakTime
        private DateTime speakTime = DateTime.Now;
        public DateTime SpeakTime
        {
            get { return speakTime; }
            set { speakTime = value; }
        }
        #endregion

    } 
}
