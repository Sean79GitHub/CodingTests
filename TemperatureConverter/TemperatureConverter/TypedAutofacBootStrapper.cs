using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using Caliburn.Micro;
using TemperatureConverterDomain.Interfaces;
using IContainer = Autofac.IContainer;

namespace TemperatureConverter
{
    public class TypedAutofacBooTStrapper<TRootViewModel> : Bootstrapper<TRootViewModel>
    {
        private IContainer autofacContainer;

        protected IContainer AutofacContainer
        {
            get { return autofacContainer; }
        }

        protected override void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              .Where(type => type.Name.EndsWith("ViewModel"))
              .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels"))
              .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
              .AsSelf()
              .InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
              .Where(type => type.Name.EndsWith("View"))
              .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
              .AsSelf()
              .InstancePerDependency();

            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();
            builder.Register<ITemperatureConverterCalculator>(c => new TemperatureConverterDomain.TemperatureConverterCalculator()).InstancePerLifetimeScope();
            builder.Register<ILanguageSettingsManager>(c => new TemperatureConverterDomain.LanguageSettingsManager()).InstancePerLifetimeScope();

            ConfigureContainer(builder);

            autofacContainer = builder.Build();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (AutofacContainer.IsRegistered(serviceType))
                    return AutofacContainer.Resolve(serviceType);
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? serviceType.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return AutofacContainer.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            AutofacContainer.InjectProperties(instance);
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }
    }
}