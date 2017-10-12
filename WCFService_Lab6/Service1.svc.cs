using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.Data.Entity;

namespace WCFService_Lab6
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        EmployeeContext db = new EmployeeContext();

       

        public List<Employee> GetEmployees() => db.Employees.ToList();
        //{
        //    return db.Employees.ToList();
        //}

        public void PostEmployee(Employee newEmployee)
        {
            db.Employees.Add(newEmployee);
            db.SaveChanges();
            return;
        }



        public void PutEmployee(Employee updateEmployee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(string id)
        {
            db.Employees.Remove(db.Employees.Find(id));
        }

        public Employee GetEmployee(string id)
        {
            return db.Employees.Find(int.Parse(id));
        }

        public List<Employee> GetByName(string name)
        {
            return db.Employees.Where(a => a.EmpName.Contains(name)).ToList();
        }
    }
}
