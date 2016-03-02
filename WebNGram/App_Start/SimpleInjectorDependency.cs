using BAL.Intefaces;
using BAL.Manager;
using BAL.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebNGram.App_Start
{
	public class SimpleInjectorDependency
	{
		public static void RegistrationContainers()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
			container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
			container.Register<IMainManager, MainManager>(Lifestyle.Scoped);
			container.Verify();
			DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
		}
	}
}