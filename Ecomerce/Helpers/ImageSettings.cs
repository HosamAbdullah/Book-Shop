namespace Ecomerce.Helpers
{
    public static class ImageSettings
    {
        public static string UploadImage(IFormFile file)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images");
            string fileName = Guid.NewGuid()+file.FileName;
            string FilePath = Path.Combine(FolderPath, fileName);
            using var fileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName; 
        }
    }
}
