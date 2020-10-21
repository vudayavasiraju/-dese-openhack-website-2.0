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
    public interface ICourse
    {

        [OperationContract]
        List<Contract.Course> Search();

        [OperationContract]
        Contract.Course GetById(int id);

        [OperationContract]
        Contract.Course Create(Contract.Course courseToCreate);

        [OperationContract]
        Contract.Course Modify(Contract.Course courseToModify);

        [OperationContract]
        Contract.Course Delete(int id);
    }


  
}
