namespace NOAA.GHCND.Data 
{
    public interface IConfiguration 
    {
        string FileSystemReader_BaseDirectory {get;}
        string FileSystemReader_DataDirectory { get; }
        string FTP_Server {get;}
        string FTP_BaseDirectory {get;}
    }
}