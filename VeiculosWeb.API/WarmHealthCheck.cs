using VeiculosWeb.Domain.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace VeiculosWeb.API
{
    public class WarmHealthCheck(IAccountService accountService,
                                 IAmbulanceService ambulanceService,
                                 IParamedicCategoryService categoryService,
                                 IParamedicChecklistService checklistService,
                                 IParamedicChecklistReviewService checklistReviewService,
                                 IParamedicItemService itemService,
                                 ITenantService tenantService,
                                 ITokenService tokenService,
                                 IUserRepository userRepository,
                                 IAmbulanceRepository ambulanceRepository,
                                 IParamedicCategoryRepository categoryRepository,
                                 IParamedicChecklistRepository checklistRepository,
                                 IParamedicChecklistReviewRepository checklistReviewRepository,
                                 IParamedicItemRepository itemRepository,
                                 ITenantRepository tenantRepository,
                                 SignInManager<User> signInManager,
                                 UserManager<User> userManager,
                                 IHttpContextAccessor httpContextAccessor) : IHealthCheck
    {
        private readonly IAccountService accountService = accountService;
        private readonly IAmbulanceService ambulanceService = ambulanceService;
        private readonly IParamedicCategoryService categoryService = categoryService;
        private readonly IParamedicChecklistService checklistService = checklistService;
        private readonly IParamedicChecklistReviewService checklistReviewService = checklistReviewService;
        private readonly IParamedicItemService itemService = itemService;
        private readonly ITenantService tenantService = tenantService;
        private readonly ITokenService tokenService = tokenService;

        private readonly IUserRepository userRepository = userRepository;
        private readonly IAmbulanceRepository ambulanceRepository = ambulanceRepository;
        private readonly IParamedicCategoryRepository categoryRepository = categoryRepository;
        private readonly IParamedicChecklistRepository checklistRepository = checklistRepository;
        private readonly IParamedicChecklistReviewRepository checklistReviewRepository = checklistReviewRepository;
        private readonly IParamedicItemRepository itemRepository = itemRepository;
        private readonly ITenantRepository tenantRepository = tenantRepository;

        private readonly SignInManager<User> signInManager = signInManager;
        private readonly UserManager<User> userManager = userManager;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var tenants = await tenantService.GetList();
            Log.Information("Healthcheck executando  {code}", tenants.Code);
            return HealthCheckResult.Healthy();
        }
    }
}
