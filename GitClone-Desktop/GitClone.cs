using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtiClone_Desktop
{
    public class GitClone
    {
        private static string BASE_PATH = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\gitProjects";
        public GitClone() { }

        public async Task<string> create_new(string cloneLink, string branch)
        {
            var projName = cloneLink.Split('/').Last().Split('.').Last();
            Directory.CreateDirectory(BASE_PATH+projName);
            ConfigurationManager.AppSettings.Add(projName, cloneLink);
            var projPath = BASE_PATH+projName;
            var rtn = await git_clone(projName, branch, projPath);
            return rtn;
        }

        public async Task<string> git_clone(string projName, string branch, string projPath)
        {
            var cloneLink = ConfigurationManager.AppSettings[projName];
            var process = new Process();

            process.StartInfo.FileName = "CMD.exe";
            process.StartInfo.Arguments = $"/C git clone -b {branch} --single-branch {cloneLink} {projPath}";

            process.Start();

            return "Success";
        }
    }
}
