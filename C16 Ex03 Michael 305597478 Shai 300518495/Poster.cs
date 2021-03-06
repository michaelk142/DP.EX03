﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace C16_Ex03_Michael_305597478_Shai_300518495
{
    public class Poster : Task
    {
        private const int k_LengthOfString = 25;
        private Services.ActionReturnStatus<string> m_PosterInvoker;
        private Action<Status> m_PosterUnInvoker;
        private Status m_StatusPosted;
        private Schedulable m_Schedule;
        private string m_TextToPost;

        public Poster(Services.ActionReturnStatus<string> i_PostPoster,Action<Status> i_PosterRemover, DateTime i_ChosenDateTime, string i_TextToPost)
        {
            m_PosterUnInvoker = i_PosterRemover;
            m_PosterInvoker = i_PostPoster;
            m_Schedule = new Schedulable(i_ChosenDateTime);
            m_TextToPost = i_TextToPost;
        }

        protected override void InvokeAction()
        {
            m_StatusPosted = m_PosterInvoker.Invoke(m_TextToPost);
        }
        protected override void InvokeReverseAction()
        {
            m_PosterUnInvoker.Invoke(m_StatusPosted);
        }
        protected override bool IsConditionSatisfied()
        {
            return m_Schedule.isTimeArrived();
        }

        public override string ToString()
        {
            return string.Format("Post at {0}: {1}", m_Schedule.DesiredDateTime, m_TextToPost.Substring(0, finalLength()));
        }

        private int finalLength()
        {
            return m_TextToPost.Length > k_LengthOfString ? k_LengthOfString : m_TextToPost.Length;
        }
    }
}
