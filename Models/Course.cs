using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models;

public partial class Course
{
    public Guid CourseId { get; set; }
    [MaxLength(5000)]
    public string Title { get; set; } = null!;

    [MaxLength(5000)]
    public string Description { get; set; } = null!;

    public Guid InstructorId { get; set; }

    [MaxLength(5000)]
    public string MediaUrl { get; set; } = null!;
}
