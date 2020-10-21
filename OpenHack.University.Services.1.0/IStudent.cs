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
    public interface IStudent
    {
        [OperationContract]
        List<Contract.Student> Search();

        [OperationContract]
        Contract.Student GetById(int id);

        [OperationContract]
        Contract.Student Create(Contract.Student studentToCreate );

        [OperationContract]
        Contract.Student Modify(Contract.Student studentToModify);

        [OperationContract]
        Contract.Student Delete(int studentId);
    }
}
