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
    public interface IInstructor
    {

        [OperationContract]
        List<Contract.Instructor> Search();

        [OperationContract]
        Contract.Instructor GetById(int id);

        [OperationContract]
        Contract.Instructor Create(Contract.Instructor instructorToCreate);

        [OperationContract]
        Contract.Instructor Modify(Contract.Instructor instructorToModify);

        [OperationContract]
        Contract.Instructor Delete(int id);
    }
}
