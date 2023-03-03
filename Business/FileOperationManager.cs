using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Business
{
    public class FileOperationManager : IFileOperationService
    {
        private readonly string path = "C:\\Users\\yusuf\\OneDrive\\Masaüstü";
        public IResult Delete(string delete)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> GetAll()
        {

            string text2 = File.ReadAllText(path + "/" + "new.txt");
            return new SuccessDataResult<string>(text2);
        }
        public IResult Write(string brandName, string ipAddress)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string FilePath = path + "\\" + "new.txt";
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, path);
            }
            else
            {
                brandName = $"{Environment.NewLine}{brandName}";
                File.AppendAllText(FilePath, brandName);

                ipAddress = $"{Environment.NewLine}{ipAddress}";
                File.AppendAllText(FilePath, ipAddress);
            }
            return new SuccessResult();
        }

        public IDataResult<string> GetOldBrand()
        {
            Regex r = new Regex("ORIANA_HOSTNAME:\\s(?<brand>\\S+)");
            using (StreamReader inputReader = new StreamReader(path + "/" + "new.txt"))
            {
                string tempLineValue;
                while (null != (tempLineValue = inputReader.ReadLine()))
                {

                    if (r.Match(tempLineValue).Success)
                    {
                        return new SuccessDataResult<string>(r.Match(tempLineValue).Groups[1].Value);
                    }
                }
                return new ErrorDataResult<string>(r.Match(tempLineValue).Groups[1].Value, "HATA");
            }
            
        }

        public IDataResult<string> GetOldIpAddress()
        {
            Regex r = new Regex("ORIANA_HOST_IPADDRESS:\\s(?<ip>\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})");
            using (StreamReader inputReader = new StreamReader(path + "/" + "new.txt"))
            {
                string tempLineValue;
                while (null != (tempLineValue = inputReader.ReadLine()))
                {

                    if (r.Match(tempLineValue).Success)
                    {
                        return new SuccessDataResult<string>(r.Match(tempLineValue).Groups[1].Value);
                    }
                }
                return new ErrorDataResult<string>(r.Match(tempLineValue).Groups[1].Value, "HATA");
            }
            
        }

        public IResult UpdateBrandNameAndIpAddress(FileOperation model)
        {
            
            Regex r2 = new Regex("ORIANA_HOSTNAME:\\s(?<brand>\\S+)");
            string temp = File.ReadAllText(path + "/" + "new.txt");
            var k = r2.Match(temp);
            var oldbrand = k.Groups[1].Value;
            temp = temp.Replace(oldbrand, model.BrandName);
            File.WriteAllText(path + "/" + "new.txt", temp);

            Regex r = new Regex("ORIANA_HOST_IPADDRESS:\\s(?<ipaddress>\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3})");
            var t = r.Match(temp);
            var oldip = t.Groups[1].Value;
            temp = temp.Replace(oldip, model.IpAddress);
            File.WriteAllText(path + "/" + "new.txt", temp);
            return new SuccessResult("Başarılıyla değiştirildi");
        }
    }
}
