using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        public UserService(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IEnumerable<UserForDisplayDto> GetAll()
        {
            var allUsers = _userRepo.GetAll();
            return allUsers.Select(user => new UserForDisplayDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Age = user.Age,
                MaritalStatus = user.MaritalStatus
            });
        }

        public UserForDisplayDto Get(int Id)
        {
            try{
                var user = _userRepo.Get(Id);
                return new UserForDisplayDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    Age = user.Age,
                    MaritalStatus = user.MaritalStatus
                };
            }
            catch(Exception){
                throw;
            }
        }

        public List<UserForDisplayDto> Filter(string name)
        {
            List<UserForDisplayDto> usersDto = new List<UserForDisplayDto>();
            var users =  _userRepo.Filter(name);
            foreach (var user in users){
                usersDto.Append(new UserForDisplayDto{
                    Address = user.Address,
                    Age = user.Age,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MaritalStatus = user.MaritalStatus, 
                });
            }
            return usersDto;
        }

        public void Insert(UserForDisplayDto userDto)
        {
            try
            {
                _userRepo.Insert(new User
                {
                    Address = userDto.Address,
                    Age = userDto.Age,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    MaritalStatus = userDto.MaritalStatus
                });

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(int Id, UserForDisplayDto userDto)
        {
            try
            {
                var user = _userRepo.Get(Id);
                if (user != null)
                {
                    _userRepo.Update(new User
                    {
                        Address = userDto.Address,
                        Age = userDto.Age,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        MaritalStatus = userDto.MaritalStatus
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int Id)
        {
            try
            {
                var user = _userRepo.Get(Id);
                _userRepo.Delete(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}