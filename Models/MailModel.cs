﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceCenter.Models
{
    internal class MailModel
    {
    }

    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }

    }
}
