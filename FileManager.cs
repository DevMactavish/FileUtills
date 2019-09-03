using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FileUtills.Enums;

namespace FileUtills
{
    public class FileManager
    {
        private static readonly object padlock = new object();
        private FileInfo _fileInfo = null;
        private DirectoryInfo _directoryInfo = null;
        private static FileManager _fileManager;
        private FileManager()
        {

        }
        public static FileManager GetInstance()
        {
            if (_fileManager == null)
            {
                lock (padlock)
                {
                    _fileManager = new FileManager();
                }
            }
            return _fileManager;
        }
        private FileInfo GetFileInfo(string path)
        {
            return new FileInfo(path);
        }
        private DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(path);
        }
        public bool FileExist(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            _fileInfo = GetFileInfo(path);
            return _fileInfo.Exists;
        }
        public bool DirectoryExist(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            _directoryInfo = GetDirectoryInfo(path);
            return _directoryInfo.Exists;
        }
        public string GetFileName(string path)
        {
            if (File.Exists(path))
            {
                _fileInfo = GetFileInfo(path);
                return _fileInfo.Name.Split('.')[0];
            }
            else
                return ReturnMessage.FileNotFound.ToString();
        }
        public string GetFileNameWitExtension(string path)
        {
            if (File.Exists(path))
            {
                _fileInfo = GetFileInfo(path);
                return _fileInfo.Name;
            }
            else
                return ReturnMessage.FileNotFound.ToString();
        }
        public string GetDirectoryName(string path)
        {
            if (Directory.Exists(path))
            {
                _directoryInfo = GetDirectoryInfo(path);
                return _directoryInfo.Name;
            }
            else
                return ReturnMessage.FileNotFound.ToString();
        }
        public long GetSize(string path)
        {
            if (File.Exists(path))
            {
                _fileInfo = GetFileInfo(path);
                return _fileInfo.Length;
            }
            else
                return 0;
        }
        public FileAttributes GetFileAttributes(string path)
        {
            if (File.Exists(path))
            {
                _fileInfo = GetFileInfo(path);
                return _fileInfo.Attributes;
            }
            else
                return 0;
        }
        public int GetFilesCount(string path)
        {
            if (Directory.Exists(path))
            {
                _directoryInfo = GetDirectoryInfo(path);

                return _directoryInfo.GetFiles().Count();
            }
            else
                return 0;
        }
        public int GetAllFilesCount(string path)
        {
            if (Directory.Exists(path))
            {
                _directoryInfo = GetDirectoryInfo(path);

                return _directoryInfo.GetFiles("*", SearchOption.AllDirectories).Count();
            }
            else
                return 0;
        }
        public ReturnMessage Delete(string path)
        {
            ReturnMessage result = ReturnMessage.FileNotFound;
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                    if (!File.Exists(path))
                        result= ReturnMessage.DeleteIsSuccessfull;
                    else
                        result = ReturnMessage.DeleteIsNotSuccessfull;
                }
                catch
                {
                    result = ReturnMessage.DeleteIsNotSuccessfull;
                }
            }

            return result;
        }
        public List<string> GetFiles(string path)
        {
            List<string> result = new List<string>();
            if(!string.IsNullOrEmpty(path))
            {
                if(DirectoryExist(path)==true)
                {
                    result= System.IO.Directory.GetFiles(path).ToList();
                }
            }
            return result;
        }
        public ReturnMessage Copy(string path, string destination)
        {
            ReturnMessage result =ReturnMessage.FileNotFound;
            if (File.Exists(path))
            {
                try
                {
                    File.Copy(path, destination);
                    if (File.Exists(destination))
                        result= ReturnMessage.CopyIsSuccessfull;
                    else
                        result = ReturnMessage.CopyIsNotSuccessfull;
                }
                catch
                {
                    result = ReturnMessage.CopyIsNotSuccessfull;
                }
            }
            return result;
        }
        public ReturnMessage Move(string path,string destination)
        {
            ReturnMessage result =ReturnMessage.FileNotFound;
            if (File.Exists(path))
            {
                try
                {
                    File.Move(path, destination);
                    if (File.Exists(destination))
                        result = ReturnMessage.MoveIsSuccessfull;
                    else
                        result = ReturnMessage.MoveIsNotSuccessfull;
                }
                catch
                {
                    result = ReturnMessage.MoveIsNotSuccessfull;
                }
            }
            return result;
        }

        public async Task<ReturnMessage> DeleteAsync(string path)
        {
            return await Task.Run(() => Delete(path));
        }

        public async Task<ReturnMessage> CopyAsync(string path,string destination)
        {
            return await Task.Run(() => Copy(path,destination));
        }

        public async Task<ReturnMessage> MoveAsync(string path,string destination)
        {
            
            return await Task.Run(() =>  Move(path,destination));
        }
        public async Task<int> GetAllFilesCountAsync(string path)
        {
            return await Task.Run(() => GetAllFilesCount(path));
        }

        public async Task<FileAttributes> GetFileAttributesAsync(string path)
        {
            return await Task.Run(() => GetFileAttributes(path));
        }
        public async Task<long> GetSizeAsync(string path)
        {
            return await Task.Run(() => GetSize(path));
        }
        public async Task<string> GetDirectoryNameAsync(string path)
        {
            return await Task.Run(() => GetDirectoryName(path));
        }
    }
}
