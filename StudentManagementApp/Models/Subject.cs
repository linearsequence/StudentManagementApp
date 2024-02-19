using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models;

public partial class Subject
{
    [Column("id")]
    public int Id { get; set; }

    [Column("subject_title")]
    public string SubjectTitle { get; set; } = null!;

}
