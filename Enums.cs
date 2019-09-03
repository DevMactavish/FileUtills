using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtills
{
    public class Enums
    {
        public enum ReturnMessage
        {
            FileNotFound,
            DirectoryNotFound,
            MoveIsSuccessfull,
            CopyIsSuccessfull,
            MoveIsNotSuccessfull,
            CopyIsNotSuccessfull,
            DeleteIsSuccessfull,
            DeleteIsNotSuccessfull
        }
    }

}
