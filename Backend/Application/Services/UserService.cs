using LibraryCrud.Application.Repository;
using LibraryCrud.Domain.DTOs;
using LibraryCrud.Domain.Entity;

namespace LibraryCrud.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid user ID", nameof(id));

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} not found");

                return MapToDto(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users.Select(MapToDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all users: {ex.Message}", ex);
            }
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto));

                // Validate email uniqueness
                if (await _userRepository.EmailExistsAsync(userDto.Email))
                    throw new InvalidOperationException($"Email {userDto.Email} already exists");

                var user = MapToEntity(userDto);
                var createdUser = await _userRepository.AddAsync(user);
                return MapToDto(createdUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating user: {ex.Message}", ex);
            }
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid user ID", nameof(id));

                if (userDto == null)
                    throw new ArgumentNullException(nameof(userDto));

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    throw new KeyNotFoundException($"User with ID {id} not found");

                user.Name = userDto.Name;
                user.Email = userDto.Email;
                user.Password = userDto.Password;

                var updatedUser = await _userRepository.UpdateAsync(user);
                return MapToDto(updatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("Invalid user ID", nameof(id));

                return await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting user: {ex.Message}", ex);
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Email cannot be empty", nameof(email));

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                    throw new KeyNotFoundException($"User with email {email} not found");

                return MapToDto(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user by email: {ex.Message}", ex);
            }
        }

        private UserDto MapToDto(User user) => new()
        {
            ID = user.ID,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };

        private User MapToEntity(UserDto dto) => new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password
        };
    }
}
