using System;
using System.Collections.Generic;
using System.Text;

namespace DapperBestBuyConsole
{
    interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();

        void InsertDepartment(string newDepo);
    }
}
