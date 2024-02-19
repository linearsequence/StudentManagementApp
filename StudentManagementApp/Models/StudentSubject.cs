using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementApp.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Subject? Subject { get; set; }

    }
}
