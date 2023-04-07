
public interface IUserService
{
    IEnumerable<UserForDisplayDto> GetAll();
    UserForDisplayDto Get(int Id);
    List<UserForDisplayDto> Filter(string name);
    void Insert(UserForDisplayDto userDto);
    void Update(int Id, UserForDisplayDto userDto);
    void Delete(int Id);
}