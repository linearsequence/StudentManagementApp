using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementApp.Models;

public partial class StudentManagementContext(DbContextOptions<StudentManagementContext> options) : DbContext(options)
{
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

}
