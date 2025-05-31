using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models;

public partial class Assessment
{
    public Guid AssessmentId { get; set; }

    public Guid CourseId { get; set; }

    public string Title { get; set; } = null!;

    [MaxLength(5000)]
    public string Questions { get; set; } = null!;

    public int MaxScore { get; set; }
}
