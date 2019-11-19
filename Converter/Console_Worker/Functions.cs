using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;


namespace Console_Worker
{
    public class Func
    {
        //最初に権限を取っ払う
        public static void FirstRun(string full_file_pas)
        {
            GrantAuthority(full_file_pas);
            EraseReadOnly(full_file_pas);
        }

        //アクセス権限付与
        //書き込み不能ファイルをなくす
        //フルパス+ファイル名含む
        public static void GrantAuthority(string file_pas)
        {
            FileSystemAccessRule rule = new FileSystemAccessRule(
                  new NTAccount("everyone"),
                  FileSystemRights.Modify,
                  AccessControlType.Allow);
            FileSecurity security = File.GetAccessControl(file_pas);
            security.AddAccessRule(rule);
            File.SetAccessControl(file_pas, security);
        }


        //読み取り専用の場合消す
        //フルパス+ファイル名含む
        public static void EraseReadOnly(string file_pas)
        {
            //file_pasの属性を取得する
            System.IO.FileAttributes attr = System.IO.File.GetAttributes(file_pas);
            //読み取り専用属性があるか調べる
            if ((attr & System.IO.FileAttributes.ReadOnly) ==
                System.IO.FileAttributes.ReadOnly)
            {
                //読み取り専用属性を削除する
                System.IO.File.SetAttributes(file_pas,
                    attr & (~System.IO.FileAttributes.ReadOnly));
            }
        }


        //隠しファイルの場合True
        //フルパス+ファイル名含む
        public static bool HiddenJudge(string full_file_pas)
        {
            return ((new System.IO.FileInfo(full_file_pas).Attributes &
                System.IO.FileAttributes.Hidden) ==
                System.IO.FileAttributes.Hidden ? true : false);
        }

        //特定の拡張子ファイルを探して返す
        //( FolderPas , 拡張子[] )
        public static string[] GetFiles(string path, params string[] extensions)
        {
            return Directory
                .GetFiles(path, "*.*")
                .Where(c => extensions.Any(extension => c.EndsWith(extension)))
                .ToArray();
        }
    }
}