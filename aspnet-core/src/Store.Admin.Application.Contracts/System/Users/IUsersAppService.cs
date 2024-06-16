using Store.Admin.Users;
using Store.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Account;

namespace Store.Admin.Users
{
    public interface IUsersAppService : ICrudAppService<
        UserDto,
        Guid,
        PagedResultRequestDto,
        CreateUserDto,
        UpdateUserDto>
    {
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);

        Task<PagedResultDto<UserInListDto>> GetListWithFilterAsync(BaseListFilterDto input);

        Task<List<UserInListDto>> GetListAllAsync(string filterKeyword);
        Task AssignRolesAsync(Guid userId, string[] roleNames);
        Task SetPasswordAsync(Guid userId, SetPasswordDto input);
    }
}