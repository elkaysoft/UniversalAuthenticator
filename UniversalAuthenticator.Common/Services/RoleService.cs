using AutoMapper;
using System.Transactions;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;
using UniversalAuthenticator.Domain.Data;
using UniversalAuthenticator.Domain.Entities;


namespace UniversalAuthenticator.Common.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<RoleClaim> _roleClaimRepository;
        private readonly IRepository<ApplicationUserRole> _applicationUserRoleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> roleRepository, IMapper mapper, IRepository<Permission> permissionRepository,
            IRepository<RoleClaim> roleClaimRepository, IRepository<ApplicationUserRole> applicationUserRoleRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
            _roleClaimRepository = roleClaimRepository;
            _applicationUserRoleRepository = applicationUserRoleRepository;
        }

        public async Task<List<RoleDto>> ValidateApplicationRole(List<Guid> roles)
        {
            var result = new List<RoleDto>();

            try
            {
                if (roles.Any())
                {
                    var role_csv = string.Join(",", roles);
                    var roleObj = await _roleRepository.ListAllAsync(x => role_csv.Contains(x.Id.ToString()) && x.Status);

                    var data = _mapper.Map<List<RoleDto>>(roleObj);
                    result = data;
                }
            }
            catch(Exception ex)
            {
                //log exception
                ex.ToString();
            }

            return result;
        }

        public async Task<Tuple<GenericResponse<bool>, ErrorResponse>> AddRole(CreateRoleRequest request)
        {
            var result = new GenericResponse<bool>();
            var error = new ErrorResponse();

            try
            {
                if (request == null)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.Role_is_required);

                
                var role_exist = await _roleRepository.GetFirstAsync(x => x.Name == request.Name);
                if (role_exist != null)
                    throw new CustomException(CustomCodes.ModelValidationError, $"{request.Name} already exist!");

                var validatedPermissions = await ValidatePermission(request.Permission);
                if(validatedPermissions.Count == 0)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.Invalid_permission_supplied);


                //implement transaction scope to enable transaction-like data persistence
                using(TransactionScope transactionScope = new TransactionScope())
                {
                    try
                    {
                        var appRole = new Role
                        {
                            Id = Guid.NewGuid(),
                            Name = request.Name,
                            Description = request.Description,
                            Status = true
                        };
                        await _roleRepository.AddAsync(appRole);
                        var roleClaim = new RoleClaim
                        {
                            ClaimType = "Bearer",
                            ClaimValue = PermissionPackers.PackPermissionsIntoHexString(request.Permission),
                            RoleId = appRole.Id,
                            CreatedBy = ""
                        };
                        await _roleClaimRepository.AddAsync(roleClaim);
                        
                        transactionScope.Complete();
                        transactionScope.Dispose();

                        result.data = true;
                        result.code = CustomCodes.Successful;
                        result.message = CustomMessages.Successful;
                    }
                    catch(Exception ex)
                    {
                        transactionScope.Dispose();
                        result.data = false;
                        result.code = CustomCodes.Something_went_wrong;
                        result.message = CustomMessages.SomethingWentWrong;
                    }
                }                             

            }
            catch(CustomException cEx)
            {
                error.Errors.Add(new ErrorModel
                {
                    code = cEx.ErrorCode,
                    message = cEx.Message,
                });
            }
            catch(Exception ex)
            {
                //log exception
                ex.ToString();
                error.Errors.Add(new ErrorModel
                {
                    code = CustomCodes.Something_went_wrong,
                    message = CustomMessages.SomethingWentWrong
                });
            }

            return new Tuple<GenericResponse<bool>, ErrorResponse>(result, error);
        }

        public async Task AddUserRole(List<Guid> roleIds, Guid userId)
        {
            if(roleIds.Count > 0)
            {
                foreach (var roleId in roleIds)
                {
                    var userrole_exist = await _applicationUserRoleRepository.GetFirstAsync(x => x.ApplicationUserId == userId && x.RoleId == roleId);
                    if (userrole_exist == null)
                    {
                        var userRole = new ApplicationUserRole
                        {
                            ApplicationUserId = userId,
                            Id = Guid.NewGuid(),
                            RoleId = roleId,
                            CreatedBy = ""
                        };
                        await _applicationUserRoleRepository.AddAsync(userRole);
                    }
                }
            }
            
        }


        #region Private Methods
        private async Task<List<Permission>> ValidatePermission(List<string> permissions)
        {
            var validatedPermissions = new List<Permission>();
            if(permissions.Count > 0)
            {
                var perm_csv = string.Join(",", permissions);
                var permission_obj = await _permissionRepository.ListAllAsync(x => perm_csv.Contains(x.Code) && x.Status);
                if(permission_obj.Count > 0)
                {
                    validatedPermissions.AddRange(permission_obj);
                }
            }

            return validatedPermissions;
        }
        #endregion


    }
}
