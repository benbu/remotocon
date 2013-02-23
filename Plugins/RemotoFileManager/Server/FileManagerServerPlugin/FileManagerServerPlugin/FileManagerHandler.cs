using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemotoconServerPlugin;
using System.IO;

namespace FileManagerServerPlugin
{
    public class FileManagerHandler : IServerPlugin
    {
        public FileManagerHandler()
        { }

        #region IServerPlugin Members

        public IXmlRpcServer Server { get; set; }

        public string Name
        {
            get { return "RemotoFileManager"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string AndroidPackageName
        {
            get { return "remoto.filemanager"; }
        }

        public void Initialize()
        {
            Server.RegisterHandler(this.GetType().Name, this);
        }

        public void Dispose()
        {

        }

        #endregion

        public List<Object> ListDirectory(string dir)
        {
            List<Object> result = new List<object>();

            if (dir == null || dir == "")
            {
                foreach (string drive in Directory.GetLogicalDrives())
                    result.Add(new List<Object>() { true, drive });
            }
            else
            {
                foreach (string d in Directory.GetDirectories(dir))
                    result.Add(new List<Object>() { true, Path.GetFileName(d) });

                foreach (string f in Directory.GetFiles(dir))
                    result.Add(new List<Object>() { false, Path.GetFileName(f) });
            }

            return result;
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDir(string dir, bool recursive)
        {
            try
            {
                Directory.Delete(dir, recursive);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MoveFile(String filePath, String dest)
        {
            try
            {
                File.Move(filePath, dest);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
