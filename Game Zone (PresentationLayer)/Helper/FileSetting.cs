namespace Game_Zone__PresentationLayer_.Helper
{
    public static class FileSettings
    {
        public static string Upload(IFormFile file, string foldername)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldername);

            var filename = $"{Guid.NewGuid()}{file.FileName}";

            var filepath = Path.Combine(folderpath, filename);

            using var fs = new FileStream(filepath, FileMode.Create);

            file.CopyTo(fs);

            return filename;

        }

        public static void DeleteFile(string foldername , string filename )
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldername,filename);

            File.Delete(folderpath);
        }
    }
}
