using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.BasicManagement.Localization;
using Lion.AbpPro.Core;
using Lion.AbpPro.FileManagement;
using Lion.AbpPro.LanguageManagement;

namespace Lion.AbpPro
{
    [DependsOn(
        typeof(AbpProCoreModule),
        typeof(BasicManagementDomainSharedModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(LanguageManagementDomainSharedModule),
        typeof(FileManagementDomainSharedModule)
    )]
    public class AbpProDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AbpProGlobalFeatureConfigurator.Configure();
            AbpProModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<AbpProDomainSharedModule>(AbpProDomainSharedConsts.NameSpace); });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpProResource>(AbpProDomainSharedConsts.DefaultCultureName)
                    .AddVirtualJson("/Localization/AbpPro")
                    .AddBaseTypes(typeof(BasicManagementResource))
                    .AddBaseTypes(typeof(AbpTimingResource));

                options.DefaultResourceType = typeof(AbpProResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace(AbpProDomainSharedConsts.NameSpace, typeof(AbpProResource)); });
        }
    }
}