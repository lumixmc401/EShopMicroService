namespace BuildingBlocks.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message):base(message)
        {
        }
        public NotFoundException(string name,object key):base($"實體 \"{name}\" {key}沒有找到")
        {  
        }
    }
}
