using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OpenHack.University.Services._1._0
{

    [ServiceContract]
    public interface IDepartment
    {

        [OperationContract]
        List<Contract.Department> Search();

        [OperationContract]
        Contract.Department GetById(int id);

        [OperationContract]
        Contract.Department Create(Contract.Department departmentToCreate);

        [OperationContract]
        Contract.Department Modify(Contract.Department departmentToModify);

        [OperationContract]
        Contract.Department Delete(int id);
    }


   
    
}
