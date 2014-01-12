using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TianPingInformation.Models.POCO
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("学员姓名")]
        public string Name { get; set; }

        [Required]
        [DisplayName("身份证号")]
        public string Identity { get; set; }

        [DisplayName("住址")]
        public string Address { get; set; }

        [Required]
        [DisplayName("报名日期")]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        [DisplayName("联系方式")]
        public string Phone { get; set; }

        [DisplayName("照片")]
        public string Photo { get; set; }

        [DisplayName("科目1")]
        public DateTime Exam1 { get; set; }

        [DisplayName("科目2")]
        public DateTime Exam2 { get; set; }

        [DisplayName("科目3")]
        public DateTime Exam3 { get; set; }

        [DisplayName("科目4")]
        public DateTime Exam4 { get; set; }

        [DisplayName("发照日期")]
        public DateTime IssueDate { get; set; }
    }
}