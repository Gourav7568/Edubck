﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.DTOs
{
    public class EventResultDTO
    {
        [Required]
        public Guid ResultId { get; set; }

        [Required]
        public Guid AssessmentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime AttemptDate { get; set; }
    }
}
