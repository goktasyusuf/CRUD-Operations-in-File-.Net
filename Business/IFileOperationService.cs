using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IFileOperationService
    {
         IDataResult<string> GetAll();
         IResult Write(string oldBrandName, string newBrandName);
         IResult Delete(string delete);
         IResult UpdateBrandNameAndIpAddress(FileOperation model);
         IDataResult<string> GetOldBrand();
         IDataResult<string> GetOldIpAddress();
    }
}
