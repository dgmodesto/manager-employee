﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCursoAngular8.Models
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        public string  EmployeeName { get; set; }
        public string  Department { get; set; }
        public string MailID { get; set; }
        public DateTime? DOJ { get; set; }

    }
}
