using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApplication.Data;
using UniversityApplication.Models;

namespace UniversityApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UniversityApplicationContext(
  serviceProvider.GetRequiredService<
  DbContextOptions<UniversityApplicationContext>>()))

            {

                if (context.Course.Any() || context.Student.Any() || context.Teacher.Any())
                {
                    return;
                }


              
                context.Student.AddRange(
                    new Student
                    { studentId = "172/2019",firstName = "Pero",lastName = "Petkov",enrollmentDate = DateTime.Parse("2019-9-20"), acquiredCredits = 150, currentSemester = 6, educationLevel = "Under graduate"  },
                    new Student
                    { studentId = "132/2016", firstName = "Petko", lastName = "Perov", enrollmentDate = DateTime.Parse("2016-9-23"), acquiredCredits = 300, currentSemester = 12, educationLevel = "PHD" },
                    new Student
                    { studentId = "45/2017", firstName = "Gjorgi", lastName = "Stojanov", enrollmentDate = DateTime.Parse("2017-9-21"), acquiredCredits = 250, currentSemester = 10, educationLevel = "Masters" },
                    new Student
                    { studentId = "2/2019", firstName = "Stojan", lastName = "Gjorgiev", enrollmentDate = DateTime.Parse("2019-9-2"), acquiredCredits = 160, currentSemester = 6, educationLevel = "Under graduate" },
                    new Student
                    { studentId = "176/2020", firstName = "Stojko", lastName = "Karanfilov", enrollmentDate = DateTime.Parse("2020-9-30"), acquiredCredits = 100, currentSemester = 4, educationLevel = "Under graduate" },
                    new Student
                    { studentId = "172/2020", firstName = "Karanfil", lastName = "Stojkov", enrollmentDate = DateTime.Parse("2020-9-20"), acquiredCredits = 120, currentSemester = 4, educationLevel = "Under graduate" },
                    new Student
                    { studentId = "200/2021", firstName = "Bojan", lastName = "Krstev", enrollmentDate = DateTime.Parse("2021-9-19"), acquiredCredits = 60, currentSemester =2, educationLevel = "Under graduate" },
                    new Student
                    { studentId = "15/2018", firstName = "Ile", lastName = "Kostov", enrollmentDate = DateTime.Parse("2018-9-1"), acquiredCredits = 220, currentSemester = 8, educationLevel = "Under graduate" }

                     );
                context.Teacher.AddRange(
                    new Teacher
                    {
                        firstName = "Krste",
                        lastName = "Bojanov",
                        degree = "PHD",
                        academicRank = "Professor",
                        officeNumber = "10A",
                        hireDate = DateTime.Parse("1993-7-10")
                    },
                    new Teacher
                    {
                        firstName = "Atanas",
                        lastName = "Anastasovski",
                        degree = "Masters",
                        academicRank = "Assistant",
                        officeNumber = "10B",
                        hireDate = DateTime.Parse("2014-2-10")
                    },
                    new Teacher
                    {
                        firstName = "Bojana",
                        lastName = "Kostova",
                        degree = "Masters",
                        academicRank = "Professor",
                        officeNumber = "13A",
                        hireDate = DateTime.Parse("2005-2-6")
                    },
                    new Teacher
                    {
                        firstName = "Kosta",
                        lastName = "Iliev",
                        degree = "Bachelor",
                        academicRank = "Docent",
                        officeNumber = "15B",
                        hireDate = DateTime.Parse("2020-12-4")
                    }
                );
                context.SaveChanges();

                context.Course.AddRange(
                    new Course
                    {
                        title = "Physics 2",
                        credits = 6,
                        semester = 2,
                        programme = "KSIAR",
                        educationLevel = "Under graduate",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Bojana" && d.lastName == "Kostova").teacherId,
                       // firstTeacher = context.Teacher.Single(d => d.firstName == "Bojana" && d.lastName == "Kostova"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Kosta" && d.lastName == "Iliev").teacherId,
                       // secondTeacher = context.Teacher.Single(d => d.firstName == "Kosta" && d.lastName == "Iliev")
                    },
                    new Course
                    {
                        
                        title = "RSWEB",
                        credits = 6,
                        semester = 6,
                        programme = "KTI",
                        educationLevel = "Under graduate",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Krste" && d.lastName == "Bojanov").teacherId,
                       // firstTeacher = context.Teacher.Single(d => d.firstName == "Krste" && d.lastName == "Bojanov"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Atanas" && d.lastName == "Anastovski").teacherId,
                        //secondTeacher = context.Teacher.Single(d => d.firstName == "Atanas" && d.lastName == "Anastasovski")
                    },
                    new Course
                    {
                        
                        title = "Web App",
                        credits = 3,
                        semester = 6,
                        programme = "TKII",
                        educationLevel = "Under graduate",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Atanas" && d.lastName == "Anastasovski").teacherId,
                        //firstTeacher = context.Teacher.Single(d => d.firstName == "Atanas" && d.lastName == "Anastasovski"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Kosta" && d.lastName == "Iliev").teacherId,
                       // secondTeacher = context.Teacher.Single(d => d.firstName == "Kosta" && d.lastName == "Iliev")
                    },
                    new Course
                    {
                        
                        title = "Embedded Systems",
                        credits = 7,
                        semester = 10,
                        programme = "KTI",
                        educationLevel = "Masters",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Krste" && d.lastName == "Bojanov").teacherId,
                       // firstTeacher = context.Teacher.Single(d => d.firstName == "Krste" && d.lastName == "Bojanov"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Bojana" && d.lastName == "Kostova").teacherId,
                       // secondTeacher = context.Teacher.Single(d => d.firstName == "Bojana" && d.lastName == "Kostova")
                    }
                );
                context.SaveChanges();

                context.Enrollment.AddRange(
                   new Enrollment
                   {   courseId = context.Course.Single(d => d.title == "Physics 2").courseId, studentId = context.Student.Single(d => d.firstName == "Bojan" && d.lastName == "Krstev").Id, semester = 2,year = 2021,grade = 10,seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna",examPoints = 90,seminalPoints = 10,projectPoints = 10,additionalPoints = 0,finishDate = DateTime.Parse("2021-6-20") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Physics 2").courseId, studentId = context.Student.Single(d => d.firstName == "Pero" && d.lastName == "Petkov").Id, semester = 4, year = 2020, grade = 8, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 70, seminalPoints = 0, projectPoints = 10, additionalPoints = 5, finishDate = DateTime.Parse("2020-9-20") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Embedded Systems").courseId, studentId = context.Student.Single(d => d.firstName == "Petko" && d.lastName == "Perov").Id, semester = 12, year = 2022, grade = 9, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 78, seminalPoints = 5, projectPoints = 10, additionalPoints = 5, finishDate = DateTime.Parse("2022-2-10") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Embedded Systems").courseId, studentId = context.Student.Single(d => d.firstName == "Gjorgi" && d.lastName == "Stojanov").Id, semester = 10, year = 2022, grade = 10, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 90, seminalPoints = 10, projectPoints = 5, additionalPoints = 10, finishDate = DateTime.Parse("2022-2-10") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "RSWEB").courseId, studentId = context.Student.Single(d => d.firstName == "Stojan" && d.lastName == "Gjorgiev").Id, semester = 6, year = 2021, grade = 6, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 55, seminalPoints = 0, projectPoints = 5, additionalPoints = 0, finishDate = DateTime.Parse("2021-2-7") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "RSWEB").courseId, studentId = context.Student.Single(d => d.firstName == "Karanfil" && d.lastName == "Stojkov").Id, semester = 6, year = 2021, grade = 7, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 66, seminalPoints = 0, projectPoints = 10, additionalPoints = 0, finishDate = DateTime.Parse("2021-2-7") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "RSWEB").courseId, studentId = context.Student.Single(d => d.firstName == "Ile" && d.lastName == "Stojkov").Id, semester = 8, year = 2022, grade = 9, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 88, seminalPoints = 0, projectPoints = 10, additionalPoints = 0, finishDate = DateTime.Parse("2022-6-30") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Physics 2").courseId, studentId = context.Student.Single(d => d.firstName == "Stojan" && d.lastName == "Gjorgiev").Id, semester = 4, year = 2020, grade = 8, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 70, seminalPoints = 0, projectPoints = 5, additionalPoints = 0, finishDate = DateTime.Parse("2020-5-6") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Web App").courseId, studentId = context.Student.Single(d => d.firstName == "Ile" && d.lastName == "Stojkov").Id, semester = 6, year = 2022, grade = 7, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 55, seminalPoints = 10, projectPoints = 10, additionalPoints = 0, finishDate = DateTime.Parse("2022-5-7") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Web App").courseId, studentId = context.Student.Single(d => d.firstName == "Pero" && d.lastName == "Petkov").Id, semester = 6, year = 2022, grade = 8, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 70, seminalPoints = 10, projectPoints = 0, additionalPoints = 0, finishDate = DateTime.Parse("2022-5-7") },
                   new Enrollment
                   { courseId = context.Course.Single(d => d.title == "Web App").courseId, studentId = context.Student.Single(d => d.firstName == "Stojko" && d.lastName == "Karanfilov").Id, semester = 6, year = 2021, grade = 10, seminalUrl = "ekursevi/seminarska", projectUrl = "ekursevi/proektna", examPoints = 95, seminalPoints = 0, projectPoints = 10, additionalPoints = 5, finishDate = DateTime.Parse("2021-6-7") }
                   );
                context.SaveChanges();
            }
        }
    }
}