﻿using System;
using System.ComponentModel.DataAnnotations;

namespace esme.Shared.Circles
{
    public class MessageEditModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // setter used by JSON deserializer

        [Required]
        public string Text { get; set; }
    }
}
